using Shadow.DDL.Constraints;
using ShadowSql.Engines;
using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shadow.DDL.Schemas;

/// <summary>
/// 表结构
/// </summary>
/// <param name="tableName">表名</param>
/// <param name="columns">列</param>
/// <param name="constraints">约束</param>
/// <param name="schema"></param>
public class TableSchema(string tableName, ColumnSchema[] columns, IConstraint[] constraints, string schema = "")
    : Identifier(tableName), ITable, IInsertTable, IUpdateTable, ISqlEntity
{
    /// <summary>
    /// 表结构
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="columns"></param>
    /// <param name="schema"></param>
    public TableSchema(string tableName, ColumnSchema[] columns, string schema = "")
        : this(tableName, columns, [], schema)
    {
    }
    #region 配置
    private readonly string _schema = schema;
    private ColumnSchema[] _columns = columns;
    /// <summary>
    /// 架构
    /// </summary>
    public string Schema
        => _schema;
    /// <summary>
    /// 字段
    /// </summary>
    public ColumnSchema[] Columns
        => _columns;
    /// <summary>
    /// 约束
    /// </summary>
    public IConstraint[] Constraints { get; } = constraints;

    ////内联的展开运算符“..”
    private ColumnSchema[] _keys = [.. ColumnSchema.GetKeys(columns)];
    private ColumnSchema[] _insertColumns = [.. ColumnSchema.GetInsertColumns(columns)];
    private ColumnSchema[] _updateColumns = [.. ColumnSchema.GetUpdateColumns(columns)];
   /// <summary>
   /// 主键
   /// </summary>
   public ColumnSchema[] Keys
        => _keys;
    #endregion
    /// <summary>
    /// 添加列
    /// </summary>
    /// <param name="columns"></param>
    public void AddColumns(params ColumnSchema[] columns)
    {
        ColumnSchema[] newColumns = [.. _columns, .. columns];
        ColumnSchema[] keys = [.. ColumnSchema.GetKeys(newColumns)];
        ColumnSchema[] insertColumns = [.. ColumnSchema.GetInsertColumns(newColumns)];
        ColumnSchema[] updateColumns = [.. ColumnSchema.GetUpdateColumns(newColumns)];
        _columns = newColumns;
        _keys = keys;
        _insertColumns = insertColumns;
        _updateColumns = updateColumns;
    }
    /// <summary>
    /// 查找字段
    /// </summary>
    /// <param name="columName">列名</param>
    /// <returns></returns>
    public ColumnSchema? GetColumn(string columName)
    {
        return _columns.FirstOrDefault(c => c.IsMatch(columName))
            ?? Table.GetFieldWithTablePrefix(_name, _columns, columName);
    }
    #region ISqlEntity
    /// <inheritdoc/>
    internal override void Write(ISqlEngine engine, StringBuilder sql)
    {
        if (!string.IsNullOrEmpty(_schema))
        {
            engine.Identifier(sql, _schema);
            sql.Append('.');
        }
        engine.Identifier(sql, _name);
    }
    /// <inheritdoc/>
    IColumn? ITable.GetColumn(string columName)
        => GetColumn(columName);
    /// <inheritdoc/>
    IField? ITableView.GetField(string fieldName)
        => GetColumn(fieldName);
    /// <inheritdoc/>
    ICompareField ITableView.GetCompareField(string fieldName)
    {
        if (GetColumn(fieldName) is ColumnSchema column)
            return column;
        throw new ArgumentException(fieldName + "字段不存在", nameof(fieldName));
    }
    /// <inheritdoc/>
    IField ITableView.NewField(string fieldName)
        => Column.Use(fieldName);
    #endregion
    #region ITable
    /// <inheritdoc/>
    IEnumerable<IColumn> IInsertTable.InsertColumns
        => _insertColumns;
    /// <inheritdoc/>
    IColumn? IInsertTable.GetInsertColumn(string columnName)
    {
        if (GetColumn(columnName) is ColumnSchema column)
        {
            if ((column.ColumnType & ColumnSchema.InsertIgnoreType) == ColumnType.Empty)
                return column;
        }
        return null;
    }
    /// <inheritdoc/>
    IEnumerable<IAssignView> IUpdateTable.AssignFields
        => _updateColumns;
    /// <inheritdoc/>
    IAssignView? IUpdateTable.GetAssignField(string fieldName)
    {
        if (GetColumn(fieldName) is ColumnSchema column)
        {
            if ((column.ColumnType & ColumnSchema.UpdateIgnoreType) == ColumnType.Empty)
                return column;
        }
        return null;
    }
    /// <inheritdoc/>
    IEnumerable<IColumn> ITable.Columns
        => _columns;
    /// <inheritdoc/>
    IEnumerable<IField> ITableView.Fields
        => _columns;
    #endregion
}

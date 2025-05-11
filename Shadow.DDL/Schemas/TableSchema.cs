using ShadowSql.Engines;
using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shadow.DDL.Schemas;

/// <summary>
/// 表
/// </summary>
/// <param name="name"></param>
/// <param name="columns"></param>
/// <param name="schema"></param>
public class TableSchema(string name, ColumnSchema[] columns, string schema = "")
    : Identifier(name), ITable, IInsertTable, IUpdateTable, ISqlEntity
{
    private readonly string _schema = schema;
    private readonly ColumnSchema[] _columns = columns;
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

    #region 配置
    ////内联的展开运算符“..”
    private readonly ColumnSchema[] _keys = [.. ColumnSchema.GetKeys(columns)];
    private readonly ColumnSchema[] _insertColumns = [.. ColumnSchema.GetInsertColumns(columns)];
    private readonly ColumnSchema[] _updateColumns = [.. ColumnSchema.GetUpdateColumns(columns)];
   /// <summary>
   /// 主键
   /// </summary>
   public ColumnSchema[] Keys
        => _keys;
    #endregion
    /// <summary>
    /// 查找字段
    /// </summary>
    /// <param name="columName"></param>
    /// <returns></returns>
    public ColumnSchema? GetColumn(string columName)
    {
        return _columns.FirstOrDefault(c => c.IsMatch(columName))
            ?? Table.GetFieldWithTablePrefix(_name, _columns, columName);
    }
    #region ISqlEntity
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    private void Write(ISqlEngine engine, StringBuilder sql)
    {
        if (!string.IsNullOrEmpty(_schema))
        {
            engine.Identifier(sql, _schema);
            sql.Append('.');
        }
        engine.Identifier(sql, _name);
    }
    void ISqlEntity.Write(ISqlEngine engine, StringBuilder sql)
        => Write(engine, sql);

    IColumn? ITable.GetColumn(string columName)
        => GetColumn(columName);

    IField? ITableView.GetField(string fieldName)
        => GetColumn(fieldName);
    ICompareField ITableView.GetCompareField(string fieldName)
    {
        if (GetColumn(fieldName) is ColumnSchema column)
            return column;
        throw new ArgumentException(fieldName + "字段不存在", nameof(fieldName));
    }
    IField ITableView.NewField(string fieldName)
        => Column.Use(fieldName);
    #endregion
    #region ITable
    IEnumerable<IColumn> IInsertTable.InsertColumns
        => _insertColumns;
    /// <summary>
    /// 获取插入列
    /// </summary>
    /// <param name="columnName"></param>
    /// <returns></returns>
    IColumn? IInsertTable.GetInsertColumn(string columnName)
    {
        if (GetColumn(columnName) is ColumnSchema column)
        {
            if ((column.ColumnType & ColumnSchema.InsertIgnoreType) == ColumnType.Empty)
                return column;
        }
        return null;
    }
    IEnumerable<IAssignView> IUpdateTable.AssignFields
        => _updateColumns;
    IAssignView? IUpdateTable.GetAssignField(string fieldName)
    {
        if (GetColumn(fieldName) is ColumnSchema column)
        {
            if ((column.ColumnType & ColumnSchema.UpdateIgnoreType) == ColumnType.Empty)
                return column;
        }
        return null;
    }
    IEnumerable<IColumn> ITable.Columns
        => _columns;
    IEnumerable<IField> ITableView.Fields
        => _columns;
    #endregion
}

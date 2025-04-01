using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.FieldInfos;
using ShadowSql.Fragments;
using ShadowSql.Identifiers;
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
    : Identifier(name), ITable, ISqlEntity
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
    private readonly ColumnSchema[] _keys = [.. GetKeys(columns)];
    private readonly ColumnSchema[] _insertColumns = [.. GetInsertColumns(columns)];
    private readonly ColumnSchema[] _updateColumns = [.. GetUpdateColumns(columns)];
   /// <summary>
   /// 主键
   /// </summary>
   public ColumnSchema[] Keys 
        => _keys;
    /// <summary>
    /// 插入列
    /// </summary>
    public ColumnSchema[] InsertColumns
        => _insertColumns;
    /// <summary>
    /// 修改列
    /// </summary>
    public ColumnSchema[] UpdateColumns
        => _updateColumns;
    #endregion

    /// <summary>
    /// 获取主键
    /// </summary>
    /// <param name="fields"></param>
    /// <returns></returns>
    public static IEnumerable<ColumnSchema> GetKeys(IEnumerable<ColumnSchema> fields)
        => fields.Where(o => (o.ColumnType & ColumnType.Key) == ColumnType.Key);
    /// <summary>
    /// 获取插入列
    /// </summary>
    /// <param name="fields"></param>
    /// <returns></returns>
    public static IEnumerable<ColumnSchema> GetInsertColumns(IEnumerable<ColumnSchema> fields)
    {
        var ignoreType = ColumnType.Identity | ColumnType.Computed;
        return fields.Where(o => (o.ColumnType & ignoreType) == ColumnType.Empty);
    }
    /// <summary>
    /// 获取修改列
    /// </summary>
    /// <param name="fields"></param>
    /// <returns></returns>
    public static IEnumerable<ColumnSchema> GetUpdateColumns(IEnumerable<ColumnSchema> fields)
    {
        var ignoreType = ColumnType.Identity | ColumnType.Key | ColumnType.Computed;
        return fields.Where(o => (o.ColumnType & ignoreType) == ColumnType.Empty);
    }
    /// <summary>
    /// 查找字段
    /// </summary>
    /// <param name="columName"></param>
    /// <returns></returns>
    public ColumnSchema? GetColumn(string columName)
    {
        return _columns.FirstOrDefault(c => c.IsMatch(columName))
            ?? Table.GetColumnWithTablePrefix(_name, _columns, columName);
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
    #endregion
    #region ITable
    IEnumerable<IColumn> IInsertTable.InsertColumns
        => _insertColumns;
    IEnumerable<IColumn> IUpdateTable.UpdateColumns
        => _updateColumns;

    IEnumerable<IColumn> ITableView.Columns
        => _columns;

    string IView.ViewName
        => _name;

    IColumn? ITableView.GetColumn(string columName)
        => GetColumn(columName);

    IField ITableView.Field(string fieldName)
        => FieldInfo.Use(fieldName);
    #endregion
}

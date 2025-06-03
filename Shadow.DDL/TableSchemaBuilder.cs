using Shadow.DDL.Schemas;
using System.Collections.Generic;

namespace Shadow.DDL;

/// <summary>
/// 构造表架构
/// </summary>
public class TableSchemaBuilder(string name, string schema = "")
{
    #region 配置
    private readonly string _name = name;
    private readonly string _schema = schema;
    private readonly List<ColumnSchema> _columns = [];
    /// <summary>
    /// 表名
    /// </summary>
    public string Name
        => _name;
    /// <summary>
    /// 架构
    /// </summary>
    public string Schema 
        => _schema;
    /// <summary>
    /// 字段
    /// </summary>
    public IEnumerable<ColumnSchema> Columns 
        => _columns;
    #endregion
    /// <summary>
    /// 定义列
    /// </summary>
    /// <param name="columnName"></param>
    /// <param name="sqlType"></param>
    /// <returns></returns>
    public ColumnSchema DefineColumn(string columnName, string sqlType = "INT")
    {
        ColumnSchema column = new(columnName, sqlType);
        _columns.Add(column);
        return column;
    }
    /// <summary>
    /// 定义主键
    /// </summary>
    /// <param name="sqlType"></param>
    /// <param name="columnName"></param>
    /// <returns></returns>
    public TableSchemaBuilder DefinColumns(string sqlType, params IEnumerable<string> columnName)
    {
        foreach (var name in columnName)
            DefineColumn(name, sqlType);
        return this;
    }
    /// <summary>
    /// 定义主键
    /// </summary>
    /// <param name="sqlType"></param>
    /// <param name="columnName"></param>
    /// <returns></returns>
    public TableSchemaBuilder DefineKeys(string sqlType, params IEnumerable<string> columnName)
    {
        foreach (var name in columnName)
            DefineColumn(name, sqlType).ColumnType = ColumnType.Key;
        return this;
    }
    /// <summary>
    /// 定义主键
    /// </summary>
    /// <param name="sqlType"></param>
    /// <param name="columnName"></param>
    /// <returns></returns>
    public TableSchemaBuilder DefineIdentity(string columnName, string sqlType = "INT")
    {
        DefineColumn(columnName, sqlType).ColumnType = ColumnType.Identity;
        return this;
    }
    /// <summary>
    /// 构造表架构
    /// </summary>
    /// <returns></returns>
    public TableSchema Build()
        => new(_name, [.. _columns], _schema);
}

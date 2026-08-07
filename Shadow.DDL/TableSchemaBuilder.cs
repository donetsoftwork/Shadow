using Shadow.DDL.Constraints;
using Shadow.DDL.Schemas;
using Shadow.DDL.SqlTypes;
using ShadowSql.Identifiers;
using System.Collections.Generic;

namespace Shadow.DDL;

/// <summary>
/// 构造表架构
/// </summary>
/// <param name="tableName">表名</param>
/// <param name="schema"></param>
public class TableSchemaBuilder(string tableName, string schema = "")
{
    #region 配置
    private readonly string _name = tableName;
    private readonly string _schema = schema;
    private readonly List<ColumnSchema> _columns = [];
    private readonly List<IConstraint> _constraints = [];
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
    /// <summary>
    /// 约束
    /// </summary>
    public IEnumerable<IConstraint> Constraints
        => _constraints;
    #endregion
    #region Column
    /// <summary>
    /// 定义列
    /// </summary>
    /// <param name="columnName">列名</param>
    /// <param name="memberName">成员名</param>
    /// <param name="sqlType">数据库字段类型</param>
    /// <param name="columnType"></param>
    /// <returns></returns>
    public ColumnSchema DefineColumn(string columnName, string memberName, ISqlType sqlType, ColumnType columnType = ColumnType.Empty)
    {
        ColumnSchema column = new(columnName, memberName, sqlType, columnType);
        _columns.Add(column);
        return column;
    }
    /// <summary>
    /// 定义列
    /// </summary>
    /// <param name="columnName">列名</param>
    /// <param name="sqlType">数据库字段类型</param>
    /// <param name="columnType"></param>
    /// <returns></returns>
    public ColumnSchema DefineColumn(string columnName, ISqlType sqlType, ColumnType columnType = ColumnType.Empty)
        => DefineColumn(columnName, columnName, sqlType, columnType);
    /// <summary>
    /// 定义列
    /// </summary>
    /// <param name="sqlType">数据库字段类型</param>
    /// <param name="columnNames">列名</param>
    /// <returns></returns>
    public TableSchemaBuilder DefinColumns(ISqlType sqlType, params IEnumerable<string> columnNames)
    {
        foreach (var name in columnNames)
            DefineColumn(name, name, sqlType);
        return this;
    }
    /// <summary>
    /// 定义主键
    /// </summary>
    /// <param name="sqlType">数据库字段类型</param>
    /// <param name="columnNames">列名</param>
    /// <returns></returns>
    public TableSchemaBuilder DefineKeys(ISqlType sqlType, params IEnumerable<string> columnNames)
    {
        foreach (var name in columnNames)
            DefineColumn(name, name, sqlType, ColumnType.Key);
        return this;
    }
    /// <summary>
    /// 定义主键
    /// </summary>
    /// <param name="columnName">列名</param>
    /// <param name="memberName">成员名</param>
    /// <param name="sqlType">数据库字段类型</param>
    /// <returns></returns>
    public TableSchemaBuilder DefineIdentity(string columnName, string memberName, ISqlType sqlType)
    {
        DefineColumn(columnName, memberName, sqlType, ColumnType.Identity);
        return this;
    }
    /// <summary>
    /// 定义主键
    /// </summary>
    /// <param name="columnName">列名</param>
    /// <param name="sqlType">数据库字段类型</param>
    /// <returns></returns>
    public TableSchemaBuilder DefineIdentity(string columnName, ISqlType sqlType)
        => DefineIdentity(columnName, columnName, sqlType);
    #endregion
    /// <summary>
    /// 定义检查规则
    /// </summary>
    /// <param name="constraintName"></param>
    /// <param name="expression"></param>
    /// <returns></returns>
    public TableSchemaBuilder DefineCheck(string constraintName, string expression)
    {
        var constraint = new CheckConstraint(constraintName, expression);
        _constraints.Add(constraint);
        return this;
    }
    /// <summary>
    /// 定义唯一约束
    /// </summary>
    /// <param name="constraintName"></param>
    /// <param name="columns"></param>
    /// <returns></returns>
    public TableSchemaBuilder DefineUnique(string constraintName, params IColumn[] columns)
    {
        var constraint = new UniqueConstraint(constraintName, columns);
        _constraints.Add(constraint);
        return this;
    }
    /// <summary>
    /// 定义唯一约束
    /// </summary>
    /// <param name="constraintName"></param>
    /// <param name="columnNames"></param>
    /// <returns></returns>
    public TableSchemaBuilder DefineUnique(string constraintName, params string[] columnNames)
    {
        var constraint = new UniqueConstraint(constraintName, columnNames);
        _constraints.Add(constraint);
        return this;
    }
    /// <summary>
    /// 构造表架构
    /// </summary>
    /// <returns></returns>
    public TableSchema Build()
        => new(_name, [.. _columns], [.. _constraints], _schema);
}

using ShadowSql.Engines;
using ShadowSql.Filters;
using ShadowSql.Identifiers;
using ShadowSql.Queries;
using ShadowSql.Simples;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShadowSql.GroupBy;

/// <summary>
/// 分组sql查询
/// </summary>
public class GroupBySqlQuery : GroupByBase<SqlQuery>, IDataSqlQuery
{
    internal GroupBySqlQuery(SqlQuery having, ITableView source, IFieldView[] fields)
        : base(fields, having)
    {
        _source = source;
    }
    #region ITable
    /// <summary>
    /// 分组sql查询
    /// </summary>
    /// <param name="table"></param>
    /// <param name="having"></param>
    /// <param name="fields"></param>
    public GroupBySqlQuery(ITable table, SqlQuery having, params IFieldView[] fields)
        : this(having, table, fields)
    {
    }
    /// <summary>
    /// 分组sql查询
    /// </summary>
    /// <param name="table"></param>
    /// <param name="fields"></param>
    public GroupBySqlQuery(ITable table, params IFieldView[] fields)
        : this(SqlQuery.CreateAndQuery(), table, fields)
    {
    }
    #endregion
    #region IAliasTable
    /// <summary>
    /// 分组sql查询
    /// </summary>
    /// <param name="table"></param>
    /// <param name="having"></param>
    /// <param name="fields"></param>
    public GroupBySqlQuery(IAliasTable table, SqlQuery having, params IFieldView[] fields)
        : this(having, table, fields)
    {
    }
    /// <summary>
    /// 分组sql查询
    /// </summary>
    /// <param name="table"></param>
    /// <param name="fields"></param>
    public GroupBySqlQuery(IAliasTable table, IFieldView[] fields)
        : this(SqlQuery.CreateAndQuery(), table, fields)
    {
    }
    #endregion
    #region tableName
    /// <summary>
    /// 分组sql查询
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="having"></param>
    /// <param name="fields"></param>
    public GroupBySqlQuery(string tableName, SqlQuery having, params IFieldView[] fields)
        : this(having, SimpleTable.Use(tableName), fields)
    {
    }
    /// <summary>
    /// 分组sql查询
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="fields"></param>
    public GroupBySqlQuery(string tableName, params IFieldView[] fields)
        : this(SqlQuery.CreateAndQuery(), SimpleTable.Use(tableName), fields)
    {
    }
    #endregion
    #region IDataFilter
    /// <summary>
    /// 查询后再分组
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="having"></param>
    /// <param name="fields"></param>
    public GroupBySqlQuery(IDataFilter filter, SqlQuery having, params IFieldView[] fields)
        : this(having, filter, fields)
    {
    }
    /// <summary>
    /// 查询后再分组
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="fields"></param>
    public GroupBySqlQuery(IDataFilter filter, params IFieldView[] fields)
        : this(SqlQuery.CreateAndQuery(), filter, fields)
    {
    }
    #endregion
    #region 配置
    /// <summary>
    /// 数据源表
    /// </summary>
    protected readonly ITableView _source;
    /// <summary>
    /// 数据源表
    /// </summary>
    public ITableView Source
        => _source;
    #endregion
    #region Create
    #region ITable
    /// <summary>
    /// 创建分组查询
    /// </summary>
    /// <param name="table"></param>
    /// <param name="fields"></param>
    /// <returns></returns>
    public static GroupBySqlQuery Create(ITable table, params IFieldView[] fields)
        => new(table, fields);
    /// <summary>
    /// 创建分组查询
    /// </summary>
    /// <param name="table"></param>
    /// <param name="columnNames"></param>
    /// <returns></returns>
    public static GroupBySqlQuery Create(ITable table, params IEnumerable<string> columnNames)
        => new(table, [.. table.SelectFields(columnNames)]);
    #endregion
    #region IAliasTable
    /// <summary>
    /// 创建分组查询
    /// </summary>
    /// <param name="table"></param>
    /// <param name="fields"></param>
    /// <returns></returns>
    public static GroupBySqlQuery Create(IAliasTable table, params IFieldView[] fields)
        => new(table, fields);
    /// <summary>
    /// 创建分组查询
    /// </summary>
    /// <param name="table"></param>
    /// <param name="columnNames"></param>
    /// <returns></returns>
    public static GroupBySqlQuery Create(IAliasTable table, params IEnumerable<string> columnNames)
        => new(table, [.. table.SelectFields(columnNames)]);
    #endregion
    #region tableName
    /// <summary>
    /// 创建分组查询
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="fields"></param>
    /// <returns></returns>
    public static GroupBySqlQuery Create(string tableName, params IFieldView[] fields)
        => new(tableName, fields);
    /// <summary>
    /// 创建分组查询
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="columnNames"></param>
    /// <returns></returns>
    public static GroupBySqlQuery Create(string tableName, params IEnumerable<string> columnNames)
        => new(tableName, [.. columnNames.Select(Column.Use)]);
    #endregion
    #region IDataFilter
    /// <summary>
    /// 创建分组查询
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="fields"></param>
    /// <returns></returns>
    public static GroupBySqlQuery Create(IDataFilter filter, params IFieldView[] fields)
        => new(filter, fields);
    /// <summary>
    /// 创建分组查询
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="columnNames"></param>
    /// <returns></returns>
    public static GroupBySqlQuery Create(IDataFilter filter, params IEnumerable<string> columnNames)
        => new(filter, [.. filter.Source.SelectFields(columnNames)]);
    #endregion
    #endregion
    #region ISqlEntity
    /// <summary>
    /// 输出数据源
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    protected override void WriteGroupBySource(ISqlEngine engine, StringBuilder sql)
        => _source.Write(engine, sql);
    #endregion
    #region IDataSqlQuery
    SqlQuery IDataSqlQuery.Query
    {
        get => _filter;
        set => _filter = value;
    }
    #endregion
}

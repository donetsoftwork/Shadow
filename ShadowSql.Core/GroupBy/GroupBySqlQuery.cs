using ShadowSql.Engines;
using ShadowSql.Filters;
using ShadowSql.Identifiers;
using ShadowSql.Queries;
using ShadowSql.Tables;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShadowSql.GroupBy;

/// <summary>
/// 分组sql查询
/// </summary>
public class GroupBySqlQuery : GroupByBase<SqlQuery>, IDataSqlQuery
{
    internal GroupBySqlQuery(SqlQuery having, ITableView source, IField[] fields)
        : base(fields, having)
    {
        _source = source;
    }
    #region ITable
    /// <summary>
    /// 分组sql查询
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="having">分组查询条件</param>
    /// <param name="fields">字段</param>
    public GroupBySqlQuery(ITable table, SqlQuery having, params IField[] fields)
        : this(having, table, fields)
    {
    }
    /// <summary>
    /// 分组sql查询
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="fields">字段</param>
    public GroupBySqlQuery(ITable table, params IField[] fields)
        : this(SqlQuery.CreateAndQuery(), table, fields)
    {
    }
    #endregion
    #region IAliasTable
    /// <summary>
    /// 分组sql查询
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="having">分组查询条件</param>
    /// <param name="fields">字段</param>
    public GroupBySqlQuery(IAliasTable table, SqlQuery having, params IField[] fields)
        : this(having, table, fields)
    {
    }
    /// <summary>
    /// 分组sql查询
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="fields">字段</param>
    public GroupBySqlQuery(IAliasTable table, IField[] fields)
        : this(SqlQuery.CreateAndQuery(), table, fields)
    {
    }
    #endregion
    #region tableName
    /// <summary>
    /// 分组sql查询
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="having">分组查询条件</param>
    /// <param name="fields">字段</param>
    public GroupBySqlQuery(string tableName, SqlQuery having, params IField[] fields)
        : this(having, EmptyTable.Use(tableName), fields)
    {
    }
    /// <summary>
    /// 分组sql查询
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="fields">字段</param>
    public GroupBySqlQuery(string tableName, params IField[] fields)
        : this(SqlQuery.CreateAndQuery(), EmptyTable.Use(tableName), fields)
    {
    }
    #endregion
    #region IDataFilter
    /// <summary>
    /// 查询后再分组
    /// </summary>
    /// <param name="filter">过滤条件</param>
    /// <param name="having">分组查询条件</param>
    /// <param name="fields">字段</param>
    public GroupBySqlQuery(IDataFilter filter, SqlQuery having, params IField[] fields)
        : this(having, filter, fields)
    {
    }
    /// <summary>
    /// 查询后再分组
    /// </summary>
    /// <param name="filter">过滤条件</param>
    /// <param name="fields">字段</param>
    public GroupBySqlQuery(IDataFilter filter, params IField[] fields)
        : this(SqlQuery.CreateAndQuery(), filter, fields)
    {
    }
    #endregion
    #region 配置
    /// <summary>
    /// 数据源表
    /// </summary>
    protected readonly ITableView _source;
    /// <inheritdoc/>
    public override ITableView Source
        => _source;
    #endregion
    #region Create
    #region ITable
    /// <summary>
    /// 创建分组查询
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="fields">字段</param>
    /// <returns></returns>
    public static GroupBySqlQuery Create(ITable table, params IField[] fields)
        => new(table, fields);
    /// <summary>
    /// 创建分组查询
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="columnNames">列名</param>
    /// <returns></returns>
    public static GroupBySqlQuery Create(ITable table, params IEnumerable<string> columnNames)
        => new(table, [.. table.Fields(columnNames)]);
    #endregion
    #region IAliasTable
    /// <summary>
    /// 创建分组查询
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="fields">字段</param>
    /// <returns></returns>
    public static GroupBySqlQuery Create(IAliasTable table, params IField[] fields)
        => new(table, fields);
    /// <summary>
    /// 创建分组查询
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="columnNames">列名</param>
    /// <returns></returns>
    public static GroupBySqlQuery Create(IAliasTable table, params IEnumerable<string> columnNames)
        => new(table, [.. table.Fields(columnNames)]);
    #endregion
    #region tableName
    /// <summary>
    /// 创建分组查询
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="fields">字段</param>
    /// <returns></returns>
    public static GroupBySqlQuery Create(string tableName, params IField[] fields)
        => new(tableName, fields);
    /// <summary>
    /// 创建分组查询
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="columnNames">列名</param>
    /// <returns></returns>
    public static GroupBySqlQuery Create(string tableName, params IEnumerable<string> columnNames)
        => new(tableName, [.. columnNames.Select(Column.Use)]);
    #endregion
    #region IDataFilter
    /// <summary>
    /// 创建分组查询
    /// </summary>
    /// <param name="filter">过滤条件</param>
    /// <param name="fields">字段</param>
    /// <returns></returns>
    public static GroupBySqlQuery Create(IDataFilter filter, params IField[] fields)
        => new(filter, fields);
    /// <summary>
    /// 创建分组查询
    /// </summary>
    /// <param name="filter">过滤条件</param>
    /// <param name="columnNames">列名</param>
    /// <returns></returns>
    public static GroupBySqlQuery Create(IDataFilter filter, params IEnumerable<string> columnNames)
        => new(filter, [.. filter.Source.Fields(columnNames)]);
    #endregion
    #endregion
    #region ISqlEntity
    /// <inheritdoc/>
    protected override void WriteGroupBySource(ISqlEngine engine, StringBuilder sql)
        => _source.Write(engine, sql);
    #endregion
    #region IDataSqlQuery
    /// <inheritdoc/>
    SqlQuery IDataSqlQuery.Query
    {
        get => _filter;
        set => _filter = value;
    }
    #endregion
}

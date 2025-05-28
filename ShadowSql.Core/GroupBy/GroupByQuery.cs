using ShadowSql.Engines;
using ShadowSql.Filters;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Queries;
using ShadowSql.Tables;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShadowSql.GroupBy;

/// <summary>
/// 分组逻辑查询
/// </summary>
public class GroupByQuery : GroupByBase<Logic>, IDataQuery
{
    internal GroupByQuery(Logic having, ITableView source, IField[] fields)
        : base(fields, having)
    {
        _source = source;
    }
    #region ITable
    /// <summary>
    /// 分组逻辑查询
    /// </summary>
    /// <param name="table"></param>
    /// <param name="having"></param>
    /// <param name="fields"></param>
    public GroupByQuery(ITable table, Logic having, params IField[] fields)
        : this(having, table, fields)
    {
    }
    /// <summary>
    /// 分组逻辑查询
    /// </summary>
    /// <param name="table"></param>
    /// <param name="fields"></param>
    public GroupByQuery(ITable table, params IField[] fields)
        : this(new AndLogic(), table, fields)
    {
    }
    #endregion
    #region IAliasTable
    /// <summary>
    /// 分组逻辑查询
    /// </summary>
    /// <param name="table"></param>
    /// <param name="having"></param>
    /// <param name="fields"></param>
    public GroupByQuery(IAliasTable table, Logic having, params IField[] fields)
        : this(having, table, fields)
    {
    }
    /// <summary>
    /// 分组逻辑查询
    /// </summary>
    /// <param name="table"></param>
    /// <param name="fields"></param>
    public GroupByQuery(IAliasTable table, params IField[] fields)
        : this(new AndLogic(), table, fields)
    {
    }
    #endregion
    #region tableName
    /// <summary>
    /// 分组逻辑查询
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="having"></param>
    /// <param name="fields"></param>
    public GroupByQuery(string tableName, Logic having, params IField[] fields)
        : this(having, EmptyTable.Use(tableName), fields)
    {
    }
    /// <summary>
    /// 分组逻辑查询
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="fields"></param>
    public GroupByQuery(string tableName, params IField[] fields)
        : this(new AndLogic(), EmptyTable.Use(tableName), fields)
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
    public GroupByQuery(IDataFilter filter, Logic having, params IField[] fields)
        : this(having, filter, fields)
    {
    }
    /// <summary>
    /// 查询后再分组
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="fields"></param>
    public GroupByQuery(IDataFilter filter, params IField[] fields)
        : this(new AndLogic(), filter, fields)
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
    public override ITableView Source
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
    public static GroupByQuery Create(ITable table, params IField[] fields)
        => new(table, fields);
    /// <summary>
    /// 创建分组查询
    /// </summary>
    /// <param name="table"></param>
    /// <param name="columnNames"></param>
    /// <returns></returns>
    public static GroupByQuery Create(ITable table, params IEnumerable<string> columnNames)
        => new(table, [.. table.Fields(columnNames)]);
    #endregion
    #region IAliasTable
    /// <summary>
    /// 创建分组查询
    /// </summary>
    /// <param name="table"></param>
    /// <param name="fields"></param>
    /// <returns></returns>
    public static GroupByQuery Create(IAliasTable table, params IField[] fields)
        => new(table, fields);
    /// <summary>
    /// 创建分组查询
    /// </summary>
    /// <param name="table"></param>
    /// <param name="columnNames"></param>
    /// <returns></returns>
    public static GroupByQuery Create(IAliasTable table, params IEnumerable<string> columnNames)
        => new(table, [.. table.Fields(columnNames)]);
    #endregion
    #region tableName
    /// <summary>
    /// 创建分组查询
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="fields"></param>
    /// <returns></returns>
    public static GroupByQuery Create(string tableName, params IField[] fields)
        => new(tableName, fields);
    /// <summary>
    /// 创建分组查询
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="columnNames"></param>
    /// <returns></returns>
    public static GroupByQuery Create(string tableName, params IEnumerable<string> columnNames)
        => new(tableName, [.. columnNames.Select(Column.Use)]);
    #endregion
    #region IDataFilter
    /// <summary>
    /// 创建分组查询
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="fields"></param>
    /// <returns></returns>
    public static GroupByQuery Create(IDataFilter filter, params IField[] fields)
        => new(filter, fields);
    /// <summary>
    /// 创建分组查询
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="columnNames"></param>
    /// <returns></returns>
    public static GroupByQuery Create(IDataFilter filter, params IEnumerable<string> columnNames)
        => new(filter, [.. filter.Source.Fields(columnNames)]);
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
    #region IDataQuery
    Logic IDataQuery.Logic
    {
        get => _filter;
        set => _filter = value;
    }
    #endregion
}

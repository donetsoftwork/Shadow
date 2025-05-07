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
    internal GroupBySqlQuery(SqlQuery having, ITableView source, IField[] fields)
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
    public GroupBySqlQuery(ITable table, SqlQuery having, params IField[] fields)
        : this(having, table, fields)
    {
    }
    /// <summary>
    /// 分组sql查询
    /// </summary>
    /// <param name="table"></param>
    /// <param name="fields"></param>
    public GroupBySqlQuery(ITable table, params IField[] fields)
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
    public GroupBySqlQuery(IAliasTable table, SqlQuery having, params IField[] fields)
        : this(having, table, fields)
    {
    }
    /// <summary>
    /// 分组sql查询
    /// </summary>
    /// <param name="table"></param>
    /// <param name="fields"></param>
    public GroupBySqlQuery(IAliasTable table, IField[] fields)
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
    public GroupBySqlQuery(string tableName, SqlQuery having, params IField[] fields)
        : this(having, SimpleTable.Use(tableName), fields)
    {
    }
    /// <summary>
    /// 分组sql查询
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="fields"></param>
    public GroupBySqlQuery(string tableName, params IField[] fields)
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
    public GroupBySqlQuery(IDataFilter filter, SqlQuery having, params IField[] fields)
        : this(having, filter, fields)
    {
    }
    /// <summary>
    /// 查询后再分组
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="fields"></param>
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
    public static GroupBySqlQuery Create(ITable table, params IField[] fields)
        => new(table, fields);
    /// <summary>
    /// 创建分组查询
    /// </summary>
    /// <param name="table"></param>
    /// <param name="columnNames"></param>
    /// <returns></returns>
    public static GroupBySqlQuery Create(ITable table, params IEnumerable<string> columnNames)
        => new(table, [.. table.Fields(columnNames)]);
    #endregion
    #region IAliasTable
    /// <summary>
    /// 创建分组查询
    /// </summary>
    /// <param name="table"></param>
    /// <param name="fields"></param>
    /// <returns></returns>
    public static GroupBySqlQuery Create(IAliasTable table, params IField[] fields)
        => new(table, fields);
    /// <summary>
    /// 创建分组查询
    /// </summary>
    /// <param name="table"></param>
    /// <param name="columnNames"></param>
    /// <returns></returns>
    public static GroupBySqlQuery Create(IAliasTable table, params IEnumerable<string> columnNames)
        => new(table, [.. table.Fields(columnNames)]);
    #endregion
    #region tableName
    /// <summary>
    /// 创建分组查询
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="fields"></param>
    /// <returns></returns>
    public static GroupBySqlQuery Create(string tableName, params IField[] fields)
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
    public static GroupBySqlQuery Create(IDataFilter filter, params IField[] fields)
        => new(filter, fields);
    /// <summary>
    /// 创建分组查询
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="columnNames"></param>
    /// <returns></returns>
    public static GroupBySqlQuery Create(IDataFilter filter, params IEnumerable<string> columnNames)
        => new(filter, [.. filter.Source.Fields(columnNames)]);
    #endregion
    #endregion
    #region TableViewBase
    /// <summary>
    /// 构造新字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    protected override IField NewField(string fieldName)
        => _source.NewField(fieldName);
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

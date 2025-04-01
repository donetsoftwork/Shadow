﻿using ShadowSql.Engines;
using ShadowSql.Fetches;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Queries;
using ShadowSql.Select;
using ShadowSql.SelectFields;
using ShadowSql.SingleSelect;
using System.Text;

namespace Dapper.Shadow.SingleSelect;

/// <summary>
/// Dapper表筛选列
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="executor"></param>
/// <param name="table"></param>
/// <param name="fields"></param>
public class DapperTableSingleSelect<TTable>(IExecutor executor, ITableView table, TableFields<TTable> fields)
    : SingleSelectBase<ITableView, TableFields<TTable>>(table, fields)
    , IDapperSingleSelect
    where TTable : ITable
{
    /// <summary>
    /// Dapper表筛选列
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="table"></param>
    public DapperTableSingleSelect(IExecutor executor, TTable table)
        : this(executor, table, new TableFields<TTable>(table))
    {
    }
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="table"></param>
    /// <param name="where"></param>
    public DapperTableSingleSelect(IExecutor executor, TTable table, ISqlLogic where)
        : this(executor, new TableFilter<TTable>(table, where), new TableFields<TTable>(table))
    {
    }
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="filter"></param>
    public DapperTableSingleSelect(IExecutor executor, TableFilter<TTable> filter)
        : this(executor, filter, new TableFields<TTable>(filter.Source))
    {
    }
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="query"></param>
    public DapperTableSingleSelect(IExecutor executor, TableQuery<TTable> query)
        : this(executor, query, new TableFields<TTable>(query.Source))
    {
    }
    #region 配置
    private readonly IExecutor _executor = executor;
    /// <summary>
    /// 执行器
    /// </summary>
    public IExecutor Executor
        => _executor;
    #endregion
}
///// <summary>
///// 表过滤筛选列
///// </summary>
///// <typeparam name="TTable"></typeparam>
///// <param name="executor"></param>
///// <param name="filter"></param>
//public sealed class DapperTableFilterSelect<TTable>(IExecutor executor, TableFilter<TTable> filter)
//    : SelectBase<ITableView, TableFields<TTable>>(filter, new TableFields<TTable>(filter.Source))
//    , IDapperSelect
//    where TTable : ITable
//{
//    #region 配置
//    private readonly IExecutor _executor = executor;
//    /// <summary>
//    /// 执行器
//    /// </summary>
//    public IExecutor Executor
//        => _executor;
//    #endregion
//}
///// <summary>
///// 表(及查询)筛选列
///// </summary>
///// <typeparam name="TTable"></typeparam>
///// <param name="executor"></param>
///// <param name="query"></param>
//public sealed class DapperTableQuerySelect<TTable>(IExecutor executor, TableQuery<TTable> query)
//    : SelectBase<ITableView, TableFields<TTable>>(query, new TableFields<TTable>(query.Source))
//    , IDapperSelect
//    where TTable : ITable
//{
//    #region 配置
//    private readonly IExecutor _executor = executor;
//    /// <summary>
//    /// 执行器
//    /// </summary>
//    public IExecutor Executor
//        => _executor;
//    #endregion
//}
/// <summary>
/// 表范围(分页)及列筛选
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="executor"></param>
/// <param name="fetch"></param>
/// <param name="fields"></param>
public sealed class DapperTableFetchSingleSelect<TTable>(IExecutor executor, TableFetch<TTable> fetch, TableFields<TTable> fields)
    : SingleSelectBase<IFetch, TableFields<TTable>>(fetch, fields)
    , IDapperSingleSelect
    where TTable : ITable
{
    /// <summary>
    /// 表范围(分页)及列筛选
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="fetch"></param>
    public DapperTableFetchSingleSelect(IExecutor executor, TableFetch<TTable> fetch)
        : this(executor, fetch, new TableFields<TTable>(fetch.Source))
    {
    }
    #region 配置
    private readonly IExecutor _executor = executor;
    /// <summary>
    /// 执行器
    /// </summary>
    public IExecutor Executor
        => _executor;
    #endregion
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    public override void Write(ISqlEngine engine, StringBuilder sql)
        => engine.SelectCursor(sql, this, _source);
}

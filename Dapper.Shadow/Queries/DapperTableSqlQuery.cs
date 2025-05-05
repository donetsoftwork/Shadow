using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Queries;
using ShadowSql.Tables;
using System;

namespace Dapper.Shadow.Queries;

/// <summary>
/// Dapper查询表
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="executor"></param>
/// <param name="table"></param>
/// <param name="query"></param>
public class DapperTableSqlQuery<TTable>(IExecutor executor, TTable table, SqlQuery query)
    : TableSqlQuery<TTable>(table, query), IDapperSource
     where TTable : ITable
{
    #region 配置
    private readonly IExecutor _executor = executor;
    /// <summary>
    /// 执行器
    /// </summary>
    public IExecutor Executor
        => _executor;
    #endregion
    #region 扩展查询功能
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    new public DapperTableSqlQuery<TTable> Where(Func<TTable, AtomicLogic> query)
    {
        _filter.AddLogic(query(_source));
        return this;
    }
    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    new public DapperTableSqlQuery<TTable> Apply(Func<SqlQuery, TTable, SqlQuery> query)
    {
        _filter = query(_filter, _source);
        return this;
    }
    #endregion
}

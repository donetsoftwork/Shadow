using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Tables;
using System;

namespace Dapper.Shadow.Queries;

/// <summary>
/// Dapper查询表
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="executor">执行器</param>
/// <param name="table">表</param>
/// <param name="query">查询</param>
public class DapperTableQuery<TTable>(IExecutor executor, TTable table, Logic query)
    : TableQuery<TTable>(table, query), IDapperSource
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
    #region 扩展查询
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="query">查询</param>
    /// <returns></returns>
    new public DapperTableQuery<TTable> And(Func<TTable, AtomicLogic> query)
    {
        _filter = _filter.And(query(_source));
        return this;
    }
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="query">查询</param>
    /// <returns></returns>
    new public DapperTableQuery<TTable> Or(Func<TTable, AtomicLogic> query)
    {
        _filter = _filter.Or(query(_source));
        return this;
    }
    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="query">查询</param>
    /// <returns></returns>
    new public DapperTableQuery<TTable> Apply(Func<Logic, TTable, Logic> query)
    {
        _filter = query(_filter, _source);
        return this;
    }
    #endregion
}

using ShadowSql;
using ShadowSql.AliasTables;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Variants;
using System;

namespace Dapper.Shadow.Queries;

/// <summary>
/// Dapper查询别名表
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="executor"></param>
/// <param name="table"></param>
/// <param name="query"></param>
public class DapperAliasTableQuery<TTable>(IExecutor executor, TableAlias<TTable> table, Logic query)
    : AliasTableQuery<TTable>(table, query), IDapperSource
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
    #region 查询扩展
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="select"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    new public DapperAliasTableQuery<TTable> And(Func<TTable, IColumn> select, Func<IPrefixField, AtomicLogic> query)
    {
        _filter = _filter.And(query(Prefix(select)));
        return this;
    }
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="select"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    new public DapperAliasTableQuery<TTable> Or(Func<TTable, IColumn> select, Func<IPrefixField, AtomicLogic> query)
    {
        _filter = _filter.Or(query(Prefix(select)));
        return this;
    }
    #endregion
}

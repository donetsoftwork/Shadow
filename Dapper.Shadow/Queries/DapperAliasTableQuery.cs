using ShadowSql.AliasTables;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using System;

namespace Dapper.Shadow.Queries;

/// <summary>
/// Dapper查询别名表
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="executor">执行器</param>
/// <param name="aliasTable">别名表</param>
/// <param name="query">查询</param>
public class DapperAliasTableQuery<TTable>(IExecutor executor, IAliasTable<TTable> aliasTable, Logic query)
    : AliasTableQuery<TTable>(aliasTable, query), IDapperSource
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
    /// <param name="select">筛选</param>
    /// <param name="query">查询</param>
    /// <returns></returns>
    new public DapperAliasTableQuery<TTable> And(Func<TTable, IColumn> select, Func<IPrefixField, AtomicLogic> query)
    {
        _filter = _filter.And(query(Prefix(select)));
        return this;
    }
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="select">筛选</param>
    /// <param name="query">查询</param>
    /// <returns></returns>
    new public DapperAliasTableQuery<TTable> Or(Func<TTable, IColumn> select, Func<IPrefixField, AtomicLogic> query)
    {
        _filter = _filter.Or(query(Prefix(select)));
        return this;
    }
    #endregion
}

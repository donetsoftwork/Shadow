using ShadowSql.AliasTables;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Queries;
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
public class DapperAliasTableSqlQuery<TTable>(IExecutor executor, TableAlias<TTable> table, SqlQuery query)
    : AliasTableSqlQuery<TTable>(table, query), IDapperSource
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
    /// 按逻辑查询
    /// </summary>
    /// <param name="select"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    new public DapperAliasTableSqlQuery<TTable> Where(Func<TTable, IColumn> select, Func<IColumn, AtomicLogic> query)
    {
        _filter.AddLogic(query(Prefix(select)));
        return this;
    }
    #endregion
}

using ShadowSql.AliasTables;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Variants;

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
}

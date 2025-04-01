using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Tables;

namespace Dapper.Shadow.Queries;

/// <summary>
/// Dapper查询表
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="executor"></param>
/// <param name="table"></param>
/// <param name="query"></param>
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
}

using ShadowSql.Identifiers;
using ShadowSql.Queries;
using ShadowSql.Tables;

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
}

using ShadowSql.Join;
using ShadowSql.Queries;

namespace Dapper.Shadow.Join;

/// <summary>
/// Dapper联表查询
/// </summary>
/// <param name="executor">执行器</param>
/// <param name="filter">过滤条件</param>
public class DapperJoinTableSqlQuery(IExecutor executor, SqlQuery filter)
    : JoinTableSqlQuery(filter), IDapperSource
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

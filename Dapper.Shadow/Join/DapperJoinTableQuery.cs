using ShadowSql.Join;
using ShadowSql.Logics;

namespace Dapper.Shadow.Join;

/// <summary>
/// Dapper联表查询
/// </summary>
/// <param name="executor">执行器</param>
/// <param name="filter">过滤条件</param>
public class DapperJoinTableQuery(IExecutor executor, Logic filter)
    : JoinTableQuery(filter), IDapperSource
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

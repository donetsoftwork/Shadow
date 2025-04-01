using ShadowSql.Generators;
using ShadowSql.Join;
using ShadowSql.Logics;

namespace Dapper.Shadow.Join;

/// <summary>
/// Dapper联表查询
/// </summary>
/// <param name="executor"></param>
/// <param name="aliasGenerator"></param>
/// <param name="filter"></param>
public class DapperJoinTableQuery(IExecutor executor, IIdentifierGenerator aliasGenerator, Logic filter)
    : JoinTableQuery(aliasGenerator, filter), IDapperSource
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

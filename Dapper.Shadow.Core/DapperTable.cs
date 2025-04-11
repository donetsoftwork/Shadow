using ShadowSql.Identifiers;

namespace Dapper.Shadow;

/// <summary>
/// Dapper表
/// </summary>
/// <param name="executor"></param>
/// <param name="name"></param>
public class DapperTable(IExecutor executor, string name)
    : Table(name), IDapperTable
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

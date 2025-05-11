using ShadowSql.Identifiers;
using ShadowSql.Update;

namespace Dapper.Shadow.Update;

/// <summary>
/// 多表(联表)修改
/// </summary>
/// <param name="executor"></param>
/// <param name="multiTable"></param>
public class DapperMultiTableUpdate(IExecutor executor, IMultiView multiTable)
    : MultiTableUpdate(multiTable), IDapperExecute
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

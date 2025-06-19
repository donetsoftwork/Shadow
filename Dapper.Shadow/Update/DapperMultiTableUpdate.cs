using ShadowSql.Identifiers;
using ShadowSql.Update;

namespace Dapper.Shadow.Update;

/// <summary>
/// 多表(联表)修改
/// </summary>
/// <param name="executor">执行器</param>
/// <param name="multiTable">多表(联表)</param>
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

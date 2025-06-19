using ShadowSql.Identifiers;
using ShadowSql.Select;

namespace Dapper.Shadow.Select;

/// <summary>
/// 多表视图筛选列
/// </summary>
/// <param name="executor">执行器</param>
/// <param name="multiView">多(联)表</param>
public class DapperMultiTableSelect(IExecutor executor, IMultiView multiView)
    : MultiSelectBase<ITableView>(multiView, multiView)
    , IDapperSelect
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

using ShadowSql.Identifiers;
using ShadowSql.Select;

namespace Dapper.Shadow.Select;

/// <summary>
/// 多表视图筛选列
/// </summary>
/// <param name="executor"></param>
/// <param name="source"></param>
public class DapperMultiTableSelect(IExecutor executor, IMultiView source)
    : MultiSelectBase<ITableView>(source, source)
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

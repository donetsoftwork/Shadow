using ShadowSql.Fetches;
using ShadowSql.Identifiers;

namespace Dapper.Shadow.Fetches;

/// <summary>
/// 多联表范围筛选
/// </summary>
/// <param name="executor"></param>
/// <param name="source"></param>
/// <param name="limit"></param>
/// <param name="offset"></param>
public class DapperMultiTableFetch(IExecutor executor, IMultiView source, int limit, int offset)
    : MultiTableFetch(source, limit, offset)
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

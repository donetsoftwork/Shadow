using ShadowSql.Fetches;
using ShadowSql.GroupBy;

namespace Dapper.Shadow.Fetches;

/// <summary>
/// 多(联)表分组后范围筛选
/// </summary>
/// <param name="executor"></param>
/// <param name="source"></param>
/// <param name="limit"></param>
/// <param name="offset"></param>
public class DapperGroupByMultiFetch(IExecutor executor, GroupByMultiQuery source, int limit, int offset)
    : GroupByMultiFetch(source, limit, offset)
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

using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using ShadowSql.Select;

namespace Dapper.Shadow.Select;

/// <summary>
/// GroupBy后再筛选列
/// </summary>
/// <param name="executor">执行器</param>
/// <param name="groupBy">分组查询</param>
/// <param name="multiView">多(联)表</param>
public class DapperGroupByMultiSelect(IExecutor executor, IGroupByView groupBy, IMultiView multiView)
    : GroupByMultiSelectBase<IGroupByView>(groupBy, groupBy, multiView)
    , IDapperSelect
{
    /// <summary>
    /// GroupBy后再筛选列
    /// </summary>
    /// <param name="executor">执行器</param>
    /// <param name="groupBy">分组查询</param>
    public DapperGroupByMultiSelect(IExecutor executor, GroupByMultiSqlQuery groupBy)
        :this(executor, groupBy, groupBy._source)
    {
    }
    /// <summary>
    /// GroupBy后再筛选列
    /// </summary>
    /// <param name="executor">执行器</param>
    /// <param name="groupBy">分组查询</param>
    public DapperGroupByMultiSelect(IExecutor executor, GroupByMultiQuery groupBy)
        : this(executor, groupBy, groupBy._source)
    {
    }
    #region 配置
    private readonly IExecutor _executor = executor;
    /// <summary>
    /// 执行器
    /// </summary>
    public IExecutor Executor
        => _executor;
    #endregion
}

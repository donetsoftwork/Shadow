using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using ShadowSql.Select;

namespace Dapper.Shadow.Select;

/// <summary>
/// GroupBy后再筛选列
/// </summary>
/// <param name="executor"></param>
/// <param name="groupBy"></param>
/// <param name="target"></param>
public class DapperGroupByMultiSelect(IExecutor executor, IGroupByView groupBy, IMultiView target)
    : GroupByMultiSelectBase<IGroupByView>(groupBy, groupBy, target)
    , IDapperSelect
{
    /// <summary>
    /// GroupBy后再筛选列
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="groupBy"></param>
    public DapperGroupByMultiSelect(IExecutor executor, GroupByMultiSqlQuery groupBy)
        :this(executor, groupBy, groupBy.Source)
    {
    }
    /// <summary>
    /// GroupBy后再筛选列
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="groupBy"></param>
    public DapperGroupByMultiSelect(IExecutor executor, GroupByMultiQuery groupBy)
        : this(executor, groupBy, groupBy.Source)
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

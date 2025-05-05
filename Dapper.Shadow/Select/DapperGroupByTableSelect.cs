using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using ShadowSql.Select;

namespace Dapper.Shadow.Select;

/// <summary>
/// GroupBy后再筛选列
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="executor"></param>
/// <param name="groupBy"></param>
/// <param name="target"></param>
public class DapperGroupByTableSelect<TTable>(IExecutor executor, IGroupByView groupBy, TTable target)
    : GroupBySelectBase<IGroupByView, TTable>(groupBy, groupBy, target)
    , IDapperSelect
    where TTable : ITable
{
    /// <summary>
    /// GroupBy后再筛选列
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="groupBy"></param>
    public DapperGroupByTableSelect(IExecutor executor, GroupByTableSqlQuery<TTable> groupBy)
        : this(executor, groupBy, groupBy.Source)
    {
    }
    /// <summary>
    /// GroupBy后再筛选列
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="groupBy"></param>
    public DapperGroupByTableSelect(IExecutor executor, GroupByTableQuery<TTable> groupBy)
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

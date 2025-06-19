using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using ShadowSql.Select;

namespace Dapper.Shadow.Select;

/// <summary>
/// GroupBy后再筛选列
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="executor">执行器</param>
/// <param name="groupBy">分组查询</param>
/// <param name="table">表</param>
public class DapperGroupByTableSelect<TTable>(IExecutor executor, IGroupByView groupBy, TTable table)
    : GroupBySelectBase<IGroupByView, TTable>(groupBy, groupBy, table)
    , IDapperSelect
    where TTable : ITable
{
    /// <summary>
    /// GroupBy后再筛选列
    /// </summary>
    /// <param name="executor">执行器</param>
    /// <param name="groupBy">分组查询</param>
    public DapperGroupByTableSelect(IExecutor executor, GroupByTableSqlQuery<TTable> groupBy)
        : this(executor, groupBy, groupBy._source)
    {
    }
    /// <summary>
    /// GroupBy后再筛选列
    /// </summary>
    /// <param name="executor">执行器</param>
    /// <param name="groupBy">分组查询</param>
    public DapperGroupByTableSelect(IExecutor executor, GroupByTableQuery<TTable> groupBy)
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

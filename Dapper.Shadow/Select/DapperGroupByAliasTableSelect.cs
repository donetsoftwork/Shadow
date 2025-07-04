using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using ShadowSql.Select;

namespace Dapper.Shadow.Select;

/// <summary>
/// GroupBy别名表后再筛选列
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="executor">执行器</param>
/// <param name="groupBy">分组查询</param>
/// <param name="aliasTable">别名表</param>
public class DapperGroupByAliasTableSelect<TTable>(IExecutor executor, IGroupByView groupBy, IAliasTable<TTable> aliasTable)
    : GroupBySelectBase<IGroupByView, IAliasTable<TTable>>(groupBy, groupBy, aliasTable)
    , IDapperSelect
    where TTable : ITable
{
    /// <summary>
    /// GroupBy别名表后再筛选列
    /// </summary>
    /// <param name="executor">执行器</param>
    /// <param name="groupBy">分组查询</param>
    public DapperGroupByAliasTableSelect(IExecutor executor, GroupByAliasTableSqlQuery<TTable> groupBy)
        : this(executor, groupBy, groupBy._source)
    {
    }
    /// <summary>
    /// GroupBy别名表后再筛选列
    /// </summary>
    /// <param name="executor">执行器</param>
    /// <param name="groupBy">分组查询</param>
    public DapperGroupByAliasTableSelect(IExecutor executor, GroupByAliasTableQuery<TTable> groupBy)
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

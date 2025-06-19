using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using ShadowSql.Variants;

namespace ShadowSql.Select;

/// <summary>
/// GroupBy别名表后再筛选列
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="groupBy">分组查询</param>
/// <param name="aliasTable">别名表</param>
public sealed class GroupByAliasTableSelect<TTable>(IGroupByView groupBy, IAliasTable<TTable> aliasTable)
    : GroupBySelectBase<IGroupByView, IAliasTable<TTable>>(groupBy, groupBy, aliasTable)
    where TTable : ITable
{
    /// <summary>
    /// GroupBy别名表后再筛选列
    /// </summary>
    /// <param name="groupBy">分组查询</param>
    public GroupByAliasTableSelect(GroupByAliasTableSqlQuery<TTable> groupBy)
        : this(groupBy, groupBy._source)
    {
    }
    /// <summary>
    /// GroupBy别名表后再筛选列
    /// </summary>
    /// <param name="groupBy">分组查询</param>
    public GroupByAliasTableSelect(GroupByAliasTableQuery<TTable> groupBy)
        : this(groupBy, groupBy._source)
    {
    }
}

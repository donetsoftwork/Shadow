using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using ShadowSql.Variants;

namespace ShadowSql.Select;

/// <summary>
/// GroupBy别名表后再筛选列
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="groupBy"></param>
/// <param name="target"></param>
public sealed class GroupByAliasTableSelect<TTable>(IGroupByView groupBy, IAliasTable<TTable> target)
    : GroupBySelectBase<IGroupByView, IAliasTable<TTable>>(groupBy, groupBy, target)
    where TTable : ITable
{
    /// <summary>
    /// GroupBy别名表后再筛选列
    /// </summary>
    /// <param name="groupBy"></param>
    public GroupByAliasTableSelect(GroupByAliasTableSqlQuery<TTable> groupBy)
        : this(groupBy, groupBy._source)
    {
    }
    /// <summary>
    /// GroupBy别名表后再筛选列
    /// </summary>
    /// <param name="groupBy"></param>
    public GroupByAliasTableSelect(GroupByAliasTableQuery<TTable> groupBy)
        : this(groupBy, groupBy._source)
    {
    }
}

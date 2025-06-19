using ShadowSql.Aggregates;
using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using System;

namespace ShadowSql.Select;

/// <summary>
/// GroupBy后再筛选列
/// </summary>
/// <typeparam name="TTable"></typeparam>
public class GroupByTableSelect<TTable> : GroupBySelectBase<IGroupByView, TTable>
    where TTable : ITable
{
    internal GroupByTableSelect(IGroupByView groupBy, TTable target)
        : base(groupBy, groupBy, target)
    {
    }
    /// <summary>
    /// GroupBy后再筛选列
    /// </summary>
    /// <param name="groupBy">分组查询</param>
    public GroupByTableSelect(GroupByTableSqlQuery<TTable> groupBy)
        : this(groupBy, groupBy._source)
    {
    }
    /// <summary>
    /// GroupBy后再筛选列
    /// </summary>
    /// <param name="groupBy">分组查询</param>
    public GroupByTableSelect(GroupByTableQuery<TTable> groupBy)
        : this(groupBy, groupBy._source)
    {
    }
    /// <summary>
    /// 聚合筛选
    /// </summary>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public GroupByTableSelect<TTable> SelectAggregate(Func<TTable, IAggregateFieldAlias> select)
    {
        SelectCore(select(_groupSource));
        return this;
    }
}
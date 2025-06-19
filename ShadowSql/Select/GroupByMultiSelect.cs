using ShadowSql.Aggregates;
using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using System;
using System.Collections.Generic;

namespace ShadowSql.Select;

/// <summary>
/// 联表分组后再筛选列
/// </summary>
public sealed class GroupByMultiSelect : GroupByMultiSelectBase<IGroupByView>
{
    /// <summary>
    /// GroupBy后再筛选列
    /// </summary>
    /// <param name="groupBy">分组查询</param>
    /// <param name="multiView">多(联)表</param>
    internal GroupByMultiSelect(IGroupByView groupBy, IMultiView multiView)
        : base(groupBy, groupBy, multiView)
    {
    }
    /// <summary>
    /// GroupBy后再筛选列
    /// </summary>
    /// <param name="groupBy">分组查询</param>
    public GroupByMultiSelect(GroupByMultiSqlQuery groupBy)
        : this(groupBy, groupBy._source)
    {
    }
    /// <summary>
    /// GroupBy后再筛选列
    /// </summary>
    /// <param name="groupBy">分组查询</param>
    public GroupByMultiSelect(GroupByMultiQuery groupBy)
        : this(groupBy, groupBy._source)
    {
    }
    #region SelectAggregate
    #region TAliasTable
    /// <summary>
    /// 聚合筛选
    /// </summary>
    /// <typeparam name="TAliasTable"></typeparam>
    /// <param name="tableName">表名</param>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public GroupByMultiSelect SelectAggregate<TAliasTable>(string tableName, Func<TAliasTable, IAggregateFieldAlias> select)
        where TAliasTable : IAliasTable
    {
        SelectCore(select(_groupSource.From<TAliasTable>(tableName)));
        return this;
    }
    /// <summary>
    /// 聚合筛选
    /// </summary>
    /// <typeparam name="TAliasTable"></typeparam>
    /// <param name="tableName">表名</param>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public GroupByMultiSelect SelectAggregate<TAliasTable>(string tableName, Func<TAliasTable, IEnumerable<IAggregateFieldAlias>> select)
        where TAliasTable : IAliasTable
    {
        SelectCore(select(_groupSource.From<TAliasTable>(tableName)));
        return this;
    }
    #endregion
    #region TTable
    /// <summary>
    /// 聚合筛选(先定位再聚合)
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="tableName">表名</param>
    /// <param name="select">筛选</param>
    /// <param name="aggregate">聚合</param>
    /// <returns></returns>
    public GroupByMultiSelect SelectAggregate<TTable>(string tableName, Func<TTable, IColumn> select, Func<IPrefixField, IAggregateFieldAlias> aggregate)
        where TTable : ITable
    {
        var member = _groupSource.Alias<TTable>(tableName);
        SelectCore(aggregate(member.Prefix(select(member.Target))));
        return this;
    }
    #endregion
    #endregion
}

using ShadowSql.Expressions.GroupBy;
using ShadowSql.Expressions.VisitSource;
using ShadowSql.Identifiers;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace ShadowSql.Expressions.Select;

/// <summary>
/// 联表分组后再筛选列
/// </summary>
public sealed class GroupByMultiSelect<TKey> : GroupByMultiSelectBase<IGroupByView>
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
    public GroupByMultiSelect(GroupByMultiSqlQuery<TKey> groupBy)
        : this(groupBy, groupBy._source)
    {
    }
    /// <summary>
    /// GroupBy后再筛选列
    /// </summary>
    /// <param name="groupBy">分组查询</param>
    public GroupByMultiSelect(GroupByMultiQuery<TKey> groupBy)
        : this(groupBy, groupBy._source)
    {
    }
    /// <summary>
    /// 筛选分组列
    /// </summary>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public GroupByMultiSelect<TKey> Select<TProperty>(Expression<Func<TKey, TProperty>> select)
    {
        GroupByKeyVisitor.Select(_source, _selected, select);
        return this;
    }
    /// <summary>
    /// 从聚合筛选
    /// </summary>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public GroupByMultiSelect<TKey> Select<TEntity, TProperty>(Expression<Func<IGrouping<TKey, TEntity>, TProperty>> select)
    {
        GroupByVisitor.Select(_source, _groupSource, _selected, select);
        return this;
    }
    /// <summary>
    /// 从聚合筛选
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="table">表</param>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public GroupByMultiSelect<TKey> Select<TEntity, TProperty>(string table, Expression<Func<IGrouping<TKey, TEntity>, TProperty>> select)
    {
        GroupByVisitor.Select(_source, _groupSource.From(table), _selected, select);
        return this;
    }
}

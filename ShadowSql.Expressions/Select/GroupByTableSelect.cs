using ShadowSql.Expressions.GroupBy;
using ShadowSql.Expressions.VisitSource;
using ShadowSql.Identifiers;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace ShadowSql.Expressions.Select;

/// <summary>
/// GroupBy后再筛选列
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TEntity"></typeparam>
public class GroupByTableSelect<TKey, TEntity> : GroupBySelectBase<IGroupByView, ITableView>
{
    internal GroupByTableSelect(IGroupByView groupBy, ITableView target)
        : base(groupBy, groupBy, target)
    {
    }
    /// <summary>
    /// GroupBy后再筛选列
    /// </summary>
    /// <param name="groupBy">分组查询</param>
    public GroupByTableSelect(GroupByTableSqlQuery<TKey, TEntity> groupBy)
        : this(groupBy, groupBy._source)
    {
    }
    /// <summary>
    /// GroupBy后再筛选列
    /// </summary>
    /// <param name="groupBy">分组查询</param>
    public GroupByTableSelect(GroupByTableQuery<TKey, TEntity> groupBy)
        : this(groupBy, groupBy._source)
    {
    }
    /// <summary>
    /// 筛选分组列
    /// </summary>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public GroupByTableSelect<TKey, TEntity> Select<TProperty>(Expression<Func<TKey, TProperty>> select)
    {
        GroupByKeyVisitor.Select(_source, _selected, select);
        return this;
    }
    /// <summary>
    /// 从聚合筛选
    /// </summary>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public GroupByTableSelect<TKey, TEntity> Select<TProperty>(Expression<Func<IGrouping<TKey, TEntity>, TProperty>> select)
    {
        GroupByVisitor.Select(_source, _selected, select);
        return this;
    }
}
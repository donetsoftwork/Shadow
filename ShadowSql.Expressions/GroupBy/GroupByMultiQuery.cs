using ShadowSql.Expressions.VisitSource;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace ShadowSql.Expressions.GroupBy;

/// <summary>
/// 对MultiQuery进行分组查询
/// </summary>
/// <param name="multiTable">多表(联表)</param>
/// <param name="fields">字段</param>
/// <param name="filter">过滤条件</param>
public class GroupByMultiQuery<TKey>(IMultiView multiTable, IField[] fields, Logic filter)
    : GroupByQueryBase<IMultiView>(multiTable, fields, filter)
{
    /// <summary>
    /// 对多表进行分组查询
    /// </summary>
    /// <param name="multiTable">多表(联表)</param>
    /// <param name="fields">字段</param>
    public GroupByMultiQuery(IMultiView multiTable, IField[] fields)
        : this(multiTable, fields, new AndLogic())
    {
    }
    #region IAliasTable
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="table">表</param>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public GroupByMultiQuery<TKey> And<TEntity>(string table, Expression<Func<IGrouping<TKey, TEntity>, bool>> query)
    {
        var visitor = GroupByVisitor.Having(this, _source.From(table), _filter.ToAnd(), query);
        _filter = visitor.Logic;
        return this;
    }
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TParameter"></typeparam>
    /// <param name="table">表</param>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public GroupByMultiQuery<TKey> And<TEntity, TParameter>(string table, Expression<Func<IGrouping<TKey, TEntity>, TParameter, bool>> query)
    {
        var visitor = GroupByVisitor.Having(this, _source.From(table), _filter.ToAnd(), query);
        _filter = visitor.Logic;
        return this;
    }
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="table">表</param>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public GroupByMultiQuery<TKey> Or<TEntity>(string table, Expression<Func<IGrouping<TKey, TEntity>, bool>> query)
    {
        var visitor = GroupByVisitor.Having(this, _source.From(table), _filter.ToOr(), query);
        _filter = visitor.Logic;
        return this;
    }
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TParameter"></typeparam>
    /// <param name="table">表</param>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public GroupByMultiQuery<TKey> Or<TEntity, TParameter>(string table, Expression<Func<IGrouping<TKey, TEntity>, TParameter, bool>> query)
    {
        var visitor = GroupByVisitor.Having(this, _source.From(table), _filter.ToOr(), query);
        _filter = visitor.Logic;
        return this;
    }
    #endregion
    #region IMultiView
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public GroupByMultiQuery<TKey> And<TEntity>(Expression<Func<IGrouping<TKey, TEntity>, bool>> query)
    {
        GroupByVisitor.Having(this, _source, _filter.ToAnd(), query);
        return this;
    }
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TParameter"></typeparam>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public GroupByMultiQuery<TKey> And<TEntity, TParameter>(Expression<Func<IGrouping<TKey, TEntity>, TParameter, bool>> query)
    {
        GroupByVisitor.Having(this, _source, _filter.ToAnd(), query);
        return this;
    }
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public GroupByMultiQuery<TKey> Or<TEntity>(Expression<Func<IGrouping<TKey, TEntity>, bool>> query)
    {
        GroupByVisitor.Having(this, _source, _filter.ToOr(), query);
        return this;
    }
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TParameter"></typeparam>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public GroupByMultiQuery<TKey> Or<TEntity, TParameter>(Expression<Func<IGrouping<TKey, TEntity>, TParameter, bool>> query)
    {
        GroupByVisitor.Having(this, _source, _filter.ToOr(), query);
        return this;
    }
    #endregion
}

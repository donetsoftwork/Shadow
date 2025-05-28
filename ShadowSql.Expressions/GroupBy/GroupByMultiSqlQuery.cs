using ShadowSql.Expressions.VisitSource;
using ShadowSql.Identifiers;
using ShadowSql.Queries;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace ShadowSql.Expressions.GroupBy;

/// <summary>
/// 对MultiQuery进行分组查询
/// </summary>
/// <param name="multiTable"></param>
/// <param name="fields"></param>
/// <param name="filter"></param>
public class GroupByMultiSqlQuery<TKey>(IMultiView multiTable, IField[] fields, SqlQuery filter)
    : GroupBySqlQueryBase<IMultiView>(multiTable, fields, filter)
{
    /// <summary>
    /// 对多表进行分组查询
    /// </summary>
    /// <param name="multiTable"></param>
    /// <param name="fields"></param>
    public GroupByMultiSqlQuery(IMultiView multiTable, IField[] fields)
        : this(multiTable, fields, SqlQuery.CreateAndQuery())
    {
    }
    #region 查询扩展
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="table"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public GroupByMultiSqlQuery<TKey> Having<TEntity>(string table, Expression<Func<IGrouping<TKey, TEntity>, bool>> query)
    {
        GroupByVisitor.Having(this, _source.From(table), _filter._complex, query);
        return this;
    }
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TParameter"></typeparam>
    /// <param name="table"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public GroupByMultiSqlQuery<TKey> Having<TEntity, TParameter>(string table, Expression<Func<IGrouping<TKey, TEntity>, TParameter, bool>> query)
    {
        GroupByVisitor.Having(this, _source.From(table), _filter._complex, query);
        return this;
    }
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="query"></param>
    /// <returns></returns>
    public GroupByMultiSqlQuery<TKey> Having<TEntity>(Expression<Func<IGrouping<TKey, TEntity>, bool>> query)
    {
        GroupByVisitor.Having(this, _source, _filter._complex, query);
        return this;
    }
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TParameter"></typeparam>
    /// <param name="query"></param>
    /// <returns></returns>
    public GroupByMultiSqlQuery<TKey> Having<TEntity, TParameter>(Expression<Func<IGrouping<TKey, TEntity>, TParameter, bool>> query)
    {
        GroupByVisitor.Having(this, _source, _filter._complex, query);
        return this;
    }
    #endregion
}

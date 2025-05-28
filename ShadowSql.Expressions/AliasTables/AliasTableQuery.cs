using ShadowSql.Expressions.Filters;
using ShadowSql.Expressions.VisitSource;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Queries;
using System;
using System.Linq.Expressions;

namespace ShadowSql.Expressions.AliasTables;

/// <summary>
/// 逻辑查询别名表
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <param name="table"></param>
/// <param name="filter"></param>
public class AliasTableQuery<TEntity>(IAliasTable table, Logic filter)
    : DataFilterBase<IAliasTable, Logic>(table, filter), IDataQuery
{
    #region TEntity
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public AliasTableQuery<TEntity> And(Expression<Func<TEntity, bool>> query)
    {
        var visitor = TableVisitor.Where(_source, _filter.ToAnd(), query);
        _filter = visitor.Logic;
        return this;
    }
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public AliasTableQuery<TEntity> Or(Expression<Func<TEntity, bool>> query)
    {
        var visitor = TableVisitor.Where(_source, _filter.ToOr(), query);
        _filter = visitor.Logic;
        return this;
    }
    #endregion
    #region TParameter
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public AliasTableQuery<TEntity> And<TParameter>(Expression<Func<TEntity, TParameter, bool>> query)
    {
        var visitor = TableVisitor.Where(_source, _filter.ToAnd(), query);
        _filter = visitor.Logic;
        return this;
    }
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public AliasTableQuery<TEntity> Or<TParameter>(Expression<Func<TEntity, TParameter, bool>> query)
    {
        var visitor = TableVisitor.Where(_source, _filter.ToOr(), query);
        _filter = visitor.Logic;
        return this;
    }
    #endregion
    #region IDataQuery
    Logic IDataQuery.Logic
    {
        get => _filter;
        set => _filter = value;
    }
    #endregion
}

using ShadowSql.Expressions.VisitSource;
using ShadowSql.Identifiers;
using ShadowSql.Join;
using ShadowSql.Logics;
using ShadowSql.Queries;
using System;
using System.Linq.Expressions;

namespace ShadowSql.Expressions.Join;

/// <summary>
/// 联表俩俩关联查询
/// </summary>
/// <param name="root"></param>
/// <param name="left"></param>
/// <param name="right"></param>
/// <param name="onQuery"></param>
public class JoinOnQuery<TLeft, TRight>(JoinTableQuery root, IAliasTable left, IAliasTable right, Logic onQuery)
    : JoinOnCoreBase<JoinTableQuery, Logic>(root, left, right, onQuery), IDataQuery
{
    /// <summary>
    /// 联表俩俩关联查询
    /// </summary>
    /// <param name="root"></param>
    /// <param name="left"></param>
    /// <param name="right"></param>
    public JoinOnQuery(JoinTableQuery root, IAliasTable left, IAliasTable right)
        : this(root, left, right, new AndLogic())
    {
    }
    /// <summary>
    /// 联表查询
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public JoinOnQuery<TLeft, TRight> And(Expression<Func<TLeft, TRight, bool>> query)
    {
        var visitor = JoinOnVisitor.On(this, _filter.ToAnd(), query);
        _filter = visitor.Logic;
        return this;
    }
    /// <summary>
    /// 联表查询
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public JoinOnQuery<TLeft, TRight> Or(Expression<Func<TLeft, TRight, bool>> query)
    {
        var visitor = JoinOnVisitor.On(this, _filter.ToOr(), query);
        _filter = visitor.Logic;
        return this;
    }
    #region IDataQuery
    Logic IDataQuery.Logic
    {
        get => _filter;
        set => _filter = value;
    }
    #endregion
}

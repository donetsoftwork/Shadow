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
/// <param name="joinTable">联表</param>
/// <param name="left">左</param>
/// <param name="right">右</param>
/// <param name="onQuery">联表逻辑</param>
public class JoinOnQuery<TLeft, TRight>(JoinTableQuery joinTable, IAliasTable left, IAliasTable right, Logic onQuery)
    : JoinOnCoreBase<JoinTableQuery, Logic>(joinTable, left, right, onQuery), IDataQuery
{
    /// <summary>
    /// 联表俩俩关联查询
    /// </summary>
    /// <param name="joinTable">联表</param>
    /// <param name="left">左</param>
    /// <param name="right">右</param>
    public JoinOnQuery(JoinTableQuery joinTable, IAliasTable left, IAliasTable right)
        : this(joinTable, left, right, new AndLogic())
    {
    }
    /// <summary>
    /// 联表查询
    /// </summary>
    /// <param name="query">查询</param>
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
    /// <param name="query">查询</param>
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

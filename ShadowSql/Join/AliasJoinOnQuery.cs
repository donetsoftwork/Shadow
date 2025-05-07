using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Queries;
using System;

namespace ShadowSql.Join;

/// <summary>
/// 联表俩俩关联查询
/// </summary>
/// <typeparam name="TLeft"></typeparam>
/// <typeparam name="TRight"></typeparam>
/// <param name="root"></param>
/// <param name="left"></param>
/// <param name="right"></param>
/// <param name="onQuery"></param>
public class AliasJoinOnQuery<TLeft, TRight>(JoinTableQuery root, TLeft left, TRight right, Logic onQuery)
    : JoinOnBase<JoinTableQuery, TLeft, TRight, ITable, ITable, Logic>(root, left, right, onQuery), IDataQuery
    where TLeft : IAliasTable<ITable>
    where TRight : IAliasTable<ITable>
{
    /// <summary>
    /// 联表俩俩关联查询
    /// </summary>
    /// <param name="root"></param>
    /// <param name="left"></param>
    /// <param name="right"></param>
    public AliasJoinOnQuery(JoinTableQuery root, TLeft left, TRight right)
        : this(root, left, right, new AndLogic())
    {
    }
    /// <summary>
    /// 按列查询
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <param name="logic"></param>
    /// <returns></returns>
    public AliasJoinOnQuery<TLeft, TRight> Apply(Func<TLeft, IPrefixField> left, Func<TRight, IPrefixField> right, Func<Logic, IPrefixField, IPrefixField, Logic> logic)
    {
        _filter = logic(_filter, left(_left), right(_source));
        return this;
    }
    /// <summary>
    /// 查询左表
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public AliasJoinOnQuery<TLeft, TRight> ApplyLeft(Func<Logic, TLeft, Logic> query)
    {
        _root._filter = query(_root._filter, _left);
        return this;
    }
    /// <summary>
    /// 查询右表
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public AliasJoinOnQuery<TLeft, TRight> ApplyRight(Func<Logic, TRight, Logic> query)
    {
        _root._filter = query(_root._filter, _source);
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

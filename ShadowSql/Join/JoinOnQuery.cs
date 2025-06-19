using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Queries;
using System;

namespace ShadowSql.Join;

/// <summary>
/// 联表俩俩关联查询
/// </summary>
/// <typeparam name="LTable"></typeparam>
/// <typeparam name="RTable"></typeparam>
/// <param name="joinTable">联表</param>
/// <param name="left">左</param>
/// <param name="right">右</param>
/// <param name="onQuery">联表逻辑</param>
public class JoinOnQuery<LTable, RTable>(JoinTableQuery joinTable, IAliasTable<LTable> left, IAliasTable<RTable> right, Logic onQuery)
    : JoinOnBase<JoinTableQuery, IAliasTable<LTable>, IAliasTable<RTable>, LTable, RTable, Logic>(joinTable, left, right, onQuery), IDataQuery
    where LTable : ITable
    where RTable : ITable
{
    /// <summary>
    /// 联表俩俩关联查询
    /// </summary>
    /// <param name="joinTable">联表</param>
    /// <param name="left">左</param>
    /// <param name="right">右</param>
    public JoinOnQuery(JoinTableQuery joinTable, IAliasTable<LTable> left, IAliasTable<RTable> right)
        : this(joinTable, left, right, new AndLogic())
    {
    }
    /// <summary>
    /// 按列查询
    /// </summary>
    /// <param name="left">左</param>
    /// <param name="right">右</param>
    /// <param name="logic">查询逻辑</param>
    /// <returns></returns>
    public JoinOnQuery<LTable, RTable> Apply(Func<LTable, IColumn> left, Func<RTable, IColumn> right, Func<Logic, IPrefixField, IPrefixField, Logic> logic)
    {
        _filter = logic(_filter, _left.Prefix(left(_left.Target)), _source.Prefix(right(_source.Target)));
        return this;
    }
    /// <summary>
    /// 查询左表
    /// </summary>
    /// <param name="left">左</param>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public JoinOnQuery<LTable, RTable> ApplyLeft(Func<LTable, IColumn> left, Func<Logic, IPrefixField, Logic> query)
    {
        _root._filter = query(_root._filter, _left.Prefix(left(_left.Target)));
        return this;
    }
    /// <summary>
    /// 查询右表
    /// </summary>
    /// <param name="right">右</param>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public JoinOnQuery<LTable, RTable> ApplyRight(Func<RTable, IColumn> right, Func<Logic, IPrefixField, Logic> query)
    {
        _root._filter = query(_root._filter, _source.Prefix(right(_source.Target)));
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

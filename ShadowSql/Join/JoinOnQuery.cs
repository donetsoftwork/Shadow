using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Queries;
using ShadowSql.Variants;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace ShadowSql.Join;

/// <summary>
/// 联表俩俩关联查询
/// </summary>
/// <typeparam name="LTable"></typeparam>
/// <typeparam name="RTable"></typeparam>
/// <param name="root"></param>
/// <param name="left"></param>
/// <param name="right"></param>
/// <param name="onQuery"></param>
public class JoinOnQuery<LTable, RTable>(JoinTableQuery root, TableAlias<LTable> left, TableAlias<RTable> right, Logic onQuery)
    : JoinOnBase<JoinTableQuery, TableAlias<LTable>, TableAlias<RTable>, LTable, RTable, Logic>(root, left, right, onQuery), IDataQuery
    where LTable : ITable
    where RTable : ITable
{
    /// <summary>
    /// 联表俩俩关联查询
    /// </summary>
    /// <param name="root"></param>
    /// <param name="left"></param>
    /// <param name="right"></param>
    public JoinOnQuery(JoinTableQuery root, TableAlias<LTable> left, TableAlias<RTable> right)
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
    public JoinOnQuery<LTable, RTable> Apply(Func<LTable, IColumn> left, Func<RTable, IColumn> right, Func<Logic, IPrefixColumn, IPrefixColumn, Logic> logic)
    {
        _filter = logic(_filter, _left.Prefix(left(_left.Target)), _source.Prefix(right(_source.Target)));
        return this;
    }
    /// <summary>
    /// 查询左表
    /// </summary>
    /// <param name="left"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public JoinOnQuery<LTable, RTable> ApplyLeft(Func<LTable, IColumn> left, Func<Logic, IColumn, Logic> query)
    {
        _root._filter = query(_root._filter, _left.Prefix(left(_left.Target)));
        return this;
    }
    /// <summary>
    /// 查询右表
    /// </summary>
    /// <param name="right"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public JoinOnQuery<LTable, RTable> ApplyRight(Func<RTable, IColumn> right, Func<Logic, IColumn, Logic> query)
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

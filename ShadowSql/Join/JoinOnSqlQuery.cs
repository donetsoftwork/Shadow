using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Queries;
using ShadowSql.Variants;
using System;

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
public class JoinOnSqlQuery<LTable, RTable>(JoinTableSqlQuery root, TableAlias<LTable> left, TableAlias<RTable> right, SqlQuery onQuery)
    : JoinOnBase<JoinTableSqlQuery, TableAlias<LTable>, TableAlias<RTable>, LTable, RTable, SqlQuery>(root, left, right, onQuery), IDataSqlQuery
    where LTable : ITable
    where RTable : ITable
{
    /// <summary>
    /// 联表俩俩关联查询
    /// </summary>
    /// <param name="root"></param>
    /// <param name="left"></param>
    /// <param name="right"></param>
    public JoinOnSqlQuery(JoinTableSqlQuery root, TableAlias<LTable> left, TableAlias<RTable> right)
        : this(root, left, right, SqlQuery.CreateAndQuery())
    { 
    }
    #region IDataQuery
    SqlQuery IDataSqlQuery.Query
    {
        get => _filter;
        set => _filter = value;
    }
    #endregion
    #region On
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <param name="select"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public JoinOnSqlQuery<LTable, RTable> OnLeft(Func<LTable, IColumn> select, Func<IPrefixField, AtomicLogic> query)
    {
        _filter.AddLogic(query(_left.Prefix(select(_left.Target))));
        return this;
    }
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <param name="select"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public JoinOnSqlQuery<LTable, RTable> OnRight(Func<RTable, IColumn> select, Func<IPrefixField, AtomicLogic> query)
    {
        _filter.AddLogic(query(_source.Prefix(select(_source.Target))));
        return this;
    }
    #endregion
    #region Where
    #region WhereLeft
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <param name="select"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public JoinOnSqlQuery<LTable, RTable> WhereLeft(Func<LTable, IColumn> select, Func<IPrefixField, AtomicLogic> query)
    {
        _root.Where(query(_left.Prefix(select(_left.Target))));
        return this;
    }
    #endregion
    #region WhereRight
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <param name="select"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public JoinOnSqlQuery<LTable, RTable> WhereRight(Func<RTable, IColumn> select, Func<IPrefixField, AtomicLogic> query)
    {
        _root.Where(query(_source.Prefix(select(_source.Target))));
        return this;
    }
    #endregion
    #endregion
}

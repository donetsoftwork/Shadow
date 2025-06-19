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
/// <param name="onQuery">联表查询</param>
public class JoinOnSqlQuery<LTable, RTable>(JoinTableSqlQuery joinTable, IAliasTable<LTable> left, IAliasTable<RTable> right, SqlQuery onQuery)
    : JoinOnBase<JoinTableSqlQuery, IAliasTable<LTable>, IAliasTable<RTable>, LTable, RTable, SqlQuery>(joinTable, left, right, onQuery), IDataSqlQuery
    where LTable : ITable
    where RTable : ITable
{
    /// <summary>
    /// 联表俩俩关联查询
    /// </summary>
    /// <param name="joinTable">联表</param>
    /// <param name="left">左</param>
    /// <param name="right">右</param>
    public JoinOnSqlQuery(JoinTableSqlQuery joinTable, IAliasTable<LTable> left, IAliasTable<RTable> right)
        : this(joinTable, left, right, SqlQuery.CreateAndQuery())
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
    /// <param name="select">筛选</param>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public JoinOnSqlQuery<LTable, RTable> OnLeft(Func<LTable, IColumn> select, Func<IPrefixField, AtomicLogic> query)
    {
        _filter.AddLogic(query(_left.Prefix(select(_left.Target))));
        return this;
    }
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <param name="select">筛选</param>
    /// <param name="query">查询</param>
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
    /// <param name="select">筛选</param>
    /// <param name="query">查询</param>
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
    /// <param name="select">筛选</param>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public JoinOnSqlQuery<LTable, RTable> WhereRight(Func<RTable, IColumn> select, Func<IPrefixField, AtomicLogic> query)
    {
        _root.Where(query(_source.Prefix(select(_source.Target))));
        return this;
    }
    #endregion
    #endregion
}

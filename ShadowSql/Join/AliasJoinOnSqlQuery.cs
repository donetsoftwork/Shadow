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
/// <param name="onSqlQuery"></param>
public class AliasJoinOnSqlQuery<TLeft, TRight>(JoinTableSqlQuery root, TLeft left, TRight right, SqlQuery onSqlQuery)
    : JoinOnBase<JoinTableSqlQuery, TLeft, TRight, ITable, ITable, SqlQuery>(root, left, right, onSqlQuery), IDataSqlQuery
    where TLeft : IAliasTable<ITable>
    where TRight : IAliasTable<ITable>
{
    /// <summary>
    /// 联表俩俩关联查询
    /// </summary>
    /// <param name="root"></param>
    /// <param name="left"></param>
    /// <param name="right"></param>
    public AliasJoinOnSqlQuery(JoinTableSqlQuery root, TLeft left, TRight right)
        : this(root, left, right, new SqlAndQuery())
    {
    }
    #region On
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public AliasJoinOnSqlQuery<TLeft, TRight> OnLeft(Func<TLeft, AtomicLogic> query)
    {
        _filter.AddLogic(query(_left));
        return this;
    }
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public AliasJoinOnSqlQuery<TLeft, TRight> OnRight(Func<TRight, AtomicLogic> query)
    {
        _filter.AddLogic(query(_source));
        return this;
    }
    #endregion
    #region Where
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public AliasJoinOnSqlQuery<TLeft, TRight> WhereLeft(Func<TLeft, AtomicLogic> query)
    {
        _root.Where(query(_left));
        return this;
    }
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public AliasJoinOnSqlQuery<TLeft, TRight> WhereRight(Func<TRight, AtomicLogic> query)
    {
        _root.Where(query(_source));
        return this;
    }
    #endregion
    #region IDataSqlQuery
    SqlQuery IDataSqlQuery.Query
    {
        get => _filter;
        set => _filter = value;
    }
    #endregion
}

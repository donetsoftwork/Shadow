using ShadowSql.Expressions.VisitSource;
using ShadowSql.Identifiers;
using ShadowSql.Join;
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
/// <param name="onQuery">联表查询</param>
public class JoinOnSqlQuery<TLeft, TRight>(JoinTableSqlQuery joinTable, IAliasTable left, IAliasTable right, SqlQuery onQuery)
    : JoinOnCoreBase<JoinTableSqlQuery, SqlQuery>(joinTable, left, right, onQuery), IDataSqlQuery
{
    /// <summary>
    /// 联表俩俩关联查询
    /// </summary>
    /// <param name="joinTable">联表</param>
    /// <param name="left">左</param>
    /// <param name="right">右</param>
    public JoinOnSqlQuery(JoinTableSqlQuery joinTable, IAliasTable left, IAliasTable right)
        : this(joinTable, left, right, SqlQuery.CreateAndQuery())
    { 
    }
    #region On
    /// <summary>
    /// 按字段联表
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="left">左</param>
    /// <param name="right">右</param>
    /// <returns></returns>
    public JoinOnSqlQuery<TLeft, TRight> On<TKey>(Expression<Func<TLeft, TKey>> left, Expression<Func<TRight, TKey>> right)
    {
        if (TableVisitor.GetFieldByExpression(_left, left) is IPrefixField leftField && TableVisitor.GetFieldByExpression(_source, right) is IPrefixField rightField)
            this.On(leftField, rightField);
        return this;
    }
    /// <summary>
    /// 联表查询
    /// </summary>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public JoinOnSqlQuery<TLeft, TRight> On(Expression<Func<TLeft, TRight, bool>> query)
    {
        JoinOnVisitor.On(this, _filter._complex, query);
        return this;
    }
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public JoinOnSqlQuery<TLeft, TRight> OnLeft(Expression<Func<TLeft, bool>> query)
    {
        TableVisitor.Where(_left, _filter._complex, query);
        return this;
    }
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public JoinOnSqlQuery<TLeft, TRight> OnRight(Expression<Func<TRight, bool>> query)
    {
        TableVisitor.Where(_source, _filter._complex, query);
        return this;
    }
    #endregion
    #region Where
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public JoinOnSqlQuery<TLeft, TRight> WhereLeft(Expression<Func<TLeft, bool>> query)
    {
        TableVisitor.Where(_left, _root._filter._complex, query);
        return this;
    }
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public JoinOnSqlQuery<TLeft, TRight> WhereRight(Expression<Func<TRight, bool>> query)
    {
        TableVisitor.Where(_source, _root._filter._complex, query);
        return this;
    }
    #endregion
    #region IDataQuery
    SqlQuery IDataSqlQuery.Query
    {
        get => _filter;
        set => _filter = value;
    }
    #endregion
}

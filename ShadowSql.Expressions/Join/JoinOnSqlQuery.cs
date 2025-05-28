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
/// <param name="root"></param>
/// <param name="left"></param>
/// <param name="right"></param>
/// <param name="onQuery"></param>
public class JoinOnSqlQuery<TLeft, TRight>(JoinTableSqlQuery root, IAliasTable left, IAliasTable right, SqlQuery onQuery)
    : JoinOnCoreBase<JoinTableSqlQuery, SqlQuery>(root, left, right, onQuery), IDataSqlQuery
{
    /// <summary>
    /// 联表俩俩关联查询
    /// </summary>
    /// <param name="root"></param>
    /// <param name="left"></param>
    /// <param name="right"></param>
    public JoinOnSqlQuery(JoinTableSqlQuery root, IAliasTable left, IAliasTable right)
        : this(root, left, right, SqlQuery.CreateAndQuery())
    { 
    }
    #region On
    /// <summary>
    /// 按字段联表
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="left"></param>
    /// <param name="right"></param>
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
    /// <param name="query"></param>
    /// <returns></returns>
    public JoinOnSqlQuery<TLeft, TRight> On(Expression<Func<TLeft, TRight, bool>> query)
    {
        JoinOnVisitor.On(this, _filter._complex, query);
        return this;
    }
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public JoinOnSqlQuery<TLeft, TRight> OnLeft(Expression<Func<TLeft, bool>> query)
    {
        TableVisitor.Where(_left, _filter._complex, query);
        return this;
    }
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <param name="query"></param>
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
    /// <param name="query"></param>
    /// <returns></returns>
    public JoinOnSqlQuery<TLeft, TRight> WhereLeft(Expression<Func<TLeft, bool>> query)
    {
        TableVisitor.Where(_left, _root._filter._complex, query);
        return this;
    }
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <param name="query"></param>
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

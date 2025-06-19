using ShadowSql.Expressions.Tables;
using ShadowSql.Expressions.Update;
using ShadowSql.Expressions.VisitSource;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Tables;
using System;
using System.Linq.Expressions;

namespace ShadowSql.Expressions;

/// <summary>
/// 构造修改扩展方法
/// </summary>
public static partial class ShadowSqlServices
{
    #region TableUpdate
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="where">查询条件</param>
    /// <returns></returns>
    public static TableUpdate<TEntity> ToUpdate<TEntity>(this ITable table, ISqlLogic where)
        => new(table, where);
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public static TableUpdate<TEntity> ToUpdate<TEntity>(this ITable table, Expression<Func<TEntity, bool>> query)
        => new(table, TableVisitor.Where(table, new AndLogic(), query).Logic);
    /// <summary>
    /// 修改
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TParameter"></typeparam>
    /// <param name="table">表</param>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public static TableUpdate<TEntity> ToUpdate<TEntity, TParameter>(this ITable table, Expression<Func<TEntity, TParameter, bool>> query)
        => new(table, TableVisitor.Where(table, new AndLogic(), query).Logic);
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public static TableUpdate<TEntity> ToUpdate<TEntity>(this TableSqlQuery<TEntity> query)
        => new(query.Source, query._filter);
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public static TableUpdate<TEntity> ToUpdate<TEntity>(this TableSqlQuery query)
        => new((ITable)query.Source, query._filter);
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public static TableUpdate<TEntity> ToUpdate<TEntity>(this TableQuery<TEntity> query)
        => new(query.Source, query._filter);
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public static TableUpdate<TEntity> ToUpdate<TEntity>(this TableQuery query)
        => new((ITable)query.Source, query._filter);
    #endregion   
}

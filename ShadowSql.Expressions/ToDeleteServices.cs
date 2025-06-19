using ShadowSql.Delete;
using ShadowSql.Expressions.AliasTables;
using ShadowSql.Expressions.Tables;
using ShadowSql.Expressions.VisitSource;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using System;
using System.Linq.Expressions;

namespace ShadowSql.Expressions;

/// <summary>
/// Delete扩展方法
/// </summary>
public static partial class ShadowSqlServices
{
    #region TableDelete
    /// <summary>
    /// 删除
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="table">表</param>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public static TableDelete ToDelete<TEntity>(this ITable table, Expression<Func<TEntity, bool>> query)
        => new(table, TableVisitor.Where(table, new AndLogic(), query).Logic);
    /// <summary>
    /// 删除
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TParameter"></typeparam>
    /// <param name="table">表</param>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public static TableDelete ToDelete<TEntity, TParameter>(this ITable table, Expression<Func<TEntity, TParameter, bool>> query)
        => new(table, TableVisitor.Where(table, new AndLogic(), query).Logic);
    /// <summary>
    /// 删除
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public static TableDelete ToDelete<TEntity>(this TableSqlQuery<TEntity> query)
        => new(query.Source, query._filter);
    /// <summary>
    /// 删除
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public static TableDelete ToDelete<TEntity>(this TableQuery<TEntity> query)
        => new(query.Source, query._filter);
    #endregion
    #region AliasTableDelete
    /// <summary>
    /// 删除
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="table">表</param>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public static AliasTableDelete ToDelete<TEntity>(this IAliasTable table, Expression<Func<TEntity, bool>> query)
        => new(table, TableVisitor.Where(table, new AndLogic(), query).Logic);
    /// <summary>
    /// 删除
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TParameter"></typeparam>
    /// <param name="table">表</param>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public static AliasTableDelete ToDelete<TEntity, TParameter>(this IAliasTable table, Expression<Func<TEntity, TParameter, bool>> query)
        => new(table, TableVisitor.Where(table, new AndLogic(), query).Logic);
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public static AliasTableDelete ToDelete<TEntity>(this AliasTableSqlQuery<TEntity> query)
        => new(query.Source, query._filter);
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public static AliasTableDelete ToDelete<TEntity>(this AliasTableQuery<TEntity> query)
        => new(query.Source, query._filter);
    #endregion
}

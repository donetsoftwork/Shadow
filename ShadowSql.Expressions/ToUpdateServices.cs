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
    /// <param name="table"></param>
    /// <param name="where"></param>
    /// <returns></returns>
    public static TableUpdate<TEntity> ToUpdate<TEntity>(this ITable table, ISqlLogic where)
        => new(table, where);
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="table"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static TableUpdate<TEntity> ToUpdate<TEntity>(this ITable table, Expression<Func<TEntity, bool>> query)
        => new(table, TableVisitor.Where(table, new AndLogic(), query).Logic);
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="tableQuery"></param>
    /// <returns></returns>
    public static TableUpdate<TEntity> ToUpdate<TEntity>(this TableSqlQuery<TEntity> tableQuery)
        => new(tableQuery.Source, tableQuery._filter);
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="tableQuery"></param>
    /// <returns></returns>
    public static TableUpdate<TEntity> ToUpdate<TEntity>(this TableSqlQuery tableQuery)
        => new((ITable)tableQuery.Source, tableQuery._filter);
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="tableQuery"></param>
    /// <returns></returns>
    public static TableUpdate<TEntity> ToUpdate<TEntity>(this TableQuery<TEntity> tableQuery)
        => new(tableQuery.Source, tableQuery._filter);
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="tableQuery"></param>
    /// <returns></returns>
    public static TableUpdate<TEntity> ToUpdate<TEntity>(this TableQuery tableQuery)
        => new((ITable)tableQuery.Source, tableQuery._filter);
    #endregion   
}

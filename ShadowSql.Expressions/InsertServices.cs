using ShadowSql.Expressions.Insert;
using ShadowSql.Expressions.Visit;
using ShadowSql.Identifiers;
using System;
using System.Linq.Expressions;

namespace ShadowSql.Expressions;

/// <summary>
/// 构造插入扩展方法
/// </summary>
public static partial class ShadowSqlServices
{    
    #region ToInsert
    /// <summary>
    /// 插入
    /// </summary>
    /// <param name="table"></param>
    /// <returns></returns>
    public static SingleInsert<TEntity> ToInsert<TEntity>(this ITable table)
        => new(table);
    /// <summary>
    /// 插入
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="table"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public static SingleInsert<TEntity> ToInsert<TEntity>(this ITable table, Expression<Func<TEntity>> select)
        => new(table, SingleInsertVisitor.Insert(table, select).Items);
    /// <summary>
    /// 插入
    /// </summary>
    /// <typeparam name="TParameter"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="table"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public static SingleInsert<TEntity> ToInsert<TParameter, TEntity>(this ITable table, Expression<Func<TParameter, TEntity>> select)
        => new(table, SingleInsertVisitor.Insert(table, select).Items);
    #endregion
}

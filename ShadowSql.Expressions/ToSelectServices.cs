using ShadowSql.Expressions.AliasTables;
using ShadowSql.Expressions.Cursors;
using ShadowSql.Expressions.CursorSelect;
using ShadowSql.Expressions.GroupBy;
using ShadowSql.Expressions.Select;
using ShadowSql.Expressions.Tables;
using ShadowSql.Expressions.VisitSource;
using ShadowSql.Identifiers;
using ShadowSql.Join;
using ShadowSql.Logics;
using ShadowSql.Tables;
using System;
using System.Linq.Expressions;

namespace ShadowSql.Expressions;

/// <summary>
/// 构造筛选列扩展方法
/// </summary>
public static partial class ShadowSqlServices
{
    #region TableSelect
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="table"></param>
    /// <returns></returns>
    public static TableSelect<TEntity> ToSelect<TEntity>(this ITable table)
        => new(table);
    /// <summary>
    /// 表过滤筛选列
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="table"></param>
    /// <param name="filter"></param>
    /// <returns></returns>
    public static TableSelect<TEntity> ToSelect<TEntity>(this ITable table, ISqlLogic filter)
        => new(table, filter);
    /// <summary>
    /// 表过滤筛选列
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="table"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static TableSelect<TEntity> ToSelect<TEntity>(this ITable table, Expression<Func<TEntity, bool>> query)
        => new(table, TableVisitor.Where(table, new AndLogic(), query).Logic);
    /// <summary>
    /// 表过滤筛选列
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TParameter"></typeparam>
    /// <param name="table"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static TableSelect<TEntity> ToSelect<TEntity, TParameter>(this ITable table, Expression<Func<TEntity, TParameter, bool>> query)
        => new(table, TableVisitor.Where(table, new AndLogic(), query).Logic);
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="query"></param>
    /// <returns></returns>
    public static TableSelect<TEntity> ToSelect<TEntity>(this TableQuery<TEntity> query)
        => new(query);
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public static TableSelect<TEntity> ToSelect<TEntity>(this TableQuery query)
        => new(query, (ITable)query.Source);
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="query"></param>
    /// <returns></returns>
    public static TableSelect<TEntity> ToSelect<TEntity>(this TableSqlQuery<TEntity> query)
        => new(query);
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public static TableSelect<TEntity> ToSelect<TEntity>(this TableSqlQuery query)
        => new(query, (ITable)query.Source);
    #endregion
    #region TableCursorSelect
    /// <summary>
    /// 表范围筛选列
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="cursor"></param>
    /// <returns></returns>
    public static TableCursorSelect<TEntity> ToSelect<TEntity>(this TableCursor<TEntity> cursor)
        => new(cursor);
    #endregion
    #region AliasTableSelect
    /// <summary>
    /// 别名表筛选列
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="table"></param>
    /// <returns></returns>
    public static TableSelect<TEntity> ToSelect<TEntity>(this IAliasTable table)
        => new(table);
    /// <summary>
    /// 别名表过滤筛选列
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="table"></param>
    /// <param name="filter"></param>
    /// <returns></returns>
    public static TableSelect<TEntity> ToSelect<TEntity>(this IAliasTable table, ISqlLogic filter)
        => new(table, filter);
    /// <summary>
    /// 别名表筛选列
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="query"></param>
    /// <returns></returns>
    public static TableSelect<TEntity> ToSelect<TEntity>(this AliasTableSqlQuery<TEntity> query)
        => new(query);
    #endregion
    #region MultiTableSelect
    /// <summary>
    /// 多(联)表筛选列
    /// </summary>
    /// <param name="table"></param>
    /// <returns></returns>
    public static MultiTableSelect ToSelect(this IMultiView table)
        => new(table);
    /// <summary>
    /// 多(联)表筛选列
    /// </summary>
    /// <param name="table"></param>
    /// <returns></returns>
    public static MultiTableSelect ToSelect(this IJoinOn table)
        => new(table.Root);
    #endregion
    #region MultiTableCursorSelect
    /// <summary>
    /// 多(联)表筛选列
    /// </summary>
    /// <param name="cursor"></param>
    /// <returns></returns>
    public static MultiTableCursorSelect ToSelect(this MultiTableCursor cursor)
        => new(cursor);
    #endregion
    #region GroupByTableSelect
    /// <summary>
    /// GroupBy后再筛选列
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static GroupByTableSelect<TKey, TEntity> ToSelect<TKey, TEntity>(this GroupByTableSqlQuery<TKey, TEntity> source)
        => new(source);
    /// <summary>
    /// GroupBy后再筛选列
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static GroupByTableSelect<TKey, TEntity> ToSelect<TKey, TEntity>(this GroupByTableQuery<TKey, TEntity> source)
        => new(source);
    #endregion
    #region GroupByTableCursorSelect
    /// <summary>
    /// GroupBy后再范围(分页)及列筛选
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="cursor"></param>
    /// <returns></returns>
    public static GroupByTableCursorSelect<TKey, TEntity> ToSelect<TKey, TEntity>(this GroupByTableCursor<TKey, TEntity> cursor)
        => new(cursor);
    #endregion
    #region GroupByMultiSelect
    /// <summary>
    /// GroupBy后再筛选列
    /// </summary>
    /// <param name="groupBy"></param>
    /// <returns></returns>
    public static GroupByMultiSelect<TKey> ToSelect<TKey>(this GroupByMultiSqlQuery<TKey> groupBy)
        => new(groupBy);
    /// <summary>
    /// GroupBy后再筛选列
    /// </summary>
    /// <param name="groupBy"></param>
    /// <returns></returns>
    public static GroupByMultiSelect<TKey> ToSelect<TKey>(this GroupByMultiQuery<TKey> groupBy)
        => new(groupBy);
    #endregion
    #region GroupByMultiCursorSelect
    /// <summary>
    /// GroupBy后再范围(分页)及列筛选
    /// </summary>
    /// <param name="cursor"></param>
    /// <returns></returns>
    public static GroupByMultiCursorSelect<TKey> ToSelect<TKey>(this GroupByMultiCursor<TKey> cursor)
        => new(cursor);
    #endregion
}

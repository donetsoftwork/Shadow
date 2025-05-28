using ShadowSql.Expressions.AliasTables;
using ShadowSql.Expressions.Cursors;
using ShadowSql.Expressions.GroupBy;
using ShadowSql.Expressions.Tables;
using ShadowSql.Identifiers;
using ShadowSql.Join;
using ShadowSql.Logics;
using ShadowSql.Tables;

namespace ShadowSql.Expressions;

/// <summary>
/// 构造范围筛选扩展方法
/// </summary>
public static partial class ShadowSqlServices
{
    #region TableCursor
    /// <summary>
    /// 表范围筛选
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="source"></param>
    /// <param name="where"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static TableCursor<TEntity> ToCursor<TEntity>(this ITable source, ISqlLogic where, int limit = 0, int offset = 0)
        => new(source, where, limit, offset);
    /// <summary>
    /// 表范围筛选
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="source"></param>
    /// <param name="where"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static TableCursor<TEntity> Take<TEntity>(this ITable source, ISqlLogic where, int limit, int offset = 0)
        => new(source, where, limit, offset);
    /// <summary>
    /// 表范围筛选
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="source"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static TableCursor<TEntity> ToCursor<TEntity>(this ITable source, int limit = 0, int offset = 0)
        => new(source, EmptyLogic.Instance, limit, offset);
    /// <summary>
    /// 表范围筛选
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="source"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static TableCursor<TEntity> Take<TEntity>(this ITable source, int limit, int offset = 0)
        => new(source, EmptyLogic.Instance, limit, offset);
    /// <summary>
    /// 表查询范围筛选
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="query"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static TableCursor<TEntity> ToCursor<TEntity>(this TableQuery<TEntity> query, int limit = 0, int offset = 0)
        => new(query.Source, query._filter, limit, offset);
    /// <summary>
    /// 表查询范围筛选
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="query"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static TableCursor<TEntity> Take<TEntity>(this TableQuery<TEntity> query, int limit, int offset = 0)
        => new(query.Source, query._filter, limit, offset);
    /// <summary>
    /// 表查询范围筛选
    /// </summary>
    /// <param name="query"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static TableCursor<ITable> ToCursor(this TableQuery query, int limit = 0, int offset = 0)
        => new((ITable)query.Source, query._filter, limit, offset);
    /// <summary>
    /// 表查询范围筛选
    /// </summary>
    /// <param name="query"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static TableCursor<ITable> Take(this TableQuery query, int limit, int offset = 0)
        => new((ITable)query.Source, query._filter, limit, offset);
    /// <summary>
    /// 表查询范围筛选
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="query"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static TableCursor<TEntity> ToCursor<TEntity>(this TableSqlQuery<TEntity> query, int limit = 0, int offset = 0)
        => new(query.Source, query._filter, limit, offset);
    /// <summary>
    /// 表查询范围筛选
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="query"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static TableCursor<TEntity> Take<TEntity>(this TableSqlQuery<TEntity> query, int limit, int offset = 0)
        => new(query.Source, query._filter, limit, offset);
    /// <summary>
    /// 表查询范围筛选
    /// </summary>
    /// <param name="query"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static TableCursor<ITable> ToCursor(this TableSqlQuery query, int limit = 0, int offset = 0)
        => new((ITable)query.Source, query._filter, limit, offset);
    /// <summary>
    /// 表查询范围筛选
    /// </summary>
    /// <param name="query"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static TableCursor<ITable> Take(this TableSqlQuery query, int limit, int offset = 0)
        => new((ITable)query.Source, query._filter, limit, offset);
    #endregion
    #region AliasTable
    /// <summary>
    /// 别名表范围筛选
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="source"></param>
    /// <param name="where"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static TableCursor<TEntity> ToCursor<TEntity>(this IAliasTable source, ISqlLogic where, int limit = 0, int offset = 0)
        => new(source, where, limit, offset);
    /// <summary>
    /// 别名表范围筛选
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="source"></param>
    /// <param name="where"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static TableCursor<TEntity> Take<TEntity>(this IAliasTable source, ISqlLogic where, int limit, int offset = 0)
        => new(source, where, limit, offset);
    /// <summary>
    /// 别名表范围筛选
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="source"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static TableCursor<TEntity> ToCursor<TEntity>(this IAliasTable source, int limit = 0, int offset = 0)
        => new(source, EmptyLogic.Instance, limit, offset);
    /// <summary>
    /// 别名表范围筛选
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="source"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static TableCursor<TEntity> Take<TEntity>(this IAliasTable source, int limit, int offset = 0)
        => new(source, EmptyLogic.Instance, limit, offset);
    /// <summary>
    /// 别名表查询范围筛选
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="query"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static TableCursor<TEntity> ToCursor<TEntity>(this AliasTableQuery<TEntity> query, int limit = 0, int offset = 0)
        => new(query.Source, query._filter, limit, offset);
    /// <summary>
    /// 别名表查询范围筛选
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="query"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static TableCursor<TEntity> Take<TEntity>(this AliasTableQuery<TEntity> query, int limit, int offset = 0)
        => new(query.Source, query._filter, limit, offset);
    /// <summary>
    /// 别名表查询范围筛选
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="query"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static TableCursor<TEntity> ToCursor<TEntity>(this AliasTableSqlQuery<TEntity> query, int limit = 0, int offset = 0)
        => new(query.Source, query._filter, limit, offset);
    /// <summary>
    /// 别名表查询范围筛选
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="query"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static TableCursor<TEntity> Take<TEntity>(this AliasTableSqlQuery<TEntity> query, int limit, int offset = 0)
        => new(query.Source, query._filter, limit, offset);
    #endregion
    #region MultiTableCursor
    /// <summary>
    /// 多联表范围筛选
    /// </summary>
    /// <param name="query"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static MultiTableCursor ToCursor(this MultiTableBase query, int limit = 0, int offset = 0)
        => new(query, limit, offset);
    /// <summary>
    /// 多联表范围筛选
    /// </summary>
    /// <param name="query"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static MultiTableCursor Take(this MultiTableBase query, int limit, int offset = 0)
        => new(query, limit, offset);
    #endregion
    #region GroupByTableCursor
    /// <summary>
    /// 表分组后范围筛选
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="groupBy"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static GroupByTableCursor<TKey, TEntity> ToCursor<TKey, TEntity>(this GroupByTableSqlQuery<TKey, TEntity> groupBy, int limit = 0, int offset = 0)
        => new(groupBy, limit, offset);
    /// <summary>
    /// 表分组后范围筛选
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="groupBy"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static GroupByTableCursor<TKey, TEntity> Take<TKey, TEntity>(this GroupByTableSqlQuery<TKey, TEntity> groupBy, int limit, int offset = 0)
        => new(groupBy, limit, offset);
    /// <summary>
    /// 表分组后范围筛选
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="groupBy"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static GroupByTableCursor<TKey, TEntity> ToCursor<TKey, TEntity>(this GroupByTableQuery<TKey, TEntity> groupBy, int limit = 0, int offset = 0)
        => new(groupBy, limit, offset);
    /// <summary>
    /// 表分组后范围筛选
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="groupBy"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static GroupByTableCursor<TKey, TEntity> Take<TKey, TEntity>(this GroupByTableQuery<TKey, TEntity> groupBy, int limit, int offset = 0)
        => new(groupBy, limit, offset);
    #endregion    
    #region GroupByMultiCursor
    /// <summary>
    /// 多(联)表分组后范围筛选
    /// </summary>
    /// <param name="groupBy"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static GroupByMultiCursor<Key> ToCursor<Key>(this GroupByMultiQuery<Key> groupBy, int limit = 0, int offset = 0)
        => new(groupBy, limit, offset);
    /// <summary>
    /// 多(联)表分组后范围筛选
    /// </summary>
    /// <param name="groupBy"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static GroupByMultiCursor<Key> Take<Key>(this GroupByMultiQuery<Key> groupBy, int limit, int offset = 0)
        => new(groupBy, limit, offset);
    /// <summary>
    /// 多(联)表分组后范围筛选
    /// </summary>
    /// <param name="groupBy"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static GroupByMultiCursor<Key> ToCursor<Key>(this GroupByMultiSqlQuery<Key> groupBy, int limit = 0, int offset = 0)
        => new(groupBy, limit, offset);
    /// <summary>
    /// 多(联)表分组后范围筛选
    /// </summary>
    /// <param name="groupBy"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static GroupByMultiCursor<Key> Take<Key>(this GroupByMultiSqlQuery<Key> groupBy, int limit, int offset = 0)
        => new(groupBy, limit, offset);
    #endregion
}

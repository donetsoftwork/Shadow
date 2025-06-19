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
    /// <param name="table">表</param>
    /// <param name="where">查询条件</param>
    /// <param name="limit">筛选数量</param>
    /// <param name="offset">跳过数量</param>
    /// <returns></returns>
    public static TableCursor<TEntity> ToCursor<TEntity>(this ITable table, ISqlLogic where, int limit = 0, int offset = 0)
        => new(table, where, limit, offset);
    /// <summary>
    /// 表范围筛选
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="table">表</param>
    /// <param name="where">查询条件</param>
    /// <param name="limit">筛选数量</param>
    /// <param name="offset">跳过数量</param>
    /// <returns></returns>
    public static TableCursor<TEntity> Take<TEntity>(this ITable table, ISqlLogic where, int limit, int offset = 0)
        => new(table, where, limit, offset);
    /// <summary>
    /// 表范围筛选
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="table">表</param>
    /// <param name="limit">筛选数量</param>
    /// <param name="offset">跳过数量</param>
    /// <returns></returns>
    public static TableCursor<TEntity> ToCursor<TEntity>(this ITable table, int limit = 0, int offset = 0)
        => new(table, EmptyLogic.Instance, limit, offset);
    /// <summary>
    /// 表范围筛选
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="table">表</param>
    /// <param name="limit">筛选数量</param>
    /// <param name="offset">跳过数量</param>
    /// <returns></returns>
    public static TableCursor<TEntity> Take<TEntity>(this ITable table, int limit, int offset = 0)
        => new(table, EmptyLogic.Instance, limit, offset);
    /// <summary>
    /// 表查询范围筛选
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="query">查询</param>
    /// <param name="limit">筛选数量</param>
    /// <param name="offset">跳过数量</param>
    /// <returns></returns>
    public static TableCursor<TEntity> ToCursor<TEntity>(this TableQuery<TEntity> query, int limit = 0, int offset = 0)
        => new(query.Source, query._filter, limit, offset);
    /// <summary>
    /// 表查询范围筛选
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="query">查询</param>
    /// <param name="limit">筛选数量</param>
    /// <param name="offset">跳过数量</param>
    /// <returns></returns>
    public static TableCursor<TEntity> Take<TEntity>(this TableQuery<TEntity> query, int limit, int offset = 0)
        => new(query.Source, query._filter, limit, offset);
    /// <summary>
    /// 表查询范围筛选
    /// </summary>
    /// <param name="query">查询</param>
    /// <param name="limit">筛选数量</param>
    /// <param name="offset">跳过数量</param>
    /// <returns></returns>
    public static TableCursor<ITable> ToCursor(this TableQuery query, int limit = 0, int offset = 0)
        => new((ITable)query.Source, query._filter, limit, offset);
    /// <summary>
    /// 表查询范围筛选
    /// </summary>
    /// <param name="query">查询</param>
    /// <param name="limit">筛选数量</param>
    /// <param name="offset">跳过数量</param>
    /// <returns></returns>
    public static TableCursor<ITable> Take(this TableQuery query, int limit, int offset = 0)
        => new((ITable)query.Source, query._filter, limit, offset);
    /// <summary>
    /// 表查询范围筛选
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="query">查询</param>
    /// <param name="limit">筛选数量</param>
    /// <param name="offset">跳过数量</param>
    /// <returns></returns>
    public static TableCursor<TEntity> ToCursor<TEntity>(this TableSqlQuery<TEntity> query, int limit = 0, int offset = 0)
        => new(query.Source, query._filter, limit, offset);
    /// <summary>
    /// 表查询范围筛选
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="query">查询</param>
    /// <param name="limit">筛选数量</param>
    /// <param name="offset">跳过数量</param>
    /// <returns></returns>
    public static TableCursor<TEntity> Take<TEntity>(this TableSqlQuery<TEntity> query, int limit, int offset = 0)
        => new(query.Source, query._filter, limit, offset);
    /// <summary>
    /// 表查询范围筛选
    /// </summary>
    /// <param name="query">查询</param>
    /// <param name="limit">筛选数量</param>
    /// <param name="offset">跳过数量</param>
    /// <returns></returns>
    public static TableCursor<ITable> ToCursor(this TableSqlQuery query, int limit = 0, int offset = 0)
        => new((ITable)query.Source, query._filter, limit, offset);
    /// <summary>
    /// 表查询范围筛选
    /// </summary>
    /// <param name="query">查询</param>
    /// <param name="limit">筛选数量</param>
    /// <param name="offset">跳过数量</param>
    /// <returns></returns>
    public static TableCursor<ITable> Take(this TableSqlQuery query, int limit, int offset = 0)
        => new((ITable)query.Source, query._filter, limit, offset);
    #endregion
    #region AliasTable
    /// <summary>
    /// 别名表范围筛选
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="aliasTable">别名表</param>
    /// <param name="where">查询条件</param>
    /// <param name="limit">筛选数量</param>
    /// <param name="offset">跳过数量</param>
    /// <returns></returns>
    public static TableCursor<TEntity> ToCursor<TEntity>(this IAliasTable aliasTable, ISqlLogic where, int limit = 0, int offset = 0)
        => new(aliasTable, where, limit, offset);
    /// <summary>
    /// 别名表范围筛选
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="aliasTable">别名表</param>
    /// <param name="where">查询条件</param>
    /// <param name="limit">筛选数量</param>
    /// <param name="offset">跳过数量</param>
    /// <returns></returns>
    public static TableCursor<TEntity> Take<TEntity>(this IAliasTable aliasTable, ISqlLogic where, int limit, int offset = 0)
        => new(aliasTable, where, limit, offset);
    /// <summary>
    /// 别名表范围筛选
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="aliasTable">别名表</param>
    /// <param name="limit">筛选数量</param>
    /// <param name="offset">跳过数量</param>
    /// <returns></returns>
    public static TableCursor<TEntity> ToCursor<TEntity>(this IAliasTable aliasTable, int limit = 0, int offset = 0)
        => new(aliasTable, EmptyLogic.Instance, limit, offset);
    /// <summary>
    /// 别名表范围筛选
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="aliasTable">别名表</param>
    /// <param name="limit">筛选数量</param>
    /// <param name="offset">跳过数量</param>
    /// <returns></returns>
    public static TableCursor<TEntity> Take<TEntity>(this IAliasTable aliasTable, int limit, int offset = 0)
        => new(aliasTable, EmptyLogic.Instance, limit, offset);
    /// <summary>
    /// 别名表查询范围筛选
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="query">查询</param>
    /// <param name="limit">筛选数量</param>
    /// <param name="offset">跳过数量</param>
    /// <returns></returns>
    public static TableCursor<TEntity> ToCursor<TEntity>(this AliasTableQuery<TEntity> query, int limit = 0, int offset = 0)
        => new(query.Source, query._filter, limit, offset);
    /// <summary>
    /// 别名表查询范围筛选
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="query">查询</param>
    /// <param name="limit">筛选数量</param>
    /// <param name="offset">跳过数量</param>
    /// <returns></returns>
    public static TableCursor<TEntity> Take<TEntity>(this AliasTableQuery<TEntity> query, int limit, int offset = 0)
        => new(query.Source, query._filter, limit, offset);
    /// <summary>
    /// 别名表查询范围筛选
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="query">查询</param>
    /// <param name="limit">筛选数量</param>
    /// <param name="offset">跳过数量</param>
    /// <returns></returns>
    public static TableCursor<TEntity> ToCursor<TEntity>(this AliasTableSqlQuery<TEntity> query, int limit = 0, int offset = 0)
        => new(query.Source, query._filter, limit, offset);
    /// <summary>
    /// 别名表查询范围筛选
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="query">查询</param>
    /// <param name="limit">筛选数量</param>
    /// <param name="offset">跳过数量</param>
    /// <returns></returns>
    public static TableCursor<TEntity> Take<TEntity>(this AliasTableSqlQuery<TEntity> query, int limit, int offset = 0)
        => new(query.Source, query._filter, limit, offset);
    #endregion
    #region MultiTableCursor
    /// <summary>
    /// 多联表范围筛选
    /// </summary>
    /// <param name="query">查询</param>
    /// <param name="limit">筛选数量</param>
    /// <param name="offset">跳过数量</param>
    /// <returns></returns>
    public static MultiTableCursor ToCursor(this MultiTableBase query, int limit = 0, int offset = 0)
        => new(query, limit, offset);
    /// <summary>
    /// 多联表范围筛选
    /// </summary>
    /// <param name="query">查询</param>
    /// <param name="limit">筛选数量</param>
    /// <param name="offset">跳过数量</param>
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
    /// <param name="groupBy">分组查询</param>
    /// <param name="limit">筛选数量</param>
    /// <param name="offset">跳过数量</param>
    /// <returns></returns>
    public static GroupByTableCursor<TKey, TEntity> ToCursor<TKey, TEntity>(this GroupByTableSqlQuery<TKey, TEntity> groupBy, int limit = 0, int offset = 0)
        => new(groupBy, limit, offset);
    /// <summary>
    /// 表分组后范围筛选
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="groupBy">分组查询</param>
    /// <param name="limit">筛选数量</param>
    /// <param name="offset">跳过数量</param>
    /// <returns></returns>
    public static GroupByTableCursor<TKey, TEntity> Take<TKey, TEntity>(this GroupByTableSqlQuery<TKey, TEntity> groupBy, int limit, int offset = 0)
        => new(groupBy, limit, offset);
    /// <summary>
    /// 表分组后范围筛选
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="groupBy">分组查询</param>
    /// <param name="limit">筛选数量</param>
    /// <param name="offset">跳过数量</param>
    /// <returns></returns>
    public static GroupByTableCursor<TKey, TEntity> ToCursor<TKey, TEntity>(this GroupByTableQuery<TKey, TEntity> groupBy, int limit = 0, int offset = 0)
        => new(groupBy, limit, offset);
    /// <summary>
    /// 表分组后范围筛选
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="groupBy">分组查询</param>
    /// <param name="limit">筛选数量</param>
    /// <param name="offset">跳过数量</param>
    /// <returns></returns>
    public static GroupByTableCursor<TKey, TEntity> Take<TKey, TEntity>(this GroupByTableQuery<TKey, TEntity> groupBy, int limit, int offset = 0)
        => new(groupBy, limit, offset);
    #endregion    
    #region GroupByMultiCursor
    /// <summary>
    /// 多(联)表分组后范围筛选
    /// </summary>
    /// <param name="groupBy">分组查询</param>
    /// <param name="limit">筛选数量</param>
    /// <param name="offset">跳过数量</param>
    /// <returns></returns>
    public static GroupByMultiCursor<Key> ToCursor<Key>(this GroupByMultiQuery<Key> groupBy, int limit = 0, int offset = 0)
        => new(groupBy, limit, offset);
    /// <summary>
    /// 多(联)表分组后范围筛选
    /// </summary>
    /// <param name="groupBy">分组查询</param>
    /// <param name="limit">筛选数量</param>
    /// <param name="offset">跳过数量</param>
    /// <returns></returns>
    public static GroupByMultiCursor<Key> Take<Key>(this GroupByMultiQuery<Key> groupBy, int limit, int offset = 0)
        => new(groupBy, limit, offset);
    /// <summary>
    /// 多(联)表分组后范围筛选
    /// </summary>
    /// <param name="groupBy">分组查询</param>
    /// <param name="limit">筛选数量</param>
    /// <param name="offset">跳过数量</param>
    /// <returns></returns>
    public static GroupByMultiCursor<Key> ToCursor<Key>(this GroupByMultiSqlQuery<Key> groupBy, int limit = 0, int offset = 0)
        => new(groupBy, limit, offset);
    /// <summary>
    /// 多(联)表分组后范围筛选
    /// </summary>
    /// <param name="groupBy">分组查询</param>
    /// <param name="limit">筛选数量</param>
    /// <param name="offset">跳过数量</param>
    /// <returns></returns>
    public static GroupByMultiCursor<Key> Take<Key>(this GroupByMultiSqlQuery<Key> groupBy, int limit, int offset = 0)
        => new(groupBy, limit, offset);
    #endregion
}

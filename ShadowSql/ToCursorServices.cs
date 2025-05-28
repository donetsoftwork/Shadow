using ShadowSql.AliasTables;
using ShadowSql.Cursors;
using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using ShadowSql.Join;
using ShadowSql.Logics;
using ShadowSql.Tables;

namespace ShadowSql;

/// <summary>
/// 构造范围筛选扩展方法
/// </summary>
public static partial class ShadowSqlServices
{
    #region TableCursor
    /// <summary>
    /// 表范围筛选
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="source"></param>
    /// <param name="where"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static TableCursor<TTable> ToCursor<TTable>(this TTable source, ISqlLogic where, int limit = 0, int offset = 0)
        where TTable : ITable
        => new(source, where, limit, offset);
    /// <summary>
    /// 表范围筛选
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="source"></param>
    /// <param name="where"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static TableCursor<TTable> Take<TTable>(this TTable source, ISqlLogic where, int limit, int offset = 0)
        where TTable : ITable
        => new(source, where, limit, offset);
    /// <summary>
    /// 表范围筛选
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="source"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static TableCursor<TTable> ToCursor<TTable>(this TTable source, int limit = 0, int offset = 0)
        where TTable : ITable
        => new(source, EmptyLogic.Instance, limit, offset);
    /// <summary>
    /// 表范围筛选
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="source"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static TableCursor<TTable> Take<TTable>(this TTable source, int limit, int offset = 0)
        where TTable : ITable
        => new(source, EmptyLogic.Instance, limit, offset);
    /// <summary>
    /// 表查询范围筛选
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static TableCursor<TTable> ToCursor<TTable>(this TableQuery<TTable> query, int limit = 0, int offset = 0)
        where TTable : ITable
        => new(query.Source, query._filter, limit, offset);
    /// <summary>
    /// 表查询范围筛选
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static TableCursor<TTable> Take<TTable>(this TableQuery<TTable> query, int limit, int offset = 0)
        where TTable : ITable
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
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static TableCursor<TTable> ToCursor<TTable>(this TableSqlQuery<TTable> query, int limit = 0, int offset = 0)
        where TTable : ITable
        => new(query.Source, query._filter, limit, offset);
    /// <summary>
    /// 表查询范围筛选
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static TableCursor<TTable> Take<TTable>(this TableSqlQuery<TTable> query, int limit, int offset = 0)
        where TTable : ITable
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
    #region AliasTableCursor
    /// <summary>
    /// 别名表范围筛选
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="source"></param>
    /// <param name="where"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static AliasTableCursor<TTable> ToCursor<TTable>(this IAliasTable<TTable> source, ISqlLogic where, int limit = 0, int offset = 0)
        where TTable : ITable
        => new(source, where, limit, offset);
    /// <summary>
    /// 别名表范围筛选
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="source"></param>
    /// <param name="where"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static AliasTableCursor<TTable> Take<TTable>(this IAliasTable<TTable> source, ISqlLogic where, int limit, int offset = 0)
        where TTable : ITable
        => new(source, where, limit, offset);
    /// <summary>
    /// 别名表范围筛选
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="source"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static AliasTableCursor<TTable> ToCursor<TTable>(this IAliasTable<TTable> source, int limit = 0, int offset = 0)
        where TTable : ITable
        => new(source, EmptyLogic.Instance, limit, offset);
    /// <summary>
    /// 别名表范围筛选
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="source"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static AliasTableCursor<TTable> Take<TTable>(this IAliasTable<TTable> source, int limit, int offset = 0)
        where TTable : ITable
        => new(source, EmptyLogic.Instance, limit, offset);
    /// <summary>
    /// 别名表查询范围筛选
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static AliasTableCursor<TTable> ToCursor<TTable>(this AliasTableQuery<TTable> query, int limit = 0, int offset = 0)
        where TTable : ITable
        => new(query.Source, query._filter, limit, offset);
    /// <summary>
    /// 别名表查询范围筛选
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static AliasTableCursor<TTable> Take<TTable>(this AliasTableQuery<TTable> query, int limit, int offset = 0)
        where TTable : ITable
        => new(query.Source, query._filter, limit, offset);
    /// <summary>
    /// 别名表查询范围筛选
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static AliasTableCursor<TTable> ToCursor<TTable>(this AliasTableSqlQuery<TTable> query, int limit = 0, int offset = 0)
        where TTable : ITable
        => new(query.Source, query._filter, limit, offset);
    /// <summary>
    /// 别名表查询范围筛选
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static AliasTableCursor<TTable> Take<TTable>(this AliasTableSqlQuery<TTable> query, int limit, int offset = 0)
        where TTable : ITable
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
    /// <typeparam name="TTable"></typeparam>
    /// <param name="groupBy"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static GroupByTableCursor<TTable> ToCursor<TTable>(this GroupByTableSqlQuery<TTable> groupBy, int limit = 0, int offset = 0)
        where TTable : ITable
        => new(groupBy, limit, offset);
    /// <summary>
    /// 表分组后范围筛选
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="groupBy"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static GroupByTableCursor<TTable> Take<TTable>(this GroupByTableSqlQuery<TTable> groupBy, int limit, int offset = 0)
        where TTable : ITable
        => new(groupBy, limit, offset);
    /// <summary>
    /// 表分组后范围筛选
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="groupBy"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static GroupByTableCursor<TTable> ToCursor<TTable>(this GroupByTableQuery<TTable> groupBy, int limit = 0, int offset = 0)
        where TTable : ITable
        => new(groupBy, limit, offset);
    /// <summary>
    /// 表分组后范围筛选
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="groupBy"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static GroupByTableCursor<TTable> Take<TTable>(this GroupByTableQuery<TTable> groupBy, int limit, int offset = 0)
        where TTable : ITable
        => new(groupBy, limit, offset);
    #endregion
    #region GroupByAliasTableCursor
    /// <summary>
    /// 别名表分组后范围筛选
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="groupBy"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static GroupByAliasTableCursor<TTable> ToCursor<TTable>(this GroupByAliasTableQuery<TTable> groupBy, int limit = 0, int offset = 0)
        where TTable : ITable
        => new(groupBy, limit, offset);
    /// <summary>
    /// 别名表分组后范围筛选
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="groupBy"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static GroupByAliasTableCursor<TTable> Take<TTable>(this GroupByAliasTableQuery<TTable> groupBy, int limit, int offset = 0)
        where TTable : ITable
        => new(groupBy, limit, offset);
    /// <summary>
    /// 别名表分组后范围筛选
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="groupBy"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static GroupByAliasTableCursor<TTable> ToCursor<TTable>(this GroupByAliasTableSqlQuery<TTable> groupBy, int limit = 0, int offset = 0)
        where TTable : ITable
        => new(groupBy, limit, offset);
    /// <summary>
    /// 别名表分组后范围筛选
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="groupBy"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static GroupByAliasTableCursor<TTable> Take<TTable>(this GroupByAliasTableSqlQuery<TTable> groupBy, int limit, int offset = 0)
        where TTable : ITable
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
    public static GroupByMultiCursor ToCursor(this GroupByMultiQuery groupBy, int limit = 0, int offset = 0)
        => new(groupBy, limit, offset);
    /// <summary>
    /// 多(联)表分组后范围筛选
    /// </summary>
    /// <param name="groupBy"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static GroupByMultiCursor Take(this GroupByMultiQuery groupBy, int limit, int offset = 0)
        => new(groupBy, limit, offset);
    /// <summary>
    /// 多(联)表分组后范围筛选
    /// </summary>
    /// <param name="groupBy"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static GroupByMultiCursor ToCursor(this GroupByMultiSqlQuery groupBy, int limit = 0, int offset = 0)
        => new(groupBy, limit, offset);
    /// <summary>
    /// 多(联)表分组后范围筛选
    /// </summary>
    /// <param name="groupBy"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static GroupByMultiCursor Take(this GroupByMultiSqlQuery groupBy, int limit, int offset = 0)
        => new(groupBy, limit, offset);
    #endregion
}

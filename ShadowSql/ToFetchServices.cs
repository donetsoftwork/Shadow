using ShadowSql.Fetches;
using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Queries;
using ShadowSql.Variants;

namespace ShadowSql;

/// <summary>
/// 构造范围筛选扩展方法
/// </summary>
public static partial class ShadowSqlServices
{
    #region TableFetch
    /// <summary>
    /// 表范围筛选
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="source"></param>
    /// <param name="where"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static TableFetch<TTable> ToFetch<TTable>(this TTable source, ISqlLogic where, int limit = 0, int offset = 0)
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
    public static TableFetch<TTable> ToFetch<TTable>(this TTable source, int limit = 0, int offset = 0)
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
    public static TableFetch<TTable> ToFetch<TTable>(this TableQuery<TTable> query, int limit = 0, int offset = 0)
        where TTable : ITable
        => new(query.Source, query.Filter, limit, offset);
    #endregion
    #region AliasTableFetch
    /// <summary>
    /// 别名表范围筛选
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="source"></param>
    /// <param name="where"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static AliasTableFetch<TTable> ToFetch<TTable>(this TableAlias<TTable> source, ISqlLogic where, int limit = 0, int offset = 0)
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
    public static AliasTableFetch<TTable> ToFetch<TTable>(this TableAlias<TTable> source, int limit = 0, int offset = 0)
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
    public static AliasTableFetch<TTable> ToFetch<TTable>(this AliasTableQuery<TTable> query, int limit = 0, int offset = 0)
        where TTable : ITable
        => new(query.Source, query.Filter, limit, offset);
    #endregion
    #region MultiTableFetch
    /// <summary>
    /// 多联表范围筛选
    /// </summary>
    /// <param name="query"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static MultiTableFetch ToFetch(this IMultiTableQuery query, int limit = 0, int offset = 0)
        => new(query, limit, offset);
    #endregion
    #region GroupByTableFetch
    /// <summary>
    /// 表分组后范围筛选
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="groupBy"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static GroupByTableFetch<TTable> ToFetch<TTable>(this GroupByTable<TTable> groupBy, int limit = 0, int offset = 0)
        where TTable : ITable
        => new(groupBy, limit, offset);
    #endregion
    #region GroupByAliasTableFetch
    /// <summary>
    /// 别名表分组后范围筛选
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="groupBy"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static GroupByAliasTableFetch<TTable> ToFetch<TTable>(this GroupByAliasTable<TTable> groupBy, int limit = 0, int offset = 0)
        where TTable : ITable
        => new(groupBy, limit, offset);
    #endregion
    #region GroupByMultiFetch
    /// <summary>
    /// 多(联)表分组后范围筛选
    /// </summary>
    /// <param name="groupBy"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static GroupByMultiFetch ToFetch(this GroupByMultiQuery groupBy, int limit = 0, int offset = 0)
        => new(groupBy, limit, offset);
    #endregion
}

using ShadowSql.Fetches;
using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Queries;
using ShadowSql.Select;
using ShadowSql.Variants;
using System;

namespace ShadowSql;

/// <summary>
/// 构造筛选列扩展方法
/// </summary>
public static partial class ShadowSqlServices
{
    #region TableFields
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <returns></returns>
    public static TableSelect<TTable> ToSelect<TTable>(this TTable table)
        where TTable : ITable
        => new(table);
    /// <summary>
    /// 表过滤筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <param name="filter"></param>
    /// <returns></returns>
    public static TableSelect<TTable> ToSelect<TTable>(this TTable table, ISqlLogic filter)
        where TTable : ITable
        => new(table, filter);
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <returns></returns>
    public static TableSelect<TTable> ToSelect<TTable>(this TableQuery<TTable> query)
        where TTable : ITable
        => new(query);
    /// <summary>
    /// 表范围筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="fetch"></param>
    /// <returns></returns>
    public static TableFetchSelect<TTable> ToSelect<TTable>(this TableFetch<TTable> fetch)
        where TTable : ITable
        => new(fetch);
    #endregion
    #region AliasTableFields
    /// <summary>
    /// 别名表筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <returns></returns>
    public static AliasTableSelect<TTable> ToSelect<TTable>(this TableAlias<TTable> table)
        where TTable : ITable
        => new(table);
    /// <summary>
    /// 别名表过滤筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <param name="filter"></param>
    /// <returns></returns>
    public static AliasTableSelect<TTable> ToSelect<TTable>(this TableAlias<TTable> table, ISqlLogic filter)
        where TTable : ITable
        => new(table, filter);
    /// <summary>
    /// 别名表筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <returns></returns>
    public static AliasTableSelect<TTable> ToSelect<TTable>(this AliasTableQuery<TTable> query)
        where TTable : ITable
        => new(query);
    /// <summary>
    /// 别名表范围筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="fetch"></param>
    /// <returns></returns>
    public static AliasTableFetchSelect<TTable> ToSelect<TTable>(this AliasTableFetch<TTable> fetch)
        where TTable : ITable
        => new(fetch);
    #endregion
    #region MultiTableFields
    /// <summary>
    /// 多(联)表筛选列
    /// </summary>
    /// <param name="table"></param>
    /// <returns></returns>
    public static MultiTableSelect ToSelect(this IMultiTableQuery table)
        => new(table);
    /// <summary>
    /// 多(联)表筛选列
    /// </summary>
    /// <param name="fetch"></param>
    /// <returns></returns>
    public static MultiTableFetchSelect ToSelect(this MultiTableFetch fetch)
        => new(fetch);
    #endregion
    #region GroupByTableFields
    /// <summary>
    /// GroupBy后再筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static GroupByTableSelect<TTable> ToSelect<TTable>(this GroupByTable<TTable> source)
        where TTable : ITable
        => new(source);
    /// <summary>
    /// GroupBy后再范围(分页)及列筛选
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="fetch"></param>
    /// <returns></returns>
    public static GroupByTableFetchSelect<TTable> ToSelect<TTable>(this GroupByTableFetch<TTable> fetch)
        where TTable : ITable
        => new(fetch);
    #endregion
    #region GroupByAliasTableFields
    /// <summary>
    /// GroupBy别名表后再筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static GroupByAliasTableSelect<TTable> ToSelect<TTable>(this GroupByAliasTable<TTable> source)
        where TTable : ITable
        => new(source);
    /// <summary>
    /// GroupBy别名表后再范围(分页)及列筛选
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="fetch"></param>
    /// <returns></returns>
    public static GroupByAliasTableFetchSelect<TTable> ToSelect<TTable>(this GroupByAliasTableFetch<TTable> fetch)
        where TTable : ITable
        => new(fetch);
    #endregion
    #region GroupByMultiFields
    /// <summary>
    /// GroupBy后再筛选列
    /// </summary>
    /// <param name="groupBy"></param>
    /// <returns></returns>
    public static GroupByMultiSelect ToSelect(this GroupByMultiQuery groupBy)
        => new(groupBy);
    /// <summary>
    /// GroupBy后再范围(分页)及列筛选
    /// </summary>
    /// <param name="fetch"></param>
    /// <returns></returns>
    public static GroupByMultiFetchSelect ToSelect(this GroupByMultiFetch fetch)
        => new(fetch);
    #endregion
}

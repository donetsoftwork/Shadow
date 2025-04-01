using ShadowSql.AliasTables;
using ShadowSql.Fetches;
using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using ShadowSql.Join;
using ShadowSql.Logics;
using ShadowSql.Select;
using ShadowSql.SingleSelect;
using ShadowSql.Tables;
using ShadowSql.Variants;

namespace ShadowSql;

/// <summary>
/// 构造筛选单列扩展方法
/// </summary>
public static partial class ShadowSqlServices
{
    #region TableFields
    /// <summary>
    /// 表筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <returns></returns>
    public static TableSingleSelect<TTable> ToSingle<TTable>(this TTable table)
        where TTable : ITable
        => new(table);
    /// <summary>
    /// 表过滤筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <param name="filter"></param>
    /// <returns></returns>
    public static TableSingleSelect<TTable> ToSingle<TTable>(this TTable table, ISqlLogic filter)
        where TTable : ITable
        => new(table, filter);
    /// <summary>
    /// 表筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <returns></returns>
    public static TableSingleSelect<TTable> ToSingle<TTable>(this TableSqlQuery<TTable> query)
        where TTable : ITable
        => new(query);
    /// <summary>
    /// 表范围筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="fetch"></param>
    /// <returns></returns>
    public static TableFetchSingleSelect<TTable> ToSingle<TTable>(this TableFetch<TTable> fetch)
        where TTable : ITable
        => new(fetch);
    #endregion
    #region AliasTableFields
    /// <summary>
    /// 别名表筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <returns></returns>
    public static AliasTableSingleSelect<TTable> ToSingle<TTable>(this TableAlias<TTable> table)
        where TTable : ITable
        => new(table);
    /// <summary>
    /// 别名表过滤筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <param name="filter"></param>
    /// <returns></returns>
    public static AliasTableSingleSelect<TTable> ToSingle<TTable>(this TableAlias<TTable> table, ISqlLogic filter)
        where TTable : ITable
        => new(table, filter);
    /// <summary>
    /// 别名表筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <returns></returns>
    public static AliasTableSingleSelect<TTable> ToSingle<TTable>(this AliasTableSqlQuery<TTable> query)
        where TTable : ITable
        => new(query);
    /// <summary>
    /// 别名表范围筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="fetch"></param>
    /// <returns></returns>
    public static AliasTableFetchSingleSelect<TTable> ToSingle<TTable>(this AliasTableFetch<TTable> fetch)
        where TTable : ITable
        => new(fetch);
    #endregion
    #region MultiTableFields
    /// <summary>
    /// 多(联)表筛选单列
    /// </summary>
    /// <param name="table"></param>
    /// <returns></returns>
    public static MultiTableSelect ToSingle(this IMultiView table)
        => new(table);
    /// <summary>
    /// 多(联)表筛选单列
    /// </summary>
    /// <param name="fetch"></param>
    /// <returns></returns>
    public static MultiTableFetchSelect ToSingle(this MultiTableFetch fetch)
        => new(fetch);
    #endregion
    #region GroupByTableFields
    /// <summary>
    /// GroupBy后再筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static GroupByTableSingleSelect<TTable> ToSingle<TTable>(this GroupByTableSqlQuery<TTable> source)
        where TTable : ITable
        => new(source);
    /// <summary>
    /// GroupBy后再范围(分页)及列筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="fetch"></param>
    /// <returns></returns>
    public static GroupByTableFetchSingleSelect<TTable> ToSingle<TTable>(this GroupByTableFetch<TTable> fetch)
        where TTable : ITable
        => new(fetch);
    #endregion
    #region GroupByAliasTableFields
    /// <summary>
    /// GroupBy别名表后再筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static GroupByAliasTableSingleSelect<TTable> ToSingle<TTable>(this GroupByAliasTableSqlQuery<TTable> source)
        where TTable : ITable
        => new(source);
    /// <summary>
    /// GroupBy别名表后再范围(分页)及列筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="fetch"></param>
    /// <returns></returns>

    public static GroupByAliasTableFetchSingleSelect<TTable> ToSingle<TTable>(this GroupByAliasTableFetch<TTable> fetch)
        where TTable : ITable
        => new(fetch);
    #endregion
    #region GroupByMultiFields
    /// <summary>
    /// GroupBy后再筛选单列
    /// </summary>
    /// <param name="groupBy"></param>
    /// <returns></returns>
    public static GroupByMultiSelect ToSingle(this GroupByMultiSqlQuery groupBy)
        => new(groupBy);
    /// <summary>
    /// GroupBy后再范围(分页)及列筛选单列
    /// </summary>
    /// <param name="fetch"></param>
    /// <returns></returns>
    public static GroupByMultiFetchSelect ToSingle(this GroupByMultiFetch fetch)
        => new(fetch);
    #endregion
}

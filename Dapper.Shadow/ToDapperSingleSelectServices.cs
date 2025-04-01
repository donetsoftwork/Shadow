using Dapper.Shadow.Fetches;
using Dapper.Shadow.GroupBy;
using Dapper.Shadow.Join;
using Dapper.Shadow.Queries;
using Dapper.Shadow.SingleSelect;
using ShadowSql.Fetches;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Queries;
using ShadowSql.Variants;

namespace Dapper.Shadow;

/// <summary>
/// 构造筛选单列扩展方法
/// </summary>
public static partial class DapperShadowServices
{
    #region TableFields
    /// <summary>
    /// 表筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <returns></returns>
    public static DapperTableSingleSelect<TTable> ToDapperSingle<TTable>(this TTable table)
        where TTable : IDapperTable
        => new(table.Executor, table);
    /// <summary>
    /// 表过滤筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <param name="filter"></param>
    /// <returns></returns>
    public static DapperTableSingleSelect<TTable> ToDapperSingle<TTable>(this TTable table, ISqlLogic filter)
        where TTable : IDapperTable
        => new(table.Executor, table, filter);
    /// <summary>
    /// 表筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <returns></returns>
    public static DapperTableSingleSelect<TTable> ToDapperSingle<TTable>(this DapperTableQuery<TTable> query)
        where TTable : ITable
        => new(query.Executor, query);
    /// <summary>
    /// 表筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <returns></returns>
    public static DapperTableSingleSelect<TTable> ToDapperSingle<TTable>(this TableQuery<TTable> query)
        where TTable : IDapperTable
        => new(query.Source.Executor, query);
    /// <summary>
    /// 表范围筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="fetch"></param>
    /// <returns></returns>
    public static DapperTableFetchSingleSelect<TTable> ToDapperSingle<TTable>(this TableFetch<TTable> fetch)
        where TTable : IDapperTable
        => new(fetch.Source.Executor, fetch);
    #endregion
    #region AliasTableFields
    /// <summary>
    /// 别名表筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <returns></returns>
    public static DapperAliasTableSingleSelect<TTable> ToDapperSingle<TTable>(this TableAlias<TTable> table)
        where TTable : IDapperTable
        => new(table.Target.Executor, table);
    /// <summary>
    /// 别名表过滤筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <param name="filter"></param>
    /// <returns></returns>
    public static DapperAliasTableSingleSelect<TTable> ToDapperSingle<TTable>(this TableAlias<TTable> table, ISqlLogic filter)
        where TTable : IDapperTable
        => new(table.Target.Executor, table, filter);
    /// <summary>
    /// 别名表筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <returns></returns>
    public static DapperAliasTableSingleSelect<TTable> ToDapperSingle<TTable>(this AliasTableQuery<TTable> query)
        where TTable : IDapperTable
        => new(query.Table.Executor, query);
    /// <summary>
    /// 别名表筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <returns></returns>
    public static DapperAliasTableSingleSelect<TTable> ToDapperSingle<TTable>(this DapperAliasTableQuery<TTable> query)
        where TTable : ITable
        => new(query.Executor, query);
    /// <summary>
    /// 别名表范围筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="fetch"></param>
    /// <returns></returns>
    public static DapperAliasTableFetchSingleSelect<TTable> ToDapperSingle<TTable>(this AliasTableFetch<TTable> fetch)
        where TTable : IDapperTable
        => new(fetch.Table.Executor, fetch);
    #endregion
    #region MultiTableFields
    /// <summary>
    /// 多表筛选列
    /// </summary>
    /// <param name="multiTable"></param>
    /// <returns></returns>
    public static DapperMultiTableSingleSelect ToDapperSingle(this DapperMultiTableQuery multiTable)
        => new(multiTable.Executor, multiTable);
    /// <summary>
    /// 联表筛选单列
    /// </summary>
    /// <param name="joinTable"></param>
    /// <returns></returns>
    public static DapperMultiTableSingleSelect ToDapperSingle(this DapperJoinTableQuery joinTable)
        => new(joinTable.Executor, joinTable);
    /// <summary>
    /// 多(联)表筛选单列
    /// </summary>
    /// <param name="fetch"></param>
    /// <returns></returns>
    public static DapperMultiTableFetchSingleSelect ToDapperSingle(this DapperMultiTableFetch fetch)
        => new(fetch.Executor, fetch);
    #endregion
    #region GroupByTableFields
    /// <summary>
    /// GroupBy后再筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static DapperGroupByTableSingleSelect<TTable> ToDapperSingle<TTable>(this DapperGroupByTable<TTable> source)
        where TTable : IDapperTable
        => new(source.Executor, source);
    /// <summary>
    /// GroupBy后再范围(分页)及单列筛选
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="fetch"></param>
    /// <returns></returns>
    public static DapperGroupByTableFetchSingleSelect<TTable> ToDapperSingle<TTable>(this GroupByTableFetch<TTable> fetch)
        where TTable : IDapperTable
        => new(fetch.Table.Executor, fetch);
    #endregion
    #region GroupByAliasTableFields
    /// <summary>
    /// GroupBy别名表后再筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static DapperGroupByAliasTableSingleSelect<TTable> ToDapperSingle<TTable>(this DapperGroupByAliasTable<TTable> source)
        where TTable : IDapperTable
        => new(source.Executor, source);
    /// <summary>
    /// GroupBy别名表后再范围(分页)及单列筛选
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="fetch"></param>
    /// <returns></returns>
    public static DapperGroupByAliasTableFetchSingleSelect<TTable> ToDapperSingle<TTable>(this GroupByAliasTableFetch<TTable> fetch)
        where TTable : IDapperTable
        => new(fetch.Table.Executor, fetch);
    #endregion
    #region GroupByMultiFields
    /// <summary>
    /// GroupBy后再筛选单列
    /// </summary>
    /// <param name="groupBy"></param>
    /// <returns></returns>
    public static DapperGroupByMultiSingleSelect ToDapperSingle(this DapperGroupByMultiQuery groupBy)
        => new(groupBy.Executor, groupBy);
    /// <summary>
    /// GroupBy后再范围(分页)及单列筛选
    /// </summary>
    /// <param name="fetch"></param>
    /// <returns></returns>
    public static DapperGroupByMultiFetchSingleSelect ToDapperSingle(this DapperGroupByMultiFetch fetch)
        => new(fetch.Executor, fetch);
    #endregion
}

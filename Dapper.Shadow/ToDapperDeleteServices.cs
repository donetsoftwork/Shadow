using Dapper.Shadow.Delete;
using ShadowSql.Filters;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Tables;
using System;

namespace Dapper.Shadow;

/// <summary>
/// Delete扩展方法
/// </summary>
public static partial class DapperShadowServices
{
    #region TableDelete
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="table"></param>
    /// <param name="where"></param>
    /// <param name="executor"></param>
    /// <returns></returns>
    public static DapperTableDelete ToDapperDelete(this ITable table, ISqlLogic where, IExecutor executor)
        => new(executor, table, where);
    /// <summary>
    /// 删除
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static DapperTableDelete ToDapperDelete<TTable>(this TTable table, Func<TTable, ISqlLogic> query)
        where TTable : IDapperTable
        => new(table.Executor, table, query(table));
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="tableQuery"></param>
    /// <returns></returns>
    public static DapperTableDelete ToDapperDelete<TSource>(this TableSqlQuery<TSource> tableQuery)
        where TSource : IDapperTable
        => new(tableQuery.Source.Executor, tableQuery.Source, ((IDataFilter)tableQuery).Filter);
    /// <summary>
    /// 删除
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <param name="query"></param>
    /// <param name="executor"></param>
    /// <returns></returns>
    public static DapperTableDelete ToDapperDelete<TTable>(this TTable table, Func<TTable, ISqlLogic> query, IExecutor executor)
        where TTable : ITable
        => new(executor, table, query(table));
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="tableQuery"></param>
    /// <param name="executor"></param>
    /// <returns></returns>
    public static DapperTableDelete ToDapperDelete<TSource>(this TableSqlQuery<TSource> tableQuery, IExecutor executor)
        where TSource : ITable
        => new(executor, tableQuery.Source, ((IDataFilter)tableQuery).Filter);
    #endregion
    #region MultiTableDelete
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="view"></param>
    /// <param name="executor"></param>
    /// <returns></returns>
    public static DapperMultiTableDelete ToDapperDelete(this IMultiView view, IExecutor executor)
        => new(executor, view);
    #endregion
    #region TruncateTable
    /// <summary>
    /// 清空表
    /// </summary>
    /// <param name="table"></param>
    /// <param name="executor"></param>
    /// <returns></returns>
    public static DapperTruncateTable ToDapperTruncate(this ITable table, IExecutor executor)
        => new(executor, table);
    #endregion
}

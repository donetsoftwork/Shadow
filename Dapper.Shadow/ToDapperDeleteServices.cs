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
    /// <param name="table">表</param>
    /// <param name="where">查询条件</param>
    /// <param name="executor">执行器</param>
    /// <returns></returns>
    public static DapperTableDelete ToDapperDelete(this ITable table, ISqlLogic where, IExecutor executor)
        => new(executor, table, where);
    /// <summary>
    /// 删除
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table">表</param>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public static DapperTableDelete ToDapperDelete<TTable>(this TTable table, Func<TTable, ISqlLogic> query)
        where TTable : IDapperTable
        => new(table.Executor, table, query(table));
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public static DapperTableDelete ToDapperDelete<TSource>(this TableSqlQuery<TSource> query)
        where TSource : IDapperTable
        => new(query.Source.Executor, query.Source, ((IDataFilter)query).Filter);
    /// <summary>
    /// 删除
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table">表</param>
    /// <param name="query">查询</param>
    /// <param name="executor">执行器</param>
    /// <returns></returns>
    public static DapperTableDelete ToDapperDelete<TTable>(this TTable table, Func<TTable, ISqlLogic> query, IExecutor executor)
        where TTable : ITable
        => new(executor, table, query(table));
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="query">查询</param>
    /// <param name="executor">执行器</param>
    /// <returns></returns>
    public static DapperTableDelete ToDapperDelete<TSource>(this TableSqlQuery<TSource> query, IExecutor executor)
        where TSource : ITable
        => new(executor, query.Source, ((IDataFilter)query).Filter);
    #endregion
    #region MultiTableDelete
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="multiView">多(联)表</param>
    /// <param name="executor">执行器</param>
    /// <returns></returns>
    public static DapperMultiTableDelete ToDapperDelete(this IMultiView multiView, IExecutor executor)
        => new(executor, multiView);
    #endregion
    #region TruncateTable
    /// <summary>
    /// 清空表
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="executor">执行器</param>
    /// <returns></returns>
    public static DapperTruncateTable ToDapperTruncate(this ITable table, IExecutor executor)
        => new(executor, table);
    #endregion
}

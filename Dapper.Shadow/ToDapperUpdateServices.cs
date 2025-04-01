using Dapper.Shadow.Join;
using Dapper.Shadow.Update;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Tables;
using ShadowSql.Update;
using System;

namespace Dapper.Shadow;

/// <summary>
/// 构造修改扩展方法
/// </summary>
public static partial class DapperShadowServices
{
    #region TableUpdate
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="table"></param>
    /// <param name="where"></param>
    /// <returns></returns>
    public static DapperTableUpdate<TTable> ToDapperUpdate<TTable>(this TTable table, ISqlLogic where)
        where TTable : IDapperTable
        => new(table.Executor, table, where);
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="table"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static DapperTableUpdate<TTable> ToDapperUpdate<TTable>(this TTable table, Func<TTable, ISqlLogic> query)
        where TTable : IDapperTable
        => new(table.Executor, table, query(table));
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="tableQuery"></param>
    /// <returns></returns>
    public static DapperTableUpdate<TTable> ToDapperUpdate<TTable>(this TableSqlQuery<TTable> tableQuery)
        where TTable : IDapperTable
        => new(tableQuery.Source.Executor, tableQuery.Source, tableQuery.Filter);
    #endregion
    #region MultiTableUpdate
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="view"></param>
    /// <returns></returns>
    public static DapperMultiTableUpdate ToDapperUpdate(this DapperMultiTableSqlQuery view)
        => new(view.Executor, view);
    #endregion

    #region TableUpdate
    /// <summary>
    /// 修改
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <param name="where"></param>
    /// <param name="executor"></param>
    /// <returns></returns>
    public static DapperTableUpdate<TTable> ToDapperUpdate<TTable>(this TTable table, ISqlLogic where, IExecutor executor)
        where TTable : ITable
        => new(executor, table, where);
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="table"></param>
    /// <param name="query"></param>
    /// <param name="executor"></param>
    /// <returns></returns>
    public static DapperTableUpdate<TTable> ToDapperUpdate<TTable>(this TTable table, Func<TTable, ISqlLogic> query, IExecutor executor)
        where TTable : ITable
        => new(executor, table, query(table));
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="tableQuery"></param>
    /// <param name="executor"></param>
    /// <returns></returns>
    public static DapperTableUpdate<TTable> ToDapperUpdate<TTable>(this TableSqlQuery<TTable> tableQuery, IExecutor executor)
        where TTable : ITable
        => new(executor, tableQuery.Source, tableQuery.Filter);
    #endregion
    #region MultiTableUpdate
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="view"></param>
    /// <param name="executor"></param>
    /// <returns></returns>
    public static DapperMultiTableUpdate ToDapperUpdate(this IMultiView view, IExecutor executor)
        => new(executor, view);
    #endregion    
}

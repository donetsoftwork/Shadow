using Dapper.Shadow.Join;
using Dapper.Shadow.Update;
using ShadowSql.Filters;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Tables;
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
    /// <param name="table">表</param>
    /// <param name="where">查询条件</param>
    /// <returns></returns>
    public static DapperTableUpdate<TTable> ToDapperUpdate<TTable>(this TTable table, ISqlLogic where)
        where TTable : IDapperTable, IUpdateTable
        => new(table.Executor, table, where);
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public static DapperTableUpdate<TTable> ToDapperUpdate<TTable>(this TTable table, Func<TTable, ISqlLogic> query)
        where TTable : IDapperTable, IUpdateTable
        => new(table.Executor, table, query(table));
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public static DapperTableUpdate<TTable> ToDapperUpdate<TTable>(this TableSqlQuery<TTable> query)
        where TTable : IDapperTable, IUpdateTable
        => new(query.Source.Executor, query.Source, ((IDataFilter)query).Filter);
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
    /// <param name="table">表</param>
    /// <param name="where">查询条件</param>
    /// <param name="executor">执行器</param>
    /// <returns></returns>
    public static DapperTableUpdate<TTable> ToDapperUpdate<TTable>(this TTable table, ISqlLogic where, IExecutor executor)
        where TTable : IUpdateTable
        => new(executor, table, where);
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="query">查询</param>
    /// <param name="executor">执行器</param>
    /// <returns></returns>
    public static DapperTableUpdate<TTable> ToDapperUpdate<TTable>(this TTable table, Func<TTable, ISqlLogic> query, IExecutor executor)
        where TTable : IUpdateTable
        => new(executor, table, query(table));
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="query">查询</param>
    /// <param name="executor">执行器</param>
    /// <returns></returns>
    public static DapperTableUpdate<TTable> ToDapperUpdate<TTable>(this TableSqlQuery<TTable> query, IExecutor executor)
        where TTable : ITable, IUpdateTable
        => new(executor, query.Source, ((IDataFilter)query).Filter);
    #endregion
    #region MultiTableUpdate
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="view"></param>
    /// <param name="executor">执行器</param>
    /// <returns></returns>
    public static DapperMultiTableUpdate ToDapperUpdate(this IMultiView view, IExecutor executor)
        => new(executor, view);
    #endregion    
}

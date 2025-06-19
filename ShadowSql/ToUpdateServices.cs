using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Tables;
using ShadowSql.Update;
using System;

namespace ShadowSql;

/// <summary>
/// 构造修改扩展方法
/// </summary>
public static partial class ShadowSqlServices
{
    #region TableUpdate
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="where">查询条件</param>
    /// <returns></returns>
    public static TableUpdate<TTable> ToUpdate<TTable>(this TTable table, ISqlLogic where)
        where TTable : IUpdateTable
        => new(table, where);
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public static TableUpdate<TTable> ToUpdate<TTable>(this TTable table, Func<TTable, ISqlLogic> query)
        where TTable : IUpdateTable
        => new(table, query(table));
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public static TableUpdate<TTable> ToUpdate<TTable>(this TableSqlQuery<TTable> query)
        where TTable : ITable, IUpdateTable
        => new(query.Source, query._filter);
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public static TableUpdate<IUpdateTable> ToUpdate(this TableSqlQuery query)
        => new(query.Source.AsUpdate(), query._filter);
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public static TableUpdate<TTable> ToUpdate<TTable>(this TableQuery<TTable> query)
        where TTable : ITable, IUpdateTable
        => new(query.Source, query._filter);
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public static TableUpdate<IUpdateTable> ToUpdate(this TableQuery query)
        => new(query.Source.AsUpdate(), query._filter);
    #endregion   
}

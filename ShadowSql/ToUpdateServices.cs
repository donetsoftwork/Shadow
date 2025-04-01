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
    /// <param name="table"></param>
    /// <param name="where"></param>
    /// <returns></returns>
    public static TableUpdate<TTable> ToUpdate<TTable>(this TTable table, ISqlLogic where)
        where TTable : ITable
        => new(table, where);
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="table"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static TableUpdate<TTable> ToUpdate<TTable>(this TTable table, Func<TTable, ISqlLogic> query)
        where TTable : ITable
        => new(table, query(table));
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="tableQuery"></param>
    /// <returns></returns>
    public static TableUpdate<TTable> ToUpdate<TTable>(this TableSqlQuery<TTable> tableQuery)
        where TTable : ITable
        => new(tableQuery.Source, tableQuery.Filter);
    #endregion
    #region MultiTableUpdate
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="view"></param>
    /// <returns></returns>
    public static MultiTableUpdate ToUpdate(this IMultiView view)
        => new(view);
    #endregion    
}

using ShadowSql.Delete;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using System;

namespace ShadowSql;

/// <summary>
/// Delete扩展方法
/// </summary>
public static partial class ShadowSqlCoreServices
{
    #region TableDelete
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="table"></param>
    /// <param name="where"></param>
    /// <returns></returns>
    public static TableDelete ToDelete(this ITable table, ISqlLogic where)
        => new(table, where);
    /// <summary>
    /// 删除
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static TableDelete ToDelete<TTable>(this TTable table, Func<TTable, ISqlLogic> query)
        where TTable : ITable
        => new(table, query(table));
    #endregion
    #region AliasTableDelete
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="table"></param>
    /// <param name="where"></param>
    /// <returns></returns>
    public static AliasTableDelete ToDelete(this IAliasTable table, ISqlLogic where)
        => new(table, where);
    #endregion
    #region MultiTableDelete
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="view"></param>
    /// <returns></returns>
    public static MultiTableDelete ToDelete(this IMultiView view)
        => new(view);
    #endregion
    #region TruncateTable
    /// <summary>
    /// 清空表
    /// </summary>
    /// <param name="table"></param>
    public static TruncateTable ToTruncate(this ITable table)
        => new(table);
    #endregion
}

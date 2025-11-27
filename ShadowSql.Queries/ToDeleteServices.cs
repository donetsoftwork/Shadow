using ShadowSql.Delete;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Tables;
using System;

namespace ShadowSql;

/// <summary>
/// Delete扩展方法
/// </summary>
public static partial class ShadowSqlQueryServices
{
    #region TableDelete
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public static TableDelete ToDelete(this TableSqlQuery query)
        => new((ITable)query.Original, query._filter);
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public static TableDelete ToDelete(this TableQuery query)
        => new((ITable)query.Original, query._filter);
    #endregion
    #region AliasTableDelete
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="where">查询条件</param>
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
}

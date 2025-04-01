using ShadowSql.Filters;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Queries;
using System;
using System.Collections.Generic;

namespace ShadowSql;

/// <summary>
/// Where查询扩展方法
/// </summary>
public static partial class ShadowSqlServices
{
    #region 基础查询功能
    /// <summary>
    /// 按原始sql查询
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="query"></param>
    /// <param name="conditions"></param>
    /// <returns></returns>
    public static Query Where<Query>(this Query query, params IEnumerable<string> conditions)
        where Query : IDataSqlQuery, IWhere
    {
        query.AddConditions(conditions);
        return query;
    }
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="query"></param>
    /// <param name="logic"></param>
    /// <returns></returns>
    public static Query Where<Query>(this Query query, AtomicLogic logic)
        where Query : IDataFilter, IWhere
    {
        query.AddLogic(logic);
        return query;
    }
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="query"></param>
    /// <param name="logic"></param>
    /// <returns></returns>
    public static Query Where<Query>(this Query query, Func<ITableView, AtomicLogic> logic)
        where Query : IDataFilter, IWhere
    {
        query.AddLogic(logic(query.Source));
        return query;
    }
    /// <summary>
    /// 按列查询
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="query"></param>
    /// <param name="columnName"></param>
    /// <param name="compare"></param>
    /// <returns></returns>
    public static Query Column<Query>(this Query query, string columnName, Func<ICompareField, AtomicLogic> compare)
        where Query : IDataFilter
    {
        query.AddLogic(compare(query.GetCompareField(columnName)));
        return query;
    }
    #endregion
    #region SqlQuery
    /// <summary>
    /// Where
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="dataQuery"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static Query Where<Query>(this Query dataQuery, Func<SqlQuery, SqlQuery> query)
        where Query : IDataSqlQuery, IWhere
    {
        dataQuery.ApplyFilter(query);
        return dataQuery;
    }
    #endregion
    #region Logic
    /// <summary>
    /// Where
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="dataQuery"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static Query Where<Query>(this Query dataQuery, Func<Logic, Logic> query)
        where Query : IDataQuery, IWhere
    {
        dataQuery.ApplyFilter(query);
        return dataQuery;
    }
    #endregion
}

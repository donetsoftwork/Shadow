﻿using ShadowSql.Filters;
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
    #region 基础查询
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
        query.Query.AddConditions(conditions);
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
        where Query : IDataSqlQuery, IWhere
    {
        query.Query.AddLogic(logic);
        return query;
    }
    #endregion
    #region ITableView
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="query"></param>
    /// <param name="logic"></param>
    /// <returns></returns>
    public static Query Where<Query>(this Query query, Func<ITableView, AtomicLogic> logic)
        where Query : IDataSqlQuery, IWhere
    {
        query.Query.AddLogic(logic(query.Source));
        return query;
    }
    #endregion
    #region SqlQuery
    /// <summary>
    /// Where
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="query"></param>
    /// <param name="where"></param>
    /// <returns></returns>
    public static Query Where<Query>(this Query query, Func<SqlQuery, SqlQuery> where)
        where Query : IDataSqlQuery, IWhere
    {
        query.Query = where(query.Query);
        return query;
    }
    #endregion
}

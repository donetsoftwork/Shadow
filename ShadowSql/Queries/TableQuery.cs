using ShadowSql.Identifiers;
using ShadowSql.Logics;
using System;
using System.Collections.Generic;

namespace ShadowSql.Queries;

/// <summary>
/// 查询表
/// </summary>
/// <param name="table"></param>
/// <param name="query"></param>
public class TableQuery<TTable>(TTable table, SqlQuery query)   
    : DataQuery<TTable>(table, query), ITableView
     where TTable : ITable
{
    #region 基础查询功能
    /// <summary>
    /// 按原始sql查询
    /// </summary>
    /// <param name="conditions"></param>
    /// <returns></returns>
    public TableQuery<TTable> Where(params IEnumerable<string> conditions)
    {
        AddConditions(conditions);
        return this;
    }
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <param name="logic"></param>
    /// <returns></returns>
    public TableQuery<TTable> Where(AtomicLogic logic)
    {
        AddLogic(logic);
        return this;
    }
    /// <summary>
    /// Where
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public TableQuery<TTable> Where(Func<SqlQuery, SqlQuery> query)
    {
        ApplyQuery(query);
        return this;
    }
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public TableQuery<TTable> Where(Func<TTable, SqlQuery, SqlQuery> query)
    {
        ApplyQuery(query);
        return this;
    }
    ///// <summary>
    ///// 切换为And
    ///// </summary>
    ///// <returns></returns>
    //public TableQuery<TSource> ToAnd()
    //{
    //    ToAndCore();
    //    return this;
    //}
    ///// <summary>
    ///// 切换为And
    ///// </summary>
    ///// <returns></returns>
    //public TableQuery<TSource> ToOr()
    //{
    //    ToOrCore();
    //    return this;
    //}
    #endregion
    #region 扩展查询功能
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public TableQuery<TTable> Where(Func<TTable, AtomicLogic> query)
    {
        AddLogic(query(_source));
        return this;
    }
    #endregion
}
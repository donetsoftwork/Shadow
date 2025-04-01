using ShadowSql.Aggregates;
using ShadowSql.Filters;
using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Queries;
using System;
using System.Collections.Generic;

namespace ShadowSql;

/// <summary>
/// GroupBy查询扩展方法
/// </summary>
public static partial class ShadowSqlServices
{
    #region 基础查询功能
    #region conditions
    /// <summary>
    /// 按原始sql查询
    /// </summary>
    /// <typeparam name="TGroupByQuery"></typeparam>
    /// <param name="groupBy"></param>
    /// <param name="conditions"></param>
    /// <returns></returns>
    public static TGroupByQuery Having<TGroupByQuery>(this TGroupByQuery groupBy, params IEnumerable<string> conditions)
        where TGroupByQuery : GroupByQueryBase
    {
        groupBy.AddConditions(conditions);
        return groupBy;
    }
    #endregion
    #region AtomicLogic
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <typeparam name="TGroupBy"></typeparam>
    /// <param name="groupBy"></param>
    /// <param name="logic"></param>
    /// <returns></returns>
    public static TGroupBy Having<TGroupBy>(this TGroupBy groupBy, AtomicLogic logic)
        where TGroupBy : GroupByBase
    {
        groupBy.AddLogic(logic);
        return groupBy;
    }
    /// <summary>
    /// Where
    /// </summary>
    /// <typeparam name="TGroupBy"></typeparam>
    /// <param name="groupBy"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static TGroupBy Having<TGroupBy>(this TGroupBy groupBy, Func<IGroupByView, AtomicLogic> query)
        where TGroupBy : GroupByBase
    {
        groupBy.AddLogic(query(groupBy));
        return groupBy;
    }
    #endregion
    #region SqlQuery
    /// <summary>
    /// 按SqlQuery查询
    /// </summary>
    /// <typeparam name="TGroupByQuery"></typeparam>
    /// <param name="groupBy"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static TGroupByQuery Having<TGroupByQuery>(this TGroupByQuery groupBy, Func<SqlQuery, SqlQuery> query)
        where TGroupByQuery : GroupByQueryBase
    {
        groupBy.ApplyHaving(query);
        return groupBy;
    }
    /// <summary>
    /// 按SqlQuery查询
    /// </summary>
    /// <typeparam name="TGroupByQuery"></typeparam>
    /// <param name="groupBy"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static TGroupByQuery Having<TGroupByQuery>(this TGroupByQuery groupBy, Func<IGroupByView, SqlQuery, SqlQuery> query)
        where TGroupByQuery : GroupByQueryBase, IDataFilter
    {
        groupBy.ApplyHaving(sqlQuery => query(groupBy, sqlQuery));
        return groupBy;
    }
    #endregion
    #region Logic
    /// <summary>
    /// 按Logic查询
    /// </summary>
    /// <typeparam name="TGroupByLogic"></typeparam>
    /// <param name="groupBy"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static TGroupByLogic Having<TGroupByLogic>(this TGroupByLogic groupBy, Func<Logic, Logic> query)
        where TGroupByLogic : GroupByLogicBase
    {
        groupBy.ApplyHaving(query);
        return groupBy;
    }
    /// <summary>
    /// 按Logic查询
    /// </summary>
    /// <typeparam name="TGroupByLogic"></typeparam>
    /// <param name="groupBy"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static TGroupByLogic Having<TGroupByLogic>(this TGroupByLogic groupBy, Func<IGroupByView, Logic, Logic> query)
        where TGroupByLogic : GroupByLogicBase
    {
        groupBy.ApplyHaving(logic => query(groupBy, logic));
        return groupBy;
    }
    #endregion
    #endregion
    #region HavingAggregate
    /// <summary>
    /// 按聚合逻辑查询
    /// </summary>
    /// <typeparam name="TGroupBy"></typeparam>
    /// <param name="groupBy"></param>
    /// <param name="select"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static TGroupBy HavingAggregate<TGroupBy>(this TGroupBy groupBy, Func<IGroupByView, IAggregateField> select, Func<IAggregateField, AtomicLogic> query)
        where TGroupBy : GroupByBase
    {
        groupBy.AddLogic(query(select(groupBy)));
        return groupBy;
    }
    /// <summary>
    /// 按列名聚合
    /// </summary>
    /// <typeparam name="TGroupBy"></typeparam>
    /// <param name="groupBy"></param>
    /// <param name="aggregate"></param>
    /// <param name="columnName"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static TGroupBy HavingAggregate<TGroupBy>(this TGroupBy groupBy, string aggregate, string columnName, Func<IAggregateField, AtomicLogic> query)
        where TGroupBy : GroupByBase, IDataFilter
    {
        groupBy.AddLogic(query(groupBy.GetCompareField(columnName).AggregateTo(aggregate)));
        return groupBy;
    }
    #endregion
}

using ShadowSql.Aggregates;
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
public static partial class ShadowSqlCoreServices
{
    #region IDataSqlQuery
    #region 基础查询功能
    #region conditions
    /// <summary>
    /// 按原始sql查询
    /// </summary>
    /// <typeparam name="TGroupBy"></typeparam>
    /// <param name="groupBy"></param>
    /// <param name="conditions"></param>
    /// <returns></returns>
    public static TGroupBy Having<TGroupBy>(this TGroupBy groupBy, params IEnumerable<string> conditions)
        where TGroupBy : GroupByBase, IDataSqlQuery
    {
        groupBy.Query.AddConditions(conditions);
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
        where TGroupBy : GroupByBase, IDataSqlQuery
    {
        groupBy.Query.AddLogic(logic);
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
        where TGroupBy : GroupByBase, IDataSqlQuery
    {
        groupBy.Query.AddLogic(query(groupBy));
        return groupBy;
    }
    #endregion
    #endregion
    #region HavingAggregate
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
        where TGroupBy : GroupByBase, IDataSqlQuery
    {
        groupBy.Query.AddLogic(query(groupBy.GetCompareField(columnName).AggregateTo(aggregate)));
        return groupBy;
    }
    #endregion
    #region Apply
    /// <summary>
    /// 按SqlQuery查询
    /// </summary>
    /// <typeparam name="TGroupBy"></typeparam>
    /// <param name="groupBy"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static TGroupBy Apply<TGroupBy>(this TGroupBy groupBy, Func<SqlQuery, IGroupByView, SqlQuery> query)
        where TGroupBy : GroupByBase, IDataSqlQuery
    {
        groupBy.Query = query(groupBy.Query, groupBy);
        return groupBy;
    }
    #endregion
    #endregion
    #region IDataQuery
    #region 与运算
    /// <summary>
    /// 与运算
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="query"></param>
    /// <param name="logic"></param>
    /// <returns></returns>
    public static Query And<Query>(this Query query, Func<IGroupByView, AtomicLogic> logic)
        where Query : GroupByBase, IDataQuery
    {
        query.Logic = query.Logic.And(logic(query));
        return query;
    }
    /// <summary>
    /// 与运算嵌套或逻辑
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="query"></param>
    /// <param name="logic"></param>
    /// <returns></returns>
    public static Query And<Query>(this Query query, Func<IGroupByView, OrLogic> logic)
        where Query : GroupByBase, IDataQuery
    {
        query.Logic = query.Logic.And(logic(query));
        return query;
    }
    #endregion
    #region 或运算
    /// <summary>
    /// 或运算
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="query"></param>
    /// <param name="logic"></param>
    /// <returns></returns>
    public static Query Or<Query>(this Query query, Func<IGroupByView, AtomicLogic> logic)
        where Query : GroupByBase, IDataQuery
    {
        query.Logic = query.Logic.Or(logic(query));
        return query;
    }
    /// <summary>
    /// 或运算嵌套与逻辑
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="query"></param>
    /// <param name="logic"></param>
    /// <returns></returns>
    public static Query Or<Query>(this Query query, Func<IGroupByView, AndLogic> logic)
        where Query : GroupByBase, IDataQuery
    {
        query.Logic = query.Logic.Or(logic(query));
        return query;
    }
    #endregion
    /// <summary>
    /// 应用逻辑
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="groupBy"></param>
    /// <param name="logic"></param>
    /// <returns></returns>
    public static Query Apply<Query>(this Query groupBy, Func<Logic, IGroupByView,  Logic> logic)
        where Query : GroupByBase, IDataQuery
    {
        groupBy.Logic = logic(groupBy.Logic, groupBy);
        return groupBy;
    }
    #endregion
}

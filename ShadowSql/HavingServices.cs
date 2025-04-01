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
public static partial class ShadowSqlServices
{
    #region 基础查询功能
    /// <summary>
    /// 按原始sql查询
    /// </summary>
    /// <typeparam name="GroupByQuery"></typeparam>
    /// <param name="groupBy"></param>
    /// <param name="conditions"></param>
    /// <returns></returns>
    public static GroupByQuery Having<GroupByQuery>(this GroupByQuery groupBy, params IEnumerable<string> conditions)
        where GroupByQuery : IGroupByQuery
    {
        groupBy.AddConditions(conditions);
        return groupBy;
    }
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <typeparam name="GroupByQuery"></typeparam>
    /// <param name="groupBy"></param>
    /// <param name="logic"></param>
    /// <returns></returns>
    public static GroupByQuery Having<GroupByQuery>(this GroupByQuery groupBy, AtomicLogic logic)
        where GroupByQuery : IGroupByQuery
    {
        groupBy.AddLogic(logic);
        return groupBy;
    }
    /// <summary>
    /// Where
    /// </summary>
    /// <typeparam name="GroupByQuery"></typeparam>
    /// <param name="groupBy"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static GroupByQuery Having<GroupByQuery>(this GroupByQuery groupBy, Func<IGroupByView, AtomicLogic> query)
        where GroupByQuery : IGroupByQuery
    {
        groupBy.AddLogic(query(groupBy));
        return groupBy;
    }
    /// <summary>
    /// Where
    /// </summary>
    /// <typeparam name="GroupByQuery"></typeparam>
    /// <param name="groupBy"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static GroupByQuery Having<GroupByQuery>(this GroupByQuery groupBy, Func<SqlQuery, SqlQuery> query)
        where GroupByQuery : IGroupByQuery
    {
        groupBy.ApplyQuery(query);
        return groupBy;
    }
    /// <summary>
    /// Where
    /// </summary>
    /// <typeparam name="GroupByQuery"></typeparam>
    /// <param name="groupBy"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static GroupByQuery Having<GroupByQuery>(this GroupByQuery groupBy, Func<IGroupByView, SqlQuery, SqlQuery> query)
        where GroupByQuery : IGroupByQuery
    {
        groupBy.ApplyQuery(sqlQuery => query(groupBy, sqlQuery));
        return groupBy;
    }

    #endregion
    #region HavingAggregate
    /// <summary>
    /// 按聚合逻辑查询
    /// </summary>
    /// <typeparam name="GroupByQuery"></typeparam>
    /// <param name="groupBy"></param>
    /// <param name="select"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static GroupByQuery HavingAggregate<GroupByQuery>(this GroupByQuery groupBy, Func<IGroupByView, IAggregateField> select, Func<IAggregateField, AtomicLogic> query)
        where GroupByQuery : GroupByBase
    {
        groupBy.InnerQuery.AddLogic(query(select(groupBy)));
        return groupBy;
    }
    /// <summary>
    /// 按列名聚合
    /// </summary>
    /// <typeparam name="GroupByQuery"></typeparam>
    /// <param name="groupBy"></param>
    /// <param name="aggregate"></param>
    /// <param name="columnName"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static GroupByQuery HavingAggregate<GroupByQuery>(this GroupByQuery groupBy, string aggregate, string columnName, Func<IAggregateField, AtomicLogic> query)
        where GroupByQuery : GroupByBase, IGroupByQuery
    {
        if (groupBy.GetColumn(columnName) is IColumn column)
            groupBy.InnerQuery.AddLogic(query(column.AggregateTo(aggregate)));
        else
            groupBy.InnerQuery.AddLogic(query(groupBy.Field(columnName).AggregateTo(aggregate)));
        return groupBy;
    }
    /// <summary>
    /// 按聚合逻辑查询
    /// </summary>
    /// <typeparam name="GroupByQuery"></typeparam>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="groupBy"></param>
    /// <param name="select"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static GroupByQuery HavingAggregate<GroupByQuery, TSource>(this GroupByQuery groupBy, Func<TSource, IAggregateField> select, Func<IAggregateField, AtomicLogic> query)
        where GroupByQuery : GroupByBase<TSource>
        where TSource : ITableView
    {
        groupBy.InnerQuery.AddLogic(query(select(groupBy.Source)));
        return groupBy;
    }
    #endregion
}

using ShadowSql.Aggregates;
using ShadowSql.Identifiers;
using ShadowSql.Variants;
using System;

namespace ShadowSql;

/// <summary>
/// 聚合扩展方法
/// </summary>
public static partial class ShadowSqlServices
{
    #region AggregateTo
    /// <summary>
    /// 计数聚合
    /// </summary>
    /// <param name="view"></param>
    /// <param name="field"></param>
    /// <returns></returns>
    public static IAggregateField Count(this ITableView view, string field)
        => view.Field(field).AggregateTo(AggregateConstants.Count);
    /// <summary>
    /// 最大值聚合
    /// </summary>
    /// <param name="view"></param>
    /// <param name="field"></param>
    /// <returns></returns>
    public static IAggregateField Sum(this ITableView view, string field)
        => view.Field(field).AggregateTo(AggregateConstants.Sum);
    /// <summary>
    /// 均值聚合
    /// </summary>
    /// <param name="view"></param>
    /// <param name="field"></param>
    /// <returns></returns>
    public static IAggregateField Avg(this ITableView view, string field)
        => view.Field(field).AggregateTo(AggregateConstants.Avg);
    /// <summary>
    /// 最大值聚合
    /// </summary>
    /// <param name="view"></param>
    /// <param name="field"></param>
    /// <returns></returns>
    public static IAggregateField Max(this ITableView view, string field)
        => view.Field(field).AggregateTo(AggregateConstants.Max);
    /// <summary>
    /// 最小值聚合
    /// </summary>
    /// <param name="view"></param>
    /// <param name="field"></param>
    /// <returns></returns>
    public static IAggregateField Min(this ITableView view, string field)
        => view.Field(field).AggregateTo(AggregateConstants.Min);
    /// <summary>
    /// 聚合
    /// </summary>
    /// <param name="view"></param>
    /// <param name="aggregate"></param>
    /// <param name="field"></param>
    /// <returns></returns>
    public static IAggregateField Aggregate(this ITableView view, string aggregate, string field)
        => view.Field(field).AggregateTo(aggregate);
    #endregion
    #region AggregateAs
    /// <summary>
    /// 计数聚合
    /// </summary>
    /// <param name="view"></param>
    /// <param name="field"></param>
    /// <param name="alias"></param>
    /// <returns></returns>
    public static IAggregateFieldAlias CountAs(this ITableView view, string field, string alias = "")
        => view.Field(field).AggregateAs(AggregateConstants.Count, alias);
    /// <summary>
    /// 最大值聚合
    /// </summary>
    /// <param name="view"></param>
    /// <param name="field"></param>
    /// <param name="alias"></param>
    /// <returns></returns>
    public static IAggregateFieldAlias SumAs(this ITableView view, string field, string alias = "")
        => view.Field(field).AggregateAs(AggregateConstants.Sum, alias);
    /// <summary>
    /// 均值聚合
    /// </summary>
    /// <param name="view"></param>
    /// <param name="field"></param>
    /// <param name="alias"></param>
    /// <returns></returns>
    public static IAggregateFieldAlias AvgAs(this ITableView view, string field, string alias = "")
        => view.Field(field).AggregateAs(AggregateConstants.Avg, alias);
    /// <summary>
    /// 最大值聚合
    /// </summary>
    /// <param name="view"></param>
    /// <param name="field"></param>
    /// <param name="alias"></param>
    /// <returns></returns>
    public static IAggregateFieldAlias MaxAs(this ITableView view, string field, string alias = "")
        => view.Field(field).AggregateAs(AggregateConstants.Max, alias);
    /// <summary>
    /// 最小值聚合
    /// </summary>
    /// <param name="view"></param>
    /// <param name="field"></param>
    /// <param name="alias"></param>
    /// <returns></returns>
    public static IAggregateFieldAlias MinAs(this ITableView view, string field, string alias = "")
        => view.Field(field).AggregateAs(AggregateConstants.Min, alias);
    /// <summary>
    /// 聚合
    /// </summary>
    /// <param name="view"></param>
    /// <param name="aggregate"></param>
    /// <param name="field"></param>
    /// <param name="alias"></param>
    /// <returns></returns>
    public static IAggregateFieldAlias AggregateAs(this ITableView view, string aggregate, string field, string alias = "")
        => view.Field(field).AggregateAs(aggregate, alias);
    #endregion
    #region Aggregate
    /// <summary>
    /// 聚合
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="alias"></param>
    /// <param name="select"></param>
    /// <param name="aggregate"></param>
    /// <returns></returns>
    public static IAggregateField Aggregate<TTable>(this TableAlias<TTable> alias, Func<TTable, IColumn> select, Func<IColumn, IAggregateField> aggregate)
        where TTable : ITable
        => aggregate(alias.Prefix(select(alias.Target)));
    /// <summary>
    /// 聚合
    /// </summary>
    /// <param name="multiTable"></param>
    /// <param name="tableName"></param>
    /// <param name="aggregate"></param>
    /// <returns></returns>
    public static IAggregateField Aggregate(this IMultiView multiTable, string tableName, Func<IAliasTable, IAggregateField> aggregate)
    {
        return aggregate(multiTable.From(tableName));
    }
    /// <summary>
    /// 聚合
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="multiTable"></param>
    /// <param name="tableName"></param>
    /// <param name="select"></param>
    /// <param name="aggregate"></param>
    /// <returns></returns>
    public static IAggregateField Aggregate<TTable>(this IMultiView multiTable, string tableName, Func<TTable, IColumn> select, Func<IColumn, IAggregateField> aggregate)
        where TTable : ITable
    {
        var member = multiTable.Table<TTable>(tableName);
        return aggregate(member.Prefix(select(member.Target)));
    }
    #endregion
}

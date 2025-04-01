using ShadowSql.Aggregates;
using ShadowSql.Identifiers;

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
    public static IAggregateField Count(this IGroupByView view, string field)
        => view.Field(field).AggregateTo(AggregateConstants.Count);
    /// <summary>
    /// 最大值聚合
    /// </summary>
    /// <param name="view"></param>
    /// <param name="field"></param>
    /// <returns></returns>
    public static IAggregateField Sum(this IGroupByView view, string field)
        => view.Field(field).AggregateTo(AggregateConstants.Sum);
    /// <summary>
    /// 均值聚合
    /// </summary>
    /// <param name="view"></param>
    /// <param name="field"></param>
    /// <returns></returns>
    public static IAggregateField Avg(this IGroupByView view, string field)
        => view.Field(field).AggregateTo(AggregateConstants.Avg);
    /// <summary>
    /// 最大值聚合
    /// </summary>
    /// <param name="view"></param>
    /// <param name="field"></param>
    /// <returns></returns>
    public static IAggregateField Max(this IGroupByView view, string field)
        => view.Field(field).AggregateTo(AggregateConstants.Max);
    /// <summary>
    /// 最小值聚合
    /// </summary>
    /// <param name="view"></param>
    /// <param name="field"></param>
    /// <returns></returns>
    public static IAggregateField Min(this IGroupByView view, string field)
        => view.Field(field).AggregateTo(AggregateConstants.Min);
    /// <summary>
    /// 聚合
    /// </summary>
    /// <param name="view"></param>
    /// <param name="aggregate"></param>
    /// <param name="field"></param>
    /// <returns></returns>
    public static IAggregateField Aggregate(this IGroupByView view, string aggregate, string field)
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
    public static IAggregateFieldAlias CountAs(this IGroupByView view, string field, string alias = "")
        => view.Field(field).AggregateAs(AggregateConstants.Count, alias);
    /// <summary>
    /// 最大值聚合
    /// </summary>
    /// <param name="view"></param>
    /// <param name="field"></param>
    /// <param name="alias"></param>
    /// <returns></returns>
    public static IAggregateFieldAlias SumAs(this IGroupByView view, string field, string alias = "")
        => view.Field(field).AggregateAs(AggregateConstants.Sum, alias);
    /// <summary>
    /// 均值聚合
    /// </summary>
    /// <param name="view"></param>
    /// <param name="field"></param>
    /// <param name="alias"></param>
    /// <returns></returns>
    public static IAggregateFieldAlias AvgAs(this IGroupByView view, string field, string alias = "")
        => view.Field(field).AggregateAs(AggregateConstants.Avg, alias);
    /// <summary>
    /// 最大值聚合
    /// </summary>
    /// <param name="view"></param>
    /// <param name="field"></param>
    /// <param name="alias"></param>
    /// <returns></returns>
    public static IAggregateFieldAlias MaxAs(this IGroupByView view, string field, string alias = "")
        => view.Field(field).AggregateAs(AggregateConstants.Max, alias);
    /// <summary>
    /// 最小值聚合
    /// </summary>
    /// <param name="view"></param>
    /// <param name="field"></param>
    /// <param name="alias"></param>
    /// <returns></returns>
    public static IAggregateFieldAlias MinAs(this IGroupByView view, string field, string alias = "")
        => view.Field(field).AggregateAs(AggregateConstants.Min, alias);
    /// <summary>
    /// 聚合
    /// </summary>
    /// <param name="view"></param>
    /// <param name="aggregate"></param>
    /// <param name="field"></param>
    /// <param name="alias"></param>
    /// <returns></returns>
    public static IAggregateFieldAlias AggregateAs(this IGroupByView view, string aggregate, string field, string alias = "")
        => view.Field(field).AggregateAs(aggregate, alias);
    #endregion
}

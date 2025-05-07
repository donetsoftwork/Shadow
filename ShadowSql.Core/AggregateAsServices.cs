using ShadowSql.Aggregates;
using ShadowSql.FieldInfos;
using ShadowSql.Identifiers;

namespace ShadowSql;

/// <summary>
/// 聚合扩展方法
/// </summary>

public static class AggregateAsServices
{
    #region ICompareField
    /// <summary>
    /// 计数聚合
    /// </summary>
    /// <returns></returns>
    public static IAggregateFieldAlias DistinctCountAs(this ICompareField field, string alias = "")
        => field.AggregateAs(AggregateConstants.Count, alias);
    /// <summary>
    /// 最大值聚合
    /// </summary>
    /// <returns></returns>
    public static IAggregateFieldAlias SumAs(this ICompareField field, string alias = "")
        => field.AggregateAs(AggregateConstants.Sum, alias);
    /// <summary>
    /// 均值聚合
    /// </summary>
    /// <returns></returns>
    public static IAggregateFieldAlias AvgAs(this ICompareField field, string alias = "")
        => field.AggregateAs(AggregateConstants.Avg, alias);
    /// <summary>
    /// 最大值聚合
    /// </summary>
    /// <returns></returns>
    public static IAggregateFieldAlias MaxAs(this ICompareField field, string alias = "")
        => field.AggregateAs(AggregateConstants.Max, alias);
    /// <summary>
    /// 最小值聚合
    /// </summary>
    /// <returns></returns>
    public static IAggregateFieldAlias MinAs(this ICompareField field, string alias = "")
        => field.AggregateAs(AggregateConstants.Min, alias);
    #endregion
    #region IGroupByView
    /// <summary>
    /// 计数别名
    /// </summary>
    /// <param name="view"></param>
    /// <param name="alias"></param>
    /// <returns></returns>
    public static CountAliasFieldInfo CountAs(this IGroupByView view, string alias = "Count")
        => CountAliasFieldInfo.Use(alias);
    /// <summary>
    /// 计数聚合
    /// </summary>
    /// <param name="view"></param>
    /// <param name="field"></param>
    /// <param name="alias"></param>
    /// <returns></returns>
    public static IAggregateFieldAlias DistinctCountAs(this IGroupByView view, string field, string alias = "")
        => new DistinctCountAliasFieldInfo(view.Source.GetCompareField(field), alias);
    /// <summary>
    /// 最大值聚合
    /// </summary>
    /// <param name="view"></param>
    /// <param name="field"></param>
    /// <param name="alias"></param>
    /// <returns></returns>
    public static IAggregateFieldAlias SumAs(this IGroupByView view, string field, string alias = "")
        => view.Source.GetCompareField(field).AggregateAs(AggregateConstants.Sum, alias);
    /// <summary>
    /// 均值聚合
    /// </summary>
    /// <param name="view"></param>
    /// <param name="field"></param>
    /// <param name="alias"></param>
    /// <returns></returns>
    public static IAggregateFieldAlias AvgAs(this IGroupByView view, string field, string alias = "")
        => view.Source.GetCompareField(field).AggregateAs(AggregateConstants.Avg, alias);
    /// <summary>
    /// 最大值聚合
    /// </summary>
    /// <param name="view"></param>
    /// <param name="field"></param>
    /// <param name="alias"></param>
    /// <returns></returns>
    public static IAggregateFieldAlias MaxAs(this IGroupByView view, string field, string alias = "")
        => view.Source.GetCompareField(field).AggregateAs(AggregateConstants.Max, alias);
    /// <summary>
    /// 最小值聚合
    /// </summary>
    /// <param name="view"></param>
    /// <param name="field"></param>
    /// <param name="alias"></param>
    /// <returns></returns>
    public static IAggregateFieldAlias MinAs(this IGroupByView view, string field, string alias = "")
        => view.Source.GetCompareField(field).AggregateAs(AggregateConstants.Min, alias);
    /// <summary>
    /// 聚合
    /// </summary>
    /// <param name="view"></param>
    /// <param name="aggregate"></param>
    /// <param name="field"></param>
    /// <param name="alias"></param>
    /// <returns></returns>
    public static IAggregateFieldAlias AggregateAs(this IGroupByView view, string aggregate, string field, string alias = "")
        => view.Source.GetCompareField(field).AggregateAs(aggregate, alias);
    #endregion
}

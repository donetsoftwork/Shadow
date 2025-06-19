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
    /// 字段去重统计
    /// </summary>
    /// <param name="field">字段</param>
    /// <param name="aliasName">别名</param>
    /// <returns></returns>
    public static DistinctCountAliasFieldInfo DistinctCountAs(this ICompareField field, string aliasName = "Count")
        => new(field, aliasName);
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
    public static IAggregateFieldAlias MinAs(this ICompareField field, string aliasName = "")
        => field.AggregateAs(AggregateConstants.Min, aliasName);
    #endregion
    #region IGroupByView
    /// <summary>
    /// 计数别名
    /// </summary>
    /// <param name="groupBy">分组查询</param>
    /// <param name="aliasName">别名</param>
    /// <returns></returns>
    public static CountAliasFieldInfo CountAs(this IGroupByView groupBy, string aliasName = "Count")
        => CountAliasFieldInfo.Use(aliasName);
    /// <summary>
    /// 计数聚合
    /// </summary>
    /// <param name="groupBy">分组查询</param>
    /// <param name="fieldName">字段名</param>
    /// <param name="aliasName">别名</param>
    /// <returns></returns>
    public static DistinctCountAliasFieldInfo DistinctCountAs(this IGroupByView groupBy, string fieldName, string aliasName = "Count")
        => new(groupBy.Source.GetCompareField(fieldName), aliasName);
    /// <summary>
    /// 最大值聚合
    /// </summary>
    /// <param name="groupBy">分组查询</param>
    /// <param name="fieldName">字段名</param>
    /// <param name="aliasName">别名</param>
    /// <returns></returns>
    public static IAggregateFieldAlias SumAs(this IGroupByView groupBy, string fieldName, string aliasName = "")
        => groupBy.Source.GetCompareField(fieldName).AggregateAs(AggregateConstants.Sum, aliasName);
    /// <summary>
    /// 均值聚合
    /// </summary>
    /// <param name="groupBy">分组查询</param>
    /// <param name="fieldName">字段名</param>
    /// <param name="aliasName">别名</param>
    /// <returns></returns>
    public static IAggregateFieldAlias AvgAs(this IGroupByView groupBy, string fieldName, string aliasName = "")
        => groupBy.Source.GetCompareField(fieldName).AggregateAs(AggregateConstants.Avg, aliasName);
    /// <summary>
    /// 最大值聚合
    /// </summary>
    /// <param name="groupBy">分组查询</param>
    /// <param name="fieldName">字段名</param>
    /// <param name="aliasName">别名</param>
    /// <returns></returns>
    public static IAggregateFieldAlias MaxAs(this IGroupByView groupBy, string fieldName, string aliasName = "")
        => groupBy.Source.GetCompareField(fieldName).AggregateAs(AggregateConstants.Max, aliasName);
    /// <summary>
    /// 最小值聚合
    /// </summary>
    /// <param name="groupBy">分组查询</param>
    /// <param name="fieldName">字段名</param>
    /// <param name="aliasName">别名</param>
    /// <returns></returns>
    public static IAggregateFieldAlias MinAs(this IGroupByView groupBy, string fieldName, string aliasName = "")
        => groupBy.Source.GetCompareField(fieldName).AggregateAs(AggregateConstants.Min, aliasName);
    /// <summary>
    /// 聚合
    /// </summary>
    /// <param name="groupBy">分组查询</param>
    /// <param name="aggregate">聚合</param>
    /// <param name="fieldName">字段名</param>
    /// <param name="aliasName">别名</param>
    /// <returns></returns>
    public static IAggregateFieldAlias AggregateAs(this IGroupByView groupBy, string aggregate, string fieldName, string aliasName = "")
        => groupBy.Source.GetCompareField(fieldName).AggregateAs(aggregate, aliasName);
    #endregion
}

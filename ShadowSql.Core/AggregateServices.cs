using ShadowSql.Aggregates;
using ShadowSql.FieldInfos;
using ShadowSql.Identifiers;

namespace ShadowSql;

/// <summary>
/// 聚合扩展方法
/// </summary>
public static partial class ShadowSqlCoreServices
{
    #region ICompareField
    /// <summary>
    /// 字段去重统计
    /// </summary>
    /// <param name="field">字段</param>
    /// <returns></returns>
    public static DistinctCountFieldInfo DistinctCount(this ICompareField field)
        => new(field);
    /// <summary>
    /// 最大值聚合
    /// </summary>
    /// <returns></returns>
    public static IAggregateField Sum(this ICompareField field)
        => field.AggregateTo(AggregateConstants.Sum);
    /// <summary>
    /// 均值聚合
    /// </summary>
    /// <returns></returns>
    public static IAggregateField Avg(this ICompareField field)
        => field.AggregateTo(AggregateConstants.Avg);
    /// <summary>
    /// 最大值聚合
    /// </summary>
    /// <returns></returns>
    public static IAggregateField Max(this ICompareField field)
        => field.AggregateTo(AggregateConstants.Max);
    /// <summary>
    /// 最小值聚合
    /// </summary>
    /// <returns></returns>
    public static IAggregateField Min(this ICompareField field)
        => field.AggregateTo(AggregateConstants.Min);
    /// <summary>
    /// 聚合
    /// </summary>
    /// <param name="field">字段</param>
    /// <param name="aggregate">聚合</param>
    /// <returns></returns>
    public static IAggregateField Aggregate(this ICompareField field, string aggregate)
        => field.AggregateTo(aggregate);
    #endregion
    #region IGroupByView
    /// <summary>
    /// 计数
    /// </summary>
    /// <param name="groupBy">分组查询</param>
    /// <returns></returns>
    public static CountFieldInfo Count(this IGroupByView groupBy)
        => CountFieldInfo.Instance;
    /// <summary>
    /// 计数聚合
    /// </summary>
    /// <param name="groupBy">分组查询</param>
    /// <param name="fieldName">字段名</param>
    /// <returns></returns>
    public static DistinctCountFieldInfo DistinctCount(this IGroupByView groupBy, string fieldName)
        => new(groupBy.Source.GetCompareField(fieldName));
    /// <summary>
    /// 最大值聚合
    /// </summary>
    /// <param name="groupBy">分组查询</param>
    /// <param name="fieldName">字段名</param>
    /// <returns></returns>
    public static IAggregateField Sum(this IGroupByView groupBy, string fieldName)
        => groupBy.Source.GetCompareField(fieldName).AggregateTo(AggregateConstants.Sum);
    /// <summary>
    /// 均值聚合
    /// </summary>
    /// <param name="groupBy">分组查询</param>
    /// <param name="fieldName">字段名</param>
    /// <returns></returns>
    public static IAggregateField Avg(this IGroupByView groupBy, string fieldName)
        => groupBy.Source.GetCompareField(fieldName).AggregateTo(AggregateConstants.Avg);
    /// <summary>
    /// 最大值聚合
    /// </summary>
    /// <param name="groupBy">分组查询</param>
    /// <param name="fieldName">字段名</param>
    /// <returns></returns>
    public static IAggregateField Max(this IGroupByView groupBy, string fieldName)
        => groupBy.Source.GetCompareField(fieldName).AggregateTo(AggregateConstants.Max);
    /// <summary>
    /// 最小值聚合
    /// </summary>
    /// <param name="groupBy">分组查询</param>
    /// <param name="fieldName">字段名</param>
    /// <returns></returns>
    public static IAggregateField Min(this IGroupByView groupBy, string fieldName)
        => groupBy.Source.GetCompareField(fieldName).AggregateTo(AggregateConstants.Min);
    /// <summary>
    /// 聚合
    /// </summary>
    /// <param name="groupBy">分组查询</param>
    /// <param name="aggregate">聚合</param>
    /// <param name="fieldName">字段名</param>
    /// <returns></returns>
    public static IAggregateField Aggregate(this IGroupByView groupBy, string aggregate, string fieldName)
        => groupBy.Source.GetCompareField(fieldName).AggregateTo(aggregate);
    #endregion
    #region IAliasTable<TTable>
    ///// <summary>
    ///// 聚合
    ///// </summary>
    ///// <typeparam name="TTable"></typeparam>
    ///// <param name="aliasTable"></param>
    ///// <param name="select">筛选</param>
    ///// <param name="aggregate">聚合</param>
    ///// <returns></returns>
    //public static IAggregateField Aggregate<TTable>(this IAliasTable<TTable> aliasTable, Func<TTable, IColumn> select, Func<IColumn, IAggregateField> aggregate)
    //    where TTable : ITable
    //    => aggregate(aliasTable.Prefix(select(aliasTable.Target)));
    #endregion
    #region IMultiView
    ///// <summary>
    ///// 聚合
    ///// </summary>
    ///// <typeparam name="TAliasTable"></typeparam>
    ///// <param name="multiTable">多表(联表)</param>
    ///// <param name="tableName">表名</param>
    ///// <param name="aggregate">聚合</param>
    ///// <returns></returns>
    //public static IAggregateField Aggregate<TAliasTable>(this IMultiView multiTable, string tableName, Func<TAliasTable, IAggregateField> aggregate)
    //    where TAliasTable : IAliasTable
    //    => aggregate(multiTable.From<TAliasTable>(tableName));
    ///// <summary>
    ///// 聚合
    ///// </summary>
    ///// <param name="multiTable">多表(联表)</param>
    ///// <param name="tableName">表名</param>
    ///// <param name="aggregate">聚合</param>
    ///// <returns></returns>
    //public static IAggregateField Aggregate(this IMultiView multiTable, string tableName, Func<IAliasTable, IAggregateField> aggregate)
    //    => aggregate(multiTable.From(tableName));
    ///// <summary>
    ///// 聚合
    ///// </summary>
    ///// <typeparam name="TTable"></typeparam>
    ///// <param name="multiTable">多表(联表)</param>
    ///// <param name="tableName">表名</param>
    ///// <param name="select">筛选</param>
    ///// <param name="aggregate">聚合</param>
    ///// <returns></returns>
    //public static IAggregateField Aggregate<TTable>(this IMultiView multiTable, string tableName, Func<TTable, IColumn> select, Func<IColumn, IAggregateField> aggregate)
    //    where TTable : ITable
    //{
    //    var member = multiTable.Alias<TTable>(tableName);
    //    return aggregate(member.Prefix(select(member.Target)));
    //}
    #endregion
}

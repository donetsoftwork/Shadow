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
    /// 计数聚合
    /// </summary>
    /// <returns></returns>
    public static IAggregateField DistinctCount(this ICompareField field)
        => field.AggregateTo(AggregateConstants.Count);
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
    /// <param name="field"></param>
    /// <param name="aggregate"></param>
    /// <returns></returns>
    public static IAggregateField Aggregate(this ICompareField field, string aggregate)
        => field.AggregateTo(aggregate);
    #endregion
    #region IGroupByView
    /// <summary>
    /// 计数
    /// </summary>
    /// <param name="view"></param>
    /// <returns></returns>
    public static CountFieldInfo Count(this IGroupByView view)
        => CountFieldInfo.Instance;
    /// <summary>
    /// 计数聚合
    /// </summary>
    /// <param name="view"></param>
    /// <param name="field"></param>
    /// <returns></returns>
    public static IAggregateField DistinctCount(this IGroupByView view, string field)
        => new DistinctCountFieldInfo(view.Source.GetCompareField(field));
    /// <summary>
    /// 最大值聚合
    /// </summary>
    /// <param name="view"></param>
    /// <param name="field"></param>
    /// <returns></returns>
    public static IAggregateField Sum(this IGroupByView view, string field)
        => view.Source.GetCompareField(field).AggregateTo(AggregateConstants.Sum);
    /// <summary>
    /// 均值聚合
    /// </summary>
    /// <param name="view"></param>
    /// <param name="field"></param>
    /// <returns></returns>
    public static IAggregateField Avg(this IGroupByView view, string field)
        => view.Source.GetCompareField(field).AggregateTo(AggregateConstants.Avg);
    /// <summary>
    /// 最大值聚合
    /// </summary>
    /// <param name="view"></param>
    /// <param name="field"></param>
    /// <returns></returns>
    public static IAggregateField Max(this IGroupByView view, string field)
        => view.Source.GetCompareField(field).AggregateTo(AggregateConstants.Max);
    /// <summary>
    /// 最小值聚合
    /// </summary>
    /// <param name="view"></param>
    /// <param name="field"></param>
    /// <returns></returns>
    public static IAggregateField Min(this IGroupByView view, string field)
        => view.Source.GetCompareField(field).AggregateTo(AggregateConstants.Min);
    /// <summary>
    /// 聚合
    /// </summary>
    /// <param name="view"></param>
    /// <param name="aggregate"></param>
    /// <param name="field"></param>
    /// <returns></returns>
    public static IAggregateField Aggregate(this IGroupByView view, string aggregate, string field)
        => view.Source.GetCompareField(field).AggregateTo(aggregate);
    #endregion
    #region IAliasTable<TTable>
    ///// <summary>
    ///// 聚合
    ///// </summary>
    ///// <typeparam name="TTable"></typeparam>
    ///// <param name="alias"></param>
    ///// <param name="select"></param>
    ///// <param name="aggregate"></param>
    ///// <returns></returns>
    //public static IAggregateField Aggregate<TTable>(this IAliasTable<TTable> alias, Func<TTable, IColumn> select, Func<IColumn, IAggregateField> aggregate)
    //    where TTable : ITable
    //    => aggregate(alias.Prefix(select(alias.Target)));
    #endregion
    #region IMultiView
    ///// <summary>
    ///// 聚合
    ///// </summary>
    ///// <typeparam name="TAliasTable"></typeparam>
    ///// <param name="multiTable"></param>
    ///// <param name="tableName"></param>
    ///// <param name="aggregate"></param>
    ///// <returns></returns>
    //public static IAggregateField Aggregate<TAliasTable>(this IMultiView multiTable, string tableName, Func<TAliasTable, IAggregateField> aggregate)
    //    where TAliasTable : IAliasTable
    //    => aggregate(multiTable.From<TAliasTable>(tableName));
    ///// <summary>
    ///// 聚合
    ///// </summary>
    ///// <param name="multiTable"></param>
    ///// <param name="tableName"></param>
    ///// <param name="aggregate"></param>
    ///// <returns></returns>
    //public static IAggregateField Aggregate(this IMultiView multiTable, string tableName, Func<IAliasTable, IAggregateField> aggregate)
    //    => aggregate(multiTable.From(tableName));
    ///// <summary>
    ///// 聚合
    ///// </summary>
    ///// <typeparam name="TTable"></typeparam>
    ///// <param name="multiTable"></param>
    ///// <param name="tableName"></param>
    ///// <param name="select"></param>
    ///// <param name="aggregate"></param>
    ///// <returns></returns>
    //public static IAggregateField Aggregate<TTable>(this IMultiView multiTable, string tableName, Func<TTable, IColumn> select, Func<IColumn, IAggregateField> aggregate)
    //    where TTable : ITable
    //{
    //    var member = multiTable.Alias<TTable>(tableName);
    //    return aggregate(member.Prefix(select(member.Target)));
    //}
    #endregion
}

using ShadowSql.Aggregates;
using ShadowSql.Identifiers;
using ShadowSql.Services;

namespace ShadowSql.FieldInfos;

/// <summary>
/// 列字段信息
/// </summary>
public sealed class FieldInfo : FieldInfoBase, IField, ICompareField
{
    private FieldInfo(string name)
        : base(name)
    {
        _count = new DistinctCountFieldInfo(this);
        _sum = FieldAggregateTo(AggregateConstants.Sum);
        _avg = FieldAggregateTo(AggregateConstants.Avg);
        _max = FieldAggregateTo(AggregateConstants.Max);
        _min = FieldAggregateTo(AggregateConstants.Min);
    }
    #region 聚合
    /// <summary>
    /// 聚合
    /// </summary>
    /// <param name="aggregate"></param>
    /// <returns></returns>
    public AggregateFieldInfo FieldAggregateTo(string aggregate)
        => new(this, aggregate);
    /// <summary>
    /// 聚合
    /// </summary>
    /// <param name="aggregate"></param>
    /// <returns></returns>
    public override IAggregateField AggregateTo(string aggregate)
    {
        return aggregate switch
        {
            "Count" or "count" or "COUNT" => Count(),
            "Sum" or "sum" or "SUM" => Sum(),
            "Avg" or "avg" or "AVG" => Avg(),
            "Max" or "max" or "MAX" => Max(),
            "Min" or "min" or "MIN" => Min(),
            _ => FieldAggregateTo(aggregate),
        };
    }
    /// <summary>
    /// 去重计数聚合
    /// </summary>
    /// <returns></returns>
    public DistinctCountFieldInfo Count()
        => _count;
    /// <summary>
    /// 最大值聚合
    /// </summary>
    /// <returns></returns>
    public AggregateFieldInfo Sum()
        => _sum;
    /// <summary>
    /// 均值聚合
    /// </summary>
    /// <returns></returns>
    public AggregateFieldInfo Avg()
        => _avg;
    /// <summary>
    /// 最大值聚合
    /// </summary>
    /// <returns></returns>
    public AggregateFieldInfo Max()
        => _max;
    /// <summary>
    /// 最小值聚合
    /// </summary>
    /// <returns></returns>
    public AggregateFieldInfo Min()
        => _min;

    private DistinctCountFieldInfo _count;
    private AggregateFieldInfo _sum;
    private AggregateFieldInfo _avg;
    private AggregateFieldInfo _max;
    private AggregateFieldInfo _min;
    #endregion
    /// <summary>
    /// 获取列字段信息(已缓存,避免重复构造)
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static FieldInfo Use(string name)
        => _cacher.Get(name);
    /// <summary>
    /// 缓存
    /// </summary>
    private static readonly CacheService<FieldInfo> _cacher = new(static name => new FieldInfo(name));

    string IView.ViewName
        => _name;
    IColumn IFieldView.ToColumn()
        => Column.Use(_name);
}

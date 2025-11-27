using ShadowSql.Identifiers;
using ShadowSql.Variants;

namespace ShadowSql.Aggregates;

/// <summary>
/// 去重统计字段信息
/// </summary>
public abstract class DistinctCountFieldInfoBase(ICompareField original)
    : VariantFieldInfoBase<ICompareField>(original), IAggregate
{
    string IAggregate.Aggregate
        => AggregateConstants.Count;
}

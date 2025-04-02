﻿using ShadowSql.FieldInfos;
using ShadowSql.Identifiers;

namespace ShadowSql.Aggregates;

/// <summary>
/// 去重统计字段信息
/// </summary>
public abstract class DistinctCountFieldInfoBase(IFieldView target)
    : VariantFieldInfoBase(target), IAggregate
{
    string IAggregate.Aggregate
        => AggregateConstants.Count;
}

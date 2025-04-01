using ShadowSql.Aggregates;
using ShadowSql.Identifiers;

namespace ShadowSql.Variants;

/// <summary>
/// 聚合(计算)列
/// </summary>
/// <typeparam name="TColumn"></typeparam>
/// <param name="aggregate"></param>
/// <param name="target"></param>
public class AggregateColumn<TColumn>(string aggregate, TColumn target)
    : AggregateColumnBase<TColumn>(aggregate, target), IAggregateField
    where TColumn : IColumn
{
    string IAggregateField.TargetName
        => _target.ViewName;
}

using ShadowSql.FieldInfos;

namespace ShadowSql.Aggregates;

/// <summary>
/// 聚合字段信息
/// </summary>
/// <param name="aggregate"></param>
/// <param name="columnName"></param>
public abstract class AggregateFieldInfoBase(string aggregate, string columnName)
    : FieldInfoBase(columnName)
{
    /// <summary>
    /// 聚合函数
    /// </summary>
    protected readonly string _aggregate = aggregate;
    /// <summary>
    /// 聚合函数
    /// </summary>
    public string Aggregate
        => _aggregate;
}

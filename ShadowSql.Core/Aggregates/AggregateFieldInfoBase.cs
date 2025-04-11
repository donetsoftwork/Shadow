using ShadowSql.FieldInfos;
using ShadowSql.Identifiers;

namespace ShadowSql.Aggregates;

/// <summary>
/// 聚合字段信息
/// </summary>
/// <param name="aggregate"></param>
/// <param name="target"></param>
public abstract class AggregateFieldInfoBase(string aggregate, IFieldView target)
    : VariantFieldInfoBase(target)
{
    #region 配置
    /// <summary>
    /// 聚合函数
    /// </summary>
    protected readonly string _aggregate = aggregate;
    /// <summary>
    /// 聚合函数
    /// </summary>
    public string Aggregate
        => _aggregate;
    #endregion

}

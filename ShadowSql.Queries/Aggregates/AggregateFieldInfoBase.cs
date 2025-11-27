using ShadowSql.Identifiers;
using ShadowSql.Variants;

namespace ShadowSql.Aggregates;

/// <summary>
/// 聚合字段信息
/// </summary>
/// <param name="aggregate">聚合</param>
/// <param name="original">原始字段</param>
public abstract class AggregateFieldInfoBase(string aggregate, ICompareField original)
    : VariantFieldInfoBase<ICompareField>(original)
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

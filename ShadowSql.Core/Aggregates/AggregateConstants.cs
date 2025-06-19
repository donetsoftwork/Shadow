using ShadowSql.Identifiers;

namespace ShadowSql.Aggregates;

/// <summary>
/// 聚合常量
/// </summary>
public static class AggregateConstants
{
    /// <summary>
    /// 计数
    /// </summary>
    public const string Count = "COUNT";
    /// <summary>
    /// 求和
    /// </summary>
    public const string Sum = "SUM";
    /// <summary>
    /// 均值
    /// </summary>
    public const string Avg = "AVG";
    /// <summary>
    /// 最大值
    /// </summary>
    public const string Max = "MAX";
    /// <summary>
    /// 最小值
    /// </summary>
    public const string Min = "MIN";

    /// <summary>
    /// 是否匹配Count聚合
    /// </summary>
    /// <param name="aggregate">聚合</param>
    /// <returns></returns>
    public static bool MatchCount(string aggregate)
        => Identifier.Match(Count, aggregate);
}

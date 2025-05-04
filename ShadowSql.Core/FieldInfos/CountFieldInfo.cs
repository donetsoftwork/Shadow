using ShadowSql.Aggregates;
using ShadowSql.Engines;
using ShadowSql.Fragments;
using System.Text;

namespace ShadowSql.FieldInfos;

/// <summary>
/// 计数字段信息
/// </summary>
public sealed class CountFieldInfo : IAggregateField
{
    /// <summary>
    /// 计数字段信息
    /// </summary>
    private CountFieldInfo()
    {
    }
    /// <summary>
    /// 单例
    /// </summary>
    public readonly static CountFieldInfo Instance = new();

    string IAggregateField.TargetName
        => "*";
    string IAggregate.Aggregate 
        => AggregateConstants.Count;

    void ISqlEntity.Write(ISqlEngine engine, StringBuilder sql)
         => engine.Count(sql);
}

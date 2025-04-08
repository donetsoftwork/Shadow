using ShadowSql.Engines;
using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using System.Text;

namespace ShadowSql.FieldInfos;

/// <summary>
/// 计数字段信息
/// </summary>
public sealed class CountFieldInfo : ICompareView
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
    void ISqlEntity.Write(ISqlEngine engine, StringBuilder sql)
         => engine.Count(sql);
}

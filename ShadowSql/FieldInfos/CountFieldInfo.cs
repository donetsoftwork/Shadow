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
    private CountFieldInfo()
    {
    }
    /// <summary>
    /// 单例
    /// </summary>
    public readonly static CountFieldInfo Instance = new();
    /// <summary>
    /// Count表达式
    /// </summary>
    public const string Count = "COUNT(*)";
    void ISqlEntity.Write(ISqlEngine engine, StringBuilder sql)
    {
        sql.Append(Count);
    }

}

using ShadowSql.Services;

namespace ShadowSql.FieldInfos;

/// <summary>
/// 计数字段别名信息
/// </summary>
public sealed class CountAliasFieldInfo : AliasFieldInfo
{
    private CountAliasFieldInfo(string alias)
        : base(CountFieldInfo.Count, alias)
    {
    }
    /// <summary>
    /// 获取计数字段信息(已缓存,避免重复构造)
    /// </summary>
    /// <param name="alias"></param>
    /// <returns></returns>
    public static CountAliasFieldInfo Use(string alias = "Count")
        => _cacher.Get(alias);
    /// <summary>
    /// 缓存
    /// </summary>
    private static readonly CacheService<CountAliasFieldInfo> _cacher = new(alias => new CountAliasFieldInfo(alias));
}

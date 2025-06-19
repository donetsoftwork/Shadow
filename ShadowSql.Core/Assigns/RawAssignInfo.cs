using ShadowSql.Engines;
using ShadowSql.Fragments;
using ShadowSql.Services;
using System.Text;

namespace ShadowSql.Assigns;

/// <summary>
/// 原始赋值信息
/// </summary>
/// <param name="assignInfo"></param>
public class RawAssignInfo(string assignInfo) : IAssignInfo
{
    private readonly string _assignInfo = assignInfo;
    /// <summary>
    /// 赋值信息
    /// </summary>
    public string AssignInfo 
        => _assignInfo;
    /// <inheritdoc/>
    void ISqlEntity.Write(ISqlEngine engine, StringBuilder sql)
        => sql.Append(_assignInfo);
    /// <summary>
    /// 获取列字段信息(已缓存,避免重复构造)
    /// </summary>
    /// <param name="assignInfo"></param>
    /// <returns></returns>
    public static RawAssignInfo Use(string assignInfo)
        => _cacher.Get(assignInfo);
    /// <summary>
    /// 缓存
    /// </summary>
    private static readonly CacheService<RawAssignInfo> _cacher = new(static assignInfo => new RawAssignInfo(assignInfo));
}

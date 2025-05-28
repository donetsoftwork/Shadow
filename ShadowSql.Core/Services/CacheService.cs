using System;
using System.Collections.Generic;
#if NET9_0_OR_GREATER
using System.Threading;
#endif

namespace ShadowSql.Services;

/// <summary>
/// 缓存服务
/// </summary>
public class CacheService<Identifier>(Func<string, Identifier> factory)
{
    /// <summary>
    /// 缓存字典
    /// </summary>
    private Dictionary<string, Identifier> _cacher = [];
    #if NET9_0_OR_GREATER
    private readonly Lock _cacherLock = new();
    #endif
    private readonly Func<string, Identifier> _factory = factory;
    /// <summary>
    /// 工厂
    /// </summary>
    public Func<string, Identifier> Factory 
        => _factory;
    /// <summary>
    /// 缓存键
    /// </summary>
    public IEnumerable<string> Names
        => _cacher.Keys;

    /// <summary>
    /// 获取
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Identifier Get(string name)
    {
        if (_cacher.TryGetValue(name, out var identifier))
            return identifier;
#if NET9_0_OR_GREATER
        lock (_cacherLock)
#else
        lock (_cacher)
#endif
        {
            if (_cacher.TryGetValue(name, out identifier))
                return identifier;
            identifier = _factory(name);
            _cacher[name] = identifier;
        }
        return identifier;
    }
    /// <summary>
    /// 覆盖
    /// </summary>
    /// <param name="name"></param>
    /// <param name="identifier"></param>
    public void Set(string name, Identifier identifier)
        => _cacher[name] = identifier;
    /// <summary>
    /// 移除
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool Remove(string name)
        => _cacher.Remove(name);
    /// <summary>
    /// 清空
    /// </summary>
    public void Clear()
        => _cacher.Clear();
}

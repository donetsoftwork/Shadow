using ShadowSql.Components;
using System.Collections;

namespace Shadow.DDL.Components;

/// <summary>
/// 字典组件(插件)工厂
/// </summary>
/// <param name="provider"></param>
public class DictionaryProvider(IDictionary provider)
    : IPluginProvider
{
    #region 配置
    private readonly IDictionary _provider = provider;
    /// <summary>
    /// 字典提供者
    /// </summary>
    public IDictionary Provider 
        => _provider;
    #endregion

    /// <summary>
    /// 获取组件
    /// </summary>
    /// <typeparam name="TComponent"></typeparam>
    /// <returns></returns>
    public TComponent? GetPlugin<TComponent>()
        where TComponent : class
    {
        if (_provider[typeof(TComponent)] is TComponent component)
            return component;
        return null;
    }
    /// <summary>
    /// 添加组件
    /// </summary>
    /// <typeparam name="TComponent"></typeparam>
    /// <param name="component"></param>
    public DictionaryProvider AddComponent<TComponent>(TComponent component)
        where TComponent : class
    {
        _provider[typeof(TComponent)] = component;
        return this;
    }
}

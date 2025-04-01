namespace ShadowSql.Components;

/// <summary>
/// 插件工厂
/// </summary>
public interface IPluginProvider
{
    /// <summary>
    /// 获取组件(插件)
    /// </summary>
    /// <typeparam name="TPlugin"></typeparam>
    /// <returns></returns>
    TPlugin? GetPlugin<TPlugin>()
        where TPlugin : class;
}

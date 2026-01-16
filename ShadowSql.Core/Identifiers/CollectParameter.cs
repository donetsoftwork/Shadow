using Hand.Cache;
using ShadowSql.Engines;
using ShadowSql.Values;
using System.Text;

namespace ShadowSql.Identifiers;

/// <summary>
/// sql参数
/// </summary>
public class CollectParameter : IdentifierBase, IParameter, IView
{
    /// <summary>
    /// sql参数
    /// </summary>
    /// <param name="name"></param>
    protected CollectParameter(string name)
        : base(name)
    {
    }
    /// <inheritdoc/>
    string IView.ViewName
        => _name;
    /// <summary>
    /// 获取参数(已缓存,避免重复构造)
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static CollectParameter Use(string name)
        => _cacher.Get(name);
    /// <summary>
    /// 获取参数
    /// </summary>
    /// <param name="name"></param>
    /// <param name="identifier"></param>
    /// <returns></returns>
    public static CollectParameter Use(string name, IView identifier)
         => _cacher.Get(CheckName(name, identifier));
    /// <summary>
    /// 获取参数
    /// </summary>
    /// <param name="name"></param>
    /// <param name="identifier"></param>
    /// <returns></returns>
    public static CollectParameter Use(string name, string identifier)
        => _cacher.Get(CheckName(name, identifier));
    /// <summary>
    /// 检查参数名
    /// </summary>
    /// <param name="name"></param>
    /// <param name="column">列</param>
    /// <returns></returns>
    public static string CheckName(string name, string column)
    {
        if (string.IsNullOrEmpty(name))
            return column;
        return name;
    }
    /// <summary>
    /// 检查参数名
    /// </summary>
    /// <param name="name"></param>
    /// <param name="identifier"></param>
    /// <returns></returns>
    public static string CheckName(string name, IView identifier)
    {
        if (string.IsNullOrEmpty(name))
            return identifier.ViewName;
        return name;
    }
    /// <inheritdoc/>
    internal override void Write(ISqlEngine engine, StringBuilder sql)
        => engine.CollectParameter(sql, _name);
    private static readonly FuncCacheService<string, CollectParameter> _cacher = new(static name => new CollectParameter(name));
}

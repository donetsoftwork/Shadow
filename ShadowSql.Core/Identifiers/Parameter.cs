using ShadowSql.Engines;
using ShadowSql.Services;
using ShadowSql.SqlVales;
using System.Text;

namespace ShadowSql.Identifiers;

/// <summary>
/// sql参数
/// </summary>
public class Parameter : IdentifierBase, IParameter, IView
{
    /// <summary>
    /// sql参数
    /// </summary>
    /// <param name="name"></param>
    protected Parameter(string name)
        : base(name)
    {
    }
    
    /// <summary>
    /// 获取参数(已缓存,避免重复构造)
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static Parameter Use(string name)
        => _cacher.Get(name);
    /// <summary>
    /// 获取参数
    /// </summary>
    /// <param name="name"></param>
    /// <param name="identifier"></param>
    /// <returns></returns>
    public static Parameter Use(string name, IView identifier)
         => _cacher.Get(CheckName(name, identifier));
    /// <summary>
    /// 获取参数
    /// </summary>
    /// <param name="name"></param>
    /// <param name="identifier"></param>
    /// <returns></returns>
    public static Parameter Use(string name, string identifier)
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
    /// <summary>
    /// 检查参数名
    /// </summary>
    /// <param name="name"></param>
    /// <param name="parameter">参数</param>
    /// <returns></returns>
    public static string CheckName2(string name, string parameter)
    {
        if (string.IsNullOrEmpty(name))
            return string.Concat(parameter, "2");
        return name;
    }
    /// <summary>
    /// 检查参数名
    /// </summary>
    /// <param name="name"></param>
    /// <param name="parameter">参数</param>
    /// <returns></returns>
    public static string CheckName2(string name, IView parameter)
    {
        if (string.IsNullOrEmpty(name))
            return string.Concat(parameter.ViewName, "2");
        return name;
    }
    /// <inheritdoc/>
    internal override void Write(ISqlEngine engine, StringBuilder sql)
        => engine.Parameter(sql, _name);
    private static readonly CacheService<Parameter> _cacher = new(static name => new Parameter(name));
    /// <inheritdoc/>
    string IView.ViewName
        => _name;
}

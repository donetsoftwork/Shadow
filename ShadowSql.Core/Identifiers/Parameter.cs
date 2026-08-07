using Hand.Cache;
using ShadowSql.Engines;
using ShadowSql.Values;
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
    /// <inheritdoc/>
    string IMember.MemberName
        => _name;
    /// <inheritdoc/>
    string IView.ViewName
        => _name;
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
    /// <param name="member"></param>
    /// <returns></returns>
    public static Parameter Use(string name, IMember member)
         => _cacher.Get(CheckName(name, member));
    /// <summary>
    /// 获取参数
    /// </summary>
    /// <param name="name"></param>
    /// <param name="memberName"></param>
    /// <returns></returns>
    public static Parameter Use(string name, string memberName)
        => _cacher.Get(CheckName(name, memberName));
    /// <summary>
    /// 检查参数名
    /// </summary>
    /// <param name="name"></param>
    /// <param name="memberName">成员</param>
    /// <returns></returns>
    public static string CheckName(string name, string memberName)
    {
        if (string.IsNullOrEmpty(name))
            return memberName;
        return name;
    }
    /// <summary>
    /// 检查参数名
    /// </summary>
    /// <param name="name"></param>
    /// <param name="member"></param>
    /// <returns></returns>
    public static string CheckName(string name, IMember member)
    {
        if (string.IsNullOrEmpty(name))
            return member.MemberName;
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
    ///// <summary>
    ///// 检查参数名
    ///// </summary>
    ///// <param name="name"></param>
    ///// <param name="parameter">参数</param>
    ///// <returns></returns>
    //public static string CheckName2(string name, IView parameter)
    //{
    //    if (string.IsNullOrEmpty(name))
    //        return string.Concat(parameter.ViewName, "2");
    //    return name;
    //}
    /// <inheritdoc/>
    internal override void Write(ISqlEngine engine, StringBuilder sql)
        => engine.Parameter(sql, _name);
    private static readonly FuncCacheService<string, Parameter> _cacher = new(static name => new Parameter(name));
}

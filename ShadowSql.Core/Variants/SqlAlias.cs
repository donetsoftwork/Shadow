using ShadowSql.Fragments;
using ShadowSql.Identifiers;

namespace ShadowSql.Variants;

/// <summary>
/// sql别名
/// </summary>
/// <typeparam name="TFragment"></typeparam>
/// <param name="target"></param>
/// <param name="aliasName">别名</param>
public abstract class SqlAlias<TFragment>(TFragment target, string aliasName)
    : IdentifierBase(aliasName), IView
    where TFragment : ISqlEntity
{  
    /// <summary>
    /// 被包裹对象
    /// </summary>
    internal readonly TFragment _target = target;
    /// <summary>
    /// 被包裹对象
    /// </summary>
    public TFragment Target
        => _target;
    /// <summary>
    /// 别名
    /// </summary>
    public string Alias
        => _name;
    /// <inheritdoc/>

    string IView.ViewName
        => _name;

    ///// <summary>
    ///// 是否匹配
    ///// </summary>
    ///// <param name="name"></param>
    ///// <returns></returns>
    //public override bool IsMatch(string name)
    //{
    //    if(base.IsMatch(name))
    //        return true;
    //    if(_target is IIdentifier identifier)
    //        return identifier.IsMatch(name);
    //    return false;
    //}
}

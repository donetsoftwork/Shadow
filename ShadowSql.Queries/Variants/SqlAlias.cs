using Hand.Structural;
using ShadowSql.Fragments;
using ShadowSql.Identifiers;

namespace ShadowSql.Variants;

/// <summary>
/// sql别名
/// </summary>
/// <typeparam name="TFragment"></typeparam>
/// <param name="original">原始对象</param>
/// <param name="aliasName">别名</param>
public abstract class SqlAlias<TFragment>(TFragment original, string aliasName)
    : IdentifierBase(aliasName), IWrapper<TFragment>, IView
    where TFragment : ISqlEntity
{  
    /// <summary>
    /// 原始对象
    /// </summary>
    internal readonly TFragment _original = original;
    /// <summary>
    /// 原始对象
    /// </summary>
    public TFragment Original
        => _original;
    /// <summary>
    /// 别名
    /// </summary>
    public string Alias
        => _name;
    /// <inheritdoc/>

    string IView.ViewName
        => _name;
}

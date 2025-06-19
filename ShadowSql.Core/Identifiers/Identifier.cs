namespace ShadowSql.Identifiers;

/// <summary>
/// sql标识符
/// </summary>
/// <param name="name">标识名</param>
public class Identifier(string name)
    : IdentifierBase(name), IIdentifier
{
    /// <summary>
    /// 匹配标识符
    /// </summary>
    /// <param name="name"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static bool Match(string name, string other)
        => string.Equals(name, other, System.StringComparison.OrdinalIgnoreCase);
    /// <summary>
    /// 别名匹配
    /// </summary>
    /// <param name="name"></param>
    /// <param name="aliasName">别名</param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static bool AliasMatch(string name, string aliasName, string other)
        => Match(aliasName, other)
        || Match(name, other);
}

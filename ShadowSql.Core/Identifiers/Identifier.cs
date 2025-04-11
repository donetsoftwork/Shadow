namespace ShadowSql.Identifiers;

/// <summary>
/// sql标识符
/// </summary>
/// <param name="name"></param>
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
    /// <param name="alias"></param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static bool AliasMatch(string name, string alias, string other)
        => Match(alias, other)
        || Match(name, other);
}

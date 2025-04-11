namespace ShadowSql.Identifiers;

/// <summary>
/// 匹配
/// </summary>
public interface IMatch
{
    /// <summary>
    /// 是否匹配
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    bool IsMatch(string name);
}

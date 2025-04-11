namespace ShadowSql.Generators;

/// <summary>
/// 标识生成器
/// </summary>
public interface IIdentifierGenerator
{
    /// <summary>
    /// 生成新标识
    /// </summary>
    /// <returns></returns>
    string NewName();
}

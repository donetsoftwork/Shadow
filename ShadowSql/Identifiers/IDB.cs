namespace ShadowSql.Identifiers;

/// <summary>
/// 库标识
/// </summary>
public interface IDB : IIdentifier
{
    /// <summary>
    /// 获取表
    /// </summary>
    /// <param name="tableName"></param>
    /// <returns></returns>
    ITable From(string tableName);
}

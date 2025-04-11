using ShadowSql.Fragments;

namespace ShadowSql.Identifiers;

/// <summary>
/// 标识符接口
/// </summary>
public interface IIdentifier : IMatch, ISqlEntity
{
    /// <summary>
    /// 标识名
    /// </summary>
    string Name { get; }
}
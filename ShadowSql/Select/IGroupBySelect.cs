using ShadowSql.Identifiers;

namespace ShadowSql.Select;

/// <summary>
/// 获取分组查询数据
/// </summary>

public interface IGroupBySelect : ISelect
{
    /// <summary>
    /// 分组
    /// </summary>
    IGroupByView Target { get; }
}

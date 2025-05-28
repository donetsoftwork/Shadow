using ShadowSql.Identifiers;
using ShadowSql.Select;

namespace ShadowSql.Expressions.Select;

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

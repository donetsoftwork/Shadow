using ShadowSql.Identifiers;
using ShadowSql.Select;

namespace ShadowSql.SingleSelect;

/// <summary>
/// 筛选单列
/// </summary>
public interface ISingleSelect : ISelect
{
    /// <summary>
    /// 单列
    /// </summary>
    IFieldView SingleField { get; }
}

using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using ShadowSql.Select;

namespace ShadowSql.SingleSelect;

/// <summary>
/// 筛选单列
/// </summary>
public interface ISingleSelect : ISelect
{
    ///// <summary>
    ///// 数据源表(视图)
    ///// </summary>
    //ITableView Source { get; }
    /// <summary>
    /// 单列
    /// </summary>
    IFieldView SingleField { get; }
}

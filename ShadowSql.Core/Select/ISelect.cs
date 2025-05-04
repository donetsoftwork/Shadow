using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using ShadowSql.SelectFields;

namespace ShadowSql.Select;

/// <summary>
/// 获取数据
/// </summary>
public interface ISelect : ISelectFields, ISqlEntity
{
    /// <summary>
    /// 数据源表(视图)
    /// </summary>
    ITableView Source { get; }
}

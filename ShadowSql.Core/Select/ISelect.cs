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
    /// 原始视图
    /// </summary>
    ITableView Original { get; }
}

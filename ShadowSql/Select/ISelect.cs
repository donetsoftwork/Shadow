using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using ShadowSql.SelectFields;

namespace ShadowSql.Select;

/// <summary>
/// 筛选列
/// </summary>
public interface ISelect : ISelectFields, ISqlEntity
{
    /// <summary>
    /// 数据源表(视图)
    /// </summary>
    ITableView Source { get; }
    ///// <summary>
    ///// 被筛选列
    ///// </summary>
    //IEnumerable<IFieldView> Fields { get; }
}

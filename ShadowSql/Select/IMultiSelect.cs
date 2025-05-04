using ShadowSql.Identifiers;
using System.Collections.Generic;

namespace ShadowSql.Select;

/// <summary>
/// 多表筛选
/// </summary>
public interface IMultiSelect : ISelect
{
    /// <summary>
    /// 多表
    /// </summary>
    IMultiView Target { get; }
    /// <summary>
    /// 选择表
    /// </summary>
    ICollection<IAliasTable> SelectTables {  get; }
}

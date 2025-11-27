using ShadowSql.Filters;
using ShadowSql.Logics;

namespace ShadowSql.Queries;

/// <summary>
/// 逻辑过滤
/// </summary>
public interface IDataQuery : IDataFilter
{
    /// <summary>
    /// 过滤逻辑
    /// </summary>
    Logic Logic { get; set; }
}

using ShadowSql.Filters;
using ShadowSql.Logics;
using System;

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
    ///// <summary>
    ///// 应用查询
    ///// </summary>
    ///// <param name="filter"></param>
    //void ApplyFilter(Func<Logic, Logic> filter);
}

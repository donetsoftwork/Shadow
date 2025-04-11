using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using ShadowSql.Logics;

namespace ShadowSql.Filters;

/// <summary>
/// 数据过滤
/// </summary>
public interface IDataFilter : ITableView
{
    /// <summary>
    /// 数据源表
    /// </summary>
    ITableView Source { get; }
    /// <summary>
    /// 过滤逻辑
    /// </summary>
    ISqlLogic Filter { get; }
}
/// <summary>
/// Where查询
/// </summary>
public interface IWhere : ISqlEntity;
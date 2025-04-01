using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using ShadowSql.Logics;

namespace ShadowSql.Delete;

/// <summary>
/// 删除数据
/// </summary>
public interface IDelete : IExecuteSql
{
    /// <summary>
    /// 源表
    /// </summary>
    ITableView Source { get; }
    /// <summary>
    /// 过滤条件
    /// </summary>
    ISqlLogic Filter { get; }
}

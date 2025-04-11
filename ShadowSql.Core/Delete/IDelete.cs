using ShadowSql.Fragments;
using ShadowSql.Identifiers;

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
}

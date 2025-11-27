using System.Collections.Generic;

namespace ShadowSql.Identifiers;

/// <summary>
/// 多表视图
/// </summary>
public interface IMultiView : ITableView
{
    /// <summary>
    /// 表
    /// </summary>
    IEnumerable<IAliasTable> Tables { get; }
    /// <summary>
    /// 获取成员表
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <returns></returns>
    IAliasTable? GetMember(string tableName);
}

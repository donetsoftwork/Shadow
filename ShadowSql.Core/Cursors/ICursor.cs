using ShadowSql.Identifiers;

namespace ShadowSql.Cursors;

/// <summary>
/// 范围筛选游标
/// </summary>
public interface ICursor : ITableView
{
    /// <summary>
    /// 获取数量
    /// </summary>
    int Limit { get; }
    /// <summary>
    /// 跳过数量
    /// </summary>
    int Offset { get; }
    /// <summary>
    /// 获取数量
    /// </summary>
    /// <param name="limit"></param>
    /// <returns></returns>
    ICursor Take(int limit);
    /// <summary>
    /// 跳过数量
    /// </summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    ICursor Skip(int offset);
}

using ShadowSql.Identifiers;
using ShadowSql.Select;

namespace ShadowSql.Fetches;

/// <summary>
/// 范围筛选
/// </summary>
public interface IFetch : ITableView, ICursor
{
    ///// <summary>
    ///// 获取数量
    ///// </summary>
    //int Limit { get; }
    ///// <summary>
    ///// 跳过数量
    ///// </summary>
    //int Offset { get; }
    ///// <summary>
    ///// 跳过数量
    ///// </summary>
    ///// <param name="offset"></param>
    ///// <returns></returns>
    //void SkipCore(int offset);
    ///// <summary>
    ///// 获取数量
    ///// </summary>
    ///// <param name="limit"></param>
    ///// <returns></returns>
    //void TakeCore(int limit);
}

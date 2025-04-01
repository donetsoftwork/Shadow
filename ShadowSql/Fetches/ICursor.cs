namespace ShadowSql.Fetches;

/// <summary>
/// 游标
/// </summary>
public interface ICursor
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
    /// 跳过数量
    /// </summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    ICursor Skip(int offset);
}

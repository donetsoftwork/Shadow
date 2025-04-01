using ShadowSql.Fetches;
using ShadowSql.Identifiers;
using System.Collections.Generic;

namespace ShadowSql;

/// <summary>
/// 范围筛选扩展方法
/// </summary>
public static partial class ShadowSqlServices
{
    /// <summary>
    /// 跳过数量
    /// </summary>
    /// <typeparam name="TFetch"></typeparam>
    /// <param name="fetch"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static TFetch Skip<TFetch>(this TFetch fetch, int offset)
        where TFetch : FetchBase
    {
        fetch.SkipCore(offset);
        return fetch;
    }
    /// <summary>
    /// 获取数量
    /// </summary>
    /// <typeparam name="TFetch"></typeparam>
    /// <param name="fetch"></param>
    /// <param name="limit"></param>
    /// <returns></returns>
    public static TFetch Take<TFetch>(this TFetch fetch, int limit)
        where TFetch : FetchBase
    {
        fetch.TakeCore(limit);
        return fetch;
    }
    #region 排序    
    /// <summary>
    /// 正序
    /// </summary>
    /// <typeparam name="TFetch"></typeparam>
    /// <param name="fetch"></param>
    /// <param name="field"></param>
    public static TFetch Asc<TFetch>(this TFetch fetch, IOrderView field)
        where TFetch : FetchBase
    {
        fetch.AscCore(field);
        return fetch;
    }
    /// <summary>
    /// 正序
    /// </summary>
    /// <typeparam name="TFetch"></typeparam>
    /// <param name="fetch"></param>
    /// <param name="fieldNames"></param>
    /// <returns></returns>
    public static TFetch Asc<TFetch>(this TFetch fetch, params IEnumerable<string> fieldNames)
        where TFetch : FetchBase
    {
        fetch.AscCore(fieldNames);
        return fetch;
    }
    /// <summary>
    /// 倒序
    /// </summary>
    /// <typeparam name="TFetch"></typeparam>
    /// <param name="fetch"></param>
    /// <param name="field"></param>
    public static TFetch Desc<TFetch>(this TFetch fetch, IOrderAsc field)
        where TFetch : FetchBase
    {
        fetch.DescCore(field);
        return fetch;
    }
    /// <summary>
    /// 倒序
    /// </summary>
    /// <typeparam name="TFetch"></typeparam>
    /// <param name="fetch"></param>
    /// <param name="fieldNames"></param>
    /// <returns></returns>
    public static TFetch Desc<TFetch>(this TFetch fetch, params IEnumerable<string> fieldNames)
        where TFetch : FetchBase
    {
        fetch.DescCore(fieldNames);
        return fetch;
    }
    /// <summary>
    /// 添加排序
    /// </summary>
    /// <typeparam name="TFetch"></typeparam>
    /// <param name="fetch"></param>
    /// <param name="orderBy"></param>
    /// <returns></returns>
    public static TFetch OrderBy<TFetch>(this TFetch fetch, string orderBy)
        where TFetch : FetchBase
    {
        fetch.OrderByCore(orderBy);
        return fetch;
    }
    #endregion
}

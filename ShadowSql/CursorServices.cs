using ShadowSql.Cursors;
using ShadowSql.Identifiers;
using System.Collections.Generic;

namespace ShadowSql;

/// <summary>
/// 范围筛选游标扩展方法
/// </summary>
public static partial class ShadowSqlServices
{
    /// <summary>
    /// 游标跳过数量
    /// </summary>
    /// <typeparam name="TCursor"></typeparam>
    /// <param name="cursor"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static TCursor Skip<TCursor>(this TCursor cursor, int offset)
        where TCursor : CursorBase
    {
        cursor.SkipCore(offset);
        return cursor;
    }
    /// <summary>
    /// 游标获取数量
    /// </summary>
    /// <typeparam name="TCursor"></typeparam>
    /// <param name="cursor"></param>
    /// <param name="limit"></param>
    /// <returns></returns>
    public static TCursor Take<TCursor>(this TCursor cursor, int limit)
        where TCursor : CursorBase
    {
        cursor.TakeCore(limit);
        return cursor;
    }
    #region 排序    
    /// <summary>
    /// 正序
    /// </summary>
    /// <typeparam name="TCursor"></typeparam>
    /// <param name="cursor"></param>
    /// <param name="field"></param>
    public static TCursor Asc<TCursor>(this TCursor cursor, IOrderView field)
        where TCursor : CursorBase
    {
        cursor.AscCore(field);
        return cursor;
    }
    /// <summary>
    /// 正序
    /// </summary>
    /// <typeparam name="TCursor"></typeparam>
    /// <param name="cursor"></param>
    /// <param name="fieldNames"></param>
    /// <returns></returns>
    public static TCursor Asc<TCursor>(this TCursor cursor, params IEnumerable<string> fieldNames)
        where TCursor : CursorBase
    {
        cursor.AscCore(fieldNames);
        return cursor;
    }
    /// <summary>
    /// 倒序
    /// </summary>
    /// <typeparam name="TCursor"></typeparam>
    /// <param name="cursor"></param>
    /// <param name="field"></param>
    public static TCursor Desc<TCursor>(this TCursor cursor, IOrderAsc field)
        where TCursor : CursorBase
    {
        cursor.DescCore(field);
        return cursor;
    }
    /// <summary>
    /// 倒序
    /// </summary>
    /// <typeparam name="TCursor"></typeparam>
    /// <param name="cursor"></param>
    /// <param name="fieldNames"></param>
    /// <returns></returns>
    public static TCursor Desc<TCursor>(this TCursor cursor, params IEnumerable<string> fieldNames)
        where TCursor : CursorBase
    {
        cursor.DescCore(fieldNames);
        return cursor;
    }
    /// <summary>
    /// 添加排序
    /// </summary>
    /// <typeparam name="TCursor"></typeparam>
    /// <param name="cursor"></param>
    /// <param name="orderBy"></param>
    /// <returns></returns>
    public static TCursor OrderBy<TCursor>(this TCursor cursor, string orderBy)
        where TCursor : CursorBase
    {
        cursor.OrderByCore(orderBy);
        return cursor;
    }
    #endregion
}

using ShadowSql.Cursors;
using ShadowSql.Identifiers;
using System;
using System.Collections.Generic;

namespace ShadowSql;

/// <summary>
/// 范围筛选游标扩展方法
/// </summary>
public static partial class ShadowSqlCoreServices
{
    /// <summary>
    /// 游标跳过数量
    /// </summary>
    /// <typeparam name="TCursor"></typeparam>
    /// <param name="cursor">游标</param>
    /// <param name="offset">跳过数量</param>
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
    /// <param name="cursor">游标</param>
    /// <param name="limit">筛选数量</param>
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
    /// <param name="cursor">游标</param>
    /// <param name="field">字段</param>
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
    /// <param name="cursor">游标</param>
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
    /// <param name="cursor">游标</param>
    /// <param name="field">字段</param>
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
    /// <param name="cursor">游标</param>
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
    /// <param name="cursor">游标</param>
    /// <param name="orderBy"></param>
    /// <returns></returns>
    public static TCursor OrderBy<TCursor>(this TCursor cursor, string orderBy)
        where TCursor : CursorBase
    {
        cursor.OrderByCore(orderBy);
        return cursor;
    }
    #region 扩展
    /// <summary>
    /// 正序
    /// </summary>
    /// <typeparam name="TCursor"></typeparam>
    /// <param name="cursor">游标</param>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public static TCursor Asc<TCursor>(this TCursor cursor, Func<ITableView, IOrderView> select)
        where TCursor : CursorBase, ICursor
    {
        cursor.AscCore(select(cursor));
        return cursor;
    }
    /// <summary>
    /// 倒序
    /// </summary>
    /// <typeparam name="TCursor"></typeparam>
    /// <param name="cursor">游标</param>
    /// <param name="select">筛选</param>
    public static TCursor Desc<TCursor>(this TCursor cursor, Func<ITableView, IOrderAsc> select)
        where TCursor : CursorBase, ICursor
    {
        cursor.DescCore(select(cursor));
        return cursor;
    }
    #endregion
    #endregion
}

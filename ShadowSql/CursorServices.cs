using ShadowSql.Aggregates;
using ShadowSql.Cursors;
using ShadowSql.FieldInfos;
using ShadowSql.Identifiers;
using System;

namespace ShadowSql;

/// <summary>
/// 范围筛选游标扩展方法
/// </summary>
public static partial class ShadowSqlServices
{
    /// <summary>
    /// 聚合正序
    /// </summary>
    /// <typeparam name="TGroupByCursor"></typeparam>
    /// <param name="cursor"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public static TGroupByCursor AggregateAsc<TGroupByCursor>(this TGroupByCursor cursor, Func<IGroupByView, IAggregateField> select)
        where TGroupByCursor : GroupByCursorBase
    {
        cursor.AscCore(select(cursor.Source));
        return cursor;
    }
    /// <summary>
    /// 聚合倒序
    /// </summary>
    /// <typeparam name="TGroupByCursor"></typeparam>
    /// <param name="cursor"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public static TGroupByCursor AggregateDesc<TGroupByCursor>(this TGroupByCursor cursor, Func<IGroupByView, IAggregateField> select)
        where TGroupByCursor : GroupByCursorBase
    {
        cursor.DescCore(select(cursor.Source));
        return cursor;
    }
    /// <summary>
    /// 计数正序
    /// </summary>
    /// <typeparam name="TGroupByCursor"></typeparam>
    /// <param name="cursor"></param>
    /// <returns></returns>
    public static TGroupByCursor CountAsc<TGroupByCursor>(this TGroupByCursor cursor)
        where TGroupByCursor : GroupByCursorBase
    {
        cursor.AscCore(CountFieldInfo.Instance);
        return cursor;
    }
    /// <summary>
    /// 计数倒序
    /// </summary>
    /// <typeparam name="TGroupByCursor"></typeparam>
    /// <param name="cursor"></param>
    /// <returns></returns>
    public static TGroupByCursor CountDesc<TGroupByCursor>(this TGroupByCursor cursor)
        where TGroupByCursor : GroupByCursorBase
    {
        cursor.DescCore(CountFieldInfo.Instance);
        return cursor;
    }
}

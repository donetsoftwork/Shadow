using ShadowSql.Engines;
using ShadowSql.Fragments;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql;

/// <summary>
/// sql拼写扩展方法
/// </summary>
public static partial class ShadowSqlServices
{
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="fragment"></param>
    /// <param name="capacity"></param>
    /// <returns></returns>
    public static string Sql(this ISqlEngine engine, ISqlFragment fragment, int capacity = 128)
    {
        var builder = new StringBuilder(capacity);
        if (fragment.TryWrite(engine, builder))
            return builder.ToString();
        return string.Empty;
    }
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="entity"></param>
    /// <param name="capacity"></param>
    /// <returns></returns>
    public static string Sql(this ISqlEngine engine, ISqlEntity entity, int capacity = 16)
    {
        var builder = new StringBuilder(capacity);
        entity.Write(engine, builder);
        return builder.ToString();
    }
    /// <summary>
    /// 前缀拼接
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="entity"></param>
    /// <param name="builder"></param>
    /// <param name="suffix"></param>
    /// <returns></returns>
    public static void ConcatPrefix(this ISqlEngine engine, ISqlEntity entity, StringBuilder builder, params IEnumerable<string> suffix)
    {
        //var point = builder.Length;
        foreach (var item in suffix)
            builder.Append(item);
        //if (fragment.Write(engine, builder))
        //    return true;
        entity.Write(engine, builder);
        //roll back
        //builder.Length = point;
        //return false;
    }

#pragma warning disable 1573
    #region Prefix
    /// <summary>
    /// WHERE前缀
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    public static void WherePrefix(this ISqlEngine engine, StringBuilder sql)
        => sql.Append(" WHERE ");
    /// <summary>
    /// 联表ON前缀
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    public static void JoinOnPrefix(this ISqlEngine engine, StringBuilder sql)
        => sql.Append(" ON ");
    /// <summary>
    /// GROUP BY前缀
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    public static void GroupByPrefix(this ISqlEngine engine, StringBuilder sql)
        => sql.Append(" GROUP BY ");
    /// <summary>
    /// HAVING前缀
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    public static void HavingPrefix(this ISqlEngine engine, StringBuilder sql)
        => sql.Append(" HAVING ");
    #endregion
#pragma warning restore 1573
}

using ShadowSql.Engines;
using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql;

/// <summary>
/// sql拼写扩展方法
/// </summary>
public static partial class ShadowSqlCoreServices
{
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine">数据库引擎</param>
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
    /// <param name="engine">数据库引擎</param>
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
    /// 拼写计数sql
    /// </summary>
    /// <param name="engine">数据库引擎</param>
    /// <param name="view"></param>
    /// <param name="capacity"></param>
    /// <returns></returns>
    public static string CountSql(this ISqlEngine engine, ITableView view, int capacity = 128)
    {
        var sql = new StringBuilder(capacity);
        engine.SelectPrefix(sql);
        engine.Count(sql);
        sql.Append(" FROM ");
        view.Write(engine, sql);
        return sql.ToString();
    }
    /// <summary>
    /// 前缀拼接
    /// </summary>
    /// <param name="engine">数据库引擎</param>
    /// <param name="entity"></param>
    /// <param name="builder"></param>
    /// <param name="suffix"></param>
    /// <returns></returns>
    public static void ConcatPrefix(this ISqlEngine engine, ISqlEntity entity, StringBuilder builder, params IEnumerable<string> suffix)
    {
        foreach (var item in suffix)
            builder.Append(item);
        entity.Write(engine, builder);
    }

#pragma warning disable 1573
    #region Prefix
    /// <summary>
    /// WHERE前缀
    /// </summary>
    /// <param name="engine">数据库引擎</param>
    /// <param name="sql">sql</param>
    public static void WherePrefix(this ISqlEngine engine, StringBuilder sql)
        => sql.Append(" WHERE ");
    /// <summary>
    /// 联表ON前缀
    /// </summary>
    /// <param name="engine">数据库引擎</param>
    /// <param name="sql">sql</param>
    public static void JoinOnPrefix(this ISqlEngine engine, StringBuilder sql)
        => sql.Append(" ON ");
    /// <summary>
    /// GROUP BY前缀
    /// </summary>
    /// <param name="engine">数据库引擎</param>
    /// <param name="sql">sql</param>
    public static void GroupByPrefix(this ISqlEngine engine, StringBuilder sql)
        => sql.Append(" GROUP BY ");
    /// <summary>
    /// HAVING前缀
    /// </summary>
    /// <param name="engine">数据库引擎</param>
    /// <param name="sql">sql</param>
    public static void HavingPrefix(this ISqlEngine engine, StringBuilder sql)
        => sql.Append(" HAVING ");
    #endregion
#pragma warning restore 1573
    /// <summary>
    /// 拼写插入列名(避免出现列名前缀可能导致错误)
    /// </summary>
    /// <param name="engine">数据库引擎</param>
    /// <param name="sql">sql</param>
    /// <param name="column">列</param>
    public static void WriteInsertColumnName(this ISqlEngine engine, StringBuilder sql, IColumn column)
        => engine.Identifier(sql, column.ViewName);
}

using ShadowSql.Identifiers;
using System.Collections.Generic;

namespace ShadowSql;

/// <summary>
/// 表对象扩展方法
/// </summary>
public static partial class ShadowSqlCoreServices
{
    /// <summary>
    /// 定义列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table">表</param>
    /// <param name="columns">列</param>
    /// <returns></returns>
    public static TTable DefineColums<TTable>(this TTable table, params IEnumerable<string> columns)
        where TTable : Table
    {
        foreach (var columName in columns)
            table.DefineColumn(columName);
        return table;
    }
    /// <summary>
    /// 添加列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table">表</param>
    /// <param name="columns">列</param>
    /// <returns></returns>
    public static TTable AddColums<TTable>(this TTable table, params IEnumerable<IColumn> columns)
        where TTable : Table
    {
        foreach (var column in columns)
            table.AddColumn(column);
        return table;
    }
    /// <summary>
    /// 忽略插入的列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table">表</param>
    /// <param name="columns">列</param>
    /// <returns></returns>
    public static TTable IgnoreInsert<TTable>(this TTable table, params IEnumerable<IColumn> columns)
        where TTable : Table
    {
        foreach (var column in columns)
            table.AddInsertIgnore(column);
        return table;
    }
    /// <summary>
    /// 忽略插入的列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table">表</param>
    /// <param name="columns">列</param>
    /// <returns></returns>
    public static TTable IgnoreInsert<TTable>(this TTable table, params IEnumerable<string> columns)
        where TTable : Table
    {
        foreach (var columnName in columns)
        {
            if (table.GetColumn(columnName) is IColumn column)
                table.AddInsertIgnore(column);
        }
        return table;
    }
    /// <summary>
    /// 忽略修改的列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table">表</param>
    /// <param name="columns">列</param>
    /// <returns></returns>
    public static TTable IgnoreUpdate<TTable>(this TTable table, params IEnumerable<IColumn> columns)
        where TTable : Table
    {
        foreach (var column in columns)
            table.AddUpdateIgnore(column);
        return table;
    }
    /// <summary>
    /// 忽略修改的列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table">表</param>
    /// <param name="columns">列</param>
    /// <returns></returns>
    public static TTable IgnoreUpdate<TTable>(this TTable table, params IEnumerable<string> columns)
        where TTable : Table
    {
        foreach (var columnName in columns)
        {
            if (table.GetColumn(columnName) is IColumn column)
                table.AddUpdateIgnore(column);
        }
        return table;
    }
}

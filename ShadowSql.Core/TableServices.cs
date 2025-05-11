using ShadowSql.Identifiers;
using ShadowSql.Variants;
using System;
using System.Collections.Generic;

namespace ShadowSql;

/// <summary>
/// 表对象扩展方法
/// </summary>
public static partial class ShadowSqlCoreServices
{
    /// <summary>
    /// 定位到表
    /// </summary>
    /// <param name="multiTable"></param>
    /// <param name="tableName"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IAliasTable From(this IMultiView multiTable, string tableName)
        => multiTable.GetMember(tableName)
        ?? throw new ArgumentException(tableName + "表不存在", nameof(tableName));
    /// <summary>
    /// 定位到别名表
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="multiTable"></param>
    /// <param name="tableName"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IAliasTable<TTable> Alias<TTable>(this IMultiView multiTable, string tableName)
        where TTable : ITable
        => multiTable.From<TableAlias<TTable>>(tableName);
    /// <summary>
    /// 定位到成员别名表
    /// </summary>
    /// <typeparam name="TAliasTable"></typeparam>
    /// <param name="multiTable"></param>
    /// <param name="tableName"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static TAliasTable From<TAliasTable>(this IMultiView multiTable, string tableName)
        where TAliasTable : IAliasTable
    {
        foreach (var table in multiTable.Tables)
        {
            if (table.IsMatch(tableName) && table is TAliasTable member)
                return member;
        }
        throw new ArgumentException(tableName + "表不存在", nameof(tableName));
    }
    /// <summary>
    /// 别名表
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <param name="alias"></param>
    /// <returns></returns>
    public static TableAlias<TTable> As<TTable>(this TTable table, string alias)
        where TTable : ITable
        => new(table, alias);
    /// <summary>
    /// 定义列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <param name="columns"></param>
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
    /// <param name="table"></param>
    /// <param name="columns"></param>
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
    /// <param name="table"></param>
    /// <param name="columns"></param>
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
    /// <param name="table"></param>
    /// <param name="columns"></param>
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
    /// <param name="table"></param>
    /// <param name="columns"></param>
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
    /// <param name="table"></param>
    /// <param name="columns"></param>
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

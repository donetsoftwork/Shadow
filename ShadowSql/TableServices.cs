using ShadowSql.FieldInfos;
using ShadowSql.Identifiers;
using ShadowSql.Variants;
using System;
using System.Collections.Generic;

namespace ShadowSql;

/// <summary>
/// 表对象扩展方法
/// </summary>
public static partial class ShadowSqlServices
{
    /// <summary>
    /// 定位到表
    /// </summary>
    /// <param name="multiTable"></param>
    /// <param name="tableName"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IAliasTable From(this IMultiTable multiTable, string tableName)
        => multiTable.GetMember(tableName)
        ?? throw new ArgumentException(tableName + "表不存在", nameof(tableName));
    /// <summary>
    /// 定位到表
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="multiTable"></param>
    /// <param name="tableName"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static TableAlias<TTable> Table<TTable>(this IMultiTable multiTable, string tableName)
        where TTable : ITable
    {
        foreach (var table in multiTable.Tables)
        {
            if (table.IsMatch(tableName) && table is TableAlias<TTable> tableAlias)
                return tableAlias;
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
    /// 选择列
    /// </summary>
    /// <param name="table"></param>
    /// <param name="columnNames"></param>
    /// <returns></returns>
    public static IEnumerable<IFieldView> SelectFields(this ITableView table, params IEnumerable<string> columnNames)
    {
        foreach (var name in columnNames)
        {
            if (table.GetColumn(name) is IColumn column)
                yield return column;
            else
                yield return FieldInfo.Use(name);
        }
    }
    /// <summary>
    /// 选择列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static IPrefixColumn SelectPrefixColumn<TTable>(this TableAlias<TTable> table, Func<TTable, IColumn> query)
        where TTable : ITable
        => table.GetPrefixColumn(query(table.Target)) ?? throw new ArgumentException("PrefixColumn不存在", nameof(query));
     
}

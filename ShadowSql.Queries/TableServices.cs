using ShadowSql.Identifiers;
using ShadowSql.Variants;
using System;

namespace ShadowSql;

/// <summary>
/// 表对象扩展方法
/// </summary>
public static partial class ShadowSqlQueryServices
{
    /// <summary>
    /// 定位到表
    /// </summary>
    /// <param name="multiTable">多表(联表)</param>
    /// <param name="tableName">表名</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IAliasTable From(this IMultiView multiTable, string tableName)
        => multiTable.GetMember(tableName)
        ?? throw new ArgumentException(tableName + "表不存在", nameof(tableName));
    /// <summary>
    /// 定位到别名表
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="multiTable">多表(联表)</param>
    /// <param name="tableName">表名</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IAliasTable<TTable> Alias<TTable>(this IMultiView multiTable, string tableName)
        where TTable : ITable
        => multiTable.From<IAliasTable<TTable>>(tableName);
    /// <summary>
    /// 定位到成员别名表
    /// </summary>
    /// <typeparam name="TAliasTable"></typeparam>
    /// <param name="multiTable">多表(联表)</param>
    /// <param name="tableName">表名</param>
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
    /// <param name="table">表</param>
    /// <param name="aliasName">别名</param>
    /// <returns></returns>
    public static TableAlias<TTable> As<TTable>(this TTable table, string aliasName)
        where TTable : ITable
        => new(table, aliasName);
}

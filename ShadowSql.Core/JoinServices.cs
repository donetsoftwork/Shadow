using ShadowSql.Identifiers;
using ShadowSql.Join;
using ShadowSql.Tables;
using ShadowSql.Variants;
using System.Collections.Generic;

namespace ShadowSql;

/// <summary>
/// 联表扩展方法
/// </summary>
public static partial class ShadowSqlCoreServices
{
    /// <summary>
    /// 添加表成员
    /// </summary>
    /// <typeparam name="MultiTable"></typeparam>
    /// <param name="multiTable">多表(联表)</param>
    /// <param name="aliasTables"></param>
    /// <returns></returns>
    public static MultiTable AddMembers<MultiTable>(this MultiTable multiTable, params IEnumerable<IAliasTable> aliasTables)
        where MultiTable : MultiTableBase
    {
        foreach (var aliasTable in aliasTables)
            multiTable.AddMemberCore(aliasTable);
        return multiTable;
    }
    /// <summary>
    /// 添加表成员
    /// </summary>
    /// <typeparam name="MultiTable"></typeparam>
    /// <param name="multiTable">多表(联表)</param>
    /// <param name="tables"></param>
    /// <returns></returns>
    public static MultiTable AddMembers<MultiTable>(this MultiTable multiTable, params IEnumerable<ITable> tables)
        where MultiTable : MultiTableBase, IMultiView
    {
        foreach (var table in tables)
            multiTable.AddMemberCore(table.As(multiTable.CreateMemberName()));
        return multiTable;
    }
    /// <summary>
    /// 添加表成员
    /// </summary>
    /// <typeparam name="MultiTable"></typeparam>
    /// <param name="multiTable">多表(联表)</param>
    /// <param name="tableNames"></param>
    /// <returns></returns>
    public static MultiTable AddMembers<MultiTable>(this MultiTable multiTable, params IEnumerable<string> tableNames)
        where MultiTable : MultiTableBase, IMultiView
    {
        foreach (var tableName in tableNames)
        {
            var member = EmptyTable.Use(tableName)
                .As(multiTable.CreateMemberName());
            multiTable.AddMemberCore(member);
        }
        return multiTable;
    }
    /// <summary>
    /// 构造表成员
    /// </summary>
    /// <typeparam name="MultiTable"></typeparam>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="multiTable">多表(联表)</param>
    /// <param name="table">表</param>
    /// <returns></returns>
    public static TableAlias<TTable> CreateMember<MultiTable, TTable>(this MultiTable multiTable, TTable table)
        where MultiTable : MultiTableBase, IMultiView
        where TTable : ITable
    {
        var member = table.As(multiTable.CreateMemberName());
        multiTable.AddMemberCore(member);
        return member;
    }
}
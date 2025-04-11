using ShadowSql.Identifiers;
using ShadowSql.Join;
using ShadowSql.Variants;

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
    /// <param name="multiTable"></param>
    /// <param name="aliasTable"></param>
    /// <returns></returns>
    public static MultiTable AddMember<MultiTable>(this MultiTable multiTable, IAliasTable aliasTable)
        where MultiTable : MultiTableBase
    {
        multiTable.AddMemberCore(aliasTable);
        return multiTable;
    }
    /// <summary>
    /// 构造表成员
    /// </summary>
    /// <typeparam name="MultiTable"></typeparam>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="multiTable"></param>
    /// <param name="table"></param>
    /// <returns></returns>
    public static MultiTable AddMember<MultiTable, TTable>(this MultiTable multiTable, TTable table)
        where MultiTable : MultiTableBase, IMultiTable
        where TTable : ITable
    {
        multiTable.AddMemberCore(table.As(multiTable.CreateMemberName()));
        return multiTable;
    }
    /// <summary>
    /// 构造表成员
    /// </summary>
    /// <typeparam name="MultiTable"></typeparam>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="multiTable"></param>
    /// <param name="table"></param>
    /// <returns></returns>
    public static TableAlias<TTable> CreateMember<MultiTable, TTable>(this MultiTable multiTable, TTable table)
        where MultiTable : MultiTableBase, IMultiTable
        where TTable : ITable
    {
        var aliasTable = table.As(multiTable.CreateMemberName());
        multiTable.AddMemberCore(aliasTable);
        return aliasTable;
    }
}
using ShadowSql.Identifiers;
using ShadowSql.Select;
using ShadowSql.SelectFields;

namespace ShadowSql;

/// <summary>
/// 多联表筛选扩展方法
/// </summary>
public static partial class ShadowSqlServices
{
    #region SelectTable
    /// <summary>
    /// 添加表
    /// </summary>
    /// <typeparam name="TMultiTableSelect"></typeparam>
    /// <param name="select">筛选</param>
    /// <param name="tableName">表名</param>
    /// <returns></returns>
    public static TMultiTableSelect SelectTable<TMultiTableSelect>(this TMultiTableSelect select, string tableName)
        where TMultiTableSelect : SelectFieldsBase, IMultiSelect
    {
        select.SelectTables.Add(select.Target.From(tableName));
        return select;
    }
    /// <summary>
    /// 添加表
    /// </summary>
    /// <typeparam name="TMultiTableSelect"></typeparam>
    /// <param name="select">筛选</param>
    /// <param name="aliasTable">别名表</param>
    /// <returns></returns>
    public static TMultiTableSelect SelectTable<TMultiTableSelect>(this TMultiTableSelect select, IAliasTable aliasTable)
        where TMultiTableSelect : SelectFieldsBase, IMultiSelect
    {
        select.SelectTables.Add(aliasTable);
        return select;
    }
    #endregion
}

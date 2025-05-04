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
    /// <param name="multiSelect"></param>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public static TMultiTableSelect SelectTable<TMultiTableSelect>(this TMultiTableSelect multiSelect, string tableName)
        where TMultiTableSelect : SelectFieldsBase, IMultiSelect
    {
        multiSelect.SelectTables.Add(multiSelect.Target.From(tableName));
        return multiSelect;
    }
    /// <summary>
    /// 添加表
    /// </summary>
    /// <typeparam name="TMultiTableSelect"></typeparam>
    /// <param name="multiSelect"></param>
    /// <param name="aliasTable"></param>
    /// <returns></returns>
    public static TMultiTableSelect SelectTable<TMultiTableSelect>(this TMultiTableSelect multiSelect, IAliasTable aliasTable)
        where TMultiTableSelect : SelectFieldsBase, IMultiSelect
    {
        multiSelect.SelectTables.Add(aliasTable);
        return multiSelect;
    }
    #endregion
}

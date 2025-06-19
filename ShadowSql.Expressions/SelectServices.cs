using ShadowSql.Expressions.Select;
using ShadowSql.FieldInfos;
using ShadowSql.Identifiers;
using ShadowSql.SelectFields;

namespace ShadowSql.Expressions;

/// <summary>
/// 字段筛选扩展方法
/// </summary>
public static partial class ShadowSqlServicess
{
    /// <summary>
    /// 筛选计数
    /// </summary>
    /// <typeparam name="TSelect"></typeparam>
    /// <param name="select">筛选</param>
    /// <param name="aliasName">别名</param>
    /// <returns></returns>
    public static TSelect SelectCount<TSelect>(this TSelect select, string aliasName = "Count")
        where TSelect : SelectFieldsBase, IGroupBySelect
    {
        select.SelectCore(CountAliasFieldInfo.Use(aliasName));
        return select;
    }
    /// <summary>
    /// 筛选分组字段
    /// </summary>
    /// <typeparam name="TSelect"></typeparam>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public static TSelect SelectKey<TSelect>(this TSelect select)
        where TSelect : SelectFieldsBase, IGroupBySelect
    {
        select.SelectCore(select.Target.GroupByFields);
        return select;
    }
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

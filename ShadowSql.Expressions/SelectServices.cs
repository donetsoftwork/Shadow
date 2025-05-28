using ShadowSql.Expressions.Select;
using ShadowSql.FieldInfos;
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
    /// <param name="select"></param>
    /// <param name="alias"></param>
    /// <returns></returns>
    public static TSelect SelectCount<TSelect>(this TSelect select, string alias = "Count")
        where TSelect : SelectFieldsBase, IGroupBySelect
    {
        select.SelectCore(CountAliasFieldInfo.Use(alias));
        return select;
    }
    /// <summary>
    /// 筛选分组字段
    /// </summary>
    /// <typeparam name="TSelect"></typeparam>
    /// <param name="select"></param>
    /// <returns></returns>
    public static TSelect SelectKey<TSelect>(this TSelect select)
        where TSelect : SelectFieldsBase, IGroupBySelect
    {
        select.SelectCore(select.Target.GroupByFields);
        return select;
    }
}

using ShadowSql.Aggregates;
using ShadowSql.FieldInfos;
using ShadowSql.Identifiers;
using ShadowSql.Select;
using ShadowSql.SelectFields;
using System;

namespace ShadowSql;

/// <summary>
/// 字段筛选扩展方法
/// </summary>
public static partial class ShadowSqlServicess
{
    #region Apply
    /// <summary>
    /// 筛选列
    /// </summary>
    /// <typeparam name="TSelect"></typeparam>
    /// <param name="select">筛选</param>
    /// <param name="action"></param>
    /// <returns></returns>
    public static TSelect Apply<TSelect>(this TSelect select, Action<TSelect> action)
        where TSelect : ISelect
    {
        action(select);
        return select;
    }
    #endregion
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
    public static TSelect SelectGroupBy<TSelect>(this TSelect select)
        where TSelect : SelectFieldsBase, IGroupBySelect
    {
        select.SelectCore(select.Target.GroupByFields);
        return select;
    }
    /// <summary>
    /// 聚合筛选
    /// </summary>
    /// <typeparam name="TSelect"></typeparam>
    /// <param name="select">筛选</param>
    /// <param name="aggregate">聚合</param>
    /// <returns></returns>
    public static TSelect SelectAggregate<TSelect>(this TSelect select, Func<IGroupByView, IAggregateFieldAlias> aggregate)
        where TSelect : SelectFieldsBase, IGroupBySelect
    {
        select.SelectCore(aggregate(select.Target));
        return select;
    }
}

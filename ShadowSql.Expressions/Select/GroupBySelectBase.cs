using ShadowSql.Aggregates;
using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.Select;
using System;
using System.Text;

namespace ShadowSql.Expressions.Select;

/// <summary>
/// 分组筛选基类
/// </summary>
/// <typeparam name="TSource"></typeparam>
/// <typeparam name="TGroupSource"></typeparam>
/// <param name="view"></param>
/// <param name="groupBy">分组查询</param>
/// <param name="groupSource"></param>
public abstract class GroupBySelectBase<TSource, TGroupSource>(TSource view, IGroupByView groupBy, TGroupSource groupSource)
    : SelectBase<TSource, IGroupByView>(view, groupBy), IGroupBySelect, ISelect
    where TSource : ITableView
    where TGroupSource : ITableView
{
    #region 配置
    /// <summary>
    /// 分组数据源
    /// </summary>
    protected readonly TGroupSource _groupSource = groupSource;
    /// <summary>
    /// 分组数据源
    /// </summary>
    public TGroupSource GroupSource
        => _groupSource;
    #endregion
    /// <summary>
    /// 筛选列
    /// </summary>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    protected void SelectAggregateCore(Func<TGroupSource, IAggregateFieldAlias> select)
        => SelectCore(select(_groupSource));
    /// <inheritdoc/>
    protected override bool WriteSelectedCore(ISqlEngine engine, StringBuilder sql, bool appended)
    {
        if(base.WriteSelectedCore(engine, sql, appended))
            return true;
        return WriteSelectFields(engine, sql, _target.GroupByFields, false);
    }
}
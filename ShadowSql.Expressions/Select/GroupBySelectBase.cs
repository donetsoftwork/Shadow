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
/// <param name="source"></param>
/// <param name="target"></param>
/// <param name="groupSource"></param>
public abstract class GroupBySelectBase<TSource, TGroupSource>(TSource source, IGroupByView target, TGroupSource groupSource)
    : SelectBase<TSource, IGroupByView>(source, target), IGroupBySelect, ISelect
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
    /// <param name="select"></param>
    /// <returns></returns>
    protected void SelectAggregateCore(Func<TGroupSource, IAggregateFieldAlias> select)
        => SelectCore(select(_groupSource));
    /// <summary>
    /// 拼写筛选字段列表
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <param name="appended"></param>
    /// <returns></returns>
    protected override bool WriteSelectedCore(ISqlEngine engine, StringBuilder sql, bool appended)
    {
        if(base.WriteSelectedCore(engine, sql, appended))
            return true;
        return WriteSelectFields(engine, sql, _target.GroupByFields, false);
    }
}
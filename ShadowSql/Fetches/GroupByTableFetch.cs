using ShadowSql.Aggregates;
using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using System;
using System.Collections.Generic;

namespace ShadowSql.Fetches;

/// <summary>
/// 表分组后范围筛选
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="source"></param>
/// <param name="limit"></param>
/// <param name="offset"></param>
public class GroupByTableFetch<TTable>(GroupByTable<TTable> source, int limit, int offset)
    : FetchBase<GroupByTable<TTable>>(source, offset, limit)
    where TTable : ITable
{
    #region 配置
    private readonly TTable _table = source.Source;
    /// <summary>
    /// 表
    /// </summary>
    public TTable Table
        => _table;
    #endregion    
    #region 功能
    #region GroupBy的列
    /// <summary>
    /// 正序
    /// </summary>
    /// <param name="select"></param>
    /// <returns></returns>
    public GroupByTableFetch<TTable> Asc(Func<IGroupByView, IOrderView> select)
    {
        AscCore(select(_source));
        return this;
    }
    /// <summary>
    /// 倒序
    /// </summary>
    /// <param name="select"></param>
    /// <returns></returns>
    public GroupByTableFetch<TTable> Desc(Func<IGroupByView, IOrderAsc> select)
    {
        DescCore(select(_source));
        return this;
    }
    #endregion
    #region Aggregate
    /// <summary>
    /// 正序
    /// </summary>
    /// <param name="select">定位列</param>
    /// <param name="aggregate">聚合</param>
    /// <returns></returns>
    public GroupByTableFetch<TTable> AggregateAsc(Func<TTable, IColumn> select, Func<IColumn, IAggregateField> aggregate)
    {
        AscCore(aggregate(select(_table)));
        return this;
    }
    /// <summary>
    /// 倒序
    /// </summary>
    /// <param name="select">定位列</param>
    /// <param name="aggregate">聚合</param>
    /// <returns></returns>
    public GroupByTableFetch<TTable> AggregateDesc(Func<TTable, IColumn> select, Func<IColumn, IAggregateField> aggregate)
    {
        DescCore(aggregate(select(_table)));
        return this;
    }
    #endregion    
    #endregion   
}

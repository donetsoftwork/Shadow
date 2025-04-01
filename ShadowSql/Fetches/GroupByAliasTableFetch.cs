using ShadowSql.Aggregates;
using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using System;
using System.Collections.Generic;

namespace ShadowSql.Fetches;

/// <summary>
/// 别名表分组后范围筛选
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="source"></param>
/// <param name="limit"></param>
/// <param name="offset"></param>
public class GroupByAliasTableFetch<TTable>(GroupByAliasTable<TTable> source, int limit, int offset)
    : FetchBase<GroupByAliasTable<TTable>>(source, offset, limit)
    where TTable : ITable
{
    #region 配置
    private readonly IAliasTable _aliasTable = source.Source;
    /// <summary>
    /// 别名表
    /// </summary>
    public IAliasTable AliasTable
        => _aliasTable;
    private readonly TTable _table = source.Table;
    /// <summary>
    /// 表
    /// </summary>
    public TTable Table
        => _table;
    #endregion 
    #region 功能
    #region 排序
    /// <summary>
    /// 正序
    /// </summary>
    /// <param name="select"></param>
    /// <returns></returns>
    public GroupByAliasTableFetch<TTable> Asc(Func<IGroupByView, IOrderView> select)
    {
        AscCore(select(_source));
        return this;
    }
    /// <summary>
    /// 倒序
    /// </summary>
    /// <param name="select"></param>
    /// <returns></returns>
    public GroupByAliasTableFetch<TTable> Desc(Func<IGroupByView, IOrderAsc> select)
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
    public GroupByAliasTableFetch<TTable> AggregateAsc(Func<IAliasTable, IColumn> select, Func<IColumn, IAggregateField> aggregate)
    {
        AscCore(aggregate(select(_aliasTable)));
        return this;
    }
    /// <summary>
    /// 倒序
    /// </summary>
    /// <param name="select">定位列</param>
    /// <param name="aggregate">聚合</param>
    /// <returns></returns>
    public GroupByAliasTableFetch<TTable> AggregateDesc(Func<IAliasTable, IColumn> select, Func<IColumn, IAggregateField> aggregate)
    {
        DescCore(aggregate(select(_aliasTable)));
        return this;
    }
    /// <summary>
    /// 正序
    /// </summary>
    /// <param name="select">定位列</param>
    /// <param name="aggregate">聚合</param>
    /// <returns></returns>
    public GroupByAliasTableFetch<TTable> AggregateAsc(Func<TTable, IColumn> select, Func<IColumn, IAggregateField> aggregate)
    {
        //增加前缀
        var prefixColumn = _aliasTable.GetPrefixColumn(select(_table));
        if (prefixColumn is not null)
            AscCore(aggregate(prefixColumn));
        return this;
    }
    /// <summary>
    /// 倒序
    /// </summary>
    /// <param name="select">定位列</param>
    /// <param name="aggregate">聚合</param>
    /// <returns></returns>
    public GroupByAliasTableFetch<TTable> AggregateDesc(Func<TTable, IColumn> select, Func<IColumn, IAggregateField> aggregate)
    {
        //增加前缀
        var prefixColumn = _aliasTable.GetPrefixColumn(select(_table));
        if (prefixColumn is not null)
            DescCore(aggregate(prefixColumn));
        return this;
    }
    #endregion    
    #endregion
}

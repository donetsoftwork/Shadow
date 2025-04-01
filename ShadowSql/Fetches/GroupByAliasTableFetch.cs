using ShadowSql.Aggregates;
using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using ShadowSql.Variants;
using System;

namespace ShadowSql.Fetches;

/// <summary>
/// 别名表分组后范围筛选
/// </summary>
/// <typeparam name="TTable"></typeparam>
public class GroupByAliasTableFetch<TTable> : FetchBase<IGroupByView>
    where TTable : ITable
{
    /// <summary>
    /// 别名表分组后范围筛选
    /// </summary>
    /// <param name="source"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    public GroupByAliasTableFetch(GroupByAliasTableQuery<TTable> source, int limit, int offset)
        : this(source, source.Source, source.Source.Target, limit, offset)
    {
    }
    /// <summary>
    /// 别名表分组后范围筛选
    /// </summary>
    /// <param name="source"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    public GroupByAliasTableFetch(GroupByAliasTableSqlQuery<TTable> source, int limit, int offset)
        : this(source, source.Source, source.Source.Target, limit, offset)
    {
    }
    private GroupByAliasTableFetch(IGroupByView groupBy, TableAlias<TTable> aliasTable, TTable table, int limit, int offset)
        : base(groupBy, limit, offset)
    {
        _aliasTable = aliasTable;
        _table = table;
    }
    #region 配置
    private readonly TableAlias<TTable> _aliasTable;
    /// <summary>
    /// 别名表
    /// </summary>
    public TableAlias<TTable> AliasTable
        => _aliasTable;
    private readonly TTable _table;
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

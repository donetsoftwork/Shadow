using ShadowSql.Aggregates;
using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using System;

namespace ShadowSql.Cursors;

/// <summary>
/// 表分组后范围筛选游标
/// </summary>
/// <typeparam name="TTable"></typeparam>
public class GroupByTableCursor<TTable> : CursorBase<IGroupByView>
    where TTable : ITable
{
    /// <summary>
    /// 表分组后范围筛选
    /// </summary>
    /// <param name="source"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    public GroupByTableCursor(GroupByTableQuery<TTable> source, int limit, int offset)
        : this(source, source.Source, limit, offset)
    {
    }
    /// <summary>
    /// 表分组后范围筛选
    /// </summary>
    /// <param name="source"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    public GroupByTableCursor(GroupByTableSqlQuery<TTable> source, int limit, int offset)
        : this(source, source.Source, limit, offset)
    {
    }
    private GroupByTableCursor(IGroupByView source, TTable table, int limit, int offset)
        : base(source, limit, offset)
    {
        _table = table;
    }
    #region 配置
    private readonly TTable _table;
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
    public GroupByTableCursor<TTable> Asc(Func<IGroupByView, IOrderView> select)
    {
        AscCore(select(_source));
        return this;
    }
    /// <summary>
    /// 倒序
    /// </summary>
    /// <param name="select"></param>
    /// <returns></returns>
    public GroupByTableCursor<TTable> Desc(Func<IGroupByView, IOrderAsc> select)
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
    public GroupByTableCursor<TTable> AggregateAsc(Func<TTable, IColumn> select, Func<IColumn, IAggregateField> aggregate)
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
    public GroupByTableCursor<TTable> AggregateDesc(Func<TTable, IColumn> select, Func<IColumn, IAggregateField> aggregate)
    {
        DescCore(aggregate(select(_table)));
        return this;
    }
    #endregion    
    #endregion   
}

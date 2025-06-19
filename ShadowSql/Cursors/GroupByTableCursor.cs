using ShadowSql.Aggregates;
using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using System;

namespace ShadowSql.Cursors;

/// <summary>
/// 表分组后范围筛选游标
/// </summary>
/// <typeparam name="TTable"></typeparam>
public class GroupByTableCursor<TTable> : GroupByCursorBase
    where TTable : ITable
{
    /// <summary>
    /// 表分组后范围筛选
    /// </summary>
    /// <param name="groupBy">分组查询</param>
    /// <param name="limit">筛选数量</param>
    /// <param name="offset">跳过数量</param>
    public GroupByTableCursor(GroupByTableQuery<TTable> groupBy, int limit, int offset)
        : this(groupBy, groupBy._source, limit, offset)
    {
    }
    /// <summary>
    /// 表分组后范围筛选
    /// </summary>
    /// <param name="groupBy">分组查询</param>
    /// <param name="limit">筛选数量</param>
    /// <param name="offset">跳过数量</param>
    public GroupByTableCursor(GroupByTableSqlQuery<TTable> groupBy, int limit, int offset)
        : this(groupBy, groupBy._source, limit, offset)
    {
    }
    private GroupByTableCursor(IGroupByView groupBy, TTable table, int limit, int offset)
        : base(groupBy, limit, offset)
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
    #region Aggregate
    /// <summary>
    /// 正序
    /// </summary>
    /// <param name="aggregate">聚合</param>
    /// <returns></returns>
    public GroupByTableCursor<TTable> AggregateAsc(Func<TTable, IAggregateField> aggregate)
    {
        AscCore(aggregate(_table));
        return this;
    }
    /// <summary>
    /// 倒序
    /// </summary>
    /// <param name="aggregate">聚合</param>
    /// <returns></returns>
    public GroupByTableCursor<TTable> AggregateDesc(Func<TTable, IAggregateField> aggregate)
    {
        DescCore(aggregate(_table));
        return this;
    }
    #endregion    
    #endregion   
}

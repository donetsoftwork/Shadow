using ShadowSql.Aggregates;
using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using ShadowSql.Variants;
using System;

namespace ShadowSql.Cursors;

/// <summary>
/// 别名表分组后范围筛选游标
/// </summary>
/// <typeparam name="TTable"></typeparam>
public class GroupByAliasTableCursor<TTable> : GroupByCursorBase
    where TTable : ITable
{
    /// <summary>
    /// 别名表分组后范围筛选
    /// </summary>
    /// <param name="groupBy">分组查询</param>
    /// <param name="limit">筛选数量</param>
    /// <param name="offset">跳过数量</param>
    public GroupByAliasTableCursor(GroupByAliasTableQuery<TTable> groupBy, int limit, int offset)
        : this(groupBy, groupBy._source, groupBy._source.Target, limit, offset)
    {
    }
    /// <summary>
    /// 别名表分组后范围筛选
    /// </summary>
    /// <param name="groupBy">分组查询</param>
    /// <param name="limit">筛选数量</param>
    /// <param name="offset">跳过数量</param>
    public GroupByAliasTableCursor(GroupByAliasTableSqlQuery<TTable> groupBy, int limit, int offset)
        : this(groupBy, groupBy._source, groupBy._source.Target, limit, offset)
    {
    }
    private GroupByAliasTableCursor(IGroupByView groupBy, IAliasTable<TTable> aliasTable, TTable table, int limit, int offset)
        : base(groupBy, limit, offset)
    {
        _aliasTable = aliasTable;
        _table = table;
    }
    #region 配置
    private readonly IAliasTable<TTable> _aliasTable;
    /// <summary>
    /// 别名表
    /// </summary>
    public IAliasTable<TTable> AliasTable
        => _aliasTable;
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
    /// <param name="select">定位列</param>
    /// <param name="aggregate">聚合</param>
    /// <returns></returns>
    public GroupByAliasTableCursor<TTable> AggregateAsc(Func<TTable, IColumn> select, Func<IPrefixField, IAggregateField> aggregate)
    {
        //增加前缀
        var prefixField = _aliasTable.GetPrefixField(select(_table));
        if (prefixField is not null)
            AscCore(aggregate(prefixField));
        return this;
    }
    /// <summary>
    /// 倒序
    /// </summary>
    /// <param name="select">定位列</param>
    /// <param name="aggregate">聚合</param>
    /// <returns></returns>
    public GroupByAliasTableCursor<TTable> AggregateDesc(Func<TTable, IColumn> select, Func<IPrefixField, IAggregateField> aggregate)
    {
        //增加前缀
        var prefixField = _aliasTable.GetPrefixField(select(_table));
        if (prefixField is not null)
            DescCore(aggregate(prefixField));
        return this;
    }
    #endregion    
    #endregion
}

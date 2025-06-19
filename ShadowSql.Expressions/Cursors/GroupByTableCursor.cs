using ShadowSql.Expressions.GroupBy;
using ShadowSql.Expressions.VisitSource;
using ShadowSql.Identifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ShadowSql.Expressions.Cursors;

/// <summary>
/// 表分组后范围筛选游标
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TEntity"></typeparam>
public class GroupByTableCursor<TKey, TEntity> : GroupByCursorBase
{
    /// <summary>
    /// 表分组后范围筛选
    /// </summary>
    /// <param name="groupBy">分组查询</param>
    /// <param name="limit">筛选数量</param>
    /// <param name="offset">跳过数量</param>
    public GroupByTableCursor(GroupByTableQuery<TKey, TEntity> groupBy, int limit, int offset)
        : this(groupBy, groupBy._source, limit, offset)
    {
    }
    /// <summary>
    /// 表分组后范围筛选
    /// </summary>
    /// <param name="groupBy">分组查询</param>
    /// <param name="limit">筛选数量</param>
    /// <param name="offset">跳过数量</param>
    public GroupByTableCursor(GroupByTableSqlQuery<TKey, TEntity> groupBy, int limit, int offset)
        : this(groupBy, groupBy._source, limit, offset)
    {
    }
    private GroupByTableCursor(IGroupByView groupBy, ITableView table, int limit, int offset)
        : base(groupBy, limit, offset)
    {
        _table = table;
    }
    #region 配置
    private readonly ITableView _table;
    /// <summary>
    /// 表
    /// </summary>
    public ITableView Table
        => _table;
    #endregion
    #region 分组键
    /// <summary>
    /// 正序
    /// </summary>
    /// <typeparam name="TOrder">排序类型</typeparam>
    /// <param name="select">用于选择分组键的表达式</param>
    /// <returns></returns>
    public GroupByTableCursor<TKey, TEntity> Asc<TOrder>(Expression<Func<TKey, TOrder>> select)
    {
        var fileds = GroupByKeyVisitor.GetKeys(_source, select);
        foreach (var field in fileds)
            AscCore(field);
        return this;
    }
    /// <summary>
    /// 倒序
    /// </summary>
    /// <typeparam name="TOrder">排序类型</typeparam>
    /// <param name="select">用于选择分组键的表达式</param>
    /// <returns></returns>
    public GroupByTableCursor<TKey, TEntity> Desc<TOrder>(Expression<Func<TKey, TOrder>> select)
    {
        var fileds = GroupByKeyVisitor.GetKeys(_source, select);
        foreach (var field in fileds)
            DescCore(field);
        return this;
    }
    #endregion
    #region 聚合
    /// <summary>
    /// 正序
    /// </summary>
    /// <typeparam name="TOrder">排序类型</typeparam>
    /// <param name="select">用于选择分组键的表达式</param>
    /// <returns></returns>
    public GroupByTableCursor<TKey, TEntity> Asc<TOrder>(Expression<Func<IGrouping<TKey, TEntity>, TOrder>> select)
    {
        var list = new List<IOrderAsc>();
        GroupByVisitor.OrderBy(_source, list, select);
        foreach (var field in list)
            AscCore(field);
        return this;
    }
    /// <summary>
    /// 倒序
    /// </summary>
    /// <typeparam name="TOrder">排序类型</typeparam>
    /// <param name="select">用于选择分组键的表达式</param>
    /// <returns></returns>
    public GroupByTableCursor<TKey, TEntity> Desc<TOrder>(Expression<Func<IGrouping<TKey, TEntity>, TOrder>> select)
    {
        var list = new List<IOrderAsc>();
        GroupByVisitor.OrderBy(_source, list, select);
        foreach (var field in list)
            DescCore(field);
        return this;
    }
    #endregion
}

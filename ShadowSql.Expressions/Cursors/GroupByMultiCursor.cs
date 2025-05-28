using ShadowSql.Expressions.GroupBy;
using ShadowSql.Expressions.VisitSource;
using ShadowSql.Identifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ShadowSql.Expressions.Cursors;

/// <summary>
/// 多(联)表分组后范围筛选游标
/// </summary>
public class GroupByMultiCursor<TKey> : GroupByCursorBase
{
    /// <summary>
    /// 多(联)表分组后范围筛选
    /// </summary>
    /// <param name="source"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    public GroupByMultiCursor(GroupByMultiQuery<TKey> source, int limit, int offset)
        : this(source, source._source, limit, offset)
    {
    }
    /// <summary>
    /// 多(联)表分组后范围筛选
    /// </summary>
    /// <param name="source"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    public GroupByMultiCursor(GroupByMultiSqlQuery<TKey> source, int limit, int offset)
        : this(source, source._source, limit, offset)
    {
    }
    private GroupByMultiCursor(IGroupByView source, IMultiView multiTable, int limit, int offset)
        : base(source, limit, offset)
    {
        _multiTable = multiTable;
    }

    #region 配置
    /// <summary>
    /// 多(联)表
    /// </summary>
    protected readonly IMultiView _multiTable;
    /// <summary>
    /// 多(联)表
    /// </summary>
    public IMultiView MultiTable
        => _multiTable;
    #endregion    
    #region 分组键
    /// <summary>
    /// 正序
    /// </summary>
    /// <typeparam name="TOrder">排序类型</typeparam>
    /// <param name="select">用于选择分组键的表达式</param>
    /// <returns></returns>
    public GroupByMultiCursor<TKey> Asc<TOrder>(Expression<Func<TKey, TOrder>> select)
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
    public GroupByMultiCursor<TKey> Desc<TOrder>(Expression<Func<TKey, TOrder>> select)
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
    /// <typeparam name="TEntity">含分组字段的类</typeparam>
    /// <typeparam name="TOrder">排序类型</typeparam>
    /// <param name="table">分组字段所在表</param>
    /// <param name="select">用于选择分组键的表达式</param>
    /// <returns></returns>
    public GroupByMultiCursor<TKey> Asc<TEntity, TOrder>(string table, Expression<Func<IGrouping<TKey, TEntity>, TOrder>> select)
    {
        var list = new List<IOrderAsc>();
        GroupByVisitor.OrderBy(_source, _multiTable.From(table), list, select);
        foreach (var field in list)
            AscCore(field);
        return this;
    }
    /// <summary>
    /// 正序
    /// </summary>
    /// <typeparam name="TEntity">含分组字段的类</typeparam>
    /// <typeparam name="TOrder">排序类型</typeparam>
    /// <param name="table">分组字段所在表</param>
    /// <param name="select">用于选择分组键的表达式</param>
    /// <returns></returns>
    public GroupByMultiCursor<TKey> Desc<TEntity, TOrder>(string table, Expression<Func<IGrouping<TKey, TEntity>, TOrder>> select)
    {
        var list = new List<IOrderAsc>();
        GroupByVisitor.OrderBy(_source, _multiTable.From(table), list, select);
        foreach (var field in list)
            DescCore(field);
        return this;
    }
    /// <summary>
    /// 正序
    /// </summary>
    /// <typeparam name="TEntity">含分组字段的类</typeparam>
    /// <typeparam name="TOrder">排序类型</typeparam>
    /// <param name="select">用于选择分组键的表达式</param>
    /// <returns></returns>
    public GroupByMultiCursor<TKey> Asc<TEntity, TOrder>(Expression<Func<IGrouping<TKey, TEntity>, TOrder>> select)
    {
        var list = new List<IOrderAsc>();
        GroupByVisitor.OrderBy(_source, _multiTable, list, select);
        foreach (var field in list)
            AscCore(field);
        return this;
    }
    /// <summary>
    /// 正序
    /// </summary>
    /// <typeparam name="TEntity">含分组字段的类</typeparam>
    /// <typeparam name="TOrder">排序类型</typeparam>
    /// <param name="select">用于选择分组键的表达式</param>
    /// <returns></returns>
    public GroupByMultiCursor<TKey> Desc<TEntity, TOrder>(Expression<Func<IGrouping<TKey, TEntity>, TOrder>> select)
    {
        var list = new List<IOrderAsc>();
        GroupByVisitor.OrderBy(_source, _multiTable, list, select);
        foreach (var field in list)
            DescCore(field);
        return this;
    }
    #endregion
}

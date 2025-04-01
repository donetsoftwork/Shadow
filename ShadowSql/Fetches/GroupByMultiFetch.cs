using ShadowSql.Aggregates;
using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using System;
using System.Collections.Generic;

namespace ShadowSql.Fetches;

/// <summary>
/// 多(联)表分组后范围筛选
/// </summary>
/// <param name="source"></param>
/// <param name="limit"></param>
/// <param name="offset"></param>
public class GroupByMultiFetch(GroupByMultiQuery source, int limit, int offset)
    : FetchBase<GroupByMultiQuery>(source, offset, limit)
{
    #region 配置
    private readonly IMultiTable _multiTable = source.Source;
    /// <summary>
    /// 多(联)表
    /// </summary>
    public IMultiTable MultiTable
        => _multiTable;
    #endregion
    
    #region 功能
    #region GroupBy的列
    /// <summary>
    /// 正序
    /// </summary>
    /// <param name="select"></param>
    /// <returns></returns>
    public GroupByMultiFetch Asc(Func<IGroupByView, IOrderView> select)
    {
        AscCore(select(_source));
        return this;
    }
    /// <summary>
    /// 倒序
    /// </summary>
    /// <param name="select"></param>
    /// <returns></returns>
    public GroupByMultiFetch Desc(Func<IGroupByView, IOrderAsc> select)
    {
        DescCore(select(_source));
        return this;
    }
    #endregion
    #region Aggregate
    /// <summary>
    /// 正序
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public GroupByMultiFetch AggregateAsc(string tableName, Func<IAliasTable, IAggregateField> select)
    {
        var member = _multiTable.From(tableName);
        AscCore(select(member));
        return this;
    }
    /// <summary>
    /// 倒序
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public GroupByMultiFetch AggregateDesc(string tableName, Func<IAliasTable, IAggregateField> select)
    {
        DescCore(select(_multiTable.From(tableName)));
        return this;
    }
    /// <summary>
    /// 正序
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="tableName">选择表</param>
    /// <param name="select">定位列</param>
    /// <param name="aggregate">聚合</param>
    /// <returns></returns>
    public GroupByMultiFetch AggregateAsc<TTable>(string tableName, Func<TTable, IColumn> select, Func<IColumn, IAggregateField> aggregate)
        where TTable : ITable
    {
        var member = _multiTable.Table<TTable>(tableName);
        //增加前缀
        if (member.GetPrefixColumn(select(member.Target)) is IPrefixColumn prefixColumn)
            AscCore(aggregate(prefixColumn));
        return this;
    }
    /// <summary>
    /// 倒序
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="tableName">选择表</param>
    /// <param name="select">定位列</param>
    /// <param name="aggregate">聚合</param>
    /// <returns></returns>
    public GroupByMultiFetch AggregateDesc<TTable>(string tableName, Func<TTable, IColumn> select, Func<IColumn, IAggregateField> aggregate)
        where TTable : ITable
    {
        var member = _multiTable.Table<TTable>(tableName);
        //增加前缀
        var prefixColumn = member.GetPrefixColumn(select(member.Target));
        if (prefixColumn is not null)
            DescCore(aggregate(prefixColumn));
        return this;
    }
    #endregion    
    #endregion 
}

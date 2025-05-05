using ShadowSql.Aggregates;
using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using System;

namespace ShadowSql.Cursors;

/// <summary>
/// 多(联)表分组后范围筛选游标
/// </summary>
public class GroupByMultiCursor : GroupByCursorBase
{
    /// <summary>
    /// 多(联)表分组后范围筛选
    /// </summary>
    /// <param name="source"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    public GroupByMultiCursor(GroupByMultiQuery source, int limit, int offset)
        : this(source, source.Source, limit, offset)
    {
    }
    /// <summary>
    /// 多(联)表分组后范围筛选
    /// </summary>
    /// <param name="source"></param>
    /// <param name="limit"></param>
    /// <param name="offset"></param>
    public GroupByMultiCursor(GroupByMultiSqlQuery source, int limit, int offset)
        : this(source, source.Source, limit, offset)
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
    #region 聚合排序
    #region TAliasTable
    /// <summary>
    /// 正序
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public GroupByMultiCursor AggregateAsc<TAliasTable>(string tableName, Func<TAliasTable, IAggregateField> select)
        where TAliasTable : IAliasTable
    {
        var member = _multiTable.From<TAliasTable>(tableName);
        AscCore(select(member));
        return this;
    }
    /// <summary>
    /// 倒序
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public GroupByMultiCursor AggregateDesc<TAliasTable>(string tableName, Func<TAliasTable, IAggregateField> select)
        where TAliasTable : IAliasTable
    {
        DescCore(select(_multiTable.From<TAliasTable>(tableName)));
        return this;
    }
    #endregion
    #region TTable
    /// <summary>
    /// 正序
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="tableName">选择表</param>
    /// <param name="select">定位列</param>
    /// <param name="aggregate">聚合</param>
    /// <returns></returns>
    public GroupByMultiCursor AggregateAsc<TTable>(string tableName, Func<TTable, IColumn> select, Func<IColumn, IAggregateField> aggregate)
        where TTable : ITable
    {
        var member = _multiTable.Alias<TTable>(tableName);
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
    public GroupByMultiCursor AggregateDesc<TTable>(string tableName, Func<TTable, IColumn> select, Func<IColumn, IAggregateField> aggregate)
        where TTable : ITable
    {
        var member = _multiTable.Alias<TTable>(tableName);
        //增加前缀
        var prefixColumn = member.GetPrefixColumn(select(member.Target));
        if (prefixColumn is not null)
            DescCore(aggregate(prefixColumn));
        return this;
    }
    #endregion    
    #endregion
}

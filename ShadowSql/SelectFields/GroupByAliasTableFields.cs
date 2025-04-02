using ShadowSql.Aggregates;
using ShadowSql.Cursors;
using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using ShadowSql.Variants;
using System;

namespace ShadowSql.SelectFields;

/// <summary>
/// 别名表分组字段筛选
/// </summary>
/// <typeparam name="TTable"></typeparam>
public class GroupByAliasTableFields<TTable>
    : SelectFieldsBase<ITableView>, ISelectFields
    where TTable : ITable
{
    /// <summary>
    /// 别名表分组字段筛选
    /// </summary>
    /// <param name="source"></param>
    public GroupByAliasTableFields(GroupByAliasTableQuery<TTable> source)
        : this(source, source, source.Source, source.Source.Target)
    {
    }
    /// <summary>
    /// 别名表分组字段筛选
    /// </summary>
    /// <param name="cursor"></param>
    public GroupByAliasTableFields(GroupByAliasTableCursor<TTable> cursor)
        : this(cursor, cursor.Source, cursor.AliasTable, cursor.Table)
    { 
    }
    /// <summary>
    /// 别名表分组字段筛选
    /// </summary>
    /// <param name="source"></param>
    public GroupByAliasTableFields(GroupByAliasTableSqlQuery<TTable> source)
        : this(source, source, source.Source, source.Source.Target)
    {
    }
    private GroupByAliasTableFields(ITableView source, IGroupByView groupBy, TableAlias<TTable> aliasTable, TTable table)
        : base(source)
    {
        _groupBy = groupBy;
        _aliasTable = aliasTable;
        _table = table;
    }
    #region 配置
    private readonly IGroupByView _groupBy;
    /// <summary>
    /// 分组查询
    /// </summary>
    public IGroupByView GroupBy
        => _groupBy;
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
    /// <summary>
    /// 筛选列
    /// </summary>
    /// <param name="select"></param>
    public GroupByAliasTableFields<TTable> Select(Func<IGroupByView, IField> select)
    {
        SelectCore(select(_groupBy));
        return this;
    }
    #region SelectAggregate
    /// <summary>
    /// 筛选列
    /// </summary>
    /// <param name="select"></param>
    /// <returns></returns>
    public GroupByAliasTableFields<TTable> SelectAggregate(Func<IAliasTable, IAggregateFieldAlias> select)
    {
        SelectCore(select(_aliasTable));
        return this;
    }
    /// <summary>
    /// 筛选列
    /// </summary>
    /// <param name="select"></param>
    /// <param name="aggregate"></param>
    /// <returns></returns>
    public GroupByAliasTableFields<TTable> SelectAggregate(Func<TTable, IColumn> select, Func<IColumn, IAggregateFieldAlias> aggregate)
    {
        if (_aliasTable.GetPrefixColumn(select(_table)) is IPrefixColumn prefixColumn)
            SelectCore(aggregate(prefixColumn));
        return this;
    }
    #endregion    
    #endregion
}

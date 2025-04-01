using ShadowSql.Aggregates;
using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using ShadowSql.Variants;
using System;

namespace ShadowSql.SelectFields;

/// <summary>
/// 别名表分组字段筛选
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="source"></param>
public class GroupByAliasTableFields<TTable>(GroupByAliasTable<TTable> source)
    : SelectFieldsBase<GroupByAliasTable<TTable>>(source), ISelectFields
    where TTable : ITable
{
    #region 配置
    private readonly TableAlias<TTable> _aliasTable = source.Source;
    /// <summary>
    /// 别名表
    /// </summary>
    public TableAlias<TTable> AliasTable
        => _aliasTable;
    private readonly TTable _table = source.Table;
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
        SelectCore(select(_source));
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

using ShadowSql.Aggregates;
using ShadowSql.Fetches;
using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using System;

namespace ShadowSql.SelectFields;

/// <summary>
/// 多(联)表分组后字段筛选
/// </summary>
public class GroupByMultiFields
    : SelectFieldsBase<IGroupByView>, ISelectFields
{
    /// <summary>
    /// 多(联)表分组后字段筛选
    /// </summary>
    /// <param name="source"></param>
    public GroupByMultiFields(GroupByMultiQuery source)
        : this(source, source.Source)
    {
    }
    /// <summary>
    /// 多(联)表分组后字段筛选
    /// </summary>
    /// <param name="source"></param>
    public GroupByMultiFields(GroupByMultiSqlQuery source)
        : this(source, source.Source)
    {
    }
    /// <summary>
    /// 多(联)表分组后字段筛选
    /// </summary>
    /// <param name="fetch"></param>
    public GroupByMultiFields(GroupByMultiFetch fetch)
        : this(fetch.Source, fetch.MultiTable)
    {
    }
    private GroupByMultiFields(IGroupByView groupBy, IMultiView multiTable)
        : base(groupBy)
    {
        _multiTable = multiTable;
    }
    #region 配置
    private readonly IMultiView _multiTable;
    /// <summary>
    /// 多(联)表
    /// </summary>
    public IMultiView MultiTable
        => _multiTable;
    #endregion
    #region 功能
    /// <summary>
    /// 筛选列
    /// </summary>
    /// <param name="select"></param>
    public GroupByMultiFields Select(Func<IGroupByView, IField> select)
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
    public GroupByMultiFields SelectAggregate(Func<IMultiView, IAggregateFieldAlias> select)
    {
        SelectCore(select(_multiTable));
        return this;
    }
    /// <summary>
    /// 筛选列
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public GroupByMultiFields SelectAggregate(string tableName, Func<IAliasTable, IAggregateFieldAlias> select)
    {
        SelectCore(select(_multiTable.From(tableName)));
        return this;
    }
    /// <summary>
    /// 筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="tableName"></param>
    /// <param name="select"></param>
    /// <param name="aggregate"></param>
    /// <returns></returns>
    public GroupByMultiFields SelectAggregate<TTable>(string tableName, Func<TTable, IColumn> select, Func<IColumn, IAggregateFieldAlias> aggregate)
        where TTable : ITable
    {
        var member = _multiTable.Table<TTable>(tableName);
        //增加前缀
        if (member.GetPrefixColumn(select(member.Target)) is IPrefixColumn prefixColumn)
            SelectCore(aggregate(prefixColumn));
        return this;
    }
    #endregion
    ///// <summary>
    ///// 筛选列
    ///// </summary>
    ///// <param name="field"></param>
    //public GroupByMultiFields Select(IField field)
    //{
    //    SelectCore(field);
    //    return this;
    //}
    ///// <summary>
    ///// 筛选列
    ///// </summary>
    ///// <param name="columns"></param>
    ///// <returns></returns>
    //public GroupByMultiFields Select(params IEnumerable<string> columns)
    //{
    //    SelectCore(columns);
    //    return this;
    //}
    ///// <summary>
    ///// 筛选别名
    ///// </summary>
    ///// <param name="alias"></param>
    ///// <param name="statement"></param>
    ///// <returns></returns>
    //public GroupByMultiFields Alias(string alias, string statement)
    //{
    //    AliasCore(alias, statement);
    //    return this;
    //}
    #endregion
}

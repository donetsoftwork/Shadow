using ShadowSql.Aggregates;
using ShadowSql.Cursors;
using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using System;

namespace ShadowSql.SelectFields;

/// <summary>
/// 表分组后字段筛选
/// </summary>
/// <typeparam name="TTable"></typeparam>
public class GroupByTableFields<TTable>
    : SelectFieldsBase<ITableView>, ISelectFields
    where TTable : ITable
{
    /// <summary>
    /// 表分组后字段筛选
    /// </summary>
    /// <param name="source"></param>
    public GroupByTableFields(GroupByTableQuery<TTable> source)
        : this(source, source, source.Source)
    {
    }
    /// <summary>
    /// 表分组后字段筛选
    /// </summary>
    /// <param name="source"></param>
    public GroupByTableFields(GroupByTableSqlQuery<TTable> source)
        : this(source, source, source.Source)
    {
    }
    /// <summary>
    /// 表分组后字段筛选
    /// </summary>
    /// <param name="cursor"></param>
    public GroupByTableFields(GroupByTableCursor<TTable> cursor)
    : this(cursor.Source, cursor.Source, cursor.Table)
    {
    }
    /// <summary>
    /// 表分组后字段筛选
    /// </summary>
    /// <param name="source"></param>
    /// <param name="groupBy"></param>
    /// <param name="table"></param>
    private GroupByTableFields(ITableView source, IGroupByView groupBy, TTable table)
        : base(source)
    {
        _groupBy = groupBy;
        _table = table;
    }
    #region 配置
    private readonly IGroupByView _groupBy;
    /// <summary>
    /// 分组查询
    /// </summary>
    public IGroupByView GroupBy
        => _groupBy;
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
    public GroupByTableFields<TTable> Select(Func<IGroupByView, IFieldView> select)
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
    public GroupByTableFields<TTable> SelectAggregate(Func<TTable, IAggregateFieldAlias> select)
    {
        SelectCore(select(_table));
        return this;
    }
    #endregion
    ///// <summary>
    ///// 筛选列
    ///// </summary>
    ///// <param name="field"></param>
    //public GroupByTableFields<TTable> Select(IField field)
    //{
    //    SelectCore(field);
    //    return this;
    //}
    ///// <summary>
    ///// 筛选列
    ///// </summary>
    ///// <param name="columns"></param>
    ///// <returns></returns>
    //public GroupByTableFields<TTable> Select(params IEnumerable<string> columns)
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
    //public GroupByTableFields<TTable> Alias(string alias, string statement)
    //{
    //    AliasCore(alias, statement);
    //    return this;
    //}
    #endregion
}

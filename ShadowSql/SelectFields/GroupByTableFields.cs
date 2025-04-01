using ShadowSql.Aggregates;
using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using System;

namespace ShadowSql.SelectFields;

/// <summary>
/// 表分组后字段筛选
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="source"></param>
public class GroupByTableFields<TTable>(GroupByTable<TTable> source)
    : SelectFieldsBase<GroupByTable<TTable>>(source), ISelectFields
    where TTable : ITable
{
    #region 配置
    private readonly TTable _table = source.Source;
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
        SelectCore(select(_source));
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

using ShadowSql.Identifiers;
using System.Collections.Generic;
using System;

namespace ShadowSql.Select;

/// <summary>
/// 多联表视图筛选列
/// </summary>
/// <param name="source"></param>
/// <param name="multiTable"></param>
public class MultiTableSelect(ITableView source, IMultiView multiTable)
    : MultiSelectBase<ITableView>(source, multiTable)
{
    /// <summary>
    /// 多联表视图筛选列
    /// </summary>
    /// <param name="source"></param>
    public MultiTableSelect(IMultiView source)
        : this(source, source)
    {
    }
    #region IColumn
    /// <summary>
    /// 筛选列
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public MultiTableSelect Select<TTable>(string tableName, Func<TTable, IColumn> select)
        where TTable : ITable
    {
        SelectCore(tableName, select);
        return this;
    }
    /// <summary>
    /// 筛选多列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="tableName"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public MultiTableSelect Select<TTable>(string tableName, Func<TTable, IEnumerable<IColumn>> select)
        where TTable : ITable
    {
        SelectCore(tableName, select);
        return this;
    }
    #endregion
    #region IFieldView
    /// <summary>
    /// 筛选列
    /// </summary>
    /// <typeparam name="TAliasTable"></typeparam>
    /// <param name="tableName"></param>
    /// <param name="select"></param>
    public MultiTableSelect Select<TAliasTable>(string tableName, Func<TAliasTable, IFieldView> select)
        where TAliasTable : IAliasTable
    {
        SelectCore(select(_target.From<TAliasTable>(tableName)));
        return this;
    }
    /// <summary>
    /// 筛选多列
    /// </summary>
    /// <typeparam name="TAliasTable"></typeparam>
    /// <param name="tableName"></param>
    /// <param name="select"></param>
    public MultiTableSelect Select<TAliasTable>(string tableName, Func<TAliasTable, IEnumerable<IFieldView>> select)
        where TAliasTable : IAliasTable
    {
        SelectCore(select(_target.From<TAliasTable>(tableName)));
        return this;
    }
    #endregion
}

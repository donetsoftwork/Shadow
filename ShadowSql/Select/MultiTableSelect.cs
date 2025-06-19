using ShadowSql.Identifiers;
using System.Collections.Generic;
using System;

namespace ShadowSql.Select;

/// <summary>
/// 多联表视图筛选列
/// </summary>
/// <param name="view"></param>
/// <param name="multiTable">多表(联表)</param>
public class MultiTableSelect(ITableView view, IMultiView multiTable)
    : MultiSelectBase<ITableView>(view, multiTable)
{
    /// <summary>
    /// 多联表视图筛选列
    /// </summary>
    /// <param name="multiView">多(联)表</param>
    public MultiTableSelect(IMultiView multiView)
        : this(multiView, multiView)
    {
    }
    #region IColumn
    /// <summary>
    /// 筛选列
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="select">筛选</param>
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
    /// <param name="tableName">表名</param>
    /// <param name="select">筛选</param>
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
    /// <param name="tableName">表名</param>
    /// <param name="select">筛选</param>
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
    /// <param name="tableName">表名</param>
    /// <param name="select">筛选</param>
    public MultiTableSelect Select<TAliasTable>(string tableName, Func<TAliasTable, IEnumerable<IFieldView>> select)
        where TAliasTable : IAliasTable
    {
        SelectCore(select(_target.From<TAliasTable>(tableName)));
        return this;
    }
    #endregion
}

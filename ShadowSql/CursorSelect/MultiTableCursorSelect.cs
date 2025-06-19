using ShadowSql.Cursors;
using System.Collections.Generic;
using System;
using ShadowSql.Identifiers;

namespace ShadowSql.CursorSelect;

/// <summary>
/// 多表视图范围(分页)及列筛选
/// </summary>
/// <param name="cursor">游标</param>
public class MultiTableCursorSelect(MultiTableCursor cursor)
    : MultiCursorSelectBase(cursor, cursor.Source)
{
    #region TTable
    /// <summary>
    /// 筛选列
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public MultiTableCursorSelect Select<TTable>(string tableName, Func<TTable, IColumn> select)
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
    public MultiTableCursorSelect Select<TTable>(string tableName, Func<TTable, IEnumerable<IColumn>> select)
        where TTable : ITable
    {
        SelectCore(tableName, select);
        return this;
    }
    #endregion
    #region TAliasTable
    /// <summary>
    /// 筛选列
    /// </summary>
    /// <typeparam name="TAliasTable"></typeparam>
    /// <param name="tableName">表名</param>
    /// <param name="select">筛选</param>
    public MultiTableCursorSelect Select<TAliasTable>(string tableName, Func<TAliasTable, IFieldView> select)
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
    public MultiTableCursorSelect Select<TAliasTable>(string tableName, Func<TAliasTable, IEnumerable<IFieldView>> select)
        where TAliasTable : IAliasTable
    {
        SelectCore(select(_target.From<TAliasTable>(tableName)));
        return this;
    }
    #endregion
}
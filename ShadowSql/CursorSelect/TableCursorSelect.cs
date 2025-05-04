using ShadowSql.Cursors;
using ShadowSql.Identifiers;
using System.Collections.Generic;
using System;

namespace ShadowSql.CursorSelect;

/// <summary>
/// 表范围(分页)及列筛选
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="cursor"></param>
public sealed class TableCursorSelect<TTable>(TableCursor<TTable> cursor)
    : CursorSelectBase<TTable>(cursor, cursor.Source)
    where TTable : ITable
{
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <param name="select"></param>
    /// <returns></returns>
    public TableCursorSelect<TTable> Select(Func<TTable, IFieldView> select)
    {
        SelectCore(select(_target));
        return this;
    }
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <param name="select"></param>
    /// <returns></returns>
    public TableCursorSelect<TTable> Select(Func<TTable, IEnumerable<IFieldView>> select)
    {
        foreach (var field in select(_target))
            SelectCore(field);
        return this;
    }
}
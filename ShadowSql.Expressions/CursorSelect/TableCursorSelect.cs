using ShadowSql.Expressions.Cursors;
using ShadowSql.Expressions.VisitSource;
using ShadowSql.Identifiers;
using System;
using System.Linq.Expressions;

namespace ShadowSql.Expressions.CursorSelect;

/// <summary>
/// 表范围(分页)及列筛选
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <param name="cursor"></param>
public sealed class TableCursorSelect<TEntity>(TableCursor<TEntity> cursor)
    : CursorSelectBase<ITableView>(cursor, cursor.Source)
{
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <param name="select"></param>
    /// <returns></returns>
    public TableCursorSelect<TEntity> Select<T>(Expression<Func<TEntity, T>> select)
    {
        TableVisitor.Select(_selected, select, _target);
        return this;
    }
}
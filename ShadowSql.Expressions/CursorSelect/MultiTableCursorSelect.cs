using ShadowSql.Expressions.Cursors;
using ShadowSql.Expressions.VisitSource;
using System;
using System.Linq.Expressions;

namespace ShadowSql.Expressions.CursorSelect;

/// <summary>
/// 多表视图范围(分页)及列筛选
/// </summary>
/// <param name="cursor">游标</param>
public class MultiTableCursorSelect(MultiTableCursor cursor)
    : MultiCursorSelectBase(cursor, cursor.Source)
{
    /// <summary>
    /// 从其中一个表筛选
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="tableName">表名</param>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public MultiTableCursorSelect Select<TEntity, TProperty>(string tableName, Expression<Func<TEntity, TProperty>> select)
    {
        TableVisitor.Select(_target.From(tableName), _selected, select);
        return this;
    }
}
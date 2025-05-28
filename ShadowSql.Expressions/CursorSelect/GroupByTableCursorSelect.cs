using ShadowSql.Expressions.Cursors;
using ShadowSql.Expressions.VisitSource;
using ShadowSql.Identifiers;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace ShadowSql.Expressions.CursorSelect;

/// <summary>
/// GroupBy后再范围(分页)及列筛选
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TEntity"></typeparam>
/// <param name="cursor"></param>
public class GroupByTableCursorSelect<TKey, TEntity>(GroupByTableCursor<TKey, TEntity> cursor)
    : GroupCursorBySelectBase<ITableView>(cursor, cursor.Source, cursor.Table)
{
    /// <summary>
    /// 筛选分组列
    /// </summary>
    /// <param name="select"></param>
    /// <returns></returns>
    public GroupByTableCursorSelect<TKey, TEntity> Select<TProperty>(Expression<Func<TKey, TProperty>> select)
    {
        GroupByKeyVisitor.Select(_target, _selected, select);
        return this;
    }
    /// <summary>
    /// 从聚合筛选
    /// </summary>
    /// <param name="select"></param>
    /// <returns></returns>
    public GroupByTableCursorSelect<TKey, TEntity> Select<TProperty>(Expression<Func<IGrouping<TKey, TEntity>, TProperty>> select)
    {
        GroupByVisitor.Select(_target, _selected, select);
        return this;
    }
}

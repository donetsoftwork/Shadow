using ShadowSql.Cursors;
using ShadowSql.Engines;
using ShadowSql.Expressions.Cursors;
using ShadowSql.Expressions.Select;
using ShadowSql.Expressions.VisitSource;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ShadowSql.Expressions.CursorSelect;

/// <summary>
/// GroupBy后再范围(分页)及列筛选
/// </summary>
/// <param name="cursor"></param>
public sealed class GroupByMultiCursorSelect<TKey>(GroupByMultiCursor<TKey> cursor)
    : GroupByMultiSelectBase<ICursor>(cursor, cursor.Source, cursor.MultiTable)
{

    #region ISqlEntity
    /// <summary>
    /// 拼写分页sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    protected override void WriteCore(ISqlEngine engine, StringBuilder sql)
        => engine.SelectCursor(sql, this, _source);
    #endregion
    /// <summary>
    /// 筛选分组列
    /// </summary>
    /// <param name="select"></param>
    /// <returns></returns>
    public GroupByMultiCursorSelect<TKey> Select<TProperty>(Expression<Func<TKey, TProperty>> select)
    {
        GroupByKeyVisitor.Select(_target, _selected, select);
        return this;
    }
    /// <summary>
    /// 从聚合筛选
    /// </summary>
    /// <param name="select"></param>
    /// <returns></returns>
    public GroupByMultiCursorSelect<TKey> Select<TEntity, TProperty>(Expression<Func<IGrouping<TKey, TEntity>, TProperty>> select)
    {
        GroupByVisitor.Select(_target, _groupSource, _selected, select);
        return this;
    }
    /// <summary>
    /// 从聚合筛选
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="table"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public GroupByMultiCursorSelect<TKey> Select<TEntity, TProperty>(string table, Expression<Func<IGrouping<TKey, TEntity>, TProperty>> select)
    {
        GroupByVisitor.Select(_target, _groupSource.From(table), _selected, select);
        return this;
    }
}

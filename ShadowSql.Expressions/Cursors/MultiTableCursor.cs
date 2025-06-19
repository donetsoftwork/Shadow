using ShadowSql.Expressions.VisitSource;
using ShadowSql.Identifiers;
using System;
using System.Linq.Expressions;

namespace ShadowSql.Expressions.Cursors;

/// <summary>
/// 多联表范围筛选游标
/// </summary>
/// <param name="multiView">多(联)表</param>
/// <param name="limit">筛选数量</param>
/// <param name="offset">跳过数量</param>
public class MultiTableCursor(IMultiView multiView, int limit = 0, int offset = 0)
    : CursorBase<IMultiView>(multiView, limit, offset)
{
    #region 排序
    /// <summary>
    /// 正序
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public MultiTableCursor Asc<TEntity, TProperty>(string tableName, Expression<Func<TEntity, TProperty>> select)
    {
        var fields = TableVisitor.GetFieldsByExpression(_source.From(tableName), select);
        foreach (var field in fields)
            AscCore(field);
        return this;
    }
    /// <summary>
    /// 倒序
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public MultiTableCursor Desc<TEntity, TProperty>(string tableName, Expression<Func<TEntity, TProperty>> select)
    {
        var fields = TableVisitor.GetFieldsByExpression(_source.From(tableName), select);
        foreach (var field in fields)
            DescCore(field);
        return this;
    }
    #endregion
}

using ShadowSql.Expressions.VisitSource;
using ShadowSql.Identifiers;
using System;
using System.Linq.Expressions;

namespace ShadowSql.Expressions.Select;

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
    /// <summary>
    /// 从其中一个表筛选
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="tableName">表名</param>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public MultiTableSelect Select<TEntity, TProperty>(string tableName, Expression<Func<TEntity, TProperty>> select)
    {
        TableVisitor.Select(_target.From(tableName), _selected, select);
        return this;
    }
}

using ShadowSql.Expressions.VisitSource;
using ShadowSql.Identifiers;
using System;
using System.Linq.Expressions;

namespace ShadowSql.Expressions.Select;

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
    /// <summary>
    /// 从其中一个表筛选
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="tableName"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public MultiTableSelect Select<TEntity, TProperty>(string tableName, Expression<Func<TEntity, TProperty>> select)
    {
        TableVisitor.Select(_target.From(tableName), _selected, select);
        return this;
    }
}

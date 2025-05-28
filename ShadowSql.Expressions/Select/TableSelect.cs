using ShadowSql.Expressions.AliasTables;
using ShadowSql.Expressions.Tables;
using ShadowSql.Expressions.VisitSource;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Tables;
using System;
using System.Linq.Expressions;

namespace ShadowSql.Expressions.Select;

/// <summary>
/// 表筛选列
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public sealed class TableSelect<TEntity> : SelectBase<ITableView, ITableView>
{
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <param name="view"></param>
    /// <param name="table"></param>
    internal TableSelect(ITableView view, ITableView table)
        : base(view, table)
    {
    }
    #region ITable
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <param name="table"></param>
    public TableSelect(ITable table)
        : base(table, table)
    {
    }
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <param name="table"></param>
    /// <param name="where"></param>
    public TableSelect(ITable table, ISqlLogic where)
        : base(new TableFilter(table, where), table)
    {
    }
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <param name="query"></param>
    public TableSelect(TableQuery<TEntity> query)
        : base(query, query.Source)
    {
    }
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <param name="query"></param>
    public TableSelect(TableSqlQuery<TEntity> query)
        : base(query, query.Source)
    {
    }
    #endregion
    #region IAliasTable
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <param name="table"></param>
    public TableSelect(IAliasTable table)
        : base(table, table)
    {
    }
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <param name="table"></param>
    /// <param name="where"></param>
    public TableSelect(IAliasTable table, ISqlLogic where)
        : base(new TableFilter(table, where), table)
    {
    }
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <param name="query"></param>
    public TableSelect(AliasTableQuery<TEntity> query)
        : base(query, query.Source)
    {
    }
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <param name="query"></param>
    public TableSelect(AliasTableSqlQuery<TEntity> query)
        : base(query, query.Source)
    {
    }
    #endregion
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <param name="select"></param>
    /// <returns></returns>
    public TableSelect<TEntity> Select<T>(Expression<Func<TEntity, T>> select)
    {
        TableVisitor.Select(_selected, select, _target);
        return this;
    }
}

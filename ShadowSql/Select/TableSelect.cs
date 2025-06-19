using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Tables;
using System;
using System.Collections.Generic;

namespace ShadowSql.Select;

/// <summary>
/// 表筛选列
/// </summary>
/// <typeparam name="TTable"></typeparam>
public sealed class TableSelect<TTable> : SelectBase<ITableView, TTable>
    where TTable : ITable
{
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <param name="view"></param>
    /// <param name="table">表</param>
    internal TableSelect(ITableView view, TTable table)
        : base(view, table)
    {
    }
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <param name="table">表</param>
    public TableSelect(TTable table)
        : base(table, table)
    {
    }
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="where">查询条件</param>
    public TableSelect(TTable table, ISqlLogic where)
        : base(new TableFilter(table, where), table)
    {
    }
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <param name="query">查询</param>
    public TableSelect(TableQuery<TTable> query)
        : base(query, query.Source)
    {
    }
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <param name="query">查询</param>
    public TableSelect(TableSqlQuery<TTable> query)
        : base(query, query.Source)
    {
    }
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public TableSelect<TTable> Select(Func<TTable, IFieldView> select)
    {
        SelectCore(select(_target));
        return this;
    }
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public TableSelect<TTable> Select(Func<TTable, IEnumerable<IFieldView>> select)
    {
        foreach (var field in select(_target))
            SelectCore(field);
        return this;
    }
}

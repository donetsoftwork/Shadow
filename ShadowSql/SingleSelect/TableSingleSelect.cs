using ShadowSql.Engines;
using ShadowSql.Fetches;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Queries;
using ShadowSql.SelectFields;
using System.Text;

namespace ShadowSql.SingleSelect;

/// <summary>
/// 表筛选单列
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="view"></param>
/// <param name="fields"></param>
public sealed class TableSingleSelect<TTable>(ITableView view, TableFields<TTable> fields)
    : SingleSelectBase<ITableView, TableFields<TTable>>(view, fields)
    where TTable : ITable
{
    /// <summary>
    /// 表筛选单列
    /// </summary>
    /// <param name="table"></param>
    public TableSingleSelect(TTable table)
        : this(table, new TableFields<TTable>(table))
    {
    }
    /// <summary>
    /// 表筛选单列
    /// </summary>
    /// <param name="table"></param>
    /// <param name="where"></param>
    public TableSingleSelect(TTable table, ISqlLogic where)
        : this(new TableFilter<TTable>(table, where), new TableFields<TTable>(table))
    {
    }
    /// <summary>
    /// 表筛选单列
    /// </summary>
    /// <param name="filter"></param>
    public TableSingleSelect(TableFilter<TTable> filter)
        : this(filter, new TableFields<TTable>(filter.Source))
    {
    }
    /// <summary>
    /// 表筛选单列
    /// </summary>
    /// <param name="query"></param>
    public TableSingleSelect(TableQuery<TTable> query)
        : this(query, new TableFields<TTable>(query.Source))
    {
    }
}
///// <summary>
///// 表过滤筛选列
///// </summary>
///// <typeparam name="TTable"></typeparam>
///// <param name="filter"></param>
//public sealed class TableFilterSingleSelect<TTable>(TableFilter<TTable> filter)
//    : SingleSelectBase<ITableView, TableFields<TTable>>(filter, new TableFields<TTable>(filter.Source))
//    where TTable : ITable
//{
//}
///// <summary>
///// 表(及查询)筛选列
///// </summary>
///// <typeparam name="TTable"></typeparam>
///// <param name="query"></param>
//public sealed class TableQuerySingleSelect<TTable>(TableQuery<TTable> query)
//    : SingleSelectBase<ITableView, TableFields<TTable>>(query, new TableFields<TTable>(query.Source))
//    where TTable : ITable
//{
//}
/// <summary>
/// 表范围(分页)及单列筛选
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="fetch"></param>
/// <param name="fields"></param>
public sealed class TableFetchSingleSelect<TTable>(TableFetch<TTable> fetch, TableFields<TTable> fields)
    : SingleSelectBase<TableFetch<TTable>, TableFields<TTable>>(fetch, fields)
    where TTable : ITable
{
    /// <summary>
    /// 表范围(分页)及单列筛选
    /// </summary>
    /// <param name="fetch"></param>
    public TableFetchSingleSelect(TableFetch<TTable> fetch)
        : this(fetch, new TableFields<TTable>(fetch.Source))
    {
    }
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    public override void Write(ISqlEngine engine, StringBuilder sql)
        => engine.SelectCursor(sql, this, _source);
}

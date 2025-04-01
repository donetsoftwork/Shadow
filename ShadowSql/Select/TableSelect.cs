using ShadowSql.Engines;
using ShadowSql.Fetches;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Queries;
using ShadowSql.SelectFields;
using System.Text;

namespace ShadowSql.Select;

/// <summary>
/// 表筛选列
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="view"></param>
/// <param name="fields"></param>
public sealed class TableSelect<TTable>(ITableView view, TableFields<TTable> fields)
    : SelectBase<ITableView, TableFields<TTable>>(view, fields)
    where TTable : ITable
{
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <param name="table"></param>
    public TableSelect(TTable table)
        : this(table, new TableFields<TTable>(table))
    {
    }
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <param name="table"></param>
    /// <param name="where"></param>
    public TableSelect(TTable table, ISqlLogic where)
        : this(new TableFilter<TTable>(table, where), new TableFields<TTable>(table))
    {
    }
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <param name="filter"></param>
    public TableSelect(TableFilter<TTable> filter)
        : this(filter, new TableFields<TTable>(filter.Source))
    {
    }
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <param name="query"></param>
    public TableSelect(TableQuery<TTable> query)
        : this(query, new TableFields<TTable>(query.Source))
    {
    }
}
///// <summary>
///// 表过滤筛选列
///// </summary>
///// <typeparam name="TTable"></typeparam>
///// <param name="filter"></param>
//public sealed class TableFilterSelect<TTable>(TableFilter<TTable> filter)
//    : SelectBase<ITableView, TableFields<TTable>>(filter, new TableFields<TTable>(filter.Source))
//    where TTable : ITable
//{
//}
///// <summary>
///// 表(及查询)筛选列
///// </summary>
///// <typeparam name="TTable"></typeparam>
///// <param name="query"></param>
//public sealed class TableQuerySelect<TTable>(TableQuery<TTable> query)
//    : SelectBase<ITableView, TableFields<TTable>>(query, new TableFields<TTable>(query.Source))
//    where TTable : ITable
//{
//}
/// <summary>
/// 表范围(分页)及列筛选
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="fetch"></param>
/// <param name="fields"></param>
public sealed class TableFetchSelect<TTable>(TableFetch<TTable> fetch, TableFields<TTable> fields)
    : SelectBase<TableFetch<TTable>, TableFields<TTable>>(fetch, fields)
    where TTable : ITable
{
    /// <summary>
    /// 表范围(分页)及列筛选
    /// </summary>
    /// <param name="fetch"></param>
    public TableFetchSelect(TableFetch<TTable> fetch)
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
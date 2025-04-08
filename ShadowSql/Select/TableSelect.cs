using ShadowSql.Cursors;
using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.SelectFields;
using ShadowSql.Tables;
using System.Text;

namespace ShadowSql.Select;

/// <summary>
/// 表筛选列
/// </summary>
/// <typeparam name="TTable"></typeparam>
public sealed class TableSelect<TTable>
    : SelectBase<ITableView, TableFields<TTable>>
    where TTable : ITable
{
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <param name="view"></param>
    /// <param name="fields"></param>
    internal TableSelect(ITableView view, TableFields<TTable> fields)
        : base(view, fields)
    {
    }
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
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <param name="query"></param>
    public TableSelect(TableSqlQuery<TTable> query)
        : this(query, new TableFields<TTable>(query.Source))
    {
    }
}
/// <summary>
/// 表范围(分页)及列筛选
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="cursor"></param>
/// <param name="fields"></param>
public sealed class TableCursorSelect<TTable>(TableCursor<TTable> cursor, TableFields<TTable> fields)
    : SelectBase<TableCursor<TTable>, TableFields<TTable>>(cursor, fields)
    where TTable : ITable
{
    /// <summary>
    /// 表范围(分页)及列筛选
    /// </summary>
    /// <param name="cursor"></param>
    public TableCursorSelect(TableCursor<TTable> cursor)
        : this(cursor, new TableFields<TTable>(cursor.Source))
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
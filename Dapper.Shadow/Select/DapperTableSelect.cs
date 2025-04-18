﻿using ShadowSql.Cursors;
using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Select;
using ShadowSql.SelectFields;
using ShadowSql.Tables;
using System.Text;

namespace Dapper.Shadow.Select;

/// <summary>
/// Dapper表筛选列
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="executor"></param>
/// <param name="table"></param>
/// <param name="fields"></param>
public class DapperTableSelect<TTable>(IExecutor executor, ITableView table, TableFields<TTable> fields)
    : SelectBase<ITableView, TableFields<TTable>>(table, fields)
    , IDapperSelect
    where TTable : ITable
{
    /// <summary>
    /// Dapper表筛选列
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="table"></param>
    public DapperTableSelect(IExecutor executor, TTable table)
        : this(executor, table, new TableFields<TTable>(table))
    {
    }
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="table"></param>
    /// <param name="where"></param>
    public DapperTableSelect(IExecutor executor, TTable table, ISqlLogic where)
        : this(executor, new TableFilter(table, where), new TableFields<TTable>(table))
    {
    }
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="query"></param>
    public DapperTableSelect(IExecutor executor, TableSqlQuery<TTable> query)
        : this(executor, query, new TableFields<TTable>(query.Source))
    {
    }
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="query"></param>
    public DapperTableSelect(IExecutor executor, TableQuery<TTable> query)
        : this(executor, query, new TableFields<TTable>(query.Source))
    {
    }
    #region 配置
    private readonly IExecutor _executor = executor;
    /// <summary>
    /// 执行器
    /// </summary>
    public IExecutor Executor
        => _executor;
    #endregion
}
/// <summary>
/// 表范围(分页)及列筛选
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="executor"></param>
/// <param name="cursor"></param>
/// <param name="fields"></param>
public sealed class DapperTableCursorSelect<TTable>(IExecutor executor, TableCursor<TTable> cursor, TableFields<TTable> fields)
    : SelectBase<ICursor, TableFields<TTable>>(cursor, fields)
    , IDapperSelect
    where TTable : ITable
{
    /// <summary>
    /// 表范围(分页)及列筛选
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="cursor"></param>
    public DapperTableCursorSelect(IExecutor executor, TableCursor<TTable> cursor)
        : this(executor, cursor, new TableFields<TTable>(cursor.Source))
    {
    }
    #region 配置
    private readonly IExecutor _executor = executor;
    /// <summary>
    /// 执行器
    /// </summary>
    public IExecutor Executor
        => _executor;
    #endregion
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    public override void Write(ISqlEngine engine, StringBuilder sql)
        => engine.SelectCursor(sql, this, _source);
}

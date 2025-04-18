﻿using ShadowSql.Cursors;
using ShadowSql.Engines;
using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using ShadowSql.Select;
using ShadowSql.SelectFields;
using ShadowSql.SingleSelect;
using System.Text;

namespace Dapper.Shadow.SingleSelect;

/// <summary>
/// GroupBy后再筛选列
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="executor"></param>
/// <param name="groupBy"></param>
/// <param name="fields"></param>
public class DapperGroupByTableSingleSelect<TTable>(IExecutor executor, IGroupByView groupBy, GroupByTableFields<TTable> fields)
    : SingleSelectBase<IGroupByView, GroupByTableFields<TTable>>(groupBy, fields)
    , IDapperSingleSelect
    where TTable : ITable
{
    /// <summary>
    /// GroupBy后再筛选列
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="groupBy"></param>
    public DapperGroupByTableSingleSelect(IExecutor executor, GroupByTableSqlQuery<TTable> groupBy)
        : this(executor, groupBy, new GroupByTableFields<TTable>(groupBy))
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
/// GroupBy后再范围(分页)及列筛选
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="executor"></param>
/// <param name="cursor"></param>
/// <param name="fields"></param>
public class DapperGroupByTableCursorSingleSelect<TTable>(IExecutor executor, ICursor cursor, GroupByTableFields<TTable> fields)
    : SingleSelectBase<ICursor, GroupByTableFields<TTable>>(cursor, fields)
    , IDapperSingleSelect
    where TTable : ITable
{
    /// <summary>
    /// GroupBy后再范围(分页)及列筛选
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="cursor"></param>
    public DapperGroupByTableCursorSingleSelect(IExecutor executor, GroupByTableCursor<TTable> cursor)
        : this(executor, cursor, new GroupByTableFields<TTable>(cursor))
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

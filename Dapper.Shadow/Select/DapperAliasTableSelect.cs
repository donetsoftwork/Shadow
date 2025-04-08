using ShadowSql.AliasTables;
using ShadowSql.Cursors;
using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Select;
using ShadowSql.SelectFields;
using ShadowSql.Tables;
using ShadowSql.Variants;
using System.Text;

namespace Dapper.Shadow.Select;

/// <summary>
/// 别名表筛选列
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="executor"></param>
/// <param name="source"></param>
/// <param name="fields"></param>
public class DapperAliasTableSelect<TTable>(IExecutor executor, ITableView source, AliasTableFields<TTable> fields)
    : SelectBase<ITableView, AliasTableFields<TTable>>(source, fields)
    , IDapperSelect
    where TTable : ITable
{
    /// <summary>
    /// 别名表筛选列
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="source"></param>
    public DapperAliasTableSelect(IExecutor executor, TableAlias<TTable> source)
        : this(executor, source, new AliasTableFields<TTable>(source))
    {
    }
    /// <summary>
    /// 别名表筛选列
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="source"></param>
    /// <param name="where"></param>
    public DapperAliasTableSelect(IExecutor executor, TableAlias<TTable> source, ISqlLogic where)
        : this(executor, new TableFilter<TableAlias<TTable>>(source, where), new AliasTableFields<TTable>(source))
    {
    }
    /// <summary>
    /// 别名表过滤筛选列
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="filter"></param>
    public DapperAliasTableSelect(IExecutor executor, TableFilter<TableAlias<TTable>> filter)
        : this(executor, filter, new AliasTableFields<TTable>(filter.Source))
    {
    }
    /// <summary>
    /// 别名表过滤筛选列
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="query"></param>
    public DapperAliasTableSelect(IExecutor executor, AliasTableSqlQuery<TTable> query)
        : this(executor, query, new AliasTableFields<TTable>(query.Source))
    {
    }
    /// <summary>
    /// 别名表过滤筛选列
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="query"></param>
    public DapperAliasTableSelect(IExecutor executor, AliasTableQuery<TTable> query)
        : this(executor, query, new AliasTableFields<TTable>(query.Source))
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
/// 别名表范围(分页)及列筛选
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="executor"></param>
/// <param name="cursor"></param>
/// <param name="fields"></param>
public class DapperAliasTableCursorSelect<TTable>(IExecutor executor, ICursor cursor, AliasTableFields<TTable> fields)
    : SelectBase<ICursor, AliasTableFields<TTable>>(cursor, fields)
    , IDapperSelect
    where TTable : ITable
{
    /// <summary>
    /// 别名表范围(分页)及列筛选
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="cursor"></param>
    public DapperAliasTableCursorSelect(IExecutor executor, AliasTableCursor<TTable> cursor)
        : this(executor, cursor, new AliasTableFields<TTable>(cursor.Source))
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

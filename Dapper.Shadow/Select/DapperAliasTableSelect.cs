using ShadowSql.AliasTables;
using ShadowSql.Cursors;
using ShadowSql.CursorSelect;
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
/// <param name="target"></param>
public class DapperAliasTableSelect<TTable>(IExecutor executor, ITableView source, TableAlias<TTable> target)
    : SelectBase<ITableView, TableAlias<TTable>>(source, target)
    , IDapperSelect
    where TTable : ITable
{
    /// <summary>
    /// 别名表筛选列
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="source"></param>
    public DapperAliasTableSelect(IExecutor executor, TableAlias<TTable> source)
        : this(executor, source, source)
    {
    }
    /// <summary>
    /// 别名表筛选列
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="source"></param>
    /// <param name="where"></param>
    public DapperAliasTableSelect(IExecutor executor, TableAlias<TTable> source, ISqlLogic where)
        : this(executor, new TableFilter(source, where), source)
    {
    }
    /// <summary>
    /// 别名表过滤筛选列
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="query"></param>
    public DapperAliasTableSelect(IExecutor executor, AliasTableSqlQuery<TTable> query)
        : this(executor, query, query.Source)
    {
    }
    /// <summary>
    /// 别名表过滤筛选列
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="query"></param>
    public DapperAliasTableSelect(IExecutor executor, AliasTableQuery<TTable> query)
        : this(executor, query, query.Source)
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
public class DapperAliasTableCursorSelect<TTable>(IExecutor executor, AliasTableCursor<TTable> cursor)
    : CursorSelectBase<TableAlias<TTable>>(cursor, cursor.Source)
    , IDapperSelect
    where TTable : ITable
{
    #region 配置
    private readonly IExecutor _executor = executor;
    /// <summary>
    /// 执行器
    /// </summary>
    public IExecutor Executor
        => _executor;
    #endregion
}

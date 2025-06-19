using ShadowSql.AliasTables;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Select;
using ShadowSql.Tables;

namespace Dapper.Shadow.Select;

/// <summary>
/// 别名表筛选列
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="executor">执行器</param>
/// <param name="view"></param>
/// <param name="aliasTable">别名表</param>
public class DapperAliasTableSelect<TTable>(IExecutor executor, ITableView view, IAliasTable<TTable> aliasTable)
    : SelectBase<ITableView, IAliasTable<TTable>>(view, aliasTable)
    , IDapperSelect
    where TTable : ITable
{
    /// <summary>
    /// 别名表筛选列
    /// </summary>
    /// <param name="executor">执行器</param>
    /// <param name="aliasTable">别名表</param>
    public DapperAliasTableSelect(IExecutor executor, IAliasTable<TTable> aliasTable)
        : this(executor, aliasTable, aliasTable)
    {
    }
    /// <summary>
    /// 别名表筛选列
    /// </summary>
    /// <param name="executor">执行器</param>
    /// <param name="aliasTable">别名表</param>
    /// <param name="where">查询条件</param>
    public DapperAliasTableSelect(IExecutor executor, IAliasTable<TTable> aliasTable, ISqlLogic where)
        : this(executor, new TableFilter(aliasTable, where), aliasTable)
    {
    }
    /// <summary>
    /// 别名表过滤筛选列
    /// </summary>
    /// <param name="executor">执行器</param>
    /// <param name="query">查询</param>
    public DapperAliasTableSelect(IExecutor executor, AliasTableSqlQuery<TTable> query)
        : this(executor, query, query.Source)
    {
    }
    /// <summary>
    /// 别名表过滤筛选列
    /// </summary>
    /// <param name="executor">执行器</param>
    /// <param name="query">查询</param>
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
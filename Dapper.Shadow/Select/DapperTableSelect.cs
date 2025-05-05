using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Select;
using ShadowSql.Tables;

namespace Dapper.Shadow.Select;

/// <summary>
/// Dapper表筛选列
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="executor"></param>
/// <param name="source"></param>
/// <param name="target"></param>
public class DapperTableSelect<TTable>(IExecutor executor, ITableView source, TTable target)
    : SelectBase<ITableView, TTable>(source, target)
    , IDapperSelect
    where TTable : ITable
{
    /// <summary>
    /// Dapper表筛选列
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="table"></param>
    public DapperTableSelect(IExecutor executor, TTable table)
        : this(executor, table, table)
    {
    }
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="table"></param>
    /// <param name="where"></param>
    public DapperTableSelect(IExecutor executor, TTable table, ISqlLogic where)
        : this(executor, new TableFilter(table, where), table)
    {
    }
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="query"></param>
    public DapperTableSelect(IExecutor executor, TableSqlQuery<TTable> query)
        : this(executor, query, query.Source)
    {
    }
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="query"></param>
    public DapperTableSelect(IExecutor executor, TableQuery<TTable> query)
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

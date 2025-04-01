using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Tables;

namespace Dapper.Shadow;

/// <summary>
/// Dapper表数据过滤
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="executor"></param>
/// <param name="source"></param>
/// <param name="filter"></param>
public class DapperFilter<TTable>(IExecutor executor, TTable source, ISqlLogic filter)
    : TableFilter<TTable>(source, filter), IDapperSource
    where TTable : ITableView
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

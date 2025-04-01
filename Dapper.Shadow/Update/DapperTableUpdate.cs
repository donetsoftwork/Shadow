using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Update;

namespace Dapper.Shadow.Update;

/// <summary>
/// 修改表
/// </summary>
/// <param name="executor"></param>
/// <param name="table"></param>
/// <param name="filter"></param>
public class DapperTableUpdate<TTable>(IExecutor executor, TTable table, ISqlLogic filter)
    : TableUpdate<TTable>(table, filter), IDapperExecute
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

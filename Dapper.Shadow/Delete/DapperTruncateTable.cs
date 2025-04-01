using ShadowSql.Delete;
using ShadowSql.Identifiers;

namespace Dapper.Shadow.Delete;

/// <summary>
/// 清空表
/// </summary>
/// <param name="executor"></param>
/// <param name="table"></param>
public class DapperTruncateTable(IExecutor executor, ITable table)
    : TruncateTable(table), IDapperExecute
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

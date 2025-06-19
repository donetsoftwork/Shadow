using ShadowSql.Identifiers;

namespace Dapper.Shadow;

/// <summary>
/// Dapper表
/// </summary>
/// <param name="executor">执行器</param>
/// <param name="tableName">表名</param>
public class DapperTable(IExecutor executor, string tableName)
    : Table(tableName), IDapperTable
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

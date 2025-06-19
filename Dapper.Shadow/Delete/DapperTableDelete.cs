using ShadowSql.Delete;
using ShadowSql.Identifiers;
using ShadowSql.Logics;

namespace Dapper.Shadow.Delete;

/// <summary>
/// 表数据删除
/// </summary>
/// <param name="executor">执行器</param>
/// <param name="table">表</param>
/// <param name="filter">过滤条件</param>
public class DapperTableDelete(IExecutor executor, ITable table, ISqlLogic filter)
    : TableDelete(table, filter), IDapperExecute
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


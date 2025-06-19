using ShadowSql.Delete;
using ShadowSql.Identifiers;
using ShadowSql.Logics;

namespace Dapper.Shadow.Delete;

/// <summary>
/// 表数据删除
/// </summary>
/// <param name="executor">执行器</param>
/// <param name="aliasTable">别名表</param>
/// <param name="filter">过滤条件</param>
public class DapperAliasTableDelete(IExecutor executor, IAliasTable aliasTable, ISqlLogic filter)
    : AliasTableDelete(aliasTable, filter), IDapperExecute
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

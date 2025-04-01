using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using ShadowSql.Logics;

namespace Dapper.Shadow.GroupBy;

/// <summary>
/// 对MultiQuery进行分组查询
/// </summary>
/// <param name="executor"></param>
/// <param name="multiTable"></param>
/// <param name="fields"></param>
/// <param name="filter"></param>
public class DapperGroupByMultiQuery(IExecutor executor, IMultiView multiTable, IFieldView[] fields, Logic filter)
    : GroupByMultiQuery(multiTable, fields, filter)
    , IDapperSource
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

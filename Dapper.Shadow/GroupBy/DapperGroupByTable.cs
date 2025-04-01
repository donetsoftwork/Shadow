using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Queries;

namespace Dapper.Shadow.GroupBy;

/// <summary>
/// 对Table进行分组查询
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="executor"></param>
/// <param name="table"></param>
/// <param name="where"></param>
/// <param name="fields"></param>
/// <param name="having"></param>
public class DapperGroupByTable<TTable>(IExecutor executor, TTable table, ISqlLogic where, IFieldView[] fields, SqlQuery having)
    : GroupByTable<TTable>(table, where, fields, having)
    , IDapperSource
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

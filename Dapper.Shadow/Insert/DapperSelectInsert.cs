using ShadowSql.Identifiers;
using ShadowSql.Insert;
using ShadowSql.Select;

namespace Dapper.Shadow.Insert;

/// <summary>
/// 插入Select子查询
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="executor"></param>
/// <param name="table"></param>
/// <param name="select"></param>
public class DapperSelectInsert<TTable>(IExecutor executor, TTable table, ISelect select)
    : SelectInsert<TTable>(table, select)
    , IDapperExecute
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

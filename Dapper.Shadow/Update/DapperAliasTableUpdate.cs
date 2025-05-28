using ShadowSql.AliasTables;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Update;

namespace Dapper.Shadow.Update;

/// <summary>
/// 修改别名表
/// </summary>
/// <param name="executor"></param>
/// <param name="table"></param>
/// <param name="filter"></param>
public class DapperAliasTableUpdate<TTable>(IExecutor executor, AliasUpdateTable<TTable> table, ISqlLogic filter)
    : AliasTableUpdate<TTable>(table, filter), IDapperExecute
    where TTable : ITable
{
    /// <summary>
    /// 修改别名表
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="table"></param>
    /// <param name="filter"></param>
    public DapperAliasTableUpdate(IExecutor executor, IAliasTable<TTable> table, ISqlLogic filter)
        : this(executor, new AliasUpdateTable<TTable>(table), filter)
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

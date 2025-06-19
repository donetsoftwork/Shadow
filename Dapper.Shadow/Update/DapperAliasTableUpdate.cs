using ShadowSql.AliasTables;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Update;

namespace Dapper.Shadow.Update;

/// <summary>
/// 修改别名表
/// </summary>
/// <param name="executor">执行器</param>
/// <param name="aliasTable">别名表</param>
/// <param name="filter">过滤条件</param>
public class DapperAliasTableUpdate<TTable>(IExecutor executor, AliasUpdateTable<TTable> aliasTable, ISqlLogic filter)
    : AliasTableUpdate<TTable>(aliasTable, filter), IDapperExecute
    where TTable : ITable
{
    /// <summary>
    /// 修改别名表
    /// </summary>
    /// <param name="executor">执行器</param>
    /// <param name="aliasTable">别名表</param>
    /// <param name="filter">过滤条件</param>
    public DapperAliasTableUpdate(IExecutor executor, IAliasTable<TTable> aliasTable, ISqlLogic filter)
        : this(executor, new AliasUpdateTable<TTable>(aliasTable), filter)
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

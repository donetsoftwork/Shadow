using ShadowSql.Identifiers;
using ShadowSql.Update;
using System.Linq;

namespace Dapper.Shadow.Update;

/// <summary>
/// 多表(联表)修改
/// </summary>
/// <param name="executor"></param>
/// <param name="multiTable"></param>
/// <param name="target"></param>
public class DapperMultiTableUpdate(IExecutor executor, IMultiView multiTable, IAliasTable target)
    : MultiTableUpdate(multiTable, target), IDapperExecute
{
    /// <summary>
    /// 多表(联表)修改
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="multiTable"></param>
    public DapperMultiTableUpdate(IExecutor executor, IMultiView multiTable)
        : this(executor, multiTable, multiTable.Tables.First())
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

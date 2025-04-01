using System.Linq;
using ShadowSql.Delete;
using ShadowSql.Identifiers;

namespace Dapper.Shadow.Delete;

/// <summary>
/// 多表(联表)数据删除
/// </summary>
/// <param name="executor"></param>
/// <param name="multiTable"></param>
/// <param name="target"></param>
public class DapperMultiTableDelete(IExecutor executor, IMultiTableQuery multiTable, IAliasTable target)
    : MultiTableDelete(multiTable, target), IDapperExecute
{
    /// <summary>
    /// 多表(联表)数据删除
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="multiTable"></param>
    public DapperMultiTableDelete(IExecutor executor, IMultiTableQuery multiTable)
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

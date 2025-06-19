using System.Linq;
using ShadowSql.Delete;
using ShadowSql.Identifiers;

namespace Dapper.Shadow.Delete;

/// <summary>
/// 多表(联表)数据删除
/// </summary>
/// <param name="executor">执行器</param>
/// <param name="multiTable">多表(联表)</param>
/// <param name="aliasTable">别名表</param>
public class DapperMultiTableDelete(IExecutor executor, IMultiView multiTable, IAliasTable aliasTable)
    : MultiTableDelete(multiTable, aliasTable), IDapperExecute
{
    /// <summary>
    /// 多表(联表)数据删除
    /// </summary>
    /// <param name="executor">执行器</param>
    /// <param name="multiTable">多表(联表)</param>
    public DapperMultiTableDelete(IExecutor executor, IMultiView multiTable)
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

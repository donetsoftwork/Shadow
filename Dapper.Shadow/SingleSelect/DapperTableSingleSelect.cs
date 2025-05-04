using ShadowSql.Cursors;
using ShadowSql.Identifiers;
using ShadowSql.SingleSelect;

namespace Dapper.Shadow.SingleSelect;

/// <summary>
/// Dapper表筛选单列
/// </summary>
/// <param name="executor"></param>
/// <param name="source"></param>
/// <param name="singleField"></param>
public class DapperTableSingleSelect(IExecutor executor, ITableView source, IFieldView singleField)
    : TableSingleSelect(source, singleField)
    , IDapperSingleSelect
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
/// <summary>
/// Dapper游标单列选择
/// </summary>
/// <param name="executor"></param>
/// <param name="cursor"></param>
/// <param name="singleField"></param>
public sealed class DapperCursorSingleSelect(IExecutor executor, ICursor cursor, IFieldView singleField)
    : CursorSingleSelect(cursor, singleField)
    , IDapperSingleSelect
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

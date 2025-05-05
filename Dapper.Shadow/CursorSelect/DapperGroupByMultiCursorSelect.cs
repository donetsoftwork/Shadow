using ShadowSql.Cursors;
using ShadowSql.Select;

namespace Dapper.Shadow.CursorSelect;

/// <summary>
/// GroupBy后再范围(分页)及列筛选
/// </summary>
/// <param name="executor"></param>
/// <param name="cursor"></param>
public class DapperGroupByMultiCursorSelect(IExecutor executor, GroupByMultiCursor cursor)
    : GroupByMultiSelectBase<ICursor>(cursor, cursor.Source, cursor.MultiTable)
    , IDapperSelect
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

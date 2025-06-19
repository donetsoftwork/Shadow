using ShadowSql.Cursors;
using ShadowSql.CursorSelect;

namespace Dapper.Shadow.CursorSelect;

/// <summary>
/// 多表视图范围(分页)及列筛选
/// </summary>
/// <param name="executor">执行器</param>
/// <param name="cursor">游标</param>
public class DapperMultiTableCursorSelect(IExecutor executor, MultiTableCursor cursor)
    : MultiCursorSelectBase(cursor, cursor.Source)
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

using ShadowSql.Cursors;
using ShadowSql.CursorSelect;
using ShadowSql.Identifiers;

namespace Dapper.Shadow.CursorSelect;

/// <summary>
/// GroupBy后再范围(分页)及列筛选
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="executor">执行器</param>
/// <param name="cursor">游标</param>
public class DapperGroupByTableCursorSelect<TTable>(IExecutor executor, GroupByTableCursor<TTable> cursor)
    : CursorSelectBase<TTable>(cursor, cursor.Table)
    , IDapperSelect
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

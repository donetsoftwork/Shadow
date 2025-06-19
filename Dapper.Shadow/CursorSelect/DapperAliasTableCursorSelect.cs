using ShadowSql.Cursors;
using ShadowSql.CursorSelect;
using ShadowSql.Identifiers;

namespace Dapper.Shadow.CursorSelect;

/// <summary>
/// 别名表范围(分页)及列筛选
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="executor">执行器</param>
/// <param name="cursor">游标</param>
public class DapperAliasTableCursorSelect<TTable>(IExecutor executor, AliasTableCursor<TTable> cursor)
    : CursorSelectBase<IAliasTable<TTable>>(cursor, cursor.Source)
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


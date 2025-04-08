using ShadowSql.Cursors;
using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.Select;
using ShadowSql.SelectFields;
using ShadowSql.SingleSelect;
using System.Text;

namespace Dapper.Shadow.SingleSelect;

/// <summary>
/// 多表视图筛选列
/// </summary>
/// <param name="executor"></param>
/// <param name="multiTable"></param>
/// <param name="fields"></param>
public class DapperMultiTableSingleSelect(IExecutor executor, IMultiView multiTable, MultiTableFields fields)
    : SingleSelectBase<IMultiView, MultiTableFields>(multiTable, fields)
    , IDapperSingleSelect
{
    /// <summary>
    /// 多表视图筛选列
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="multiTable"></param>
    public DapperMultiTableSingleSelect(IExecutor executor, IMultiView multiTable)
        : this(executor, multiTable, new MultiTableFields(multiTable))
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
/// <summary>
/// 多表视图范围(分页)及列筛选
/// </summary>
/// <param name="executor"></param>
/// <param name="cursor"></param>
/// <param name="fields"></param>
public class DapperMultiTableCursorSingleSelect(IExecutor executor, MultiTableCursor cursor, MultiTableFields fields)
    : SingleSelectBase<MultiTableCursor, MultiTableFields>(cursor, fields)
    , IDapperSingleSelect
{
    /// <summary>
    /// 多表视图范围(分页)及列筛选
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="cursor"></param>
    public DapperMultiTableCursorSingleSelect(IExecutor executor, MultiTableCursor cursor)
        : this(executor, cursor, new MultiTableFields(cursor.Source))
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
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    public override void Write(ISqlEngine engine, StringBuilder sql)
        => engine.SelectCursor(sql, this, _source);
}

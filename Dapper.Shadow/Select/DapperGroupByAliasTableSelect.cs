using ShadowSql.Cursors;
using ShadowSql.CursorSelect;
using ShadowSql.Engines;
using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using ShadowSql.Select;
using ShadowSql.SelectFields;
using ShadowSql.Variants;
using System.Text;

namespace Dapper.Shadow.Select;

/// <summary>
/// GroupBy别名表后再筛选列
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="executor"></param>
/// <param name="groupBy"></param>
/// <param name="target"></param>
public class DapperGroupByAliasTableSelect<TTable>(IExecutor executor, IGroupByView groupBy, IAliasTable<TTable> target)
    : GroupBySelectBase<IGroupByView, IAliasTable<TTable>>(groupBy, groupBy, target)
    , IDapperSelect
    where TTable : ITable
{
    /// <summary>
    /// GroupBy别名表后再筛选列
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="groupBy"></param>
    public DapperGroupByAliasTableSelect(IExecutor executor, GroupByAliasTableSqlQuery<TTable> groupBy)
        : this(executor, groupBy, groupBy.Source)
    {
    }
    /// <summary>
    /// GroupBy别名表后再筛选列
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="groupBy"></param>
    public DapperGroupByAliasTableSelect(IExecutor executor, GroupByAliasTableQuery<TTable> groupBy)
        : this(executor, groupBy, groupBy.Source)
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
/// GroupBy别名表后再范围(分页)及列筛选
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="executor"></param>
/// <param name="cursor"></param>
public class DapperGroupByAliasTableCursorSelect<TTable>(IExecutor executor, GroupByAliasTableCursor<TTable> cursor)
    : GroupCursorBySelectBase<IAliasTable<TTable>>(cursor, cursor.Source, cursor.AliasTable)
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

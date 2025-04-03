using ShadowSql.Cursors;
using ShadowSql.Engines;
using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using ShadowSql.Select;
using ShadowSql.SelectFields;
using System.Text;

namespace Dapper.Shadow.Select;

/// <summary>
/// GroupBy别名表后再筛选列
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="executor"></param>
/// <param name="groupBy"></param>
/// <param name="fields"></param>
public class DapperGroupByAliasTableSelect<TTable>(IExecutor executor, IGroupByView groupBy, GroupByAliasTableFields<TTable> fields)
    : SelectBase<IGroupByView, GroupByAliasTableFields<TTable>>(groupBy, fields)
    , IDapperSelect
    where TTable : ITable
{
    /// <summary>
    /// GroupBy别名表后再筛选列
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="groupBy"></param>
    public DapperGroupByAliasTableSelect(IExecutor executor, GroupByAliasTableSqlQuery<TTable> groupBy)
        : this(executor, groupBy, new GroupByAliasTableFields<TTable>(groupBy))
    {
    }
    /// <summary>
    /// GroupBy别名表后再筛选列
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="groupBy"></param>
    public DapperGroupByAliasTableSelect(IExecutor executor, GroupByAliasTableQuery<TTable> groupBy)
        : this(executor, groupBy, new GroupByAliasTableFields<TTable>(groupBy))
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
/// <param name="fields"></param>
public class DapperGroupByAliasTableFetchSelect<TTable>(IExecutor executor, ICursor cursor, GroupByAliasTableFields<TTable> fields)
    : SelectBase<ICursor, GroupByAliasTableFields<TTable>>(cursor, fields)
    , IDapperSelect
    where TTable : ITable
{
    /// <summary>
    /// GroupBy别名表后再范围(分页)及列筛选
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="cursor"></param>
    public DapperGroupByAliasTableFetchSelect(IExecutor executor, GroupByAliasTableCursor<TTable> cursor)
        : this(executor, cursor, new GroupByAliasTableFields<TTable>(cursor))
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

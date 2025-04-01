using ShadowSql.Engines;
using ShadowSql.Fetches;
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
    public DapperGroupByAliasTableSelect(IExecutor executor, GroupByAliasTable<TTable> groupBy)
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
/// <param name="fetch"></param>
/// <param name="fields"></param>
public class DapperGroupByAliasTableFetchSelect<TTable>(IExecutor executor, IFetch fetch, GroupByAliasTableFields<TTable> fields)
    : SelectBase<IFetch, GroupByAliasTableFields<TTable>>(fetch, fields)
    , IDapperSelect
    where TTable : ITable
{
    /// <summary>
    /// GroupBy别名表后再范围(分页)及列筛选
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="fetch"></param>
    public DapperGroupByAliasTableFetchSelect(IExecutor executor, GroupByAliasTableFetch<TTable> fetch)
        : this(executor, fetch, new GroupByAliasTableFields<TTable>(fetch.Source))
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

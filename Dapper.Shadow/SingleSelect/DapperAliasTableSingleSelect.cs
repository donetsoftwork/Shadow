using ShadowSql.Engines;
using ShadowSql.Fetches;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Queries;
using ShadowSql.Select;
using ShadowSql.SelectFields;
using ShadowSql.SingleSelect;
using ShadowSql.Variants;
using System.Text;

namespace Dapper.Shadow.SingleSelect;

/// <summary>
/// 别名表筛选列
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="executor"></param>
/// <param name="source"></param>
/// <param name="fields"></param>
public class DapperAliasTableSingleSelect<TTable>(IExecutor executor, ITableView source, AliasTableFields<TTable> fields)
    : SingleSelectBase<ITableView, AliasTableFields<TTable>>(source, fields)
    , IDapperSingleSelect
    where TTable : ITable
{
    /// <summary>
    /// 别名表筛选列
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="source"></param>
    public DapperAliasTableSingleSelect(IExecutor executor, TableAlias<TTable> source)
        : this(executor, source, new AliasTableFields<TTable>(source))
    {
    }
    /// <summary>
    /// 别名表筛选列
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="source"></param>
    /// <param name="where"></param>
    public DapperAliasTableSingleSelect(IExecutor executor, TableAlias<TTable> source, ISqlLogic where)
        : this(executor, new TableFilter<TableAlias<TTable>>(source, where), new AliasTableFields<TTable>(source))
    {
    }
    /// <summary>
    /// 别名表过滤筛选列
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="filter"></param>
    public DapperAliasTableSingleSelect(IExecutor executor, TableFilter<TableAlias<TTable>> filter)
        : this(executor, filter, new AliasTableFields<TTable>(filter.Source))
    {
    }
    /// <summary>
    /// 别名表过滤筛选列
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="query"></param>
    public DapperAliasTableSingleSelect(IExecutor executor, AliasTableQuery<TTable> query)
        : this(executor, query, new AliasTableFields<TTable>(query.Source))
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
///// <summary>
///// 别名表过滤筛选列
///// </summary>
///// <typeparam name="TTable"></typeparam>
///// <param name="executor"></param>
///// <param name="filter"></param>
//public sealed class DapperAliasTableFilterSelect<TTable>(IExecutor executor, TableFilter<TableAlias<TTable>> filter)
//    : SelectBase<ITableView, AliasTableFields<TTable>>(filter, new AliasTableFields<TTable>(filter.Source))
//    , IDapperSelect
//    where TTable : ITable
//{

//    #region 配置
//    private readonly IExecutor _executor = executor;
//    /// <summary>
//    /// 执行器
//    /// </summary>
//    public IExecutor Executor
//        => _executor;
//    #endregion
//}
///// <summary>
///// 别名表(及查询)筛选列
///// </summary>
///// <typeparam name="TTable"></typeparam>
///// <param name="executor"></param>
///// <param name="query"></param>
//public class DapperAliasTableQuerySelect<TTable>(IExecutor executor, AliasTableQuery<TTable> query)
//    : SelectBase<ITableView, AliasTableFields<TTable>>(query, new AliasTableFields<TTable>(query.Source))
//    , IDapperSelect
//    where TTable : ITable
//{
//    #region 配置
//    private readonly IExecutor _executor = executor;
//    /// <summary>
//    /// 执行器
//    /// </summary>
//    public IExecutor Executor
//        => _executor;
//    #endregion
//}
/// <summary>
/// 别名表范围(分页)及列筛选
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="executor"></param>
/// <param name="fetch"></param>
/// <param name="fields"></param>
public class DapperAliasTableFetchSingleSelect<TTable>(IExecutor executor, IFetch fetch, AliasTableFields<TTable> fields)
    : SingleSelectBase<IFetch, AliasTableFields<TTable>>(fetch, fields)
    , IDapperSingleSelect
    where TTable : ITable
{
    /// <summary>
    /// 别名表范围(分页)及列筛选
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="fetch"></param>
    public DapperAliasTableFetchSingleSelect(IExecutor executor, AliasTableFetch<TTable> fetch)
        : this(executor, fetch, new AliasTableFields<TTable>(fetch.Source))
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

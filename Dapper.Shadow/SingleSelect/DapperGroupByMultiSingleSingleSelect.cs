using ShadowSql.Engines;
using ShadowSql.Fetches;
using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using ShadowSql.Select;
using ShadowSql.SelectFields;
using ShadowSql.SingleSelect;
using System.Text;

namespace Dapper.Shadow.SingleSelect;

/// <summary>
/// GroupBy后再筛选列
/// </summary>
/// <param name="executor"></param>
/// <param name="groupBy"></param>
/// <param name="fields"></param>
public class DapperGroupByMultiSingleSelect(IExecutor executor, IGroupByView groupBy, GroupByMultiFields fields)
    : SingleSelectBase<IGroupByView, GroupByMultiFields>(groupBy, fields)
    , IDapperSingleSelect
{
    /// <summary>
    /// GroupBy后再筛选列
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="groupBy"></param>
    public DapperGroupByMultiSingleSelect(IExecutor executor, GroupByMultiSqlQuery groupBy)
        :this(executor, groupBy, new GroupByMultiFields(groupBy))
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
/// GroupBy后再范围(分页)及列筛选
/// </summary>
/// <param name="executor"></param>
/// <param name="fetch"></param>
/// <param name="fields"></param>
public class DapperGroupByMultiFetchSingleSelect(IExecutor executor, GroupByMultiFetch fetch, GroupByMultiFields fields)
    : SingleSelectBase<IFetch, GroupByMultiFields>(fetch, fields)
    , IDapperSingleSelect
{
    /// <summary>
    /// GroupBy后再范围(分页)及列筛选
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="fetch"></param>
    public DapperGroupByMultiFetchSingleSelect(IExecutor executor, GroupByMultiFetch fetch)
        : this(executor, fetch, new GroupByMultiFields(fetch))
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

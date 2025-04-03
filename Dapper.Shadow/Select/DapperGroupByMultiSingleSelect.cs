using ShadowSql.Cursors;
using ShadowSql.Engines;
using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using ShadowSql.Select;
using ShadowSql.SelectFields;
using System.Text;

namespace Dapper.Shadow.Select;

/// <summary>
/// GroupBy后再筛选列
/// </summary>
/// <param name="executor"></param>
/// <param name="groupBy"></param>
/// <param name="fields"></param>
public class DapperGroupByMultiSelect(IExecutor executor, IGroupByView groupBy, GroupByMultiFields fields)
    : SelectBase<IGroupByView, GroupByMultiFields>(groupBy, fields)
    , IDapperSelect
{
    /// <summary>
    /// GroupBy后再筛选列
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="groupBy"></param>
    public DapperGroupByMultiSelect(IExecutor executor, GroupByMultiSqlQuery groupBy)
        :this(executor, groupBy, new GroupByMultiFields(groupBy))
    {
    }
    /// <summary>
    /// GroupBy后再筛选列
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="groupBy"></param>
    public DapperGroupByMultiSelect(IExecutor executor, GroupByMultiQuery groupBy)
        : this(executor, groupBy, new GroupByMultiFields(groupBy))
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
/// <param name="cursor"></param>
/// <param name="fields"></param>
public class DapperGroupByMultiFetchSelect(IExecutor executor, GroupByMultiCursor cursor, GroupByMultiFields fields)
    : SelectBase<ICursor, GroupByMultiFields>(cursor, fields)
    , IDapperSelect
{
    /// <summary>
    /// GroupBy后再范围(分页)及列筛选
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="cursor"></param>
    public DapperGroupByMultiFetchSelect(IExecutor executor, GroupByMultiCursor cursor)
        : this(executor, cursor, new GroupByMultiFields(cursor))
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

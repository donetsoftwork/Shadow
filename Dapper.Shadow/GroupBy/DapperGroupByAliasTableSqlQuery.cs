using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Queries;
using ShadowSql.Variants;

namespace Dapper.Shadow.GroupBy;

/// <summary>
/// 对别名表分组
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="executor"></param>
/// <param name="source"></param>
/// <param name="where"></param>
/// <param name="fields"></param>
/// <param name="having"></param>
public class DapperGroupByAliasTableSqlQuery<TTable>(IExecutor executor, TableAlias<TTable> source, ISqlLogic where, IFieldView[] fields, SqlQuery having)
    : GroupByAliasTableSqlQuery<TTable>(source, where, fields, having)
    , IDapperSource
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

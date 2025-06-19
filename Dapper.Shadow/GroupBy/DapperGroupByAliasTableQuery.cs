using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using ShadowSql.Logics;

namespace Dapper.Shadow.GroupBy;

/// <summary>
/// 对别名表分组
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="executor">执行器</param>
/// <param name="aliasTable">别名表</param>
/// <param name="where">查询条件</param>
/// <param name="fields">字段</param>
/// <param name="having">分组查询条件</param>
public class DapperGroupByAliasTableQuery<TTable>(IExecutor executor, IAliasTable<TTable> aliasTable, ISqlLogic where, IField[] fields, Logic having)
    : GroupByAliasTableQuery<TTable>(aliasTable, where, fields, having)
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

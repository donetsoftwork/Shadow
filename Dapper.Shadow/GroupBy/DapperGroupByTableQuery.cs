using ShadowSql.Aggregates;
using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using System;

namespace Dapper.Shadow.GroupBy;

/// <summary>
/// 对Table进行分组查询
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="executor"></param>
/// <param name="table"></param>
/// <param name="where"></param>
/// <param name="fields"></param>
/// <param name="having"></param>
public class DapperGroupByTableQuery<TTable>(IExecutor executor, TTable table, ISqlLogic where, IField[] fields, Logic having)
    : GroupByTableQuery<TTable>(table, where, fields, having)
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
    #region 查询扩展
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <param name="aggregate"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    new public GroupByTableQuery<TTable> Apply(Func<TTable, IAggregateField> aggregate, Func<Logic, IAggregateField, Logic> query)
    {
        _filter = query(_filter, aggregate(_source));
        return this;
    }
    #endregion
}

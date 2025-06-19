using ShadowSql.Aggregates;
using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Queries;
using System;

namespace Dapper.Shadow.GroupBy;

/// <summary>
/// 对Table进行分组查询
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="executor">执行器</param>
/// <param name="table">表</param>
/// <param name="where">查询条件</param>
/// <param name="fields">字段</param>
/// <param name="having">分组查询条件</param>
public class DapperGroupByTableSqlQuery<TTable>(IExecutor executor, TTable table, ISqlLogic where, IField[] fields, SqlQuery having)
    : GroupByTableSqlQuery<TTable>(table, where, fields, having)
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
    /// <param name="aggregate">聚合</param>
    /// <param name="query">查询</param>
    /// <returns></returns>
    new public GroupByTableSqlQuery<TTable> HavingAggregate(Func<TTable, IAggregateField> aggregate, Func<IAggregateField, AtomicLogic> query)
    {
        _filter.AddLogic(query(aggregate(_source)));
        return this;
    }
    #endregion
}

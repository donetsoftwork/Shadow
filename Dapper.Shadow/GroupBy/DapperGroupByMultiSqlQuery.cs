using ShadowSql;
using ShadowSql.Aggregates;
using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Queries;
using System;

namespace Dapper.Shadow.GroupBy;

/// <summary>
/// 对MultiQuery进行分组查询
/// </summary>
/// <param name="executor">执行器</param>
/// <param name="multiTable">多表(联表)</param>
/// <param name="fields">字段</param>
/// <param name="filter">过滤条件</param>
public class DapperGroupByMultiSqlQuery(IExecutor executor, IMultiView multiTable, IField[] fields, SqlQuery filter)
    : GroupByMultiSqlQuery(multiTable, fields, filter)
    , IDapperSource
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
    /// <typeparam name="TAliasTable"></typeparam>
    /// <param name="tableName">表名</param>
    /// <param name="aggregate">聚合</param>
    /// <param name="query">查询</param>
    /// <returns></returns>
    new public GroupByMultiSqlQuery HavingAggregate<TAliasTable>(string tableName, Func<TAliasTable, IAggregateField> aggregate, Func<IAggregateField, AtomicLogic> query)
         where TAliasTable : IAliasTable
    {
        _filter.AddLogic(query(aggregate(_source.From<TAliasTable>(tableName))));
        return this;
    }
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="tableName">表名</param>
    /// <param name="select">筛选</param>
    /// <param name="aggregate">聚合</param>
    /// <param name="query">查询</param>
    /// <returns></returns>
    new public GroupByMultiSqlQuery HavingAggregate<TTable>(string tableName, Func<TTable, IColumn> select, Func<IPrefixField, IAggregateField> aggregate, Func<IAggregateField, AtomicLogic> query)
        where TTable : ITable
    {
        var table = _source.Alias<TTable>(tableName);
        _filter.AddLogic(query(aggregate(table.Prefix(select(table.Target)))));
        return this;
    }
    #endregion
}

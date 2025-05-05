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
/// <param name="executor"></param>
/// <param name="multiTable"></param>
/// <param name="fields"></param>
/// <param name="filter"></param>
public class DapperGroupByMultiSqlQuery(IExecutor executor, IMultiView multiTable, IFieldView[] fields, SqlQuery filter)
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
    /// <param name="tableName"></param>
    /// <param name="aggregate"></param>
    /// <param name="query"></param>
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
    /// <param name="tableName"></param>
    /// <param name="select"></param>
    /// <param name="aggregate"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    new public GroupByMultiSqlQuery HavingAggregate<TTable>(string tableName, Func<TTable, IColumn> select, Func<IColumn, IAggregateField> aggregate, Func<IAggregateField, AtomicLogic> query)
        where TTable : ITable
    {
        var table = _source.Alias<TTable>(tableName);
        _filter.AddLogic(query(aggregate(table.Prefix(select(table.Target)))));
        return this;
    }
    #endregion
}

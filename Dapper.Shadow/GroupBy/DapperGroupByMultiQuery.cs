using ShadowSql;
using ShadowSql.Aggregates;
using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using System;

namespace Dapper.Shadow.GroupBy;

/// <summary>
/// 对MultiQuery进行分组查询
/// </summary>
/// <param name="executor"></param>
/// <param name="multiTable"></param>
/// <param name="fields"></param>
/// <param name="filter"></param>
public class DapperGroupByMultiQuery(IExecutor executor, IMultiView multiTable, IFieldView[] fields, Logic filter)
    : GroupByMultiQuery(multiTable, fields, filter)
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
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <typeparam name="TAliasTable"></typeparam>
    /// <param name="tableName"></param>
    /// <param name="aggregate"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    new public DapperGroupByMultiQuery Apply<TAliasTable>(string tableName, Func<TAliasTable, IAggregateField> aggregate, Func<Logic, IAggregateField, Logic> query)
         where TAliasTable : IAliasTable
    {
        _filter = query(_filter, aggregate(_source.From<TAliasTable>(tableName)));
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
    new public DapperGroupByMultiQuery Apply<TTable>(string tableName, Func<TTable, IColumn> select, Func<IColumn, IAggregateField> aggregate, Func<Logic, IAggregateField, Logic> query)
        where TTable : ITable
    {
        var table = _source.Alias<TTable>(tableName);
        _filter = query(_filter, aggregate(table.Prefix(select(table.Target))));
        return this;
    }
}

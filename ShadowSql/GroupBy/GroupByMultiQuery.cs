using ShadowSql.Aggregates;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using System;

namespace ShadowSql.GroupBy;

/// <summary>
/// 对MultiQuery进行分组查询
/// </summary>
/// <param name="multiTable">多表(联表)</param>
/// <param name="fields">字段</param>
/// <param name="filter">过滤条件</param>
public class GroupByMultiQuery(IMultiView multiTable, IField[] fields, Logic filter)
    : GroupByQueryBase<IMultiView>(multiTable, fields, filter)
{
    /// <summary>
    /// 对多表进行分组查询
    /// </summary>
    /// <param name="multiTable">多表(联表)</param>
    /// <param name="fields">字段</param>
    public GroupByMultiQuery(IMultiView multiTable, IField[] fields)
        : this(multiTable, fields, new AndLogic())
    {
    }
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <typeparam name="TAliasTable"></typeparam>
    /// <param name="tableName">表名</param>
    /// <param name="aggregate">聚合</param>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public GroupByMultiQuery Apply<TAliasTable>(string tableName, Func<TAliasTable, IAggregateField> aggregate, Func<Logic, IAggregateField, Logic> query)
         where TAliasTable : IAliasTable
    {
        _filter = query(_filter, aggregate(_source.From<TAliasTable>(tableName)));
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
    public GroupByMultiQuery Apply<TTable>(string tableName, Func<TTable, IColumn> select, Func<IPrefixField, IAggregateField> aggregate, Func<Logic, IAggregateField, Logic> query)
        where TTable : ITable
    {
        var table = _source.Alias<TTable>(tableName);
        _filter = query(_filter, aggregate(table.Prefix(select(table.Target))));
        return this;
    }
}

using ShadowSql.Aggregates;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using System;

namespace ShadowSql.GroupBy;

/// <summary>
/// 对MultiQuery进行分组查询
/// </summary>
/// <param name="multiTable"></param>
/// <param name="fields"></param>
/// <param name="filter"></param>
public class GroupByMultiQuery(IMultiView multiTable, IField[] fields, Logic filter)
    : GroupByQueryBase<IMultiView>(multiTable, fields, filter)
{
    /// <summary>
    /// 对多表进行分组查询
    /// </summary>
    /// <param name="multiTable"></param>
    /// <param name="fields"></param>
    public GroupByMultiQuery(IMultiView multiTable, IField[] fields)
        : this(multiTable, fields, new AndLogic())
    {
    }
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <typeparam name="TAliasTable"></typeparam>
    /// <param name="tableName"></param>
    /// <param name="aggregate"></param>
    /// <param name="query"></param>
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
    /// <param name="tableName"></param>
    /// <param name="select"></param>
    /// <param name="aggregate"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public GroupByMultiQuery Apply<TTable>(string tableName, Func<TTable, IColumn> select, Func<IPrefixField, IAggregateField> aggregate, Func<Logic, IAggregateField, Logic> query)
        where TTable : ITable
    {
        var table = _source.Alias<TTable>(tableName);
        _filter = query(_filter, aggregate(table.Prefix(select(table.Target))));
        return this;
    }
}

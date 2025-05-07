using ShadowSql.Aggregates;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Queries;
using System;

namespace ShadowSql.GroupBy;

/// <summary>
/// 对MultiQuery进行分组查询
/// </summary>
/// <param name="multiTable"></param>
/// <param name="fields"></param>
/// <param name="filter"></param>
public class GroupByMultiSqlQuery(IMultiView multiTable, IField[] fields, SqlQuery filter)
    : GroupBySqlQueryBase<IMultiView>(multiTable, fields, filter)
{
    /// <summary>
    /// 对多表进行分组查询
    /// </summary>
    /// <param name="multiTable"></param>
    /// <param name="fields"></param>
    public GroupByMultiSqlQuery(IMultiView multiTable, IField[] fields)
        : this(multiTable, fields, SqlQuery.CreateAndQuery())
    {
    }
    #region 查询扩展
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <typeparam name="TAliasTable"></typeparam>
    /// <param name="tableName"></param>
    /// <param name="aggregate"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public GroupByMultiSqlQuery HavingAggregate<TAliasTable>(string tableName, Func<TAliasTable, IAggregateField> aggregate, Func<IAggregateField, AtomicLogic> query)
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
    public GroupByMultiSqlQuery HavingAggregate<TTable>(string tableName,  Func<TTable, IColumn> select, Func<IPrefixField, IAggregateField> aggregate, Func<IAggregateField, AtomicLogic> query)
        where TTable : ITable
    {
        var table = _source.Alias<TTable>(tableName);
        _filter.AddLogic(query(aggregate(table.Prefix(select(table.Target)))));
        return this;
    }
    #endregion
}

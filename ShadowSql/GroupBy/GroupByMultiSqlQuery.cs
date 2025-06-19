using ShadowSql.Aggregates;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Queries;
using System;

namespace ShadowSql.GroupBy;

/// <summary>
/// 对MultiQuery进行分组查询
/// </summary>
/// <param name="multiTable">多表(联表)</param>
/// <param name="fields">字段</param>
/// <param name="filter">过滤条件</param>
public class GroupByMultiSqlQuery(IMultiView multiTable, IField[] fields, SqlQuery filter)
    : GroupBySqlQueryBase<IMultiView>(multiTable, fields, filter)
{
    /// <summary>
    /// 对多表进行分组查询
    /// </summary>
    /// <param name="multiTable">多表(联表)</param>
    /// <param name="fields">字段</param>
    public GroupByMultiSqlQuery(IMultiView multiTable, IField[] fields)
        : this(multiTable, fields, SqlQuery.CreateAndQuery())
    {
    }
    #region 查询扩展
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <typeparam name="TAliasTable"></typeparam>
    /// <param name="tableName">表名</param>
    /// <param name="aggregate">聚合</param>
    /// <param name="query">查询</param>
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
    /// <param name="tableName">表名</param>
    /// <param name="select">筛选</param>
    /// <param name="aggregate">聚合</param>
    /// <param name="query">查询</param>
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

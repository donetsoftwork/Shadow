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
public class GroupByMultiQuery(IMultiTableQuery multiTable, IFieldView[] fields, SqlQuery filter)
    : GroupByBase<IMultiTableQuery>(multiTable, fields, filter), IGroupByMultiQuery
{
    /// <summary>
    /// 对多表进行分组查询
    /// </summary>
    /// <param name="multiTable"></param>
    /// <param name="fields"></param>
    public GroupByMultiQuery(IMultiTableQuery multiTable, IFieldView[] fields)
        : this(multiTable, fields, SqlQuery.CreateAndQuery())
    {
    }
    #region 扩展查询功能
    #region HavingAggregate
    /// <summary>
    /// 按聚合逻辑查询
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="aggregate"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public GroupByMultiQuery HavingAggregate(string tableName, Func<IAliasTable, IAggregateField> aggregate, Func<IAggregateField, AtomicLogic> query)
    {
        var member = _source.From(tableName);
        _innerQuery.AddLogic(query(aggregate(member)));
        return this;
    }
    /// <summary>
    /// 按聚合逻辑查询
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="tableName"></param>
    /// <param name="select"></param>
    /// <param name="aggregate"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public GroupByMultiQuery HavingAggregate<TTable>(string tableName, Func<TTable, IColumn> select, Func<IColumn, IAggregateField> aggregate, Func<IAggregateField, AtomicLogic> query)
        where TTable : ITable
    {
        var member = _source.Table<TTable>(tableName);
        //增加前缀
        var prefixColumn = member.GetPrefixColumn(select(member.Target));
        if (prefixColumn is not null)
            _innerQuery.AddLogic(query(aggregate(prefixColumn)));
        return this;
    }
    #endregion
    #endregion

    IMultiTableQuery IGroupByMultiQuery.MultiSource
        => _source;
}

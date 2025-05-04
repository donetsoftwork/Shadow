using ShadowSql.Filters;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Queries;
using System;

namespace ShadowSql.Tables;

/// <summary>
/// 逻辑查询表
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="table"></param>
/// <param name="filter"></param>
public class TableQuery<TTable>(TTable table, Logic filter)
    : DataFilterBase<TTable, Logic>(table, filter), IDataQuery
    where TTable : ITable
{
    /// <summary>
    /// 逻辑查询表
    /// </summary>
    /// <param name="source"></param>
    public TableQuery(TTable source)
        : this(source, new AndLogic())
    {
    }
    #region 扩展查询
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public TableQuery<TTable> And(Func<TTable, AtomicLogic> query)
    {
        _filter = _filter.And(query(_source));
        return this;
    }
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public TableQuery<TTable> Or(Func<TTable, AtomicLogic> query)
    {
        _filter = _filter.Or(query(_source));
        return this;
    }
    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public TableQuery<TTable> Apply(Func<Logic, TTable, Logic> query)
    {
        _filter = query(_filter, _source);
        return this;
    }
    #endregion
    #region IDataQuery
    Logic IDataQuery.Logic 
    {
        get => _filter;
        set => _filter = value;
    }
    #endregion
}

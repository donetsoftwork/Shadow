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
/// <param name="table">表</param>
/// <param name="filter">过滤条件</param>
public class TableQuery<TTable>(TTable table, Logic filter)
    : DataFilterBase<TTable, Logic>(table, filter), IDataQuery
    where TTable : ITable
{
    /// <summary>
    /// 逻辑查询表
    /// </summary>
    /// <param name="table">表</param>
    public TableQuery(TTable table)
        : this(table, new AndLogic())
    {
    }
    #region 扩展查询
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public TableQuery<TTable> And(Func<TTable, AtomicLogic> query)
    {
        _filter = _filter.And(query(_source));
        return this;
    }
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public TableQuery<TTable> Or(Func<TTable, AtomicLogic> query)
    {
        _filter = _filter.Or(query(_source));
        return this;
    }
    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="query">查询</param>
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

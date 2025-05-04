using ShadowSql.Filters;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Queries;
using System;

namespace ShadowSql.Tables;

/// <summary>
/// sql查询表
/// </summary>
/// <param name="table"></param>
/// <param name="query"></param>
public class TableSqlQuery<TTable>(TTable table, SqlQuery query)
    : DataFilterBase<TTable, SqlQuery>(table, query), IDataSqlQuery, IWhere
    where TTable : ITable
{
    #region 扩展查询功能
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public TableSqlQuery<TTable> Where(Func<TTable, AtomicLogic> query)
    {
        _filter.AddLogic(query(_source));
        return this;
    }
    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public TableSqlQuery<TTable> Apply(Func<SqlQuery, TTable, SqlQuery> query)
    {
        _filter = query(_filter, _source);
        return this;
    }
    #endregion
    #region IDataQuery
    SqlQuery IDataSqlQuery.Query
    {
        get => _filter;
        set => _filter = value;
    }
    #endregion
}
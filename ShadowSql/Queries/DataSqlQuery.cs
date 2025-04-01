using ShadowSql.Filters;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using System;
using System.Collections.Generic;

namespace ShadowSql.Queries;

/// <summary>
/// 数据sql查询
/// </summary>
/// <typeparam name="TSource"></typeparam>
/// <param name="source"></param>
/// <param name="query"></param>
public abstract class DataSqlQuery<TSource>(TSource source, SqlQuery query)
    : DataFilterBase<TSource, SqlQuery>(source, query), IDataSqlQuery
    where TSource : ITableView
{
    #region FilterBase
    /// <summary>
    /// 切换为And
    /// </summary>
    internal override void ToAndCore()
    {
        _filter = _filter.ToAnd();
    }
    /// <summary>
    /// 切换为Or
    /// </summary>
    internal override void ToOrCore() 
    {
        _filter = _filter.ToOr();
    }
    /// <summary>
    /// 添加子逻辑
    /// </summary>
    /// <param name="condition"></param>
    internal override void AddLogic(AtomicLogic condition)
    {
        _filter.AddLogic(condition); 
    }
    #endregion
    #region IDataQuery
    /// <summary>
    /// And查询
    /// </summary>
    /// <param name="conditions"></param>
    /// <returns></returns>
    internal void AddConditions(params IEnumerable<string> conditions)
        => _filter.AddConditions(conditions);
    void IDataSqlQuery.AddConditions(IEnumerable<string> conditions)
        => _filter.AddConditions(conditions);
    void IDataSqlQuery.ApplyFilter(Func<SqlQuery, SqlQuery> query)
        => ApplyFilter(query);
    #endregion
}

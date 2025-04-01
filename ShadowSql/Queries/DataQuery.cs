using ShadowSql.Identifiers;
using ShadowSql.Logics;
using System;
using System.Collections.Generic;

namespace ShadowSql.Queries;

/// <summary>
/// 数据查询
/// </summary>
/// <typeparam name="TSource"></typeparam>
/// <param name="source"></param>
/// <param name="query"></param>
public class DataQuery<TSource>(TSource source, SqlQuery query)
    : DataFilterBase<TSource, SqlQuery>(source, query), IDataQuery
    where TSource : ITableView
{
    #region Sql查询功能
    /// <summary>
    /// 切换为And
    /// </summary>
    internal void ToAndCore()
    {
        _filter = _filter.ToAnd();
    }
    /// <summary>
    /// 切换为Or
    /// </summary>
    internal void ToOrCore() 
    {
        _filter = _filter.ToOr();
    }
    /// <summary>
    /// 应用sql查询
    /// </summary>
    /// <param name="query"></param>
    public void ApplyQuery(Func<SqlQuery, SqlQuery> query)
        => _filter = query(_filter);
    /// <summary>
    /// 执行查询
    /// </summary>
    /// <param name="query"></param>
    internal void ApplyQuery(Func<TSource, SqlQuery, SqlQuery> query)
    {
        _filter = query(_source, _filter);
    }
    /// <summary>
    /// And查询
    /// </summary>
    /// <param name="conditions"></param>
    /// <returns></returns>
    internal void AddConditions(IEnumerable<string> conditions)
    {
        _filter.AddConditions(conditions);
    }
    /// <summary>
    /// 添加子逻辑
    /// </summary>
    /// <param name="condition"></param>
    internal void AddLogic(AtomicLogic condition)
    {
        _filter.AddLogic(condition); 
    }
    #endregion   
    #region IDataQuery
    ITableView IDataQuery.Source
         => _source;

    void IDataQuery.AddConditions(IEnumerable<string> conditions)
        => _filter.AddConditions(conditions);
    void IDataQuery.AddLogic(AtomicLogic condition)
        => _filter.AddLogic(condition);
    /// <summary>
    /// 获取比较字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    public virtual ICompareField GetCompareField(string fieldName)
    {
        if (GetColumn(fieldName) is IColumn column)
            return column;
        return Field(fieldName);
    }
    #endregion
}

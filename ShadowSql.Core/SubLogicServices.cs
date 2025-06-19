using ShadowSql.Identifiers;
using ShadowSql.Queries;
using ShadowSql.SingleSelect;
using ShadowSql.SubQueries;

namespace ShadowSql;

/// <summary>
/// 子查询扩展方法
/// </summary>
public static partial class ShadowSqlCoreServices
{
    #region ITableView
    /// <summary>
    /// EXISTS子查询逻辑
    /// </summary>
    /// <param name="view"></param>
    /// <returns></returns>
    public static ExistsLogic AsExists(this ITableView view)
        => new(view);
    /// <summary>
    /// NOT EXISTS子查询逻辑
    /// </summary>
    /// <param name="view"></param>
    /// <returns></returns>
    public static NotExistsLogic AsNotExists(this ITableView view)
        => new(view);
    #endregion
    #region ICompareView
    /// <summary>
    /// IN子查询逻辑
    /// </summary>
    /// <param name="field">字段</param>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public static SubInLogic In(this ICompareView field, ISingleSelect select)
        => new(field, select);
    /// <summary>
    /// NOT IN子查询逻辑
    /// </summary>
    /// <param name="field">字段</param>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public static SubNotInLogic NotIn(this ICompareView field, ISingleSelect select)
        => new(field, select);
    #endregion
    #region IDataFilter
    /// <summary>
    /// EXISTS子查询逻辑
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="query">查询</param>
    /// <param name="view"></param>
    /// <returns></returns>
    public static Query Exists<Query>(this Query query, ITableView view)
        where Query : IDataSqlQuery
    {
        query.Query.AddLogic(view.AsExists());
        return query;
    }
    /// <summary>
    /// NOT EXISTS子查询逻辑
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="query">查询</param>
    /// <param name="view"></param>
    /// <returns></returns>
    public static Query NotExists<Query>(this Query query, ITableView view)
        where Query : IDataSqlQuery
    {
        query.Query.AddLogic(view.AsNotExists());
        return query;
    }
    /// <summary>
    /// IN子查询逻辑
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="query">查询</param>
    /// <param name="field">字段</param>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public static Query In<Query>(this Query query, ICompareView field, ISingleSelect select)
        where Query : IDataSqlQuery
    {
        query.Query.AddLogic(field.In(select));
        return query;
    }
    /// <summary>
    /// NOT IN子查询逻辑
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="query">查询</param>
    /// <param name="field">字段</param>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public static Query NotIn<Query>(this Query query, ICompareView field, ISingleSelect select)
        where Query : IDataSqlQuery
    {
        query.Query.AddLogic(field.NotIn(select));
        return query;
    }
    #endregion
}

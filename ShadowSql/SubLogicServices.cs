using ShadowSql.Filters;
using ShadowSql.Identifiers;
using ShadowSql.SingleSelect;
using ShadowSql.SubQueries;

namespace ShadowSql;

/// <summary>
/// 子查询扩展方法
/// </summary>
public static partial class ShadowSqlServices
{
    #region ITableView
    /// <summary>
    /// EXISTS子查询逻辑
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static ExistsLogic AsExists(this ITableView source)
        => new(source);
    /// <summary>
    /// NOT EXISTS子查询逻辑
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static NotExistsLogic AsNotExists(this ITableView source)
        => new(source);
    #endregion
    #region ICompareView
    /// <summary>
    /// IN子查询逻辑
    /// </summary>
    /// <param name="field"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public static SubInLogic In(this ICompareView field, ISingleSelect select)
        => new(field, select);
    /// <summary>
    /// NOT IN子查询逻辑
    /// </summary>
    /// <param name="field"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public static SubNotInLogic NotIn(this ICompareView field, ISingleSelect select)
        => new(field, select);
    #endregion
    #region IDataFilter
    /// <summary>
    /// EXISTS子查询逻辑
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="query"></param>
    /// <param name="source"></param>
    /// <returns></returns>
    public static Query Exists<Query>(this Query query, ITableView source)
        where Query : IDataFilter
    {
        query.AddLogic(source.AsExists());
        return query;
    }
    /// <summary>
    /// NOT EXISTS子查询逻辑
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="query"></param>
    /// <param name="source"></param>
    /// <returns></returns>
    public static Query NotExists<Query>(this Query query, ITableView source)
        where Query : IDataFilter
    {
        query.AddLogic(source.AsNotExists());
        return query;
    }
    /// <summary>
    /// IN子查询逻辑
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="query"></param>
    /// <param name="field"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public static Query In<Query>(this Query query, ICompareView field, ISingleSelect select)
        where Query : IDataFilter
    {
        query.AddLogic(field.In(select));
        return query;
    }
    /// <summary>
    /// NOT IN子查询逻辑
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="query"></param>
    /// <param name="field"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public static Query NotIn<Query>(this Query query, ICompareView field, ISingleSelect select)
        where Query : IDataFilter
    {
        query.AddLogic(field.NotIn(select));
        return query;
    }
    #endregion
}

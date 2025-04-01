using ShadowSql.Filters;
using ShadowSql.Logics;
using ShadowSql.Queries;

namespace ShadowSql;

/// <summary>
/// 链式查询
/// </summary>
public static partial class ShadowSqlServices
{
    #region ToAnd
    /// <summary>
    /// 切换为And
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="dataQuery"></param>
    /// <returns></returns>
    public static Query ToAnd<Query>(this Query dataQuery)
        where Query : IDataSqlQuery
    {
        dataQuery.ApplyFilter(query => query.ToAnd());
        return dataQuery;
    }
    #endregion
    #region ToOr
    /// <summary>
    /// 切换为Or
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="dataQuery"></param>
    /// <returns></returns>
    public static Query ToOr<Query>(this Query dataQuery)
        where Query : IDataSqlQuery
    {
        dataQuery.ApplyFilter(query => query.ToOr());
        return dataQuery;
    }
    #endregion
    #region Logic
    /// <summary>
    /// 与运算
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="query"></param>
    /// <param name="condition"></param>
    /// <returns></returns>
    public static Query And<Query>(this Query query, AtomicLogic condition)
        where Query : FilterBase, IDataQuery
    {
        query.AndCore(condition);
        return query;
    }
    /// <summary>
    /// 与运算
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="query"></param>
    /// <param name="condition"></param>
    public static Query And<Query>(this Query query, AndLogic condition)
        where Query : FilterBase, IDataQuery
    {
        query.AndCore(condition);
        return query;
    }
    /// <summary>
    /// 与运算
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="query"></param>
    /// <param name="condition"></param>
    public static Query And<Query>(this Query query, ComplexAndLogic condition)
        where Query : FilterBase, IDataQuery
    {
        query.AndCore(condition);
        return query;
    }
    /// <summary>
    /// 与运算
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="query"></param>
    /// <param name="condition"></param>
    public static Query And<Query>(this Query query, OrLogic condition)
        where Query : FilterBase, IDataQuery
    {
        query.AndCore(condition);
        return query;
    }
    /// <summary>
    /// 与运算
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="query"></param>
    /// <param name="condition"></param>
    public static Query And<Query>(this Query query, ComplexOrLogic condition)
        where Query : FilterBase, IDataQuery
    {
        query.AndCore(condition);
        return query;
    }
    /// <summary>
    /// 与运算
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="query"></param>
    /// <param name="condition"></param>
    public static Query And<Query>(this Query query, Logic condition)
        where Query : FilterBase, IDataQuery
    {
        query.AndCore(condition);
        return query;
    }
    /// <summary>
    /// 或运算
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="query"></param>
    /// <param name="condition"></param>
    public static Query Or<Query>(this Query query, AtomicLogic condition)
        where Query : FilterBase, IDataQuery
    {
        query.OrCore(condition);
        return query;
    }
    /// <summary>
    /// 或运算
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="query"></param>
    /// <param name="condition"></param>
    public static Query Or<Query>(this Query query, AndLogic condition)
        where Query : FilterBase, IDataQuery
    {
        query.OrCore(condition);
        return query;
    }
    /// <summary>
    /// 或运算
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="query"></param>
    /// <param name="condition"></param>
    public static Query Or<Query>(this Query query, ComplexAndLogic condition)
        where Query : FilterBase, IDataQuery
    {
        query.OrCore(condition);
        return query;
    }
    /// <summary>
    /// 或运算
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="query"></param>
    /// <param name="condition"></param>
    public static Query Or<Query>(this Query query, OrLogic condition)
        where Query : FilterBase, IDataQuery
    {
        query.OrCore(condition);
        return query;
    }
    /// <summary>
    /// 或运算
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="query"></param>
    /// <param name="condition"></param>
    public static Query Or<Query>(this Query query, ComplexOrLogic condition)
        where Query : FilterBase, IDataQuery
    {
        query.OrCore(condition);
        return query;
    }
    /// <summary>
    /// 或运算
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="query"></param>
    /// <param name="condition"></param>
    public static Query Or<Query>(this Query query, Logic condition)
        where Query : FilterBase, IDataQuery
    {
        query.OrCore(condition);
        return query;
    }
    #endregion
}

using ShadowSql.Filters;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Queries;
using System;

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
    /// <param name="query"></param>
    /// <returns></returns>
    public static Query ToAnd<Query>(this Query query)
        where Query : IDataSqlQuery
    {
        query.Query = query.Query.ToAnd();
        return query;
    }
    #endregion
    #region ToOr
    /// <summary>
    /// 切换为Or
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="query"></param>
    /// <returns></returns>
    public static Query ToOr<Query>(this Query query)
        where Query : IDataSqlQuery
    {
        query.Query = query.Query.ToOr();
        return query;
    }
    #endregion
    #region 基础查询
    #region 与运算
    /// <summary>
    /// 与运算
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="query"></param>
    /// <param name="logic"></param>
    /// <returns></returns>
    public static Query And<Query>(this Query query, AtomicLogic logic)
        where Query : FilterBase, IDataQuery
    {
        query.Logic = query.Logic.And(logic);
        return query;
    }
    /// <summary>
    /// 与运算
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="query"></param>
    /// <param name="logic"></param>
    public static Query And<Query>(this Query query, AndLogic logic)
        where Query : FilterBase, IDataQuery
    {
        query.Logic = query.Logic.And(logic);
        return query;
    }
    /// <summary>
    /// 与运算
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="query"></param>
    /// <param name="logic"></param>
    public static Query And<Query>(this Query query, ComplexAndLogic logic)
        where Query : FilterBase, IDataQuery
    {
        query.Logic = query.Logic.And(logic);
        return query;
    }
    /// <summary>
    /// 与运算
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="query"></param>
    /// <param name="logic"></param>
    public static Query And<Query>(this Query query, OrLogic logic)
        where Query : FilterBase, IDataQuery
    {
        query.Logic = query.Logic.And(logic);
        return query;
    }
    /// <summary>
    /// 与运算
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="query"></param>
    /// <param name="logic"></param>
    public static Query And<Query>(this Query query, ComplexOrLogic logic)
        where Query : FilterBase, IDataQuery
    {
        query.Logic = query.Logic.And(logic);
        return query;
    }
    /// <summary>
    /// 与运算
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="query"></param>
    /// <param name="logic"></param>
    public static Query And<Query>(this Query query, Logic logic)
        where Query : FilterBase, IDataQuery
    {
        query.Logic = query.Logic.And(logic);
        return query;
    }
    #endregion
    #region 或运算
    /// <summary>
    /// 或运算
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="query"></param>
    /// <param name="logic"></param>
    public static Query Or<Query>(this Query query, AtomicLogic logic)
        where Query : FilterBase, IDataQuery
    {
        query.Logic = query.Logic.Or(logic);
        return query;
    }
    /// <summary>
    /// 或运算
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="query"></param>
    /// <param name="logic"></param>
    public static Query Or<Query>(this Query query, AndLogic logic)
        where Query : FilterBase, IDataQuery
    {
        query.Logic = query.Logic.Or(logic);
        return query;
    }
    /// <summary>
    /// 或运算
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="query"></param>
    /// <param name="logic"></param>
    public static Query Or<Query>(this Query query, ComplexAndLogic logic)
        where Query : FilterBase, IDataQuery
    {
        query.Logic = query.Logic.Or(logic);
        return query;
    }
    /// <summary>
    /// 或运算
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="query"></param>
    /// <param name="logic"></param>
    public static Query Or<Query>(this Query query, OrLogic logic)
        where Query : FilterBase, IDataQuery
    {
        query.Logic = query.Logic.Or(logic);
        return query;
    }
    /// <summary>
    /// 或运算
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="query"></param>
    /// <param name="logic"></param>
    public static Query Or<Query>(this Query query, ComplexOrLogic logic)
        where Query : FilterBase, IDataQuery
    {
        query.Logic = query.Logic.Or(logic);
        return query;
    }
    /// <summary>
    /// 或运算
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="query"></param>
    /// <param name="logic"></param>
    public static Query Or<Query>(this Query query, Logic logic)
        where Query : FilterBase, IDataQuery
    {
        query.Logic = query.Logic.Or(logic);
        return query;
    }
    #endregion
    #endregion
    #region 扩展逻辑
    #region 与运算
    /// <summary>
    /// 与运算
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="query"></param>
    /// <param name="logic"></param>
    /// <returns></returns>
    public static Query And<Query>(this Query query, Func<ITableView, AtomicLogic> logic)
        where Query : FilterBase, IDataQuery
    {
        query.Logic = query.Logic.And(logic(query.Source));
        return query;
    }
    /// <summary>
    /// 与运算嵌套或逻辑
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="query"></param>
    /// <param name="logic"></param>
    /// <returns></returns>
    public static Query And<Query>(this Query query, Func<ITableView, OrLogic> logic)
        where Query : FilterBase, IDataQuery
    {
        query.Logic = query.Logic.And(logic(query.Source));
        return query;
    }
    #endregion
    #region 或运算
    /// <summary>
    /// 或运算
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="query"></param>
    /// <param name="logic"></param>
    /// <returns></returns>
    public static Query Or<Query>(this Query query, Func<ITableView, AtomicLogic> logic)
        where Query : FilterBase, IDataQuery
    {
        query.Logic = query.Logic.Or(logic(query.Source));
        return query;
    }
    /// <summary>
    /// 或运算嵌套与逻辑
    /// </summary>
    /// <typeparam name="Query"></typeparam>
    /// <param name="query"></param>
    /// <param name="logic"></param>
    /// <returns></returns>
    public static Query Or<Query>(this Query query, Func<ITableView, AndLogic> logic)
        where Query : FilterBase, IDataQuery
    {
        query.Logic = query.Logic.Or(logic(query.Source));
        return query;
    }
    #endregion
    #region TableQuery

    #endregion
    #endregion
}

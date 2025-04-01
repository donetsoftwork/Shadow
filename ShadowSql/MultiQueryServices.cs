using ShadowSql.CompareLogics;
using ShadowSql.Compares;
using ShadowSql.Identifiers;
using ShadowSql.Join;
using ShadowSql.Logics;
using ShadowSql.Queries;
using System;

namespace ShadowSql;

/// <summary>
/// 多联表查询
/// </summary>
public static partial class ShadowSqlServices
{
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <typeparam name="MultiQuery"></typeparam>
    /// <param name="multiQuery"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static MultiQuery Where<MultiQuery>(this MultiQuery multiQuery, Func<IMultiTable, AtomicLogic> query)
        where MultiQuery : MultiTableBase
    {
        multiQuery.InnerQuery.AddLogic(query(multiQuery));
        return multiQuery;
    }
    /// <summary>
    /// Where
    /// </summary>
    /// <typeparam name="MultiQuery"></typeparam>
    /// <param name="multiQuery"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static MultiQuery Where<MultiQuery>(this MultiQuery multiQuery, Func<IMultiTable, SqlQuery, SqlQuery> query)
        where MultiQuery : MultiTableBase
    {
        multiQuery.InnerQuery.ApplyQuery(query);
        return multiQuery;
    }
    /// <summary>
    /// 指定表查询
    /// </summary>
    /// <typeparam name="MultiQuery"></typeparam>
    /// <param name="multiQuery"></param>
    /// <param name="tableName"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static MultiQuery Where<MultiQuery>(this MultiQuery multiQuery, string tableName, Func<IAliasTable, AtomicLogic> query)
        where MultiQuery : MultiTableBase
    {
        multiQuery.InnerQuery.AddLogic(query(multiQuery.From(tableName)));
        return multiQuery;
    }
    /// <summary>
    /// 指定表查询
    /// </summary>
    /// <typeparam name="MultiQuery"></typeparam>
    /// <param name="multiQuery"></param>
    /// <param name="tableName"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static MultiQuery Where<MultiQuery>(this MultiQuery multiQuery, string tableName, Func<IAliasTable, SqlQuery, SqlQuery> query)
    where MultiQuery : MultiTableBase
    {
        multiQuery.InnerQuery.ApplyQuery(q => query(multiQuery.From(tableName), q));
        return multiQuery;
    }
}

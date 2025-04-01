using ShadowSql.Identifiers;
using ShadowSql.Queries;
using ShadowSql.Variants;
using System.Collections.Generic;

namespace ShadowSql;

/// <summary>
/// 构造查询扩展方法
/// </summary>
public static partial class ShadowSqlServices
{
    #region TableQuery
    /// <summary>
    /// 查询(And查询)
    /// </summary>
    /// <param name="table"></param>
    /// <returns></returns>
    public static TableQuery<TTable> Where<TTable>(this TTable table)
        where TTable : ITable
        => ToQuery(table);

    /// <summary>
    /// And查询
    /// </summary>
    /// <param name="table">表对象</param>
    /// <returns></returns>
    public static TableQuery<TTable> ToQuery<TTable>(this TTable table)
        where TTable : ITable
    {
        SqlQuery sqlQuery = SqlQuery.CreateAndQuery();
        TableQuery<TTable> tableQuery = new(table, sqlQuery);
        return tableQuery;
    }
    /// <summary>
    /// Or查询
    /// </summary>
    /// <param name="table">表对象</param>
    /// <returns></returns>
    public static TableQuery<TTable> ToOrQuery<TTable>(this TTable table)
        where TTable : ITable
    {
        SqlQuery sqlQuery = SqlQuery.CreateOrQuery();
        TableQuery<TTable> query = new(table, sqlQuery);
        return query;
    }
    #endregion
    #region AliasTableQuery
    /// <summary>
    /// And查询
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <returns></returns>
    public static AliasTableQuery<TTable> Where<TTable>(this TableAlias<TTable> table)
        where TTable : ITable
        => ToQuery(table);
    /// <summary>
    /// And查询
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <returns></returns>
    public static AliasTableQuery<TTable> ToQuery<TTable>(this TableAlias<TTable> table)
        where TTable : ITable
    {
        SqlQuery sqlQuery = SqlQuery.CreateAndQuery();
        AliasTableQuery<TTable> query = new(table, sqlQuery);
        return query;
    }
    /// <summary>
    /// Or查询
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <returns></returns>
    public static AliasTableQuery<TTable> ToOrQuery<TTable>(this TableAlias<TTable> table)
        where TTable : ITable
    {
        SqlQuery sqlQuery = SqlQuery.CreateOrQuery();
        AliasTableQuery<TTable> query = new(table, sqlQuery);
        return query;
    }
    #endregion
}

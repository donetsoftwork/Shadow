using ShadowSql.AliasTables;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Queries;
using ShadowSql.Tables;
using ShadowSql.Variants;

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
    #region TableQuery
    /// <summary>
    /// And查询
    /// </summary>
    /// <param name="table">表对象</param>
    /// <returns></returns>
    public static TableQuery<TTable> ToQuery<TTable>(this TTable table)
        where TTable : ITable
        => new(table);
    /// <summary>
    /// Or查询
    /// </summary>
    /// <param name="table">表对象</param>
    /// <returns></returns>
    public static TableQuery<TTable> ToOrQuery<TTable>(this TTable table)
        where TTable : ITable
        => new(table, new OrLogic());
    #endregion
    #region SlimQuery
    /// <summary>
    /// And查询
    /// </summary>
    /// <param name="table">表对象</param>
    /// <returns></returns>
    public static TableQuery ToSlimQuery(this ITable table)
        => new(table);
    /// <summary>
    /// Or查询
    /// </summary>
    /// <param name="table">表对象</param>
    /// <returns></returns>
    public static TableQuery ToSlimOrQuery(this ITable table)
        => new(table, new OrLogic());
    #endregion
    #region TableSqlQuery
    /// <summary>
    /// And查询
    /// </summary>
    /// <param name="table">表对象</param>
    /// <returns></returns>
    public static TableSqlQuery<TTable> ToSqlQuery<TTable>(this TTable table)
        where TTable : ITable
        => new(table, SqlQuery.CreateAndQuery());
    /// <summary>
    /// Or查询
    /// </summary>
    /// <param name="table">表对象</param>
    /// <returns></returns>
    public static TableSqlQuery<TTable> ToSqlOrQuery<TTable>(this TTable table)
        where TTable : ITable
        => new(table, SqlQuery.CreateOrQuery());
    #endregion
    #region SlimSqlQuery
    /// <summary>
    /// And查询
    /// </summary>
    /// <param name="table">表对象</param>
    /// <returns></returns>
    public static TableSqlQuery ToSlimSqlQuery(this ITable table)
        => new(table, SqlQuery.CreateAndQuery());
    /// <summary>
    /// Or查询
    /// </summary>
    /// <param name="table">表对象</param>
    /// <returns></returns>
    public static TableSqlQuery ToSlimSqlOrQuery(this ITable table)
        => new(table, SqlQuery.CreateOrQuery());
    #endregion
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
    #region Query
    /// <summary>
    /// And查询
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <returns></returns>
    public static AliasTableQuery<TTable> ToQuery<TTable>(this TableAlias<TTable> table)
        where TTable : ITable
        => new(table);
    /// <summary>
    /// Or查询
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <returns></returns>
    public static AliasTableQuery<TTable> ToOrQuery<TTable>(this TableAlias<TTable> table)
        where TTable : ITable
        => new(table, new OrLogic());
    #endregion
    #region SqlQuery
    /// <summary>
    /// And查询
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <returns></returns>
    public static AliasTableSqlQuery<TTable> ToSqlQuery<TTable>(this TableAlias<TTable> table)
        where TTable : ITable
    {
        SqlQuery sqlQuery = SqlQuery.CreateAndQuery();
        AliasTableSqlQuery<TTable> query = new(table, sqlQuery);
        return query;
    }
    /// <summary>
    /// Or查询
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <returns></returns>
    public static AliasTableSqlQuery<TTable> ToSqlOrQuery<TTable>(this TableAlias<TTable> table)
        where TTable : ITable
    {
        SqlQuery sqlQuery = SqlQuery.CreateOrQuery();
        AliasTableSqlQuery<TTable> query = new(table, sqlQuery);
        return query;
    }
    #endregion
    #endregion
}

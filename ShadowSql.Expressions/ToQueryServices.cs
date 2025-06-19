using ShadowSql.Expressions.AliasTables;
using ShadowSql.Expressions.Tables;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Queries;

namespace ShadowSql.Expressions;

/// <summary>
/// 构造查询扩展方法
/// </summary>
public static partial class ShadowSqlServices
{
    #region TableQuery
    #region TableQuery
    /// <summary>
    /// And查询
    /// </summary>
    /// <param name="table">表对象</param>
    /// <returns></returns>
    public static TableQuery<TEntity> ToQuery<TEntity>(this ITable table)
        => new(table);
    /// <summary>
    /// Or查询
    /// </summary>
    /// <param name="table">表对象</param>
    /// <returns></returns>
    public static TableQuery<TEntity> ToOrQuery<TEntity>(this ITable table)
        => new(table);
    #endregion
    #region TableSqlQuery
    /// <summary>
    /// And查询
    /// </summary>
    /// <param name="table">表对象</param>
    /// <returns></returns>
    public static TableSqlQuery<TEntity> ToSqlQuery<TEntity>(this ITable table)
        => new(table);
    /// <summary>
    /// Or查询
    /// </summary>
    /// <param name="table">表对象</param>
    /// <returns></returns>
    public static TableSqlQuery<TEntity> ToSqlOrQuery<TEntity>(this ITable table)
        => new(table, SqlQuery.CreateOrQuery());
    #endregion
    /// <summary>
    /// 查询(And查询)
    /// </summary>
    /// <param name="table">表</param>
    /// <returns></returns>
    public static TableSqlQuery<TEntity> Where<TEntity>(this ITable table)
        => table.ToSqlQuery<TEntity>();
    #endregion
    #region AliasTableQuery
    #region Query
    /// <summary>
    /// And查询
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="table">表</param>
    /// <returns></returns>
    public static AliasTableQuery<TEntity> ToQuery<TEntity>(this IAliasTable table)
        => new(table, new AndLogic());
    /// <summary>
    /// Or查询
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="table">表</param>
    /// <returns></returns>
    public static AliasTableQuery<TEntity> ToOrQuery<TEntity>(this IAliasTable table)
        => new(table, new OrLogic());
    #endregion
    #region SqlQuery
    /// <summary>
    /// And查询
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="table">表</param>
    /// <returns></returns>
    public static AliasTableSqlQuery<TEntity> ToSqlQuery<TEntity>(this IAliasTable table)
        => new(table, SqlQuery.CreateAndQuery());
    /// <summary>
    /// Or查询
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="table">表</param>
    /// <returns></returns>
    public static AliasTableSqlQuery<TEntity> ToSqlOrQuery<TEntity>(this IAliasTable table)
        => new(table, SqlQuery.CreateOrQuery());
    #endregion
    /// <summary>
    /// And查询
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="table">表</param>
    /// <returns></returns>
    public static AliasTableSqlQuery<TEntity> Where<TEntity>(this IAliasTable table)
        => table.ToSqlQuery<TEntity>();
    #endregion
}

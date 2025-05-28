using ShadowSql.Delete;
using ShadowSql.Expressions.AliasTables;
using ShadowSql.Expressions.Tables;

namespace ShadowSql.Expressions;

/// <summary>
/// Delete扩展方法
/// </summary>
public static partial class ShadowSqlServices
{
    #region TableDelete
    /// <summary>
    /// 删除
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="query"></param>
    /// <returns></returns>
    public static TableDelete ToDelete<TEntity>(this TableSqlQuery<TEntity> query)
        => new(query.Source, query._filter);
    /// <summary>
    /// 删除
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="query"></param>
    /// <returns></returns>
    public static TableDelete ToDelete<TEntity>(this TableQuery<TEntity> query)
        => new(query.Source, query._filter);
    #endregion
    #region AliasTableDelete
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public static AliasTableDelete ToDelete<TEntity>(this AliasTableSqlQuery<TEntity> query)
        => new(query.Source, query._filter);
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public static AliasTableDelete ToDelete<TEntity>(this AliasTableQuery<TEntity> query)
        => new(query.Source, query._filter);
    #endregion
}

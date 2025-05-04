using ShadowSql.AliasTables;
using ShadowSql.Delete;
using ShadowSql.Identifiers;
using ShadowSql.Tables;

namespace ShadowSql;

/// <summary>
/// Delete扩展方法
/// </summary>
public static partial class ShadowSqlServices
{
    #region TableDelete
    /// <summary>
    /// 删除
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="query"></param>
    /// <returns></returns>
    public static TableDelete ToDelete<TSource>(this TableSqlQuery<TSource> query)
        where TSource : ITable
        => new(query.Source, query._filter);
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public static TableDelete ToDelete(this TableSqlQuery query)
        => new((ITable)query.Source, query._filter);
    /// <summary>
    /// 删除
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="query"></param>
    /// <returns></returns>
    public static TableDelete ToDelete<TSource>(this TableQuery<TSource> query)
        where TSource : ITable
        => new(query.Source, query._filter);
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public static TableDelete ToDelete(this TableQuery query)
        => new((ITable)query.Source, query._filter);
    #endregion
    #region AliasTableDelete
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public static AliasTableDelete ToDelete<TSource>(this AliasTableSqlQuery<TSource> query)
        where TSource : ITable
        => new(query.Source, query._filter);
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public static AliasTableDelete ToDelete<TSource>(this AliasTableQuery<TSource> query)
        where TSource : ITable
        => new(query.Source, query._filter);
    #endregion
}

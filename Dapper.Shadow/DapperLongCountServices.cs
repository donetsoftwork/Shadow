using ShadowSql.AliasTables;
using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Select;
using ShadowSql.SingleSelect;
using ShadowSql.Tables;
using ShadowSql.Variants;

namespace Dapper.Shadow;

/// <summary>
/// Dapper计数扩展方法
/// </summary>
public static partial class DapperShadowServices
{
    #region IDapperTable
    #region TTable
    /// <summary>
    /// 计数
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <returns></returns>
    public static long LongCount<TTable>(this TTable table)
        where TTable : IDapperTable
        => table.Executor.Count<long>(table);
    /// <summary>
    /// 计数
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <param name="where"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static long LongCount<TTable>(this TTable table, ISqlLogic where, object? param = null)
        where TTable : IDapperTable
        => table.Executor.Count<long>(new TableFilter(table, where), param);
    /// <summary>
    /// 计数
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static long LongCount<TTable>(this TableSqlQuery<TTable> query, object? param = null)
        where TTable : IDapperTable
        => query.Source.Executor.Count<long>(query, param);
    /// <summary>
    /// 计数
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static long LongCount<TTable>(this TableQuery<TTable> query, object? param = null)
        where TTable : IDapperTable
        => query.Source.Executor.Count<long>(query, param);
    #endregion
    #region AliasTable
    /// <summary>
    /// 别名表计数
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static long LongCount<TTable>(this TableAlias<TTable> table, object? param = null)
        where TTable : IDapperTable
        => table.Target.Executor.Count<long>(table, param);
    /// <summary>
    /// 别名表过滤计数
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <param name="filter"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static long LongCount<TTable>(this TableAlias<TTable> table, ISqlLogic filter, object? param = null)
        where TTable : IDapperTable
        => table.Target.Executor.Count<long>(new TableFilter(table, filter), param);
    /// <summary>
    /// 别名表计数
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static long LongCount<TTable>(this AliasTableSqlQuery<TTable> query, object? param = null)
        where TTable : IDapperTable
        => query.Table.Executor.Count<long>(query, param);
    /// <summary>
    /// 别名表计数
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static long LongCount<TTable>(this AliasTableQuery<TTable> query, object? param = null)
        where TTable : IDapperTable
        => query.Table.Executor.Count<long>(query, param);
    #endregion
    #region GroupByTable
    /// <summary>
    /// GroupBy后计数
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static long LongCount<TTable>(this GroupByTableSqlQuery<TTable> query, object? param = null)
        where TTable : IDapperTable
        => query.Source.Executor.Count<long>(query, param);
    /// <summary>
    /// GroupBy后计数
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static long LongCount<TTable>(this GroupByTableQuery<TTable> query, object? param = null)
        where TTable : IDapperTable
        => query.Source.Executor.Count<long>(query, param);
    #endregion
    #region GroupByAliasTable
    /// <summary>
    /// GroupBy别名表后计数
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static long LongCount<TTable>(this GroupByAliasTableSqlQuery<TTable> query, object? param = null)
        where TTable : IDapperTable
        => query.Source.Target.Executor.Count<long>(query, param);
    /// <summary>
    /// GroupBy别名表后计数
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static long LongCount<TTable>(this GroupByAliasTableQuery<TTable> query, object? param = null)
        where TTable : IDapperTable
        => query.Source.Target.Executor.Count<long>(query, param);
    #endregion
    #endregion
}

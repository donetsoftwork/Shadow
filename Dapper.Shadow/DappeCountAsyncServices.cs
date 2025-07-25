using ShadowSql.AliasTables;
using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Tables;
using System.Threading.Tasks;

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
    /// <param name="table">表</param>
    /// <returns></returns>
    public static Task<int> CountAsync<TTable>(this TTable table)
        where TTable : IDapperTable
        => table.Executor.CountAsync<int>(table);
    /// <summary>
    /// 计数
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table">表</param>
    /// <param name="where">查询条件</param>
    /// <param name="param">参数</param>
    /// <returns></returns>
    public static Task<int> CountAsync<TTable>(this TTable table, ISqlLogic where, object? param = null)
        where TTable : IDapperTable
        => table.Executor.CountAsync<int>(new TableFilter(table, where), param);
    /// <summary>
    /// 计数
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query">查询</param>
    /// <param name="param">参数</param>
    /// <returns></returns>
    public static Task<int> CountAsync<TTable>(this TableSqlQuery<TTable> query, object? param = null)
        where TTable : IDapperTable
        => query.Source.Executor.CountAsync<int>(query, param);
    /// <summary>
    /// 计数
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query">查询</param>
    /// <param name="param">参数</param>
    /// <returns></returns>
    public static Task<int> CountAsync<TTable>(this TableQuery<TTable> query, object? param = null)
        where TTable : IDapperTable
        => query.Source.Executor.CountAsync<int>(query, param);
    #endregion
    #region AliasTable
    /// <summary>
    /// 别名表计数
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="aliasTable">别名表</param>
    /// <param name="param">参数</param>
    /// <returns></returns>
    public static Task<int> CountAsync<TTable>(this IAliasTable<TTable> aliasTable, object? param = null)
        where TTable : IDapperTable
        => aliasTable.Target.Executor.CountAsync<int>(aliasTable, param);
    /// <summary>
    /// 别名表过滤计数
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="aliasTable">别名表</param>
    /// <param name="filter">过滤条件</param>
    /// <param name="param">参数</param>
    /// <returns></returns>
    public static Task<int> CountAsync<TTable>(this IAliasTable<TTable> aliasTable, ISqlLogic filter, object? param = null)
        where TTable : IDapperTable
        => aliasTable.Target.Executor.CountAsync<int>(new TableFilter(aliasTable, filter), param);
    /// <summary>
    /// 别名表计数
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query">查询</param>
    /// <param name="param">参数</param>
    /// <returns></returns>
    public static Task<int> CountAsync<TTable>(this AliasTableSqlQuery<TTable> query, object? param = null)
        where TTable : IDapperTable
        => query.Table.Executor.CountAsync<int>(query, param);
    /// <summary>
    /// 别名表计数
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query">查询</param>
    /// <param name="param">参数</param>
    /// <returns></returns>
    public static Task<int> CountAsync<TTable>(this AliasTableQuery<TTable> query, object? param = null)
        where TTable : IDapperTable
        => query.Table.Executor.CountAsync<int>(query, param);
    #endregion
    #region GroupByTable
    /// <summary>
    /// GroupBy后计数
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query">查询</param>
    /// <param name="param">参数</param>
    /// <returns></returns>
    public static Task<int> CountAsync<TTable>(this GroupByTableSqlQuery<TTable> query, object? param = null)
        where TTable : IDapperTable
        => query._source.Executor.CountAsync<int>(query, param);
    /// <summary>
    /// GroupBy后计数
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query">查询</param>
    /// <param name="param">参数</param>
    /// <returns></returns>
    public static Task<int> CountAsync<TTable>(this GroupByTableQuery<TTable> query, object? param = null)
        where TTable : IDapperTable
        => query._source.Executor.CountAsync<int>(query, param);
    #endregion
    #region GroupByAliasTable
    /// <summary>
    /// GroupBy别名表后计数
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query">查询</param>
    /// <param name="param">参数</param>
    /// <returns></returns>
    public static Task<int> CountAsync<TTable>(this GroupByAliasTableSqlQuery<TTable> query, object? param = null)
        where TTable : IDapperTable
        => query._source.Target.Executor.CountAsync<int>(query, param);
    /// <summary>
    /// GroupBy别名表后计数
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query">查询</param>
    /// <param name="param">参数</param>
    /// <returns></returns>
    public static Task<int> CountAsync<TTable>(this GroupByAliasTableQuery<TTable> query, object? param = null)
        where TTable : IDapperTable
        => query._source.Target.Executor.CountAsync<int>(query, param);
    #endregion
    #endregion    
}

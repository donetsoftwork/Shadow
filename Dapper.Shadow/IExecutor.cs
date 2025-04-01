using ShadowSql.Fragments;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dapper.Shadow;

/// <summary>
/// 执行器
/// </summary>
public interface IExecutor
{
    #region Execute
    /// <summary>
    /// Execute parameterized SQL.
    /// </summary>
    /// <param name="sql">The SQL to execute for this query.</param>
    /// <param name="param">The parameters to use for this query.</param>
    /// <returns>The number of rows affected.</returns>
    public int Execute(ISqlEntity sql, object? param = null);
    #endregion
    #region ExecuteScalar
    /// <summary>
    /// Execute parameterized SQL that selects a single value.
    /// </summary>
    /// <typeparam name="T">The type to return.</typeparam>
    /// <param name="sql">The SQL to execute.</param>
    /// <param name="param">The parameters to use for this command.</param>
    /// <returns>The first cell returned, as <typeparamref name="T"/>.</returns>
    public T? ExecuteScalar<T>(ISqlEntity sql, object? param = null);
    #endregion
    #region ExecuteAsync
    /// <summary>
    /// Execute parameterized SQL.
    /// </summary>
    /// <param name="sql">The SQL to execute for this query.</param>
    /// <param name="param">The parameters to use for this query.</param>
    /// <returns>The number of rows affected.</returns>
    public Task<int> ExecuteAsync(ISqlEntity sql, object? param = null);
    #endregion
    #region ExecuteScalarAsync
    /// <summary>
    /// Execute parameterized SQL that selects a single value.
    /// </summary>
    /// <typeparam name="T">The type to return.</typeparam>
    /// <param name="sql">The SQL to execute.</param>
    /// <param name="param">The parameters to use for this command.</param>
    /// <returns>The first cell returned, as <typeparamref name="T"/>.</returns>
    public Task<T?> ExecuteScalarAsync<T>(ISqlEntity sql, object? param = null);
    #endregion
    #region Query
    /// <summary>
    /// Executes a query, returning the data typed as <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of results to return.</typeparam>
    /// <param name="sql">The SQL to execute for the query.</param>
    /// <param name="param">The parameters to pass, if any.</param>
    /// <returns>
    /// A sequence of data of the supplied type; if a basic type (int, string, etc) is queried then the data from the first column is assumed, otherwise an instance is
    /// created per row, and a direct column-name===member-name mapping is assumed (case insensitive).
    /// </returns>
    public IEnumerable<T> Query<T>(ISqlEntity sql, object? param = null);
    /// <summary>
    /// Executes a single-row query, returning the data typed as <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of result to return.</typeparam>
    /// <param name="sql">The SQL to execute for the query.</param>
    /// <param name="param">The parameters to pass, if any.</param>
    /// <returns>
    /// A sequence of data of the supplied type; if a basic type (int, string, etc) is queried then the data from the first column is assumed, otherwise an instance is
    /// created per row, and a direct column-name===member-name mapping is assumed (case insensitive).
    /// </returns>
    public T? QueryFirstOrDefault<T>(ISqlEntity sql, object? param = null);
    #endregion
    #region QueryAsync
    /// <summary>
    /// Execute a query asynchronously using Task.
    /// </summary>
    /// <typeparam name="T">The type of results to return.</typeparam>
    /// <param name="sql">The SQL to execute for the query.</param>
    /// <param name="param">The parameters to pass, if any.</param>
    public Task<IEnumerable<T>> QueryAsync<T>(ISqlEntity sql, object? param = null);
    /// <summary>
    /// Execute a single-row query asynchronously using Task.
    /// </summary>
    /// <typeparam name="T">The type of result to return.</typeparam>
    /// <param name="sql">The SQL to execute for the query.</param>
    /// <param name="param">The parameters to pass, if any.</param>
    public Task<T?> QueryFirstOrDefaultAsync<T>(ISqlEntity sql, object? param = null);
    #endregion
}

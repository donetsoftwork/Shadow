using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Dapper.Shadow;

/// <summary>
/// Dapper执行器
/// </summary>
/// <param name="engine">数据库引擎</param>
/// <param name="connection">数据库连接</param>
/// <param name="buffered"></param>
/// <param name="capacity"></param>
public class DapperExecutor(ISqlEngine engine, IDbConnection connection, bool buffered = true, int capacity = 128)
    : IExecutor
{
    #region 配置
    /// <summary>
    /// 数据库引擎
    /// </summary>
    protected readonly ISqlEngine _engine = engine;
    private readonly IDbConnection _connection = connection;
    private readonly bool _buffered = buffered;
    /// <summary>
    /// sql语句默认大小
    /// </summary>
    protected readonly int _capacity = capacity;
    private int? _commandTimeout;
    private IDbTransaction? _transaction = null;
    /// <summary>
    /// 数据库引擎
    /// </summary>
    public ISqlEngine Engine
        => _engine;
    /// <summary>
    /// 数据库连接
    /// </summary>
    public IDbConnection Connection
        => _connection;
    /// <summary>
    /// sql语句默认大小
    /// </summary>
    public int Capacity
        => _capacity;
    /// <summary>
    /// Number of seconds before command execution timeout.
    /// </summary>
    public int? CommandTimeout
    { 
        get => _commandTimeout; 
        set => _commandTimeout = value; 
    }
    /// <summary>
    /// The transaction to use for this command.
    /// </summary>
    public IDbTransaction? Transaction 
        => _transaction;
    /// <summary>
    /// Whether to buffer results in memory.
    /// </summary>
    public bool Buffered 
        => _buffered;
    #endregion
    #region 事务
    /// <summary>
    /// 开启事务
    /// </summary>
    public void BeginTransaction()
        => _transaction = _connection.BeginTransaction();
    /// <summary>
    /// 开启事务
    /// </summary>
    /// <param name="level"></param>
    public void BeginTransaction(IsolationLevel level)
        => _transaction = _connection.BeginTransaction(level);
    /// <summary>
    /// 提交事务
    /// </summary>
    public void Commit()
        => _transaction?.Commit();
    /// <summary>
    /// 回滚事务
    /// </summary>
    public void Rollback()
        => _transaction?.Rollback();
    #endregion
    ///// <summary>
    ///// 构造sql
    ///// </summary>
    ///// <param name="entity"></param>
    ///// <returns></returns>
    //public string BuildSql(ISqlEntity entity)
    //    => _engine.Sql(entity, _capacity);
    ///// <summary>
    ///// 构造计数sql
    ///// </summary>
    ///// <param name="view"></param>
    ///// <returns></returns>
    //public string BuildCountSql(ITableView view)
    //    => _engine.CountSql(view, _capacity);
    #region ISqlEntity
    #region Execute
    /// <summary>
    /// 执行
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="param">参数</param>
    /// <returns></returns>
    public virtual int Execute(ISqlEntity entity, object? param = null)
        => Execute(_engine.Sql(entity, _capacity), param);
    #endregion
    #region ExecuteScalar
    /// <summary>
    /// 执行(返回一行一列)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    /// <param name="param">参数</param>
    /// <returns></returns>
    public virtual T? ExecuteScalar<T>(ISqlEntity entity, object? param = null)
        => ExecuteScalar<T>(_engine.Sql(entity, _capacity), param);
    #endregion
    #region Count
    /// <summary>
    /// 计数
    /// </summary>
    /// <param name="view"></param>
    /// <param name="param">参数</param>
    /// <returns></returns>
    public virtual T? Count<T>(ITableView view, object? param = null)
        => ExecuteScalar<T>(_engine.CountSql(view, _capacity), param);
    #endregion
    #region CountAsync
    /// <summary>
    /// 异步计数
    /// </summary>
    /// <param name="view"></param>
    /// <param name="param">参数</param>
    /// <returns></returns>
    public virtual Task<T?> CountAsync<T>(ITableView view, object? param = null)
        => ExecuteScalarAsync<T>(_engine.CountSql(view, _capacity), param);
    #endregion
    #region ExecuteAsync
    /// <summary>
    /// 异步执行
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="param">参数</param>
    /// <returns></returns>
    public virtual Task<int> ExecuteAsync(ISqlEntity entity, object? param = null)
        => ExecuteAsync(_engine.Sql(entity, _capacity), param);
    #endregion
    #region ExecuteScalarAsync
    /// <summary>
    /// 异步执行(返回一行一列)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    /// <param name="param">参数</param>
    /// <returns></returns>
    public virtual Task<T?> ExecuteScalarAsync<T>(ISqlEntity entity, object? param = null)
        => ExecuteScalarAsync<T>(_engine.Sql(entity, _capacity), param);
    #endregion
    #region Query
    /// <summary>
    /// 获取列表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    /// <param name="param">参数</param>
    /// <returns></returns>
    public virtual IEnumerable<T> Query<T>(ISqlEntity entity, object? param = null)
        => Query<T>(_engine.Sql(entity, _capacity), param);
    /// <summary>
    /// 获取单条
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    /// <param name="param">参数</param>
    /// <returns></returns>
    public virtual T? QueryFirstOrDefault<T>(ISqlEntity entity, object? param = null)
        => QueryFirstOrDefault<T>(_engine.Sql(entity, _capacity), param);
    #endregion
    #region QueryAsync
    /// <summary>
    /// Execute a query asynchronously using Task.
    /// </summary>
    /// <typeparam name="T">The type of results to return.</typeparam>
    /// <param name="entity">The SQL to execute for the query.</param>
    /// <param name="param">The parameters to pass, if any.</param>
    public virtual Task<IEnumerable<T>> QueryAsync<T>(ISqlEntity entity, object? param = null)
        => QueryAsync<T>(_engine.Sql(entity, _capacity), param);
    /// <summary>
    /// Execute a single-row query asynchronously using Task.
    /// </summary>
    /// <typeparam name="T">The type of result to return.</typeparam>
    /// <param name="entity">The SQL to execute for the query.</param>
    /// <param name="param">The parameters to pass, if any.</param>
    public virtual Task<T?> QueryFirstOrDefaultAsync<T>(ISqlEntity entity, object? param = null)
        => QueryFirstOrDefaultAsync<T>(_engine.Sql(entity, _capacity), param);
    #endregion
    #endregion
    #region Dapper
    #region Execute
    /// <summary>
    /// Execute parameterized SQL.
    /// </summary>
    /// <param name="sql">The SQL to execute for this query.</param>
    /// <param name="param">The parameters to use for this query.</param>
    /// <returns>The number of rows affected.</returns>
    public int Execute(string sql, object? param = null)
        => _connection.Execute(sql, param, _transaction, _commandTimeout, CommandType.Text);
    #endregion
    #region ExecuteScalar
    /// <summary>
    /// Execute parameterized SQL that selects a single value.
    /// </summary>
    /// <typeparam name="T">The type to return.</typeparam>
    /// <param name="sql">The SQL to execute.</param>
    /// <param name="param">The parameters to use for this command.</param>
    /// <returns>The first cell returned, as <typeparamref name="T"/>.</returns>
    public T? ExecuteScalar<T>(string sql, object? param = null)
        => _connection.ExecuteScalar<T>(sql, param, _transaction, _commandTimeout, CommandType.Text);
    #endregion
    #region ExecuteAsync
    /// <summary>
    /// Execute parameterized SQL.
    /// </summary>
    /// <param name="sql">The SQL to execute for this query.</param>
    /// <param name="param">The parameters to use for this query.</param>
    /// <returns>The number of rows affected.</returns>
    public Task<int> ExecuteAsync(string sql, object? param = null)
        => _connection.ExecuteAsync(sql, param, _transaction, _commandTimeout, CommandType.Text);
    #endregion
    #region ExecuteScalarAsync
    /// <summary>
    /// Execute parameterized SQL that selects a single value.
    /// </summary>
    /// <typeparam name="T">The type to return.</typeparam>
    /// <param name="sql">The SQL to execute.</param>
    /// <param name="param">The parameters to use for this command.</param>
    /// <returns>The first cell returned, as <typeparamref name="T"/>.</returns>
    public Task<T?> ExecuteScalarAsync<T>(string sql, object? param = null)
        => _connection.ExecuteScalarAsync<T>(sql, param, _transaction, _commandTimeout, CommandType.Text);
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
    public IEnumerable<T> Query<T>(string sql, object? param = null)
        => _connection.Query<T>(sql, param, _transaction, _buffered, _commandTimeout, CommandType.Text);
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
    public T? QueryFirstOrDefault<T>(string sql, object? param = null)
        => _connection.QueryFirstOrDefault<T>(sql, param, _transaction, _commandTimeout, CommandType.Text);
    #endregion
    #region QueryAsync
    /// <summary>
    /// Execute a query asynchronously using Task.
    /// </summary>
    /// <typeparam name="T">The type of results to return.</typeparam>
    /// <param name="sql">The SQL to execute for the query.</param>
    /// <param name="param">The parameters to pass, if any.</param>
    public Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null)
        => _connection.QueryAsync<T>(sql, param, _transaction, _commandTimeout, CommandType.Text);
    /// <summary>
    /// Execute a single-row query asynchronously using Task.
    /// </summary>
    /// <typeparam name="T">The type of result to return.</typeparam>
    /// <param name="sql">The SQL to execute for the query.</param>
    /// <param name="param">The parameters to pass, if any.</param>
    public Task<T?> QueryFirstOrDefaultAsync<T>(string sql, object? param = null)
        => _connection.QueryFirstOrDefaultAsync<T>(sql, param, _transaction, _commandTimeout, CommandType.Text);
    #endregion
    #endregion
}

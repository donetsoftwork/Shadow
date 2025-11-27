using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using System.Collections.Generic;
using System.Data;
using System.Threading;
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
    private readonly CommandFlags _flags = buffered ? CommandFlags.Buffered : CommandFlags.None;
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
    /// Whether the results should be buffered during query execution.
    /// </summary>
    public CommandFlags Flags
        => _flags;
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
    #region CountAsync
    /// <summary>
    /// 计数
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="view"></param>
    /// <param name="param"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public virtual Task<T?> CountAsync<T>(ITableView view, object? param = null, CancellationToken token = default)
        => ExecuteScalarAsync<T>(_engine.CountSql(view, _capacity), param, token);
    #endregion
    #region ExecuteAsync
    /// <summary>
    /// 执行
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="param"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public virtual Task<int> ExecuteAsync(ISqlEntity entity, object? param = null, CancellationToken token = default)
        => ExecuteAsync(_engine.Sql(entity, _capacity), param, token);
    #endregion
    #region ExecuteScalarAsync
    /// <summary>
    /// 返回一行一列
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    /// <param name="param"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public virtual Task<T?> ExecuteScalarAsync<T>(ISqlEntity entity, object? param = null, CancellationToken token = default)
        => ExecuteScalarAsync<T>(_engine.Sql(entity, _capacity), param, token);
    #endregion
    #region QueryAsync
    /// <summary>
    /// 获取多行结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    /// <param name="param"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public virtual Task<IEnumerable<T>> QueryAsync<T>(ISqlEntity entity, object? param = null, CancellationToken token = default)
        => QueryAsync<T>(_engine.Sql(entity, _capacity), param, token);
    /// <summary>
    /// 获取一行结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    /// <param name="param"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public virtual Task<T?> QueryFirstOrDefaultAsync<T>(ISqlEntity entity, object? param = null, CancellationToken token = default)
        => QueryFirstOrDefaultAsync<T>(_engine.Sql(entity, _capacity), param, token);
    #endregion
    #endregion
    #region Dapper
    #region ExecuteAsync
    /// <summary>
    /// 执行SQL
    /// 并返回受影响的行数
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<int> ExecuteAsync(string sql, object? param = null, CancellationToken token = default)
        => _connection.ExecuteAsync(new CommandDefinition(sql, param, _transaction, _commandTimeout, CommandType.Text, CommandFlags.None, token));
    #endregion
    #region ExecuteScalarAsync
    /// <summary>
    /// 获取第一行第一列
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<T?> ExecuteScalarAsync<T>(string sql, object? param = null, CancellationToken token = default)
        => _connection.ExecuteScalarAsync<T>(new CommandDefinition(sql, param, _transaction, _commandTimeout, CommandType.Text, CommandFlags.None, token));
    #endregion
    #region QueryAsync
    /// <summary>
    /// 获取多行结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null, CancellationToken token = default)
        => _connection.QueryAsync<T>(new CommandDefinition(sql, param, _transaction, _commandTimeout, CommandType.Text, _flags, token));
    /// <summary>
    /// 获取一行结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<T?> QueryFirstOrDefaultAsync<T>(string sql, object? param = null, CancellationToken token = default)
        => _connection.QueryFirstOrDefaultAsync<T>(new CommandDefinition(sql, param, _transaction, _commandTimeout, CommandType.Text, CommandFlags.None, token));
    #endregion
    #endregion
}

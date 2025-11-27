using ShadowSql.Fragments;
using ShadowSql.Identifiers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Dapper.Shadow;

/// <summary>
/// 执行器
/// </summary>
public interface IExecutor
{
    #region CountAsync
    /// <summary>
    /// 异步计数
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="view"></param>
    /// <param name="param"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<T?> CountAsync<T>(ITableView view, object? param, CancellationToken token);
    #endregion
    #region ExecuteAsync
    /// <summary>
    /// 执行SQL
    /// 并返回受影响的行数
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="param"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<int> ExecuteAsync(ISqlEntity entity, object? param, CancellationToken token);
    #endregion
    #region ExecuteScalarAsync
    /// <summary>
    /// 获取第一行第一列
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    /// <param name="param"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<T?> ExecuteScalarAsync<T>(ISqlEntity entity, object? param, CancellationToken token);
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
    public Task<IEnumerable<T>> QueryAsync<T>(ISqlEntity entity, object? param, CancellationToken token);
    /// <summary>
    /// 获取一行结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    /// <param name="param"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<T?> QueryFirstOrDefaultAsync<T>(ISqlEntity entity, object? param, CancellationToken token);
    #endregion
}

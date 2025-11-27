using ShadowSql.Select;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Dapper.Shadow;

/// <summary>
/// Dapper筛选列扩展方法
/// </summary>
public static partial class DapperShadowCoreServices
{
    #region IDapperSelect
    #region GetAsync
    /// <summary>
    /// 获取异步列表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="select">筛选</param>
    /// <param name="param">参数</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public static Task<IEnumerable<T>> GetAsync<T>(this IDapperSelect select, object? param = null, CancellationToken token = default)
        => select.Executor.QueryAsync<T>(select, param, token);
    #endregion
    #region FirstAsync
    /// <summary>
    /// 异步获取单条
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="select">筛选</param>
    /// <param name="param">参数</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public static Task<T?> FirstAsync<T>(this IDapperSelect select, object? param = null, CancellationToken token = default)
        => select.Executor.QueryFirstOrDefaultAsync<T>(select, param, token);
    #endregion
    #endregion
    #region ISelect
    #region GetAsync
    /// <summary>
    /// 获取异步列表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="select">筛选</param>
    /// <param name="executor">执行器</param>
    /// <param name="param">参数</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public static Task<IEnumerable<T>> GetAsync<T>(this ISelect select, IExecutor executor, object? param = null, CancellationToken token = default)
        => executor.QueryAsync<T>(select, param, token);
    #endregion
    #region FirstAsync
    /// <summary>
    /// 异步获取单条
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="select">筛选</param>
    /// <param name="executor">执行器</param>
    /// <param name="param">参数</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public static Task<T?> FirstAsync<T>(this ISelect select, IExecutor executor, object? param = null, CancellationToken token = default)
        => executor.QueryFirstOrDefaultAsync<T>(select, param, token);
    #endregion
    #endregion
}

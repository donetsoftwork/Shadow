using ShadowSql.Select;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dapper.Shadow;

/// <summary>
/// Dapper筛选列扩展方法
/// </summary>
public static partial class DapperShadowCoreServices
{
    #region IDapperSelect
    #region Get
    /// <summary>
    /// 获取列表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="select">筛选</param>
    /// <param name="param">参数</param>
    /// <returns></returns>
    public static IEnumerable<T> Get<T>(this IDapperSelect select, object? param = null)
        => select.Executor.Query<T>(select, param);
    #endregion
    #region GetAsync
    /// <summary>
    /// 获取异步列表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="select">筛选</param>
    /// <param name="param">参数</param>
    /// <returns></returns>
    public static Task<IEnumerable<T>> GetAsync<T>(this IDapperSelect select, object? param = null)
        => select.Executor.QueryAsync<T>(select, param);
    #endregion
    #region First
    /// <summary>
    /// 获取单条
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="select">筛选</param>
    /// <param name="param">参数</param>
    /// <returns></returns>
    public static T? First<T>(this IDapperSelect select, object? param = null)
        => select.Executor.QueryFirstOrDefault<T>(select, param);
    #endregion
    #region FirstAsync
    /// <summary>
    /// 异步获取单条
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="select">筛选</param>
    /// <param name="param">参数</param>
    /// <returns></returns>
    public static Task<T?> FirstAsync<T>(this IDapperSelect select, object? param = null)
        => select.Executor.QueryFirstOrDefaultAsync<T>(select, param);
    #endregion
    #endregion
    #region ISelect
    #region Get
    /// <summary>
    /// 获取列表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="select">筛选</param>
    /// <param name="executor">执行器</param>
    /// <param name="param">参数</param>
    /// <returns></returns>
    public static IEnumerable<T> Get<T>(this ISelect select, IExecutor executor, object? param = null)
        => executor.Query<T>(select, param);
    #endregion
    #region GetAsync
    /// <summary>
    /// 获取异步列表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="select">筛选</param>
    /// <param name="executor">执行器</param>
    /// <param name="param">参数</param>
    /// <returns></returns>
    public static Task<IEnumerable<T>> GetAsync<T>(this ISelect select, IExecutor executor, object? param = null)
        => executor.QueryAsync<T>(select, param);
    #endregion
    #region First
    /// <summary>
    /// 获取单条
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="select">筛选</param>
    /// <param name="executor">执行器</param>
    /// <param name="param">参数</param>
    /// <returns></returns>
    public static T? First<T>(this ISelect select, IExecutor executor, object? param = null)
        => executor.QueryFirstOrDefault<T>(select, param);
    #endregion
    #region FirstAsync
    /// <summary>
    /// 异步获取单条
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="select">筛选</param>
    /// <param name="executor">执行器</param>
    /// <param name="param">参数</param>
    /// <returns></returns>
    public static Task<T?> FirstAsync<T>(this ISelect select, IExecutor executor, object? param = null)
        => executor.QueryFirstOrDefaultAsync<T>(select, param);
    #endregion
    #endregion
}

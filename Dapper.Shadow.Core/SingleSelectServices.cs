using ShadowSql.SingleSelect;
using System.Threading;
using System.Threading.Tasks;

namespace Dapper.Shadow;

/// <summary>
/// 筛选单列扩展方法
/// </summary>
public static partial class DapperShadowCoreServices
{
    #region IDapperSingleSelect
    #region ExecuteScalarAsync
    /// <summary>
    /// 异步获取单值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="select">筛选</param>
    /// <param name="param">参数</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public static Task<T?> ScalarAsync<T>(this IDapperSingleSelect select, object? param = null, CancellationToken token = default)
        => select.Executor.ExecuteScalarAsync<T>(select, param, token);
    #endregion
    #endregion
    #region ISingleSelect
    #region ExecuteScalarAsync
    /// <summary>
    /// 异步获取单值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="select">筛选</param>
    /// <param name="executor">执行器</param>
    /// <param name="param">参数</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public static Task<T?> ScalarAsync<T>(this ISingleSelect select, IExecutor executor, object? param = null, CancellationToken token = default)
        => executor.ExecuteScalarAsync<T>(select, param, token);
    #endregion
    #endregion
}

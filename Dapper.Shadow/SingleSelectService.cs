using ShadowSql.SingleSelect;
using System.Threading.Tasks;

namespace Dapper.Shadow;

/// <summary>
/// 筛选单列扩展方法
/// </summary>
public static partial class DapperShadowServices
{
    #region IDapperSingleSelect
    #region ExecuteScalar
    /// <summary>
    /// 获取单值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="select"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static T? Scalar<T>(this IDapperSingleSelect select, object? param = null)
        => select.Executor.ExecuteScalar<T>(select, param);
    #endregion
    #region ExecuteScalarAsync
    /// <summary>
    /// 异步获取单值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="select"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static Task<T?> ScalarAsync<T>(this IDapperSingleSelect select, object? param = null)
        => select.Executor.ExecuteScalarAsync<T>(select, param);
    #endregion
    #endregion
    #region ISingleSelect
    #region ExecuteScalar
    /// <summary>
    /// 获取单值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="select"></param>
    /// <param name="executor"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static T? Scalar<T>(this ISingleSelect select, IExecutor executor, object? param = null)
        => executor.ExecuteScalar<T>(select, param);
    #endregion
    #region ExecuteScalarAsync
    /// <summary>
    /// 异步获取单值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="select"></param>
    /// <param name="executor"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static Task<T?> ScalarAsync<T>(this ISingleSelect select, IExecutor executor, object? param = null)
        => executor.ExecuteScalarAsync<T>(select, param);
    #endregion
    #endregion
}

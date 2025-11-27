using ShadowSql.Fragments;
using System.Threading;
using System.Threading.Tasks;

namespace Dapper.Shadow;

/// <summary>
/// sql执行扩展方法
/// </summary>
public static partial class DapperShadowCoreServices
{
    #region IDapperExecute
    #region ExecuteAsync
    /// <summary>
    /// 异步执行
    /// </summary>
    /// <param name="sql">sql</param>
    /// <param name="param">参数</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public static Task<int> ExecuteAsync(this IDapperExecute sql, object? param = null, CancellationToken token = default)
        => sql.Executor.ExecuteAsync(sql, param, token);
    #endregion
    #endregion
    #region IExecuteSql
    #region ExecuteAsync
    /// <summary>
    /// 异步执行
    /// </summary>
    /// <param name="sql">sql</param>
    /// <param name="executor">执行器</param>
    /// <param name="param">参数</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public static Task<int> ExecuteAsync(this IExecuteSql sql, IExecutor executor, object? param = null, CancellationToken token = default)
        => executor.ExecuteAsync(sql, param, token);
    #endregion
    #endregion
}

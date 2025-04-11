using ShadowSql.Fragments;
using System.Threading.Tasks;

namespace Dapper.Shadow;

/// <summary>
/// sql执行扩展方法
/// </summary>
public static partial class DapperShadowCoreServices
{
    #region IDapperExecute
    #region Execute
    /// <summary>
    /// 执行
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static int Execute(this IDapperExecute sql, object? param = null)
        => sql.Executor.Execute(sql, param);
    #endregion
    #region ExecuteAsync
    /// <summary>
    /// 异步执行
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static Task<int> ExecuteAsync(this IDapperExecute sql, object? param = null)
        => sql.Executor.ExecuteAsync(sql, param);
    #endregion
    #endregion
    #region IExecuteSql
    #region Execute
    /// <summary>
    /// 执行
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="executor"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static int Execute(this IExecuteSql sql, IExecutor executor, object? param = null)
        => executor.Execute(sql, param);    
    #endregion
    #region ExecuteAsync
    /// <summary>
    /// 异步执行
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="executor"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static Task<int> ExecuteAsync(this IExecuteSql sql, IExecutor executor, object? param = null)
        => executor.ExecuteAsync(sql, param);
    #endregion
    #endregion
}

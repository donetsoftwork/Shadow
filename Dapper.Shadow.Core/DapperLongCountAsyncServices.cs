using ShadowSql.Identifiers;
using ShadowSql.Select;
using ShadowSql.SingleSelect;
using System.Threading;
using System.Threading.Tasks;

namespace Dapper.Shadow;

/// <summary>
/// Dapper计数扩展方法
/// </summary>
public static partial class DapperShadowCoreServices
{
    #region IDapperSelect
    /// <summary>
    /// 计数
    /// </summary>
    /// <param name="select">筛选</param>
    /// <param name="param">参数</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public static Task<long> LongCountAsync(this IDapperSelect select, object? param = null, CancellationToken token = default)
        => select.Executor.CountAsync<long>(select.Original, param, token);
    #endregion
    #region Dapper
    #region IDapperSource
    /// <summary>
    /// 计数
    /// </summary>
    /// <param name="source"></param>
    /// <param name="param">参数</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public static Task<long> LongCountAsync(this IDapperSource source, object? param = null, CancellationToken token = default)
        => source.Executor.CountAsync<long>(source, param, token);
    #endregion
    #endregion
    #region IExecutor    
    #region ITableView
    /// <summary>
    /// 计数
    /// </summary>
    /// <param name="view"></param>
    /// <param name="executor">执行器</param>
    /// <param name="param">参数</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public static Task<long> LongCountAsync(this ITableView view, IExecutor executor, object? param = null, CancellationToken token = default)
        => executor.CountAsync<long>(view, param, token);
    #endregion
    #region CountSelect
    /// <summary>
    /// 计数
    /// </summary>
    /// <param name="select">筛选</param>
    /// <param name="executor">执行器</param>
    /// <param name="param">参数</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public static Task<long> LongCountAsync(this CountSelect select, IExecutor executor, object? param = null, CancellationToken token = default)
        => executor.ExecuteScalarAsync<long>(select, param, token);
    #endregion
    #region ISelect
    /// <summary>
    /// 计数
    /// </summary>
    /// <param name="select">筛选</param>
    /// <param name="executor">执行器</param>
    /// <param name="param">参数</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public static Task<long> LongCountAsync(this ISelect select, IExecutor executor, object? param = null, CancellationToken token = default)
        => executor.CountAsync<long>(select.Original, param, token);
    #endregion
    #endregion
}

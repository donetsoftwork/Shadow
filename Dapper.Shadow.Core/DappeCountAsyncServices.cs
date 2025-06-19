using ShadowSql.Identifiers;
using ShadowSql.Select;
using ShadowSql.SingleSelect;
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
    /// <returns></returns>
    public static Task<int> CountAsync(this IDapperSelect select, object? param = null)
        => select.Executor.CountAsync<int>(select.Source, param);
    #endregion
    #region Dapper
    #region IDapperSource
    /// <summary>
    /// 计数
    /// </summary>
    /// <param name="source"></param>
    /// <param name="param">参数</param>
    /// <returns></returns>
    public static Task<int> CountAsync(this IDapperSource source, object? param = null)
        => source.Executor.CountAsync<int>(source, param);
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
    /// <returns></returns>
    public static Task<int> CountAsync(this ITableView view, IExecutor executor, object? param = null)
        => executor.CountAsync<int>(view, param);
    #endregion
    #region CountSelect
    /// <summary>
    /// 计数
    /// </summary>
    /// <param name="select">筛选</param>
    /// <param name="executor">执行器</param>
    /// <param name="param">参数</param>
    /// <returns></returns>
    public static Task<int> CountAsync(this CountSelect select, IExecutor executor, object? param = null)
        => executor.ExecuteScalarAsync<int>(select, param);
    #endregion
    #region ISelect
    /// <summary>
    /// 计数
    /// </summary>
    /// <param name="select">筛选</param>
    /// <param name="executor">执行器</param>
    /// <param name="param">参数</param>
    /// <returns></returns>
    public static Task<int> CountAsync(this ISelect select, IExecutor executor, object? param = null)
        => executor.CountAsync<int>(select.Source, param);
    #endregion
    #endregion
}

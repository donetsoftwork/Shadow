using ShadowSql.Identifiers;
using ShadowSql.Select;
using ShadowSql.SingleSelect;

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
    public static long LongCount(this IDapperSelect select, object? param = null)
        => select.Executor.Count<long>(select.Source, param);
    #endregion
    #region Dapper
    #region IDapperSource
    /// <summary>
    /// 计数
    /// </summary>
    /// <param name="source"></param>
    /// <param name="param">参数</param>
    /// <returns></returns>
    public static long LongCount(this IDapperSource source, object? param = null)
        => source.Executor.Count<long>(source, param);
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
    public static long LongCount(this ITableView view, IExecutor executor, object? param = null)
        => executor.Count<long>(view, param);
    #endregion
    #region CountSelect
    /// <summary>
    /// 计数
    /// </summary>
    /// <param name="select">筛选</param>
    /// <param name="executor">执行器</param>
    /// <param name="param">参数</param>
    /// <returns></returns>
    public static long LongCount(this CountSelect select, IExecutor executor, object? param = null)
        => executor.ExecuteScalar<int>(select, param);
    #endregion
    #region ISelect
    /// <summary>
    /// 计数
    /// </summary>
    /// <param name="select">筛选</param>
    /// <param name="executor">执行器</param>
    /// <param name="param">参数</param>
    /// <returns></returns>
    public static long LongCount(this ISelect select, IExecutor executor, object? param = null)
        => executor.Count<long>(select.Source, param);
    #endregion
    #endregion
}

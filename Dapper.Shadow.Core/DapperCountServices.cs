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
    /// <param name="select"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static int Count(this IDapperSelect select, object? param = null)
        => select.Executor.Count<int>(select.Source, param);
    #endregion
    #region Dapper
    #region IDapperSource
    /// <summary>
    /// 计数
    /// </summary>
    /// <param name="source"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static int Count(this IDapperSource source, object? param = null)
        => source.Executor.Count<int>(source, param);
    #endregion
    #endregion
    #region IExecutor    
    #region ITableView
    /// <summary>
    /// 计数
    /// </summary>
    /// <param name="view"></param>
    /// <param name="executor"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static int Count(this ITableView view, IExecutor executor, object? param = null)
        => executor.Count<int>(view, param);
    #endregion
    #region CountSelect
    /// <summary>
    /// 计数
    /// </summary>
    /// <param name="select"></param>
    /// <param name="executor"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static int Count(this CountSelect select, IExecutor executor, object? param = null)
        => executor.ExecuteScalar<int>(select, param);
    #endregion
    #region ISelect
    /// <summary>
    /// 计数
    /// </summary>
    /// <param name="select"></param>
    /// <param name="executor"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static int Count(this ISelect select, IExecutor executor, object? param = null)
        => executor.Count<int>(select.Source, param);
    #endregion
    #endregion
}

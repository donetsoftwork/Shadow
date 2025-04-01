using ShadowSql.Identifiers;
using ShadowSql.SingleSelect;
using ShadowSql.SubQueries;

namespace ShadowSql;

/// <summary>
/// 子查询扩展方法
/// </summary>
public static partial class ShadowSqlServices
{
    #region 子查询逻辑
    /// <summary>
    /// EXISTS子查询逻辑
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static ExistsLogic AsExists(this ITableView source)
        => new(source);
    /// <summary>
    /// NOT EXISTS子查询逻辑
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static NotExistsLogic AsNotExists(this ITableView source)
        => new(source);
    /// <summary>
    /// IN子查询逻辑
    /// </summary>
    /// <param name="field"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public static SubInLogic In(this ICompareView field, ISingleSelect select)
        => new(field, select);
    /// <summary>
    /// NOT IN子查询逻辑
    /// </summary>
    /// <param name="field"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public static SubNotInLogic NotIn(this ICompareView field, ISingleSelect select)
        => new(field, select);
    #endregion
}

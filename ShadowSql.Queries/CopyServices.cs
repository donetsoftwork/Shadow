using ShadowSql.Fragments;
using ShadowSql.Logics;
using ShadowSql.Queries;

namespace ShadowSql;

/// <summary>
/// 复制扩展方法
/// </summary>
public static partial class ShadowSqlQueryServices
{
    #region Copy
    /// <summary>
    /// 复制sql片段组合
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static JoinFragment Copy(this JoinFragment source)
        => new([.. source._items], source._separator);
    #region And
    /// <summary>
    /// 复制复合与逻辑
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static ComplexAndLogic Copy(this ComplexAndLogic source)
        => new([.. source._logics], [.. source._others]);
    /// <summary>
    /// 复制And查询
    /// </summary>
    /// <param name="source"></param>
    public static SqlAndQuery Copy(this SqlAndQuery source)
        => new(source.Complex.Copy(), source.Conditions.Copy());
    #endregion
    #region Or
    /// <summary>
    /// 复制复合或逻辑
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static ComplexOrLogic Copy(this ComplexOrLogic source)
        => new([.. source._logics], [.. source._others]);
    /// <summary>
    /// 复制Or查询
    /// </summary>
    /// <param name="source"></param>
    public static SqlOrQuery Copy(this SqlOrQuery source)
        => new(source.Complex.Copy(), source.Conditions.Copy());
    #endregion
    #endregion
    #region CopyTo
    /// <summary>
    /// 复制到
    /// </summary>
    /// <param name="source"></param>
    /// <param name="destination"></param>
    internal static void CopyTo(this JoinFragment source, JoinFragment destination)
        => destination.Add(source.Items);
    /// <summary>
    /// 复制sql条件查询
    /// </summary>
    /// <param name="source"></param>
    /// <param name="destination"></param>
    internal static void CopyTo(this SqlConditionLogic source, SqlConditionLogic destination)
        => source.Fragment.CopyTo(destination.Fragment);
    /// <summary>
    /// 复制sql条件查询
    /// </summary>
    /// <param name="source"></param>
    internal static SqlConditionLogic Copy(this SqlConditionLogic source)
        => new(source.Fragment.Copy(), source.Separator);
    #endregion
}

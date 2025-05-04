using ShadowSql.Identifiers;
using ShadowSql.Update;

namespace ShadowSql;

/// <summary>
/// 构造修改扩展方法
/// </summary>
public static partial class ShadowSqlServices
{
    #region MultiTableUpdate
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="view"></param>
    /// <returns></returns>
    public static MultiTableUpdate ToUpdate(this IMultiView view)
        => new(view);
    #endregion 
}

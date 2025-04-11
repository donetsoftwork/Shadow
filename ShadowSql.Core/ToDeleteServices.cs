using ShadowSql.Delete;
using ShadowSql.Identifiers;

namespace ShadowSql;

/// <summary>
/// Delete扩展方法
/// </summary>
public static partial class ShadowSqlCoreServices
{    
    #region TruncateTable
    /// <summary>
    /// 清空表
    /// </summary>
    /// <param name="table"></param>
    public static TruncateTable ToTruncate(this ITable table)
        => new(table);
    #endregion
}

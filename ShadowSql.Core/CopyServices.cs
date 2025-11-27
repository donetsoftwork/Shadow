using ShadowSql.Identifiers;
using ShadowSql.Logics;

namespace ShadowSql;

/// <summary>
/// 复制扩展方法
/// </summary>
public static partial class ShadowSqlCoreServices
{
    #region Copy
    /// <summary>
    /// 复制表
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="newName"></param>
    /// <returns></returns>
    public static Table Copy(this Table table, string newName = "")
    {
        if (string.IsNullOrWhiteSpace(newName))
            newName = table.Name;
        var destination = new Table(newName);
        table.CopyColumnsTo(destination);
        return destination;
    }
    #region And
    /// <summary>
    /// 复制与逻辑
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static AndLogic Copy(this AndLogic source)
        => new([.. source._logics]);
    #endregion
    #region Or
    /// <summary>
    /// 复制或逻辑
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static OrLogic Copy(this OrLogic source)
        => new([.. source._logics]);
    #endregion
    #endregion
    #region CopyTo
    /// <summary>
    /// 复制到
    /// </summary>
    /// <param name="source"></param>
    /// <param name="destination"></param>
    internal static void CopyColumnsTo(this Table source, Table destination)
    {
        destination.AddColums(source.Columns);
        destination.IgnoreInsert(source.InsertIgnores);
        destination.IgnoreUpdate(source.UpdateIgnores);
    }
    #endregion
}

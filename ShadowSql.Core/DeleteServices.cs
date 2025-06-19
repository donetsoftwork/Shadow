using ShadowSql.Delete;
using ShadowSql.Identifiers;

namespace ShadowSql;

/// <summary>
/// 范围筛选游标扩展方法
/// </summary>
public static partial class ShadowSqlCoreServices
{
    /// <summary>
    /// 指定被删除的表
    /// </summary>
    /// <typeparam name="TDelete"></typeparam>
    /// <param name="delete"></param>
    /// <param name="tableName">表名</param>
    /// <returns></returns>
    public static TDelete Delete<TDelete>(this TDelete delete, string tableName)
        where TDelete : MultiTableDelete
    {
        if (delete.MultiTable.GetMember(tableName) is IAliasTable table)
            delete._source = table;
        return delete;
    }
    /// <summary>
    /// 指定被删除的表
    /// </summary>
    /// <typeparam name="TDelete"></typeparam>
    /// <param name="delete"></param>
    /// <param name="table">表</param>
    /// <returns></returns>
    public static TDelete Delete<TDelete>(this TDelete delete, IAliasTable table)
        where TDelete : MultiTableDelete
    {
        delete._source = table;
        return delete;
    }
}

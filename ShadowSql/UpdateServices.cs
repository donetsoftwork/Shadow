using ShadowSql.Assigns;
using ShadowSql.Identifiers;
using ShadowSql.SqlVales;
using ShadowSql.Update;
using System.Collections.Generic;

namespace ShadowSql;

/// <summary>
/// Update扩展方法
/// </summary>
public static partial class ShadowSqlServices
{    
    #region MultiTableUpdate
    /// <summary>
    /// 指定被修改的表
    /// </summary>
    /// <typeparam name="TMultiUpdate"></typeparam>
    /// <param name="update"></param>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public static MultiTableUpdate Update<TMultiUpdate>(this TMultiUpdate update, string tableName)
        where TMultiUpdate : MultiTableUpdate
    {
        update._source = update.MultiTable.From(tableName);
        return update;
    }
    /// <summary>
    /// 指定被修改的表
    /// </summary>
    /// <typeparam name="TMultiUpdate"></typeparam>
    /// <param name="update"></param>
    /// <param name="table"></param>
    /// <returns></returns>
    public static TMultiUpdate Update<TMultiUpdate>(this TMultiUpdate update, IAliasTable table)
        where TMultiUpdate : MultiTableUpdate
    {
        update._source = table;
        return update;
    }
    #endregion
}

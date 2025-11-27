using ShadowSql.Assigns;
using ShadowSql.Identifiers;
using ShadowSql.Update;
using ShadowSql.Variants;
using System;
using System.Linq;

namespace ShadowSql;

/// <summary>
/// Update扩展方法
/// </summary>
public static partial class ShadowSqlQueryServices
{
    #region MultiTableUpdate
    /// <summary>
    /// 指定被修改的表
    /// </summary>
    /// <typeparam name="TMultiUpdate"></typeparam>
    /// <param name="update"></param>
    /// <param name="tableName">表名</param>
    /// <returns></returns>
    public static MultiTableUpdate Update<TMultiUpdate>(this TMultiUpdate update, string tableName)
        where TMultiUpdate : MultiTableUpdate
    {
        update._table = update.MultiTable.From<IAliasTable<ITable>>(tableName);
        return update;
    }
    /// <summary>
    /// 指定被修改的表
    /// </summary>
    /// <typeparam name="TMultiUpdate"></typeparam>
    /// <param name="update"></param>
    /// <param name="table">表</param>
    /// <returns></returns>
    public static TMultiUpdate Update<TMultiUpdate>(this TMultiUpdate update, IAliasTable<ITable> table)
        where TMultiUpdate : MultiTableUpdate
    {
        update._table = table;
        return update;
    }
    #endregion
    #region 赋值sql
    /// <summary>
    /// 赋值原生sql
    /// </summary>
    /// <typeparam name="TUpdate"></typeparam>
    /// <param name="update"></param>
    /// <param name="assignSql"></param>
    /// <returns></returns>
    public static TUpdate SetRaw<TUpdate>(this TUpdate update, string assignSql)
        where TUpdate : UpdateBase, IUpdate
    {
        update.SetCore(RawAssignInfo.Use(assignSql));
        return update;
    }
    #endregion
    /// <summary>
    /// 获取被修改字段
    /// </summary>
    /// <param name="aliasTable">别名表</param>
    /// <param name="fieldName">字段名</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static IAssignView? GetAssignField<TTable>(this IAliasTable<TTable> aliasTable, string fieldName)
        where TTable : ITable
    {
        if (aliasTable.GetPrefixField(fieldName) is PrefixField prefixField)
        {
            IAssignView field = prefixField.Original;
            if (aliasTable.Original.AssignFields.Any(c => c == field))
                return prefixField;
        }
        if (aliasTable.Original.GetAssignField(fieldName) is IColumn column)
            return aliasTable.NewPrefixField(column);
        return null;
    }
}

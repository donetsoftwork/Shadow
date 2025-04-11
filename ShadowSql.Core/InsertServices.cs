using ShadowSql.Identifiers;
using ShadowSql.Insert;
using System.Collections.Generic;
using System.Linq;

namespace ShadowSql;

/// <summary>
/// 构造插入扩展方法
/// </summary>
public static partial class ShadowSqlCoreServices
{
    #region ISingleInsert
    /// <summary>
    /// 设置插入值
    /// </summary>
    /// <typeparam name="TInsert"></typeparam>
    /// <param name="insert"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static TInsert Insert<TInsert>(this TInsert insert, IInsertValue value)
        where TInsert : SingleInsertBase
    {
        insert.Add(value);
        return insert;
    }
    /// <summary>
    /// 按指定列插入
    /// </summary>
    /// <typeparam name="TInsert"></typeparam>
    /// <param name="insert"></param>
    /// <param name="column"></param>
    /// <returns></returns>
    public static TInsert InsertColumn<TInsert>(this TInsert insert, IColumn column)
        where TInsert : SingleInsertBase
        => insert.Insert(column.Insert());
    /// <summary>
    /// 按指定列插入
    /// </summary>
    /// <typeparam name="TInsert"></typeparam>
    /// <param name="insert"></param>
    /// <param name="columns"></param>
    /// <returns></returns>
    public static TInsert InsertColumns<TInsert>(this TInsert insert, params IEnumerable<IColumn> columns)
        where TInsert : SingleInsertBase
    {
        foreach (var column in columns)
            insert.InsertColumn(column);
        return insert;
    }
    /// <summary>
    /// 按自己的列插入
    /// </summary>
    /// <typeparam name="TInsert"></typeparam>
    /// <param name="insert"></param>
    /// <returns></returns>
    public static TInsert InsertSelfColumns<TInsert>(this TInsert insert)
        where TInsert : SingleInsertBase, ISingleInsert
        => insert.InsertColumns(insert.Table.InsertColumns);
    #endregion
    #region IMultiInsert
    /// <summary>
    /// 设置插入值
    /// </summary>
    /// <typeparam name="TInsert"></typeparam>
    /// <param name="insert"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static TInsert Insert<TInsert>(this TInsert insert, IInsertValues value)
        where TInsert : MultiInsertBase
    {
        insert.Add(value);
        return insert;
    }
    #endregion
    #region ISelectInsert
    /// <summary>
    /// 设置列
    /// </summary>
    /// <typeparam name="TInsert"></typeparam>
    /// <param name="insert"></param>
    /// <param name="column"></param>
    /// <returns></returns>
    public static TInsert Insert<TInsert>(this TInsert insert, IColumn column)
        where TInsert : SelectInsertBase
    {
        insert.Add(column);
        return insert;
    }
    /// <summary>
    /// 设置列
    /// </summary>
    /// <typeparam name="TInsert"></typeparam>
    /// <param name="insert"></param>
    /// <param name="columnName"></param>
    /// <returns></returns>
    public static TInsert Insert<TInsert>(this TInsert insert, string columnName)
        where TInsert : SelectInsertBase, ISelectInsert
    {
        if (insert.Table.InsertColumns.FirstOrDefault(c => c.IsMatch(columnName)) is IColumn column)
            insert.Add(column);
        return insert;
    }
    #endregion
}

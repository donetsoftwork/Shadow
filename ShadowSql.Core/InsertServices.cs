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
    /// <param name="value">值</param>
    /// <returns></returns>
    public static TInsert Insert<TInsert>(this TInsert insert, IInsertValue value)
        where TInsert : SingleInsertBase, ISingleInsert
    {
        insert.Add(value);
        return insert;
    }
    /// <summary>
    /// 按指定列插入
    /// </summary>
    /// <typeparam name="TInsert"></typeparam>
    /// <param name="insert"></param>
    /// <param name="column">列</param>
    /// <returns></returns>
    public static TInsert InsertColumn<TInsert>(this TInsert insert, IColumn column)
        where TInsert : SingleInsertBase, ISingleInsert
        => insert.Insert(column.Insert());
    /// <summary>
    /// 按指定列插入
    /// </summary>
    /// <typeparam name="TInsert"></typeparam>
    /// <param name="insert"></param>
    /// <param name="columns">列</param>
    /// <returns></returns>
    public static TInsert InsertColumns<TInsert>(this TInsert insert, params IEnumerable<IColumn> columns)
        where TInsert : SingleInsertBase, ISingleInsert
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
    /// <param name="value">值</param>
    /// <returns></returns>
    public static TInsert Insert<TInsert>(this TInsert insert, IInsertValues value)
        where TInsert : MultiInsertBase, IMultiInsert
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
    /// <param name="columns">列</param>
    /// <returns></returns>
    public static TInsert Insert<TInsert>(this TInsert insert, params IEnumerable<IColumn> columns)
        where TInsert : SelectInsertBase, ISelectInsert
    {
        foreach (var column in columns)
            insert.Add(column);
        return insert;
    }
    /// <summary>
    /// 设置列
    /// </summary>
    /// <typeparam name="TInsert"></typeparam>
    /// <param name="insert"></param>
    /// <param name="columnNames">列名</param>
    /// <returns></returns>
    public static TInsert Insert<TInsert>(this TInsert insert, params IEnumerable<string> columnNames)
        where TInsert : SelectInsertBase, ISelectInsert
    {
        var columns = insert.Table.InsertColumns;
        foreach (var columnName in columnNames)
        {
            var column = columns.FirstOrDefault(c => c.IsMatch(columnName))
                ?? Identifiers.Column.Use(columnName);
            insert.Add(column);
        }
        return insert;
    }
    #endregion
}

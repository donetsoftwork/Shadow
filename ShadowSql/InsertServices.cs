using ShadowSql.Identifiers;
using ShadowSql.Insert;
using ShadowSql.Select;
using System;
using System.Collections.Generic;

namespace ShadowSql;

/// <summary>
/// 构造插入扩展方法
/// </summary>
public static partial class ShadowSqlServices
{
    #region ISingleInsert
    /// <summary>
    /// 插入
    /// </summary>
    /// <typeparam name="TInsert"></typeparam>
    /// <param name="insert"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static TInsert Insert<TInsert>(this TInsert insert, IInsertValue value)
        where TInsert : ISingleInsert
    {
        insert.InsertCore(value);
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
        where TInsert : ISingleInsert
        => insert.Insert(column.Insert());
    /// <summary>
    /// 按指定列插入
    /// </summary>
    /// <typeparam name="TInsert"></typeparam>
    /// <param name="insert"></param>
    /// <param name="columns"></param>
    /// <returns></returns>
    public static TInsert InsertColumns<TInsert>(this TInsert insert, params IEnumerable<IColumn> columns)
        where TInsert : ISingleInsert
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
        where TInsert : ISingleInsert
        => insert.InsertColumns(insert.Table.InsertColumns);
    ///// <summary>
    ///// 插入
    ///// </summary>
    ///// <typeparam name="TInsert"></typeparam>
    ///// <param name="insert"></param>
    ///// <param name="select"></param>
    ///// <returns></returns>
    //public static TInsert Insert<TInsert>(this TInsert insert, Func<ITable, IInsertValue> select)
    //    where TInsert : ISingleInsert
    //{
    //    insert.InsertCore(select);
    //    return insert;
    //}
    #endregion
    #region IMultiInsert
    /// <summary>
    /// 插入多条
    /// </summary>
    /// <typeparam name="TInsert"></typeparam>
    /// <param name="insert"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static TInsert Insert<TInsert>(this TInsert insert, IInsertValues value)
        where TInsert : IMultiInsert
    {
        insert.InsertCore(value);
        return insert;
    }
    #endregion
    #region ToInsert
    /// <summary>
    /// 插入
    /// </summary>
    /// <param name="table"></param>
    /// <returns></returns>
    public static SingleInsert<TTable> ToInsert<TTable>(this TTable table)
        where TTable : ITable
        => new(table);
    #endregion
    #region ToMultiInsert
    /// <summary>
    /// 插入多条
    /// </summary>
    /// <param name="table"></param>
    /// <returns></returns>
    public static MultiInsert<TTable> ToMultiInsert<TTable>(this TTable table)
        where TTable : ITable
        => new(table);
    #endregion
    #region ToInsert
    /// <summary>
    /// 插入Select
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public static SelectInsert<TTable> ToInsert<TTable>(this TTable table, ISelect select)
        where TTable : ITable
        => new(table, select);
    #endregion
    #region InsertTo
    /// <summary>
    /// 插入Select
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="select"></param>
    /// <param name="table"></param>
    /// <returns></returns>
    public static SelectInsert<TTable> InsertTo<TTable>(this ISelect select, TTable table)
      where TTable : ITable
      => new(table, select);
    #endregion
}

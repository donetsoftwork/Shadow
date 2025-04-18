﻿using ShadowSql.Identifiers;
using ShadowSql.Join;
using ShadowSql.Logics;
using ShadowSql.Queries;
using System;

namespace ShadowSql;

/// <summary>
/// 多联表查询
/// </summary>
public static partial class ShadowSqlCoreServices
{
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <typeparam name="TMultiTable"></typeparam>
    /// <param name="multiTable"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static TMultiTable Where<TMultiTable>(this TMultiTable multiTable, Func<IMultiView, AtomicLogic> query)
        where TMultiTable : MultiTableBase, IDataSqlQuery
    {
        multiTable.Query.AddLogic(query(multiTable));
        return multiTable;
    }
    /// <summary>
    /// 指定表查询
    /// </summary>
    /// <typeparam name="TMultiTable"></typeparam>
    /// <param name="multiTable"></param>
    /// <param name="tableName"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static TMultiTable Where<TMultiTable>(this TMultiTable multiTable, string tableName, Func<IAliasTable, AtomicLogic> query)
        where TMultiTable : MultiTableBase, IDataSqlQuery
    {
        multiTable.Query.AddLogic(query(multiTable.From(tableName)));
        return multiTable;
    }
    /// <summary>
    /// 指定表查询
    /// </summary>
    /// <typeparam name="TMultiTable"></typeparam>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="multiTable"></param>
    /// <param name="tableName"></param>
    /// <param name="select"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static TMultiTable Where<TMultiTable, TTable>(this TMultiTable multiTable, string tableName, Func<TTable, IColumn> select, Func<IColumn, AtomicLogic> query)
        where TMultiTable : MultiTableBase, IDataSqlQuery
        where TTable : ITable
    {
        var aliasTable = multiTable.Table<TTable>(tableName);
        var prefixColumn = aliasTable.GetPrefixColumn(select(aliasTable.Target));
        if (prefixColumn is not null)
            multiTable.Query.AddLogic(query(prefixColumn));
        return multiTable;
    }
    #region IDataSqlQuery
    /// <summary>
    /// Where
    /// </summary>
    /// <typeparam name="TMultiTable"></typeparam>
    /// <param name="multiTable"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static TMultiTable Where<TMultiTable>(this TMultiTable multiTable, Func<IMultiView, SqlQuery, SqlQuery> query)
        where TMultiTable : MultiTableBase, IDataSqlQuery
    {
        multiTable.Query = query(multiTable, multiTable.Query);
        return multiTable;
    }
    /// <summary>
    /// 指定表查询
    /// </summary>
    /// <typeparam name="TMultiTable"></typeparam>
    /// <param name="multiTable"></param>
    /// <param name="tableName"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static TMultiTable Where<TMultiTable>(this TMultiTable multiTable, string tableName, Func<IAliasTable, SqlQuery, SqlQuery> query)
        where TMultiTable : MultiTableBase, IDataSqlQuery
    {
        multiTable.Query = query(multiTable.From(tableName), multiTable.Query);
        return multiTable;
    }
    #endregion
}

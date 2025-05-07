using ShadowSql.Identifiers;
using ShadowSql.Join;
using ShadowSql.Logics;
using ShadowSql.Queries;
using System;

namespace ShadowSql;

/// <summary>
/// 多联表扩展方法
/// </summary>
public static partial class ShadowSqlServices
{
    #region IDataSqlQuery
    #region Where
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
    /// <typeparam name="TTable"></typeparam>
    /// <param name="multiTable"></param>
    /// <param name="tableName"></param>
    /// <param name="select"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static MultiTableSqlQuery Where<TTable>(this MultiTableSqlQuery multiTable, string tableName, Func<TTable, IColumn> select, Func<IPrefixField, AtomicLogic> query)
        where TTable : ITable
    {
        var aliasTable = multiTable.Alias<TTable>(tableName);
        var prefixField = aliasTable.GetPrefixField(select(aliasTable.Target));
        if (prefixField is not null)
            multiTable._filter.AddLogic(query(prefixField));
        return multiTable;
    }
    #endregion
    #region Apply
    #region TAliasTable
    /// <summary>
    /// 指定表查询
    /// </summary>
    /// <typeparam name="TAliasTable"></typeparam>
    /// <param name="multiTable"></param>
    /// <param name="tableName"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static JoinTableSqlQuery Apply<TAliasTable>(this JoinTableSqlQuery multiTable, string tableName, Func<SqlQuery, TAliasTable, SqlQuery> query)
        where TAliasTable : IAliasTable
    {
        multiTable._filter = query(multiTable._filter, multiTable.From<TAliasTable>(tableName));
        return multiTable;
    }
    /// <summary>
    /// 指定表查询
    /// </summary>
    /// <typeparam name="TAliasTable"></typeparam>
    /// <param name="multiTable"></param>
    /// <param name="tableName"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static MultiTableSqlQuery Apply<TAliasTable>(this MultiTableSqlQuery multiTable, string tableName, Func<SqlQuery, TAliasTable, SqlQuery> query)
        where TAliasTable : IAliasTable
    {
        multiTable._filter = query(multiTable._filter, multiTable.From<TAliasTable>(tableName));
        return multiTable;
    }
    #endregion
    #region IAliasTable
    /// <summary>
    /// 指定表查询
    /// </summary>
    /// <typeparam name="TMultiTable"></typeparam>
    /// <param name="multiTable"></param>
    /// <param name="tableName"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static TMultiTable Apply<TMultiTable>(this TMultiTable multiTable, string tableName, Func<SqlQuery, IAliasTable, SqlQuery> query)
        where TMultiTable : MultiTableBase, IDataSqlQuery
    {
        multiTable.Query = query(multiTable.Query, multiTable.From(tableName));
        return multiTable;
    }
    #endregion
    #endregion
    #endregion
    #region IDataQuery
    #region Apply
    #region IAliasTable
    /// <summary>
    /// 指定表查询
    /// </summary>
    /// <typeparam name="TMultiTable"></typeparam>
    /// <param name="multiTable"></param>
    /// <param name="tableName"></param>
    /// <param name="logic"></param>
    /// <returns></returns>
    public static TMultiTable Apply<TMultiTable>(this TMultiTable multiTable, string tableName, Func<Logic, IAliasTable, Logic> logic)
        where TMultiTable : MultiTableBase, IDataQuery
    {
        multiTable.Logic = logic(multiTable.Logic, multiTable.From(tableName));
        return multiTable;
    }
    #endregion
    #region TAliasTable
    /// <summary>
    /// 指定表查询
    /// </summary>
    /// <typeparam name="TAliasTable"></typeparam>
    /// <param name="multiTable"></param>
    /// <param name="tableName"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static JoinTableQuery Apply<TAliasTable>(this JoinTableQuery multiTable, string tableName, Func<Logic, TAliasTable, Logic> query)
        where TAliasTable : IAliasTable
    {
        multiTable._filter = query(multiTable._filter, multiTable.From<TAliasTable>(tableName));
        return multiTable;
    }
    /// <summary>
    /// 指定表查询
    /// </summary>
    /// <typeparam name="TAliasTable"></typeparam>
    /// <param name="multiTable"></param>
    /// <param name="tableName"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static MultiTableQuery Apply<TAliasTable>(this MultiTableQuery multiTable, string tableName, Func<Logic, TAliasTable, Logic> query)
        where TAliasTable : IAliasTable
    {
        multiTable._filter = query(multiTable._filter, multiTable.From<TAliasTable>(tableName));
        return multiTable;
    }
    #endregion
    #endregion
    #endregion
}

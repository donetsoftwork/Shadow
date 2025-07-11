using ShadowSql.AliasTables;
using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using ShadowSql.Join;
using ShadowSql.Logics;
using ShadowSql.Tables;
using ShadowSql.Variants;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShadowSql;

/// <summary>
/// 分组扩展方法
/// </summary>
public static partial class ShadowSqlServices
{
    #region GroupBy
    #region GroupByTableQuery
    #region TTable
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="fields">字段</param>
    /// <returns></returns>
    public static GroupByTableQuery<TTable> GroupBy<TTable>(this TTable table, params IField[] fields)
        where TTable : ITable
        => new(table, EmptyLogic.Instance, fields);
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public static GroupByTableQuery<TTable> GroupBy<TTable>(this TTable table, Func<TTable, IField[]> select)
        where TTable : ITable
        => new(table, EmptyLogic.Instance, select(table));
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="columnNames">列名</param>
    /// <returns></returns>
    public static GroupByTableQuery<TTable> GroupBy<TTable>(this TTable table, params IEnumerable<string> columnNames)
        where TTable : ITable
        => new(table, EmptyLogic.Instance, [.. table.Fields(columnNames)]);
    #endregion
    #region TTable & ISqlLogic
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table">表</param>
    /// <param name="where">查询条件</param>
    /// <param name="fields">字段</param>
    /// <returns></returns>
    public static GroupByTableQuery<TTable> GroupBy<TTable>(this TTable table, ISqlLogic where, params IField[] fields)
        where TTable : ITable
        => new(table, where, fields);
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table">表</param>
    /// <param name="where">查询条件</param>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public static GroupByTableQuery<TTable> GroupBy<TTable>(this TTable table, Func<TTable, ISqlLogic> where, Func<TTable, IField[]> select)
        where TTable : ITable
        => new(table, where(table), select(table));
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table">表</param>
    /// <param name="where">查询条件</param>
    /// <param name="columnNames">列名</param>
    /// <returns></returns>
    public static GroupByTableQuery<TTable> GroupBy<TTable>(this TTable table, ISqlLogic where, params IEnumerable<string> columnNames)
        where TTable : ITable
        => new(table, where, [.. table.Fields(columnNames)]);
    #endregion
    #region TableQuery
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="query">查询</param>
    /// <param name="fields">字段</param>
    /// <returns></returns>
    public static GroupByTableQuery<TTable> GroupBy<TTable>(this TableQuery<TTable> query, params IField[] fields)
        where TTable : ITable
        => new(query.Source, query._filter, fields);
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="query">查询</param>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public static GroupByTableQuery<TTable> GroupBy<TTable>(this TableQuery<TTable> query, Func<TTable, IField[]> select)
        where TTable : ITable
        => new(query.Source, query._filter, select(query.Source));
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="query">查询</param>
    /// <param name="columnNames">列名</param>
    /// <returns></returns>
    public static GroupByTableQuery<TTable> GroupBy<TTable>(this TableQuery<TTable> query, params IEnumerable<string> columnNames)
        where TTable : ITable
    {
        var table = query.Source;
        return new GroupByTableQuery<TTable>(table, query._filter, [.. table.Fields(columnNames)]);
    }
    #endregion
    #region TableQuery
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="query">查询</param>
    /// <param name="fields">字段</param>
    /// <returns></returns>
    public static GroupByTableQuery<ITable> GroupBy(this TableQuery query, params IField[] fields)
        => new((ITable)query.Source, query._filter, fields);
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="query">查询</param>
    /// <param name="columnNames">列名</param>
    /// <returns></returns>
    public static GroupByTableQuery<ITable> GroupBy(this TableQuery query, params IEnumerable<string> columnNames)
    {
        var table = (ITable)query.Source;
        return new GroupByTableQuery<ITable>(table, query._filter, [.. table.Fields(columnNames)]);
    }
    #endregion
    #endregion
    #region GroupByAliasTableQuery
    #region TableAlias
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="aliasTable">别名表</param>
    /// <param name="fields">字段</param>
    /// <returns></returns>
    public static GroupByAliasTableQuery<TTable> GroupBy<TTable>(this IAliasTable<TTable> aliasTable, params IField[] fields)
        where TTable : ITable
        => new(aliasTable, EmptyLogic.Instance, fields);
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="aliasTable">别名表</param>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public static GroupByAliasTableQuery<TTable> GroupBy<TTable>(this IAliasTable<TTable> aliasTable, Func<TTable, IColumn[]> select)
        where TTable : ITable
        => new(aliasTable, EmptyLogic.Instance, [.. select(aliasTable.Target).Select(aliasTable.Prefix)]);
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="aliasTable">别名表</param>
    /// <param name="columnNames">列名</param>
    /// <returns></returns>
    public static GroupByAliasTableQuery<TTable> GroupBy<TTable>(this IAliasTable<TTable> aliasTable, params IEnumerable<string> columnNames)
        where TTable : ITable
        => new(aliasTable, EmptyLogic.Instance, [.. aliasTable.Fields(columnNames)]);
    #endregion
    #region TableAlias & ISqlLogic
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="aliasTable">别名表</param>
    /// <param name="where">查询条件</param>
    /// <param name="fields">字段</param>
    /// <returns></returns>
    public static GroupByAliasTableQuery<TTable> GroupBy<TTable>(this IAliasTable<TTable> aliasTable, ISqlLogic where, params IField[] fields)
        where TTable : ITable
        => new(aliasTable, where, fields);
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="aliasTable">别名表</param>
    /// <param name="where">查询条件</param>
    /// <param name="columnNames">列名</param>
    /// <returns></returns>
    public static GroupByAliasTableQuery<TTable> GroupBy<TTable>(this IAliasTable<TTable> aliasTable, ISqlLogic where, params IEnumerable<string> columnNames)
        where TTable : ITable
        => new(aliasTable, where, [.. aliasTable.Fields(columnNames)]);
    #endregion
    #region AliasTableQuery
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="query">查询</param>
    /// <param name="fields">字段</param>
    /// <returns></returns>
    public static GroupByAliasTableQuery<TTable> GroupBy<TTable>(this AliasTableQuery<TTable> query, params IField[] fields)
        where TTable : ITable
        => new(query.Source, query._filter, fields);
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="query">查询</param>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public static GroupByAliasTableQuery<TTable> GroupBy<TTable>(this AliasTableQuery<TTable> query, Func<TTable, IColumn[]> select)
        where TTable : ITable
    {
        var table = query.Source;
        return new GroupByAliasTableQuery<TTable>(table, query._filter, [.. select(table.Target).Select(table.Prefix)]);
    }
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="query">查询</param>
    /// <param name="columnNames">列名</param>
    /// <returns></returns>
    public static GroupByAliasTableQuery<TTable> GroupBy<TTable>(this AliasTableQuery<TTable> query, params IEnumerable<string> columnNames)
        where TTable : ITable
    {
        var table = query.Source;
        return new GroupByAliasTableQuery<TTable>(table, query._filter, [.. table.Fields(columnNames)]);
    }
    #endregion
    #endregion
    #region GroupByMultiQuery
    #region MultiTableQuery
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="multiTable">多表(联表)</param>
    /// <param name="fields">字段</param>
    /// <returns></returns>
    public static GroupByMultiQuery GroupBy(this MultiTableQuery multiTable, params IField[] fields)
        => new(multiTable, fields);
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="multiTable">多表(联表)</param>
    /// <param name="columnNames">列名</param>
    /// <returns></returns>
    public static GroupByMultiQuery GroupBy(this MultiTableQuery multiTable, params IEnumerable<string> columnNames)
        => new(multiTable, [.. multiTable.Fields(columnNames)]);
    #endregion
    #region JoinTableQuery
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="multiTable">多表(联表)</param>
    /// <param name="fields">字段</param>
    /// <returns></returns>
    public static GroupByMultiQuery GroupBy(this JoinTableQuery multiTable, params IField[] fields)
        => new(multiTable, fields);
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="multiTable">多表(联表)</param>
    /// <param name="columnNames">列名</param>
    /// <returns></returns>
    public static GroupByMultiQuery GroupBy(this JoinTableQuery multiTable, params IEnumerable<string> columnNames)
        => new(multiTable, [.. multiTable.Fields(columnNames)]);
    #endregion
    #region JoinOnQuery<LTable, RTable>
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <typeparam name="TLeft"></typeparam>
    /// <typeparam name="TRight"></typeparam>
    /// <param name="joinOn">联接</param>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public static GroupByMultiQuery GroupBy<TLeft, TRight>(this AliasJoinOnQuery<TLeft, TRight> joinOn, Func<TLeft, TRight, IField[]> select)
        where TLeft : IAliasTable<ITable>
        where TRight : IAliasTable<ITable>
        => new(joinOn.Root, [.. select(joinOn.Left, joinOn.Source)]);
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <typeparam name="LTable"></typeparam>
    /// <typeparam name="RTable"></typeparam>
    /// <param name="joinOn">联接</param>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public static GroupByMultiQuery GroupBy<LTable, RTable>(this JoinOnQuery<LTable, RTable> joinOn, Func<LTable, RTable, IColumn[]> select)
        where LTable : ITable
        where RTable : ITable
        => new(joinOn.Root, [.. select(joinOn.Left.Target, joinOn.Source.Target).Select(joinOn.Prefix)]);
    #endregion
    #endregion
    #endregion
    #region SqlGroupBy
    #region GroupByTableSqlQuery
    #region TTable
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="fields">字段</param>
    /// <returns></returns>
    public static GroupByTableSqlQuery<TTable> SqlGroupBy<TTable>(this TTable table, params IField[] fields)
        where TTable : ITable
        => new(table, EmptyLogic.Instance, fields);
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public static GroupByTableSqlQuery<TTable> SqlGroupBy<TTable>(this TTable table, Func<TTable, IField[]> select)
        where TTable : ITable
        => new(table, EmptyLogic.Instance, select(table));
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="columnNames">列名</param>
    /// <returns></returns>
    public static GroupByTableSqlQuery<TTable> SqlGroupBy<TTable>(this TTable table, params IEnumerable<string> columnNames)
        where TTable : ITable
        => new(table, EmptyLogic.Instance, [.. table.Fields(columnNames)]);
    #endregion
    #region TTable & ISqlLogic
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table">表</param>
    /// <param name="where">查询条件</param>
    /// <param name="fields">字段</param>
    /// <returns></returns>
    public static GroupByTableSqlQuery<TTable> SqlGroupBy<TTable>(this TTable table, ISqlLogic where, params IField[] fields)
        where TTable : ITable
        => new(table, where, fields);
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table">表</param>
    /// <param name="where">查询条件</param>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public static GroupByTableSqlQuery<TTable> SqlGroupBy<TTable>(this TTable table, Func<TTable, ISqlLogic> where, Func<TTable, IField[]> select)
        where TTable : ITable
        => new(table, where(table), select(table));
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table">表</param>
    /// <param name="where">查询条件</param>
    /// <param name="columnNames">列名</param>
    /// <returns></returns>
    public static GroupByTableSqlQuery<TTable> SqlGroupBy<TTable>(this TTable table, ISqlLogic where, params IEnumerable<string> columnNames)
        where TTable : ITable
        => new(table, where, [.. table.Fields(columnNames)]);
    #endregion
    #region TableSqlQuery
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="query">查询</param>
    /// <param name="fields">字段</param>
    /// <returns></returns>
    public static GroupByTableSqlQuery<TTable> SqlGroupBy<TTable>(this TableSqlQuery<TTable> query, params IField[] fields)
        where TTable : ITable
        => new(query.Source, query._filter, fields);
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="query">查询</param>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public static GroupByTableSqlQuery<TTable> SqlGroupBy<TTable>(this TableSqlQuery<TTable> query, Func<TTable, IField[]> select)
        where TTable : ITable
        => new(query.Source, query._filter, select(query.Source));
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="query">查询</param>
    /// <param name="columnNames">列名</param>
    /// <returns></returns>
    public static GroupByTableSqlQuery<TTable> SqlGroupBy<TTable>(this TableSqlQuery<TTable> query, params IEnumerable<string> columnNames)
        where TTable : ITable
    {
        var table = query.Source;
        return new GroupByTableSqlQuery<TTable>(table, query._filter, [.. table.Fields(columnNames)]);
    }
    #endregion
    #region TableSqlQuery
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="query">查询</param>
    /// <param name="fields">字段</param>
    /// <returns></returns>
    public static GroupByTableSqlQuery<ITable> SqlGroupBy(this TableSqlQuery query, params IField[] fields)
        => new((ITable)query.Source, query._filter, fields);
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="query">查询</param>
    /// <param name="columnNames">列名</param>
    /// <returns></returns>
    public static GroupByTableSqlQuery<ITable> SqlGroupBy(this TableSqlQuery query, params IEnumerable<string> columnNames)
    {
        var table = (ITable)query.Source;
        return new GroupByTableSqlQuery<ITable>(table, query._filter, [.. table.Fields(columnNames)]);
    }
    #endregion
    #endregion
    #region GroupByAliasTableSqlQuery
    #region TableAlias
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="fields">字段</param>
    /// <returns></returns>
    public static GroupByAliasTableSqlQuery<TTable> SqlGroupBy<TTable>(this IAliasTable<TTable> table, params IField[] fields)
        where TTable : ITable
        => new(table, EmptyLogic.Instance, fields);
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public static GroupByAliasTableSqlQuery<TTable> SqlGroupBy<TTable>(this IAliasTable<TTable> table, Func<TTable, IColumn[]> select)
        where TTable : ITable
        => new(table, EmptyLogic.Instance, [.. select(table.Target).Select(table.Prefix)]);
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="columnNames">列名</param>
    /// <returns></returns>
    public static GroupByAliasTableSqlQuery<TTable> SqlGroupBy<TTable>(this IAliasTable<TTable> table, params IEnumerable<string> columnNames)
        where TTable : ITable
        => new(table, EmptyLogic.Instance, [.. table.Fields(columnNames)]);
    #endregion
    #region TableAlias & ISqlLogic
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table">表</param>
    /// <param name="where">查询条件</param>
    /// <param name="fields">字段</param>
    /// <returns></returns>
    public static GroupByAliasTableSqlQuery<TTable> SqlGroupBy<TTable>(this IAliasTable<TTable> table, ISqlLogic where, params IField[] fields)
        where TTable : ITable
        => new(table, where, fields);
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table">表</param>
    /// <param name="where">查询条件</param>
    /// <param name="columnNames">列名</param>
    /// <returns></returns>
    public static GroupByAliasTableSqlQuery<TTable> SqlGroupBy<TTable>(this IAliasTable<TTable> table, ISqlLogic where, params IEnumerable<string> columnNames)
        where TTable : ITable
        => new(table, where, [.. table.Fields(columnNames)]);
    #endregion
    #region AliasTableSqlQuery
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="query">查询</param>
    /// <param name="fields">字段</param>
    /// <returns></returns>
    public static GroupByAliasTableSqlQuery<TTable> SqlGroupBy<TTable>(this AliasTableSqlQuery<TTable> query, params IField[] fields)
        where TTable : ITable
    {
        return new GroupByAliasTableSqlQuery<TTable>(query.Source, query._filter, fields);
    }
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="query">查询</param>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public static GroupByAliasTableSqlQuery<TTable> SqlGroupBy<TTable>(this AliasTableSqlQuery<TTable> query, Func<TTable, IColumn[]> select)
        where TTable : ITable
    {
        var table = query.Source;
        return new GroupByAliasTableSqlQuery<TTable>(table, query._filter, [.. select(table.Target).Select(table.Prefix)]);
    }
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="query">查询</param>
    /// <param name="columnNames">列名</param>
    /// <returns></returns>
    public static GroupByAliasTableSqlQuery<TTable> SqlGroupBy<TTable>(this AliasTableSqlQuery<TTable> query, params IEnumerable<string> columnNames)
        where TTable : ITable
    {
        var table = query.Source;
        return new GroupByAliasTableSqlQuery<TTable>(table, query._filter, [.. table.Fields(columnNames)]);
    }
    #endregion
    #endregion
    #region GroupByMultiSqlQuery
    #region MultiTableSqlQuery
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="multiTable">多表(联表)</param>
    /// <param name="fields">字段</param>
    /// <returns></returns>
    public static GroupByMultiSqlQuery SqlGroupBy(this MultiTableSqlQuery multiTable, params IField[] fields)
        => new(multiTable, fields);
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="multiTable">多表(联表)</param>
    /// <param name="columnNames">列名</param>
    /// <returns></returns>
    public static GroupByMultiSqlQuery SqlGroupBy(this MultiTableSqlQuery multiTable, params IEnumerable<string> columnNames)
        => new(multiTable, [.. multiTable.Fields(columnNames)]);
    #endregion
    #region JoinTableSqlQuery
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="multiTable">多表(联表)</param>
    /// <param name="fields">字段</param>
    /// <returns></returns>
    public static GroupByMultiSqlQuery SqlGroupBy(this JoinTableSqlQuery multiTable, params IField[] fields)
        => new(multiTable, fields);
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="multiTable">多表(联表)</param>
    /// <param name="columnNames">列名</param>
    /// <returns></returns>
    public static GroupByMultiSqlQuery SqlGroupBy(this JoinTableSqlQuery multiTable, params IEnumerable<string> columnNames)
        => new(multiTable, [.. multiTable.Fields(columnNames)]);
    #endregion
    #region JoinOnSqlQuery<LTable, RTable>
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <typeparam name="TLeft"></typeparam>
    /// <typeparam name="TRight"></typeparam>
    /// <param name="joinOn">联接</param>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public static GroupByMultiSqlQuery SqlGroupBy<TLeft, TRight>(this AliasJoinOnSqlQuery<TLeft, TRight> joinOn, Func<TLeft, TRight, IPrefixField[]> select)
        where TLeft : IAliasTable<ITable>
        where TRight : IAliasTable<ITable>
        => new(joinOn.Root, [.. select(joinOn.Left, joinOn.Source)]);
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <typeparam name="LTable"></typeparam>
    /// <typeparam name="RTable"></typeparam>
    /// <param name="joinOn">联接</param>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public static GroupByMultiSqlQuery SqlGroupBy<LTable, RTable>(this JoinOnSqlQuery<LTable, RTable> joinOn, Func<LTable, RTable, IColumn[]> select)
        where LTable : ITable
        where RTable : ITable
        => new(joinOn.Root, [.. select(joinOn.Left.Target, joinOn.Source.Target).Select(joinOn.Prefix)]);
    #endregion
    #endregion
    #endregion
}

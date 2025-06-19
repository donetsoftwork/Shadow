using ShadowSql.Cursors;
using ShadowSql.Filters;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.SingleSelect;
using ShadowSql.Tables;
using ShadowSql.Variants;
using System;

namespace ShadowSql;

/// <summary>
/// 构造筛选单列扩展方法
/// </summary>
public static partial class ShadowSqlServices
{
    #region ITable
    /// <summary>
    /// 表筛选单列
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="field">字段</param>
    /// <returns></returns>
    public static TableSingleSelect ToSingle(this ITable table, IFieldView field)
        => new(table, field);
    /// <summary>
    /// 表筛选单列
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="columnName">列名</param>
    /// <returns></returns>
    public static TableSingleSelect ToSingle(this ITable table, string columnName)
        => new(table, table.Field(columnName));
    /// <summary>
    /// 表筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table">表</param>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public static TableSingleSelect ToSingle<TTable>(this TTable table, Func<TTable, IFieldView> select)
        where TTable : ITable
        => new(table, select(table));
    /// <summary>
    /// 表过滤筛选单列
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="filter">过滤条件</param>
    /// <param name="field">字段</param>
    /// <returns></returns>
    public static TableSingleSelect ToSingle(this ITable table, ISqlLogic filter, IFieldView field)
        => new(new TableFilter(table, filter), field);
    /// <summary>
    /// 表过滤筛选单列
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="filter">过滤条件</param>
    /// <param name="columnName">列名</param>
    /// <returns></returns>
    public static TableSingleSelect ToSingle(this ITable table, ISqlLogic filter, string columnName)
        => new(new TableFilter(table, filter), table.Field(columnName));
    /// <summary>
    /// 表过滤筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table">表</param>
    /// <param name="filter">过滤条件</param>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public static TableSingleSelect ToSingle<TTable>(this TTable table, ISqlLogic filter, Func<TTable, IFieldView> select)
        where TTable : ITable
        => new(new TableFilter(table, filter), select(table));
    #endregion
    #region IAliasTable
    /// <summary>
    /// 别名表筛选单列
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="field">字段</param>
    /// <returns></returns>
    public static TableSingleSelect ToSingle(this IAliasTable table, IFieldView field)
        => new(table, field);
    /// <summary>
    /// 别名表筛选单列
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="columnName">列名</param>
    /// <returns></returns>
    public static TableSingleSelect ToSingle(this IAliasTable table, string columnName)
        => new(table, table.Field(columnName));
    /// <summary>
    /// 表筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table">表</param>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public static TableSingleSelect ToSingle<TTable>(this IAliasTable<TTable> table, Func<TTable, IColumn> select)
        where TTable : ITable
        => new(table, table.Prefix(select(table.Target)));
    /// <summary>
    /// 别名表过滤筛选单列
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="filter">过滤条件</param>
    /// <param name="field">字段</param>
    /// <returns></returns>
    public static TableSingleSelect ToSingle(this IAliasTable table, ISqlLogic filter, IFieldView field)
        => new(new TableFilter(table, filter), field);
    /// <summary>
    /// 别名表过滤筛选单列
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="filter">过滤条件</param>
    /// <param name="columnName">列名</param>
    /// <returns></returns>
    public static TableSingleSelect ToSingle(this IAliasTable table, ISqlLogic filter, string columnName)
        => new(new TableFilter(table, filter), table.Field(columnName));
    /// <summary>
    /// 表过滤筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table">表</param>
    /// <param name="filter">过滤条件</param>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public static TableSingleSelect ToSingle<TTable>(this IAliasTable<TTable> table, ISqlLogic filter, Func<TTable, IColumn> select)
        where TTable : ITable
        => new(new TableFilter(table, filter), table.Prefix(select(table.Target)));
    #endregion
    #region IDataFilter
    /// <summary>
    /// 表筛选单列
    /// </summary>
    /// <param name="query">查询</param>
    /// <param name="field">字段</param>
    /// <returns></returns>
    public static TableSingleSelect ToSingle(this IDataFilter query, IFieldView field)
        => new(query, field);
    /// <summary>
    /// 表筛选单列
    /// </summary>
    /// <param name="query">查询</param>
    /// <param name="columnName">列名</param>
    /// <returns></returns>
    public static TableSingleSelect ToSingle(this IDataFilter query, string columnName)
        => new(query, query.Field(columnName));
    #endregion
    #region ICursor
    /// <summary>
    /// 表范围筛选单列
    /// </summary>
    /// <param name="cursor">游标</param>
    /// <param name="field">字段</param>
    /// <returns></returns>
    public static CursorSingleSelect ToSingle(this ICursor cursor, IFieldView field)
        => new(cursor, field);
    /// <summary>
    /// 表范围筛选单列
    /// </summary>
    /// <param name="cursor">游标</param>
    /// <param name="columnName">列名</param>
    /// <returns></returns>
    public static CursorSingleSelect ToSingle(this ICursor cursor, string columnName)
        => new(cursor, cursor.Field(columnName));
    #endregion
    #region IMultiView
    /// <summary>
    /// 多(联)表筛选单列
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public static TableSingleSelect ToSingle(this IMultiView table, Func<IMultiView, IFieldView> select)
        => new(table, select(table));
    #endregion
    #region IGroupByView
    /// <summary>
    /// 分组筛选单列
    /// </summary>
    /// <param name="groupBy">分组查询</param>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public static TableSingleSelect ToSingle(this IGroupByView groupBy, Func<IGroupByView, IFieldView> select)
        => new(groupBy, select(groupBy));
    #endregion
}

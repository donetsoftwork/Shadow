using ShadowSql.AliasTables;
using ShadowSql.Cursors;
using ShadowSql.CursorSelect;
using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using ShadowSql.Join;
using ShadowSql.Logics;
using ShadowSql.Select;
using ShadowSql.Tables;
using ShadowSql.Variants;
using System;

namespace ShadowSql;

/// <summary>
/// 构造筛选列扩展方法
/// </summary>
public static partial class ShadowSqlServices
{
    #region TableSelect
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <returns></returns>
    public static TableSelect<TTable> ToSelect<TTable>(this TTable table)
        where TTable : ITable
        => new(table);
    /// <summary>
    /// 表过滤筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <param name="filter"></param>
    /// <returns></returns>
    public static TableSelect<TTable> ToSelect<TTable>(this TTable table, ISqlLogic filter)
        where TTable : ITable
        => new(table, filter);
    /// <summary>
    /// 表过滤筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <param name="filter"></param>
    /// <returns></returns>
    public static TableSelect<TTable> ToSelect<TTable>(this TTable table, Func<TTable, ISqlLogic> filter)
        where TTable : ITable
        => new(table, filter(table));
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <returns></returns>
    public static TableSelect<TTable> ToSelect<TTable>(this TableQuery<TTable> query)
        where TTable : ITable
        => new(query);
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public static TableSelect<ITable> ToSelect(this TableQuery query)
        => new(query, (ITable)query.Source);
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <returns></returns>
    public static TableSelect<TTable> ToSelect<TTable>(this TableSqlQuery<TTable> query)
        where TTable : ITable
        => new(query);
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public static TableSelect<ITable> ToSelect(this TableSqlQuery query)
        => new(query, (ITable)query.Source);
    #endregion
    #region TableCursorSelect
    /// <summary>
    /// 表范围筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="cursor"></param>
    /// <returns></returns>
    public static TableCursorSelect<TTable> ToSelect<TTable>(this TableCursor<TTable> cursor)
        where TTable : ITable
        => new(cursor);
    #endregion
    #region AliasTableSelect
    /// <summary>
    /// 别名表筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <returns></returns>
    public static AliasTableSelect<TTable> ToSelect<TTable>(this TableAlias<TTable> table)
        where TTable : ITable
        => new(table);
    /// <summary>
    /// 别名表过滤筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <param name="filter"></param>
    /// <returns></returns>
    public static AliasTableSelect<TTable> ToSelect<TTable>(this TableAlias<TTable> table, ISqlLogic filter)
        where TTable : ITable
        => new(table, filter);
    /// <summary>
    /// 别名表筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <returns></returns>
    public static AliasTableSelect<TTable> ToSelect<TTable>(this AliasTableSqlQuery<TTable> query)
        where TTable : ITable
        => new(query);
    /// <summary>
    /// 别名表范围筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="cursor"></param>
    /// <returns></returns>
    public static AliasTableCursorSelect<TTable> ToSelect<TTable>(this AliasTableCursor<TTable> cursor)
        where TTable : ITable
        => new(cursor);
    #endregion
    #region MultiTableSelect
    /// <summary>
    /// 多(联)表筛选列
    /// </summary>
    /// <param name="table"></param>
    /// <returns></returns>
    public static MultiTableSelect ToSelect(this IMultiView table)
        => new(table);
    /// <summary>
    /// 多(联)表筛选列
    /// </summary>
    /// <param name="table"></param>
    /// <returns></returns>
    public static MultiTableSelect ToSelect(this IJoinOn table)
        => new(table.Root);
    #endregion
    #region MultiTableCursorSelect
    /// <summary>
    /// 多(联)表筛选列
    /// </summary>
    /// <param name="cursor"></param>
    /// <returns></returns>
    public static MultiTableCursorSelect ToSelect(this MultiTableCursor cursor)
        => new(cursor);
    #endregion
    #region GroupByTableSelect
    /// <summary>
    /// GroupBy后再筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static GroupByTableSelect<TTable> ToSelect<TTable>(this GroupByTableSqlQuery<TTable> source)
        where TTable : ITable
        => new(source);
    /// <summary>
    /// GroupBy后再筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static GroupByTableSelect<TTable> ToSelect<TTable>(this GroupByTableQuery<TTable> source)
        where TTable : ITable
        => new(source);
    #endregion
    #region GroupByTableCursorSelect
    /// <summary>
    /// GroupBy后再范围(分页)及列筛选
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="cursor"></param>
    /// <returns></returns>
    public static GroupByTableCursorSelect<TTable> ToSelect<TTable>(this GroupByTableCursor<TTable> cursor)
        where TTable : ITable
        => new(cursor);
    #endregion
    #region GroupByAliasTableSelect
    /// <summary>
    /// GroupBy别名表后再筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static GroupByAliasTableSelect<TTable> ToSelect<TTable>(this GroupByAliasTableSqlQuery<TTable> source)
        where TTable : ITable
        => new(source);
    /// <summary>
    /// GroupBy别名表后再范围(分页)及列筛选
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="cursor"></param>
    /// <returns></returns>
    public static GroupByAliasTableCursorSelect<TTable> ToSelect<TTable>(this GroupByAliasTableCursor<TTable> cursor)
        where TTable : ITable
        => new(cursor);
    #endregion
    #region GroupByMultiSelect
    /// <summary>
    /// GroupBy后再筛选列
    /// </summary>
    /// <param name="groupBy"></param>
    /// <returns></returns>
    public static GroupByMultiSelect ToSelect(this GroupByMultiSqlQuery groupBy)
        => new(groupBy);
    /// <summary>
    /// GroupBy后再筛选列
    /// </summary>
    /// <param name="groupBy"></param>
    /// <returns></returns>
    public static GroupByMultiSelect ToSelect(this GroupByMultiQuery groupBy)
        => new(groupBy);
    #endregion
    #region GroupByMultiCursorSelect
    /// <summary>
    /// GroupBy后再范围(分页)及列筛选
    /// </summary>
    /// <param name="cursor"></param>
    /// <returns></returns>
    public static GroupByMultiCursorSelect ToSelect(this GroupByMultiCursor cursor)
        => new(cursor);
    #endregion
}

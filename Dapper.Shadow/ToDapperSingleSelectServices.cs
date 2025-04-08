using Dapper.Shadow.Cursors;
using Dapper.Shadow.GroupBy;
using Dapper.Shadow.Join;
using Dapper.Shadow.Queries;
using Dapper.Shadow.SingleSelect;
using ShadowSql.AliasTables;
using ShadowSql.Cursors;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Tables;
using ShadowSql.Variants;

namespace Dapper.Shadow;

/// <summary>
/// 构造筛选单列扩展方法
/// </summary>
public static partial class DapperShadowServices
{
    #region IDapperTable
    #region TTable
    /// <summary>
    /// 表筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <returns></returns>
    public static DapperTableSingleSelect<TTable> ToDapperSingle<TTable>(this TTable table)
        where TTable : IDapperTable
        => new(table.Executor, table);
    /// <summary>
    /// 表过滤筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <param name="filter"></param>
    /// <returns></returns>
    public static DapperTableSingleSelect<TTable> ToDapperSingle<TTable>(this TTable table, ISqlLogic filter)
        where TTable : IDapperTable
        => new(table.Executor, table, filter);
    /// <summary>
    /// 表筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <returns></returns>
    public static DapperTableSingleSelect<TTable> ToDapperSingle<TTable>(this DapperTableSqlQuery<TTable> query)
        where TTable : ITable
        => new(query.Executor, query);
    /// <summary>
    /// 表筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <returns></returns>
    public static DapperTableSingleSelect<TTable> ToDapperSingle<TTable>(this TableSqlQuery<TTable> query)
        where TTable : IDapperTable
        => new(query.Source.Executor, query);
    /// <summary>
    /// 表范围筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="cursor"></param>
    /// <returns></returns>
    public static DapperTableCursorSingleSelect<TTable> ToDapperSingle<TTable>(this TableCursor<TTable> cursor)
        where TTable : IDapperTable
        => new(cursor.Source.Executor, cursor);
    #endregion
    #region Alias
    /// <summary>
    /// 别名表筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <returns></returns>
    public static DapperAliasTableSingleSelect<TTable> ToDapperSingle<TTable>(this TableAlias<TTable> table)
        where TTable : IDapperTable
        => new(table.Target.Executor, table);
    /// <summary>
    /// 别名表过滤筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <param name="filter"></param>
    /// <returns></returns>
    public static DapperAliasTableSingleSelect<TTable> ToDapperSingle<TTable>(this TableAlias<TTable> table, ISqlLogic filter)
        where TTable : IDapperTable
        => new(table.Target.Executor, table, filter);
    /// <summary>
    /// 别名表筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <returns></returns>
    public static DapperAliasTableSingleSelect<TTable> ToDapperSingle<TTable>(this AliasTableSqlQuery<TTable> query)
        where TTable : IDapperTable
        => new(query.Table.Executor, query);
    /// <summary>
    /// 别名表筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <returns></returns>
    public static DapperAliasTableSingleSelect<TTable> ToDapperSingle<TTable>(this DapperAliasTableSqlQuery<TTable> query)
        where TTable : ITable
        => new(query.Executor, query);
    /// <summary>
    /// 别名表范围筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="cursor"></param>
    /// <returns></returns>
    public static DapperAliasTableCursorSingleSelect<TTable> ToDapperSingle<TTable>(this AliasTableCursor<TTable> cursor)
        where TTable : IDapperTable
        => new(cursor.Table.Executor, cursor);
    #endregion
    #region GroupByTable
    /// <summary>
    /// GroupBy后再筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static DapperGroupByTableSingleSelect<TTable> ToDapperSingle<TTable>(this DapperGroupByTableSqlQuery<TTable> source)
        where TTable : IDapperTable
        => new(source.Executor, source);
    /// <summary>
    /// GroupBy后再范围(分页)及单列筛选
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="cursor"></param>
    /// <returns></returns>
    public static DapperGroupByTableCursorSingleSelect<TTable> ToDapperSingle<TTable>(this GroupByTableCursor<TTable> cursor)
        where TTable : IDapperTable
        => new(cursor.Table.Executor, cursor);
    #endregion
    #region GroupByAliasTable
    /// <summary>
    /// GroupBy别名表后再筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static DapperGroupByAliasTableSingleSelect<TTable> ToDapperSingle<TTable>(this DapperGroupByAliasTableSqlQuery<TTable> source)
        where TTable : IDapperTable
        => new(source.Executor, source);
    /// <summary>
    /// GroupBy别名表后再范围(分页)及单列筛选
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="cursor"></param>
    /// <returns></returns>
    public static DapperGroupByAliasTableCursorSingleSelect<TTable> ToDapperSingle<TTable>(this GroupByAliasTableCursor<TTable> cursor)
        where TTable : IDapperTable
        => new(cursor.Table.Executor, cursor);
    #endregion
    #endregion
    #region Dapper
    #region Multi
    /// <summary>
    /// 多表筛选列
    /// </summary>
    /// <param name="multiTable"></param>
    /// <returns></returns>
    public static DapperMultiTableSingleSelect ToDapperSingle(this DapperMultiTableSqlQuery multiTable)
        => new(multiTable.Executor, multiTable);
    /// <summary>
    /// 联表筛选单列
    /// </summary>
    /// <param name="joinTable"></param>
    /// <returns></returns>
    public static DapperMultiTableSingleSelect ToDapperSingle(this DapperJoinTableSqlQuery joinTable)
        => new(joinTable.Executor, joinTable);
    /// <summary>
    /// 多(联)表筛选单列
    /// </summary>
    /// <param name="cursor"></param>
    /// <returns></returns>
    public static DapperMultiTableCursorSingleSelect ToDapperSingle(this DapperMultiTableCursor cursor)
        => new(cursor.Executor, cursor);
    #endregion
    #region GroupByMulti
    /// <summary>
    /// GroupBy后再筛选单列
    /// </summary>
    /// <param name="groupBy"></param>
    /// <returns></returns>
    public static DapperGroupByMultiSingleSelect ToDapperSingle(this DapperGroupByMultiSqlQuery groupBy)
        => new(groupBy.Executor, groupBy);
    /// <summary>
    /// GroupBy后再范围(分页)及单列筛选
    /// </summary>
    /// <param name="cursor"></param>
    /// <returns></returns>
    public static DapperGroupByMultiCursorSingleSelect ToDapperSingle(this DapperGroupByMultiCursor cursor)
        => new(cursor.Executor, cursor);
    #endregion
    #endregion
}

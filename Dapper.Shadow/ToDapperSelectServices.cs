using Dapper.Shadow.Cursors;
using Dapper.Shadow.CursorSelect;
using Dapper.Shadow.GroupBy;
using Dapper.Shadow.Join;
using Dapper.Shadow.Queries;
using Dapper.Shadow.Select;
using ShadowSql.AliasTables;
using ShadowSql.Cursors;
using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Tables;
using ShadowSql.Variants;

namespace Dapper.Shadow;

/// <summary>
/// 构造筛选列扩展方法
/// </summary>
public static partial class DapperShadowServices
{
    #region IDapperTable
    #region Table
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <returns></returns>
    public static DapperTableSelect<TTable> ToDapperSelect<TTable>(this TTable table)
        where TTable : IDapperTable
        => new(table.Executor, table);
    /// <summary>
    /// 表过滤筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <param name="filter"></param>
    /// <returns></returns>
    public static DapperTableSelect<TTable> ToDapperSelect<TTable>(this TTable table, ISqlLogic filter)
        where TTable : IDapperTable
        => new(table.Executor, table, filter);
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <returns></returns>
    public static DapperTableSelect<TTable> ToDapperSelect<TTable>(this TableSqlQuery<TTable> query)
        where TTable : IDapperTable
        => new(query.Source.Executor, query);
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <returns></returns>
    public static DapperTableSelect<TTable> ToDapperSelect<TTable>(this TableQuery<TTable> query)
        where TTable : IDapperTable
        => new(query.Source.Executor, query);
    /// <summary>
    /// 表范围筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="cursor"></param>
    /// <returns></returns>
    public static DapperTableCursorSelect<TTable> ToDapperSelect<TTable>(this TableCursor<TTable> cursor)
        where TTable : IDapperTable
        => new(cursor.Source.Executor, cursor);
    #endregion
    #region AliasTable
    /// <summary>
    /// 别名表筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <returns></returns>
    public static DapperAliasTableSelect<TTable> ToDapperSelect<TTable>(this TableAlias<TTable> table)
        where TTable : IDapperTable
        => new(table.Target.Executor, table);
    /// <summary>
    /// 别名表过滤筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <param name="filter"></param>
    /// <returns></returns>
    public static DapperAliasTableSelect<TTable> ToDapperSelect<TTable>(this TableAlias<TTable> table, ISqlLogic filter)
        where TTable : IDapperTable
        => new(table.Target.Executor, table, filter);
    /// <summary>
    /// 别名表筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <returns></returns>
    public static DapperAliasTableSelect<TTable> ToDapperSelect<TTable>(this AliasTableSqlQuery<TTable> query)
        where TTable : IDapperTable
        => new(query.Table.Executor, query);
    /// <summary>
    /// 别名表筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <returns></returns>
    public static DapperAliasTableSelect<TTable> ToDapperSelect<TTable>(this AliasTableQuery<TTable> query)
        where TTable : IDapperTable
        => new(query.Table.Executor, query);
    /// <summary>
    /// 别名表范围筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="cursor"></param>
    /// <returns></returns>
    public static DapperAliasTableCursorSelect<TTable> ToDapperSelect<TTable>(this AliasTableCursor<TTable> cursor)
        where TTable : IDapperTable
        => new(cursor.Table.Executor, cursor);
    #endregion
    #region GroupByTable
    /// <summary>
    /// GroupBy后再筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <returns></returns>
    public static DapperGroupByTableSelect<TTable> ToDapperSelect<TTable>(this GroupByTableSqlQuery<TTable> query)
        where TTable : IDapperTable
        => new(query.Source.Executor, query);
    /// <summary>
    /// GroupBy后再筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <returns></returns>
    public static DapperGroupByTableSelect<TTable> ToDapperSelect<TTable>(this GroupByTableQuery<TTable> query)
        where TTable : IDapperTable
        => new(query.Source.Executor, query);
    /// <summary>
    /// GroupBy后再范围(分页)及列筛选
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="cursor"></param>
    /// <returns></returns>
    public static DapperGroupByTableCursorSelect<TTable> ToDapperSelect<TTable>(this GroupByTableCursor<TTable> cursor)
        where TTable : IDapperTable
        => new(cursor.Table.Executor, cursor);
    #endregion
    #region GroupByAliasTable
    /// <summary>
    /// GroupBy别名表后再筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <returns></returns>
    public static DapperGroupByAliasTableSelect<TTable> ToDapperSelect<TTable>(this GroupByAliasTableSqlQuery<TTable> query)
        where TTable : IDapperTable
        => new(query.Source.Target.Executor, query);
    /// <summary>
    /// GroupBy别名表后再筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <returns></returns>
    public static DapperGroupByAliasTableSelect<TTable> ToDapperSelect<TTable>(this GroupByAliasTableQuery<TTable> query)
        where TTable : IDapperTable
        => new(query.Source.Target.Executor, query);
    /// <summary>
    /// GroupBy别名表后再范围(分页)及列筛选
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="cursor"></param>
    /// <returns></returns>
    public static DapperGroupByAliasTableCursorSelect<TTable> ToDapperSelect<TTable>(this GroupByAliasTableCursor<TTable> cursor)
        where TTable : IDapperTable
        => new(cursor.Table.Executor, cursor);
    #endregion
    #endregion
    #region Dapper
    #region TTable
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <returns></returns>
    public static DapperTableSelect<TTable> ToDapperSelect<TTable>(this DapperTableSqlQuery<TTable> query)
        where TTable : ITable
        => new(query.Executor, query);
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <returns></returns>
    public static DapperTableSelect<TTable> ToDapperSelect<TTable>(this DapperTableQuery<TTable> query)
        where TTable : ITable
        => new(query.Executor, query);
    #endregion
    #region AliasTable
    /// <summary>
    /// 别名表筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <returns></returns>
    public static DapperAliasTableSelect<TTable> ToDapperSelect<TTable>(this DapperAliasTableSqlQuery<TTable> query)
        where TTable : ITable
        => new(query.Executor, query);
    /// <summary>
    /// 别名表筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <returns></returns>
    public static DapperAliasTableSelect<TTable> ToDapperSelect<TTable>(this DapperAliasTableQuery<TTable> query)
        where TTable : ITable
        => new(query.Executor, query);
    #endregion
    #region MultiTable
    /// <summary>
    /// 多表筛选列
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public static DapperMultiTableSelect ToDapperSelect(this DapperMultiTableSqlQuery query)
        => new(query.Executor, query);
    /// <summary>
    /// 联表筛选列
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public static DapperMultiTableSelect ToDapperSelect(this DapperJoinTableSqlQuery query)
        => new(query.Executor, query);
    /// <summary>
    /// 联表筛选列
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public static DapperMultiTableSelect ToDapperSelect(this DapperJoinTableQuery query)
        => new(query.Executor, query);
    /// <summary>
    /// 多(联)表筛选列
    /// </summary>
    /// <param name="cursor"></param>
    /// <returns></returns>
    public static DapperMultiTableCursorSelect ToDapperSelect(this DapperMultiTableCursor cursor)
        => new(cursor.Executor, cursor);
    #endregion
    #region GroupByTable
    /// <summary>
    /// GroupBy后再筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <returns></returns>
    public static DapperGroupByTableSelect<TTable> ToDapperSelect<TTable>(this DapperGroupByTableSqlQuery<TTable> query)
        where TTable : ITable
        => new(query.Executor, query);
    /// <summary>
    /// GroupBy后再筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <returns></returns>
    public static DapperGroupByTableSelect<TTable> ToDapperSelect<TTable>(this DapperGroupByTableQuery<TTable> query)
        where TTable : ITable
        => new(query.Executor, query);
    #endregion
    #region GroupByAliasTable
    /// <summary>
    /// GroupBy别名表后再筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <returns></returns>
    public static DapperGroupByAliasTableSelect<TTable> ToDapperSelect<TTable>(this DapperGroupByAliasTableSqlQuery<TTable> query)
        where TTable : ITable
        => new(query.Executor, query);
    /// <summary>
    /// GroupBy别名表后再筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <returns></returns>
    public static DapperGroupByAliasTableSelect<TTable> ToDapperSelect<TTable>(this DapperGroupByAliasTableQuery<TTable> query)
        where TTable : ITable
        => new(query.Executor, query);
    #endregion
    #region GroupByMulti
    /// <summary>
    /// GroupBy后再筛选列
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public static DapperGroupByMultiSelect ToDapperSelect(this DapperGroupByMultiSqlQuery query)
        => new(query.Executor, query);
    /// <summary>
    /// GroupBy后再筛选列
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public static DapperGroupByMultiSelect ToDapperSelect(this DapperGroupByMultiQuery query)
        => new(query.Executor, query);
    /// <summary>
    /// GroupBy后再范围(分页)及列筛选
    /// </summary>
    /// <param name="cursor"></param>
    /// <returns></returns>
    public static DapperGroupByMultiCursorSelect ToDapperSelect(this DapperGroupByMultiCursor cursor)
        => new(cursor.Executor, cursor);
    #endregion
    #endregion
    #region IExecutor
    #region Table
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <param name="executor"></param>
    /// <returns></returns>
    public static DapperTableSelect<TTable> ToDapperSelect<TTable>(this TTable table, IExecutor executor)
        where TTable : ITable
        => new(executor, table);
    /// <summary>
    /// 表过滤筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <param name="filter"></param>
    /// <param name="executor"></param>
    /// <returns></returns>
    public static DapperTableSelect<TTable> ToDapperSelect<TTable>(this TTable table, ISqlLogic filter, IExecutor executor)
        where TTable : ITable
        => new(executor, table, filter);
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <param name="executor"></param>
    /// <returns></returns>
    public static DapperTableSelect<TTable> ToDapperSelect<TTable>(this TableSqlQuery<TTable> query, IExecutor executor)
        where TTable : ITable
        => new(executor, query);
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <param name="executor"></param>
    /// <returns></returns>
    public static DapperTableSelect<TTable> ToDapperSelect<TTable>(this TableQuery<TTable> query, IExecutor executor)
        where TTable : ITable
        => new(executor, query);
    /// <summary>
    /// 表范围筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="cursor"></param>
    /// <param name="executor"></param>
    /// <returns></returns>
    public static DapperTableCursorSelect<TTable> ToDapperSelect<TTable>(this TableCursor<TTable> cursor, IExecutor executor)
        where TTable : ITable
        => new(executor, cursor);
    #endregion
    #region AliasTable
    /// <summary>
    /// 别名表筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <param name="executor"></param>
    /// <returns></returns>
    public static DapperAliasTableSelect<TTable> ToDapperSelect<TTable>(this TableAlias<TTable> table, IExecutor executor)
        where TTable : ITable
        => new(executor, table);
    /// <summary>
    /// 别名表过滤筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <param name="filter"></param>
    /// <param name="executor"></param>
    /// <returns></returns>
    public static DapperAliasTableSelect<TTable> ToDapperSelect<TTable>(this TableAlias<TTable> table, ISqlLogic filter, IExecutor executor)
        where TTable : ITable
        => new(executor, table, filter);
    /// <summary>
    /// 别名表筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <param name="executor"></param>
    /// <returns></returns>
    public static DapperAliasTableSelect<TTable> ToDapperSelect<TTable>(this AliasTableSqlQuery<TTable> query, IExecutor executor)
        where TTable : ITable
        => new(executor, query);
    /// <summary>
    /// 别名表筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <param name="executor"></param>
    /// <returns></returns>
    public static DapperAliasTableSelect<TTable> ToDapperSelect<TTable>(this AliasTableQuery<TTable> query, IExecutor executor)
        where TTable : ITable
        => new(executor, query);
    /// <summary>
    /// 别名表范围筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="cursor"></param>
    /// <param name="executor"></param>
    /// <returns></returns>
    public static DapperAliasTableCursorSelect<TTable> ToDapperSelect<TTable>(this AliasTableCursor<TTable> cursor, IExecutor executor)
        where TTable : ITable
        => new(executor, cursor);
    #endregion
    #region MultiTable
    /// <summary>
    /// 多表筛选列
    /// </summary>
    /// <param name="multiTable"></param>
    /// <param name="executor"></param>
    /// <returns></returns>
    public static DapperMultiTableSelect ToDapperSelect(this IMultiView multiTable, IExecutor executor)
        => new(executor, multiTable);
    /// <summary>
    /// 多(联)表筛选列
    /// </summary>
    /// <param name="cursor"></param>
    /// <param name="executor"></param>
    /// <returns></returns>
    public static DapperMultiTableCursorSelect ToDapperSelect(this MultiTableCursor cursor, IExecutor executor)
        => new(executor, cursor);
    #endregion
    #region GroupByTable
    /// <summary>
    /// GroupBy后再筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <param name="executor"></param>
    /// <returns></returns>
    public static DapperGroupByTableSelect<TTable> ToDapperSelect<TTable>(this GroupByTableSqlQuery<TTable> query, IExecutor executor)
        where TTable : ITable
        => new(executor, query);
    /// <summary>
    /// GroupBy后再筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <param name="executor"></param>
    /// <returns></returns>
    public static DapperGroupByTableSelect<TTable> ToDapperSelect<TTable>(this GroupByTableQuery<TTable> query, IExecutor executor)
        where TTable : ITable
        => new(executor, query);
    /// <summary>
    /// GroupBy后再范围(分页)及列筛选
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="cursor"></param>
    /// <param name="executor"></param>
    /// <returns></returns>
    public static DapperGroupByTableCursorSelect<TTable> ToDapperSelect<TTable>(this GroupByTableCursor<TTable> cursor, IExecutor executor)
        where TTable : ITable
        => new(executor, cursor);
    #endregion
    #region GroupByAliasTable
    /// <summary>
    /// GroupBy别名表后再筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <param name="executor"></param>
    /// <returns></returns>
    public static DapperGroupByAliasTableSelect<TTable> ToDapperSelect<TTable>(this GroupByAliasTableSqlQuery<TTable> query, IExecutor executor)
        where TTable : ITable
        => new(executor, query);
    /// <summary>
    /// GroupBy别名表后再筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <param name="executor"></param>
    /// <returns></returns>
    public static DapperGroupByAliasTableSelect<TTable> ToDapperSelect<TTable>(this GroupByAliasTableQuery<TTable> query, IExecutor executor)
        where TTable : ITable
        => new(executor, query);
    /// <summary>
    /// GroupBy别名表后再范围(分页)及列筛选
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="cursor"></param>
    /// <param name="executor"></param>
    /// <returns></returns>
    public static DapperGroupByAliasTableCursorSelect<TTable> ToDapperSelect<TTable>(this GroupByAliasTableCursor<TTable> cursor, IExecutor executor)
        where TTable : ITable
        => new(executor, cursor);
    #endregion
    #region GroupByMulti
    /// <summary>
    /// GroupBy后再筛选列
    /// </summary>
    /// <param name="query"></param>
    /// <param name="executor"></param>
    /// <returns></returns>
    public static DapperGroupByMultiSelect ToDapperSelect(this GroupByMultiSqlQuery query, IExecutor executor)
        => new(executor, query);
    /// <summary>
    /// GroupBy后再筛选列
    /// </summary>
    /// <param name="query"></param>
    /// <param name="executor"></param>
    /// <returns></returns>
    public static DapperGroupByMultiSelect ToDapperSelect(this GroupByMultiQuery query, IExecutor executor)
        => new(executor, query);
    /// <summary>
    /// GroupBy后再范围(分页)及列筛选
    /// </summary>
    /// <param name="cursor"></param>
    /// <param name="executor"></param>
    /// <returns></returns>
    public static DapperGroupByMultiCursorSelect ToDapperSelect(this GroupByMultiCursor cursor, IExecutor executor)
        => new(executor, cursor);
    #endregion
    #endregion
}

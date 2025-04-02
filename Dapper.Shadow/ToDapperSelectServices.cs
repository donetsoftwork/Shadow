using Dapper.Shadow.Fetches;
using Dapper.Shadow.GroupBy;
using Dapper.Shadow.Join;
using Dapper.Shadow.Queries;
using Dapper.Shadow.Select;
using ShadowSql.AliasTables;
using ShadowSql.Cursors;
using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using ShadowSql.Join;
using ShadowSql.Logics;
using ShadowSql.Tables;
using ShadowSql.Variants;

namespace Dapper.Shadow;

/// <summary>
/// 构造筛选列扩展方法
/// </summary>
public static partial class DapperShadowServices
{
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
    public static DapperTableSelect<TTable> ToDapperSelect<TTable>(this DapperTableSqlQuery<TTable> query)
        where TTable : ITable
        => new(query.Executor, query);
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
    /// 表范围筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="cursor"></param>
    /// <returns></returns>
    public static DapperTableFetchSelect<TTable> ToDapperSelect<TTable>(this TableCursor<TTable> cursor)
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
    public static DapperAliasTableSelect<TTable> ToDapperSelect<TTable>(this DapperAliasTableSqlQuery<TTable> query)
        where TTable : ITable
        => new(query.Executor, query);
    /// <summary>
    /// 别名表范围筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="cursor"></param>
    /// <returns></returns>
    public static DapperAliasTableFetchSelect<TTable> ToDapperSelect<TTable>(this AliasTableCursor<TTable> cursor)
        where TTable : IDapperTable
        => new(cursor.Table.Executor, cursor);
    #endregion
    #region MultiTable
    /// <summary>
    /// 多表筛选列
    /// </summary>
    /// <param name="multiTable"></param>
    /// <returns></returns>
    public static DapperMultiTableSelect ToDapperSelect(this DapperMultiTableSqlQuery multiTable)
        => new(multiTable.Executor, multiTable);
    /// <summary>
    /// 联表筛选列
    /// </summary>
    /// <param name="joinTable"></param>
    /// <returns></returns>
    public static DapperMultiTableSelect ToDapperSelect(this DapperJoinTableSqlQuery joinTable)
        => new(joinTable.Executor, joinTable);
    /// <summary>
    /// 多(联)表筛选列
    /// </summary>
    /// <param name="cursor"></param>
    /// <returns></returns>
    public static DapperMultiTableFetchSelect ToDapperSelect(this DapperMultiTableFetch cursor)
        => new(cursor.Executor, cursor);
    #endregion
    #region GroupByTable
    /// <summary>
    /// GroupBy后再筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static DapperGroupByTableSelect<TTable> ToDapperSelect<TTable>(this DapperGroupByTableSqlQuery<TTable> source)
        where TTable : IDapperTable
        => new(source.Executor, source);
    /// <summary>
    /// GroupBy后再范围(分页)及列筛选
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="cursor"></param>
    /// <returns></returns>
    public static DapperGroupByTableFetchSelect<TTable> ToDapperSelect<TTable>(this GroupByTableCursor<TTable> cursor)
        where TTable : IDapperTable
        => new(cursor.Table.Executor, cursor);
    #endregion
    #region GroupByAliasTable
    /// <summary>
    /// GroupBy别名表后再筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static DapperGroupByAliasTableSelect<TTable> ToDapperSelect<TTable>(this DapperGroupByAliasTableSqlQuery<TTable> source)
        where TTable : IDapperTable
        => new(source.Executor, source);
    /// <summary>
    /// GroupBy别名表后再范围(分页)及列筛选
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="cursor"></param>
    /// <returns></returns>
    public static DapperGroupByAliasTableFetchSelect<TTable> ToDapperSelect<TTable>(this GroupByAliasTableCursor<TTable> cursor)
        where TTable : IDapperTable
        => new(cursor.Table.Executor, cursor);
    #endregion
    #region GroupByMulti
    /// <summary>
    /// GroupBy后再筛选列
    /// </summary>
    /// <param name="groupBy"></param>
    /// <returns></returns>
    public static DapperGroupByMultiSelect ToDapperSelect(this DapperGroupByMultiSqlQuery groupBy)
        => new(groupBy.Executor, groupBy);
    /// <summary>
    /// GroupBy后再范围(分页)及列筛选
    /// </summary>
    /// <param name="cursor"></param>
    /// <returns></returns>
    public static DapperGroupByMultiFetchSelect ToDapperSelect(this DapperGroupByMultiFetch cursor)
        => new(cursor.Executor, cursor);
    #endregion
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
    /// 表范围筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="cursor"></param>
    /// <param name="executor"></param>
    /// <returns></returns>
    public static DapperTableFetchSelect<TTable> ToDapperSelect<TTable>(this TableCursor<TTable> cursor, IExecutor executor)
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
    /// 别名表范围筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="cursor"></param>
    /// <param name="executor"></param>
    /// <returns></returns>
    public static DapperAliasTableFetchSelect<TTable> ToDapperSelect<TTable>(this AliasTableCursor<TTable> cursor, IExecutor executor)
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
    public static DapperMultiTableSelect ToDapperSelect(this MultiTableSqlQuery multiTable, IExecutor executor)
        => new(executor, multiTable);
    /// <summary>
    /// 联表筛选列
    /// </summary>
    /// <param name="joinTable"></param>
    /// <param name="executor"></param>
    /// <returns></returns>
    public static DapperMultiTableSelect ToDapperSelect(this JoinTableQuery joinTable, IExecutor executor)
        => new(executor, joinTable);
    /// <summary>
    /// 多(联)表筛选列
    /// </summary>
    /// <param name="cursor"></param>
    /// <param name="executor"></param>
    /// <returns></returns>
    public static DapperMultiTableFetchSelect ToDapperSelect(this MultiTableCursor cursor, IExecutor executor)
        => new(executor, cursor);
    #endregion
    #region GroupByTable
    /// <summary>
    /// GroupBy后再筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="source"></param>
    /// <param name="executor"></param>
    /// <returns></returns>
    public static DapperGroupByTableSelect<TTable> ToDapperSelect<TTable>(this GroupByTableSqlQuery<TTable> source, IExecutor executor)
        where TTable : ITable
        => new(executor, source);
    /// <summary>
    /// GroupBy后再范围(分页)及列筛选
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="cursor"></param>
    /// <param name="executor"></param>
    /// <returns></returns>
    public static DapperGroupByTableFetchSelect<TTable> ToDapperSelect<TTable>(this GroupByTableCursor<TTable> cursor, IExecutor executor)
        where TTable : ITable
        => new(executor, cursor);
    #endregion
    #region GroupByAliasTable
    /// <summary>
    /// GroupBy别名表后再筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="source"></param>
    /// <param name="executor"></param>
    /// <returns></returns>
    public static DapperGroupByAliasTableSelect<TTable> ToDapperSelect<TTable>(this GroupByAliasTableSqlQuery<TTable> source, IExecutor executor)
        where TTable : ITable
        => new(executor, source);
    /// <summary>
    /// GroupBy别名表后再范围(分页)及列筛选
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="cursor"></param>
    /// <param name="executor"></param>
    /// <returns></returns>
    public static DapperGroupByAliasTableFetchSelect<TTable> ToDapperSelect<TTable>(this GroupByAliasTableCursor<TTable> cursor, IExecutor executor)
        where TTable : ITable
        => new(executor, cursor);
    #endregion
    #region GroupByMulti
    /// <summary>
    /// GroupBy后再筛选列
    /// </summary>
    /// <param name="groupBy"></param>
    /// <param name="executor"></param>
    /// <returns></returns>
    public static DapperGroupByMultiSelect ToDapperSelect(this GroupByMultiSqlQuery groupBy, IExecutor executor)
        => new(executor, groupBy);
    /// <summary>
    /// GroupBy后再范围(分页)及列筛选
    /// </summary>
    /// <param name="cursor"></param>
    /// <param name="executor"></param>
    /// <returns></returns>
    public static DapperGroupByMultiFetchSelect ToDapperSelect(this GroupByMultiCursor cursor, IExecutor executor)
        => new(executor, cursor);
    #endregion
}

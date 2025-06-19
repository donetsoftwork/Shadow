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
    /// <param name="table">表</param>
    /// <param name="singleField">单列</param>
    /// <returns></returns>
    public static DapperTableSingleSelect ToDapperSingle<TTable>(this TTable table, IFieldView singleField)
        where TTable : IDapperTable
        => new(table.Executor, table, singleField);
    /// <summary>
    /// 表过滤筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table">表</param>
    /// <param name="filter">过滤条件</param>
    /// <param name="singleField">单列</param>
    /// <returns></returns>
    public static DapperTableSingleSelect ToDapperSingle<TTable>(this TTable table, ISqlLogic filter, IFieldView singleField)
        where TTable : IDapperTable
        => new(table.Executor, new TableFilter(table, filter), singleField);
    /// <summary>
    /// 表筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query">查询</param>
    /// <param name="singleField">单列</param>
    /// <returns></returns>
    public static DapperTableSingleSelect ToDapperSingle<TTable>(this DapperTableSqlQuery<TTable> query, IFieldView singleField)
        where TTable : ITable
        => new(query.Executor, query, singleField);
    /// <summary>
    /// 表筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query">查询</param>
    /// <param name="singleField">单列</param>
    /// <returns></returns>
    public static DapperTableSingleSelect ToDapperSingle<TTable>(this TableSqlQuery<TTable> query, IFieldView singleField)
        where TTable : IDapperTable
        => new(query.Source.Executor, query, singleField);
    /// <summary>
    /// 表范围筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="cursor">游标</param>
    /// <param name="singleField">单列</param>
    /// <returns></returns>
    public static DapperCursorSingleSelect ToDapperSingle<TTable>(this TableCursor<TTable> cursor, IFieldView singleField)
        where TTable : IDapperTable
        => new(cursor.Source.Executor, cursor, singleField);
    #endregion
    #region Alias
    /// <summary>
    /// 别名表筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="aliasTable">别名表</param>
    /// <param name="singleField">单列</param>
    /// <returns></returns>
    public static DapperTableSingleSelect ToDapperSingle<TTable>(this IAliasTable<TTable> aliasTable, IFieldView singleField)
        where TTable : IDapperTable
        => new(aliasTable.Target.Executor, aliasTable, singleField);
    /// <summary>
    /// 别名表过滤筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="aliasTable">别名表</param>
    /// <param name="filter">过滤条件</param>
    /// <param name="singleField">单列</param>
    /// <returns></returns>
    public static DapperTableSingleSelect ToDapperSingle<TTable>(this IAliasTable<TTable> aliasTable, ISqlLogic filter, IFieldView singleField)
        where TTable : IDapperTable
        => new(aliasTable.Target.Executor, new TableFilter(aliasTable, filter), singleField);
    /// <summary>
    /// 别名表筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query">查询</param>
    /// <param name="singleField">单列</param>
    /// <returns></returns>
    public static DapperTableSingleSelect ToDapperSingle<TTable>(this AliasTableSqlQuery<TTable> query, IFieldView singleField)
        where TTable : IDapperTable
        => new(query.Table.Executor, query, singleField);
    /// <summary>
    /// 别名表筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query">查询</param>
    /// <param name="singleField">单列</param>
    /// <returns></returns>
    public static DapperTableSingleSelect ToDapperSingle<TTable>(this DapperAliasTableSqlQuery<TTable> query, IFieldView singleField)
        where TTable : ITable
        => new(query.Executor, query, singleField);
    /// <summary>
    /// 别名表范围筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="cursor">游标</param>
    /// <param name="singleField">单列</param>
    /// <returns></returns>
    public static DapperCursorSingleSelect ToDapperSingle<TTable>(this AliasTableCursor<TTable> cursor, IFieldView singleField)
        where TTable : IDapperTable
        => new(cursor.Table.Executor, cursor, singleField);
    #endregion
    #region GroupByTable
    /// <summary>
    /// GroupBy后再筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="groupBy">分组查询</param>
    /// <param name="singleField">单列</param>
    /// <returns></returns>
    public static DapperTableSingleSelect ToDapperSingle<TTable>(this DapperGroupByTableSqlQuery<TTable> groupBy, IFieldView singleField)
        where TTable : IDapperTable
        => new(groupBy.Executor, groupBy, singleField);
    /// <summary>
    /// GroupBy后再范围(分页)及单列筛选
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="cursor">游标</param>
    /// <param name="singleField">单列</param>
    /// <returns></returns>
    public static DapperCursorSingleSelect ToDapperSingle<TTable>(this GroupByTableCursor<TTable> cursor, IFieldView singleField)
        where TTable : IDapperTable
        => new(cursor.Table.Executor, cursor, singleField);
    #endregion
    #region GroupByAliasTable
    /// <summary>
    /// GroupBy别名表后再筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="groupBy">分组查询</param>
    /// <param name="singleField">单列</param>
    /// <returns></returns>
    public static DapperTableSingleSelect ToDapperSingle<TTable>(this DapperGroupByAliasTableSqlQuery<TTable> groupBy, IFieldView singleField)
        where TTable : IDapperTable
        => new(groupBy.Executor, groupBy, singleField);
    /// <summary>
    /// GroupBy别名表后再范围(分页)及单列筛选
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="cursor">游标</param>
    /// <param name="singleField">单列</param>
    /// <returns></returns>
    public static DapperCursorSingleSelect ToDapperSingle<TTable>(this GroupByAliasTableCursor<TTable> cursor, IFieldView singleField)
        where TTable : IDapperTable
        => new(cursor.Table.Executor, cursor, singleField);
    #endregion
    #endregion
    #region Dapper
    #region Multi
    /// <summary>
    /// 多表筛选列
    /// </summary>
    /// <param name="multiTable">多表(联表)</param>
    /// <param name="singleField">单列</param>
    /// <returns></returns>
    public static DapperTableSingleSelect ToDapperSingle(this DapperMultiTableSqlQuery multiTable, IFieldView singleField)
        => new(multiTable.Executor, multiTable, singleField);
    /// <summary>
    /// 联表筛选单列
    /// </summary>
    /// <param name="joinTable">联表</param>
    /// <param name="singleField">单列</param>
    /// <returns></returns>
    public static DapperTableSingleSelect ToDapperSingle(this DapperJoinTableSqlQuery joinTable, IFieldView singleField)
        => new(joinTable.Executor, joinTable, singleField);
    /// <summary>
    /// 多(联)表筛选单列
    /// </summary>
    /// <param name="cursor">游标</param>
    /// <param name="singleField">单列</param>
    /// <returns></returns>
    public static DapperCursorSingleSelect ToDapperSingle(this DapperMultiTableCursor cursor, IFieldView singleField)
        => new(cursor.Executor, cursor, singleField);
    #endregion
    #region GroupByMulti
    /// <summary>
    /// GroupBy后再筛选单列
    /// </summary>
    /// <param name="groupBy">分组查询</param>
    /// <param name="singleField">单列</param>
    /// <returns></returns>
    public static DapperTableSingleSelect ToDapperSingle(this DapperGroupByMultiSqlQuery groupBy, IFieldView singleField)
        => new(groupBy.Executor, groupBy, singleField);
    /// <summary>
    /// GroupBy后再范围(分页)及单列筛选
    /// </summary>
    /// <param name="cursor">游标</param>
    /// <param name="singleField">单列</param>
    /// <returns></returns>
    public static DapperCursorSingleSelect ToDapperSingle(this DapperGroupByMultiCursor cursor, IFieldView singleField)
        => new(cursor.Executor, cursor, singleField);
    #endregion
    #endregion
}

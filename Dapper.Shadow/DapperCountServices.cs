using Dapper.Shadow.GroupBy;
using Dapper.Shadow.Join;
using Dapper.Shadow.Queries;
using ShadowSql.AliasTables;
using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Select;
using ShadowSql.SingleSelect;
using ShadowSql.Tables;
using ShadowSql.Variants;

namespace Dapper.Shadow;

/// <summary>
/// Dapper计数扩展方法
/// </summary>
public static partial class DapperShadowServices
{
    #region IDapperSelect
    /// <summary>
    /// 计数
    /// </summary>
    /// <param name="select"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static int Count(this IDapperSelect select, object? param = null)
        => select.Executor.Count<int>(select.Source, param);
    #endregion
    #region IDapperTable
    #region TTable
    /// <summary>
    /// 计数
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <returns></returns>
    public static int Count<TTable>(this TTable table)
        where TTable : IDapperTable
        => table.Executor.Count<int>(table);
    /// <summary>
    /// 计数
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <param name="where"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static int Count<TTable>(this TTable table, ISqlLogic where, object? param = null)
        where TTable : IDapperTable
        => table.Executor.Count<int>(new TableFilter<TTable>(table, where), param);
    /// <summary>
    /// 计数
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="filter"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static int Count<TTable>(this TableFilter<TTable> filter, object? param = null)
        where TTable : IDapperTable
        => filter.Source.Executor.Count<int>(filter, param);
    /// <summary>
    /// 计数
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static int Count<TTable>(this TableSqlQuery<TTable> query, object? param = null)
        where TTable : IDapperTable
        => query.Source.Executor.Count<int>(query, param);
    /// <summary>
    /// 计数
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static int Count<TTable>(this TableQuery<TTable> query, object? param = null)
        where TTable : IDapperTable
        => query.Source.Executor.Count<int>(query, param);
    #endregion
    #region AliasTable
    /// <summary>
    /// 别名表计数
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static int Count<TTable>(this TableAlias<TTable> table, object? param = null)
        where TTable : IDapperTable
        => table.Target.Executor.Count<int>(table, param);
    /// <summary>
    /// 别名表过滤计数
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <param name="filter"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static int Count<TTable>(this TableAlias<TTable> table, ISqlLogic filter, object? param = null)
        where TTable : IDapperTable
        => table.Target.Executor.Count<int>(new TableFilter<TableAlias<TTable>>(table, filter), param);
    /// <summary>
    /// 别名表计数
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static int Count<TTable>(this AliasTableSqlQuery<TTable> query, object? param = null)
        where TTable : IDapperTable
        => query.Table.Executor.Count<int>(query, param);
    /// <summary>
    /// 别名表计数
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static int Count<TTable>(this AliasTableQuery<TTable> query, object? param = null)
        where TTable : IDapperTable
        => query.Table.Executor.Count<int>(query, param);
    #endregion
    #region GroupByTable
    /// <summary>
    /// GroupBy后计数
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static int Count<TTable>(this GroupByTableSqlQuery<TTable> query, object? param = null)
        where TTable : IDapperTable
        => query.Source.Executor.Count<int>(query, param);
    /// <summary>
    /// GroupBy后计数
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static int Count<TTable>(this GroupByTableQuery<TTable> query, object? param = null)
        where TTable : IDapperTable
        => query.Source.Executor.Count<int>(query, param);
    #endregion
    #region GroupByAliasTable
    /// <summary>
    /// GroupBy别名表后计数
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static int Count<TTable>(this GroupByAliasTableSqlQuery<TTable> query, object? param = null)
        where TTable : IDapperTable
        => query.Source.Target.Executor.Count<int>(query, param);
    /// <summary>
    /// GroupBy别名表后计数
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static int Count<TTable>(this GroupByAliasTableQuery<TTable> query, object? param = null)
        where TTable : IDapperTable
        => query.Source.Target.Executor.Count<int>(query, param);
    #endregion
    #endregion
    #region Dapper
    #region TTable
    /// <summary>
    /// 计数
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static int Count<TTable>(this DapperTableSqlQuery<TTable> query, object? param = null)
        where TTable : ITable
        => query.Executor.Count<int>(query, param);
    /// <summary>
    /// 计数
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static int Count<TTable>(this DapperTableQuery<TTable> query, object? param = null)
        where TTable : ITable
        => query.Executor.Count<int>(query, param);
    #endregion
    #region AliasTable
    /// <summary>
    /// 别名表计数
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static int Count<TTable>(this DapperAliasTableSqlQuery<TTable> query, object? param = null)
        where TTable : ITable
        => query.Executor.Count<int>(query, param);
    /// <summary>
    /// 别名表计数
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static int Count<TTable>(this DapperAliasTableQuery<TTable> query, object? param = null)
        where TTable : ITable
        => query.Executor.Count<int>(query, param);
    #endregion
    #region MultiTable
    /// <summary>
    /// 多表计数
    /// </summary>
    /// <param name="query"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static int Count<TTable>(this DapperMultiTableSqlQuery query, object? param = null)
        => query.Executor.Count<int>(query, param);
    /// <summary>
    /// 联表计数
    /// </summary>
    /// <param name="query"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static int Count<TTable>(this DapperJoinTableSqlQuery query, object? param = null)
        => query.Executor.Count<int>(query, param);
    /// <summary>
    /// 联表计数
    /// </summary>
    /// <param name="query"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static int Count<TTable>(this DapperJoinTableQuery query, object? param = null)
        => query.Executor.Count<int>(query, param);
    #endregion
    #region GroupByTable
    /// <summary>
    /// GroupBy后计数
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static int Count<TTable>(this DapperGroupByTableSqlQuery<TTable> query, object? param = null)
        where TTable : ITable
        => query.Executor.Count<int>(query, param);
    /// <summary>
    /// GroupBy后计数
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static int Count<TTable>(this DapperGroupByTableQuery<TTable> query, object? param = null)
        where TTable : ITable
        => query.Executor.Count<int>(query, param);
    #endregion
    #region GroupByAliasTable
    /// <summary>
    /// GroupBy别名表后计数
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static int Count<TTable>(this DapperGroupByAliasTableSqlQuery<TTable> query, object? param = null)
        where TTable : ITable
        => query.Executor.Count<int>(query, param);
    /// <summary>
    /// GroupBy别名表后计数
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static int Count<TTable>(this DapperGroupByAliasTableQuery<TTable> query, object? param = null)
        where TTable : ITable
        => query.Executor.Count<int>(query, param);
    #endregion
    #region GroupByMulti
    /// <summary>
    /// GroupBy后计数
    /// </summary>
    /// <param name="query"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static int Count(this DapperGroupByMultiSqlQuery query, object? param = null)
        => query.Executor.Count<int>(query, param);
    /// <summary>
    /// GroupBy后计数
    /// </summary>
    /// <param name="query"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static int Count(this DapperGroupByMultiQuery query, object? param = null)
        => query.Executor.Count<int>(query, param);
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
    /// <param name="param"></param>
    /// <returns></returns>
    public static int Count<TTable>(this TTable table, IExecutor executor, object? param = null)
        where TTable : ITable
        => executor.Count<int>(table, param);
    /// <summary>
    /// 表过滤筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <param name="filter"></param>
    /// <param name="executor"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static int Count<TTable>(this TTable table, ISqlLogic filter, IExecutor executor, object? param = null)
        where TTable : ITable
        => executor.Count<int>(new TableFilter<TTable>(table, filter), param);
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <param name="executor"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static int Count<TTable>(this TableSqlQuery<TTable> query, IExecutor executor, object? param = null)
        where TTable : ITable
        => executor.Count<int>(query, param);
    /// <summary>
    /// 表筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <param name="executor"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static int Count<TTable>(this TableQuery<TTable> query, IExecutor executor, object? param = null)
        where TTable : ITable
        => executor.Count<int>(query, param);
    #endregion
    #region AliasTable
    /// <summary>
    /// 别名表筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <param name="executor"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static int Count<TTable>(this TableAlias<TTable> table, IExecutor executor, object? param = null)
        where TTable : ITable
        => executor.Count<int>(table, param);
    /// <summary>
    /// 别名表过滤筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table"></param>
    /// <param name="filter"></param>
    /// <param name="executor"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static int Count<TTable>(this TableAlias<TTable> table, ISqlLogic filter, IExecutor executor, object? param = null)
        where TTable : ITable
       => executor.Count<int>(new TableFilter<TableAlias<TTable>>(table, filter), param);
    /// <summary>
    /// 别名表筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <param name="executor"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static int Count<TTable>(this AliasTableSqlQuery<TTable> query, IExecutor executor, object? param = null)
        where TTable : ITable
        => executor.Count<int>(query, param);
    /// <summary>
    /// 别名表筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <param name="executor"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static int Count<TTable>(this AliasTableQuery<TTable> query, IExecutor executor, object? param = null)
        where TTable : ITable
        => executor.Count<int>(query, param);
    #endregion
    #region MultiTable
    /// <summary>
    /// 多表筛选列
    /// </summary>
    /// <param name="multiTable"></param>
    /// <param name="executor"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static int Count(this IMultiView multiTable, IExecutor executor, object? param = null)
        => executor.Count<int>(multiTable, param);
    #endregion
    #region GroupByTable
    /// <summary>
    /// GroupBy后再筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <param name="executor"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static int Count<TTable>(this GroupByTableSqlQuery<TTable> query, IExecutor executor, object? param = null)
        where TTable : ITable
        => executor.Count<int>(query, param);
    /// <summary>
    /// GroupBy后再筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <param name="executor"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static int Count<TTable>(this GroupByTableQuery<TTable> query, IExecutor executor, object? param = null)
        where TTable : ITable
        => executor.Count<int>(query, param);
    #endregion
    #region GroupByAliasTable
    /// <summary>
    /// GroupBy别名表后再筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <param name="executor"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static int Count<TTable>(this GroupByAliasTableSqlQuery<TTable> query, IExecutor executor, object? param = null)
        where TTable : ITable
        => executor.Count<int>(query, param);
    /// <summary>
    /// GroupBy别名表后再筛选列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <param name="executor"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static int Count<TTable>(this GroupByAliasTableQuery<TTable> query, IExecutor executor, object? param = null)
        where TTable : ITable
        => executor.Count<int>(query, param);
    #endregion
    #region GroupByMulti
    /// <summary>
    /// GroupBy后再筛选列
    /// </summary>
    /// <param name="query"></param>
    /// <param name="executor"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static int Count(this GroupByMultiSqlQuery query, IExecutor executor, object? param = null)
        => executor.Count<int>(query, param);
    /// <summary>
    /// GroupBy后再筛选列
    /// </summary>
    /// <param name="query"></param>
    /// <param name="executor"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static int Count(this GroupByMultiQuery query, IExecutor executor, object? param = null)
        => executor.Count<int>(query, param);
    #endregion
    #region ITableView
    /// <summary>
    /// 计数
    /// </summary>
    /// <param name="view"></param>
    /// <param name="executor"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static int Count(this ITableView view, IExecutor executor, object? param = null)
        => executor.Count<int>(view, param);
    #endregion
    #region CountSelect
    /// <summary>
    /// 计数
    /// </summary>
    /// <param name="select"></param>
    /// <param name="executor"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static int Count(this CountSelect select, IExecutor executor, object? param = null)
        => executor.ExecuteScalar<int>(select, param);
    #endregion
    #region ISelect
    /// <summary>
    /// 计数
    /// </summary>
    /// <param name="select"></param>
    /// <param name="executor"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static int Count(this ISelect select, IExecutor executor, object? param = null)
        => executor.Count<int>(select.Source, param);
    #endregion
    #endregion
}

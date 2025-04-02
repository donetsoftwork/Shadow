using Dapper.Shadow.Insert;
using Dapper.Shadow.Select;
using ShadowSql;
using ShadowSql.AliasTables;
using ShadowSql.Engines;
using ShadowSql.Filters;
using ShadowSql.Identifiers;
using ShadowSql.Insert;
using ShadowSql.Select;
using ShadowSql.Tables;
using System.Data;

namespace Dapper.Shadow;

/// <summary>
/// 调用执行器
/// </summary>
public static partial class DapperShadowServices
{
    /// <summary>
    /// 构造执行器
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="connection"></param>
    /// <param name="buffered"></param>
    /// <param name="capacity"></param>
    /// <returns></returns>
    public static DapperExecutor Use(this ISqlEngine engine, IDbConnection connection, bool buffered = true, int capacity = 32)
        => new(engine, connection, buffered, capacity);
    #region Queries
    /// <summary>
    /// 封装执行器
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <param name="executor"></param>
    /// <returns></returns>
    public static DapperFilter<TTable> Use<TTable>(this TableSqlQuery<TTable> query, IExecutor executor)
        where TTable : ITable
        => new(executor, query.Source, ((IDataFilter)query).Filter);
    /// <summary>
    /// 封装执行器
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <param name="executor"></param>
    /// <returns></returns>
    public static DapperFilter<IAliasTable> Use<TTable>(this AliasTableSqlQuery<TTable> query, IExecutor executor)
        where TTable : ITable
        => new(executor, query.Source, ((IDataFilter)query).Filter);
    #endregion
    #region Select
    /// <summary>
    /// 封装执行器
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="select"></param>
    /// <param name="executor"></param>
    /// <returns></returns>
    public static DapperAliasTableSelect<TTable> Use<TTable>(this AliasTableSelect<TTable> select, IExecutor executor)
        where TTable : ITable
        => new(executor, select.Source, select.Fields);
    /// <summary>
    /// 封装执行器
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="select"></param>
    /// <param name="executor"></param>
    /// <returns></returns>
    public static DapperGroupByAliasTableSelect<TTable> Use<TTable>(this GroupByAliasTableSelect<TTable> select, IExecutor executor)
        where TTable : ITable
        => new(executor, select.Source, select.Fields);
    /// <summary>
    /// 封装执行器
    /// </summary>
    /// <param name="select"></param>
    /// <param name="executor"></param>
    /// <returns></returns>
    public static DapperGroupByMultiSelect Use(this GroupByMultiSelect select, IExecutor executor)
        => new(executor, select.Source, select.Fields);
    /// <summary>
    /// 封装执行器
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="select"></param>
    /// <param name="executor"></param>
    /// <returns></returns>
    public static DapperGroupByTableSelect<TTable> Use<TTable>(this GroupByTableSelect<TTable> select, IExecutor executor)
        where TTable : ITable
        => new(executor, select.Source, select.Fields);
    /// <summary>
    /// 封装执行器
    /// </summary>
    /// <param name="select"></param>
    /// <param name="executor"></param>
    /// <returns></returns>
    public static DapperMultiTableSelect Use(this MultiTableSelect select, IExecutor executor)
        => new(executor, select.Source, select.Fields);
    /// <summary>
    /// 封装执行器
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="select"></param>
    /// <param name="executor"></param>
    /// <returns></returns>
    public static DapperTableSelect<TTable> Use<TTable>(this TableSelect<TTable> select, IExecutor executor)
        where TTable : ITable
        => new(executor, select.Source, select.Fields);
    #endregion
    #region Insert
    /// <summary>
    /// 封装执行器
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="insert"></param>
    /// <param name="executor"></param>
    /// <returns></returns>
    public static DapperSingleInsert<TTable> Use<TTable>(this SingleInsert<TTable> insert, IExecutor executor)
        where TTable : ITable
    {
        var result = new DapperSingleInsert<TTable>(executor, insert.Table);
        foreach (var item in insert.Items)
            result.Insert(item);
        return result;
    }
    /// <summary>
    /// 封装执行器
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="insert"></param>
    /// <param name="executor"></param>
    /// <returns></returns>
    public static DapperMultiInsert<TTable> Use<TTable>(this MultiInsert<TTable> insert, IExecutor executor)
        where TTable : ITable
    {
        var result = new DapperMultiInsert<TTable>(executor, insert.Table);
        foreach (var item in insert.Items)
            result.Insert(item);
        return result;
    }
    /// <summary>
    /// 封装执行器
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="insert"></param>
    /// <param name="executor"></param>
    /// <returns></returns>
    public static DapperSelectInsert<TTable> Use<TTable>(this SelectInsert<TTable> insert, IExecutor executor)
        where TTable : ITable
        => new(executor, insert.Table, insert.Select);
    #endregion

}

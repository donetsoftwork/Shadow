using ShadowSql.Expressions.VisitSource;
using ShadowSql.Join;
using ShadowSql.Queries;
using System;
using System.Linq.Expressions;

namespace ShadowSql.Expressions;

/// <summary>
/// 多联表扩展方法
/// </summary>
public static partial class ShadowSqlServices
{
    #region IDataSqlQuery
    #region Where
    /// <summary>
    /// 指定表查询
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="multiTable">多表(联表)</param>
    /// <param name="table">表</param>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public static MultiTableSqlQuery Where<TEntity>(this MultiTableSqlQuery multiTable, string table, Expression<Func<TEntity, bool>> query)
    {
        TableVisitor.Where(multiTable.From(table), multiTable._filter._complex, query);
        return multiTable;
    }
    /// <summary>
    /// 指定表查询
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TParameter"></typeparam>
    /// <param name="multiTable">多表(联表)</param>
    /// <param name="table">表</param>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public static MultiTableSqlQuery Where<TEntity, TParameter>(this MultiTableSqlQuery multiTable, string table, Expression<Func<TEntity, TParameter, bool>> query)
    {
        TableVisitor.Where(multiTable.From(table), multiTable._filter._complex, query);
        return multiTable;
    }
    /// <summary>
    /// 直接查询(不建议对重名列查询)
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="multiTable">多表(联表)</param>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public static MultiTableSqlQuery Where<TEntity>(this MultiTableSqlQuery multiTable, Expression<Func<TEntity, bool>> query)
    {
        TableVisitor.Where(multiTable._filter._complex, query, multiTable);
        return multiTable;
    }
    /// <summary>
    /// 直接查询(不建议对重名列查询)
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TParameter"></typeparam>
    /// <param name="multiTable">多表(联表)</param>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public static MultiTableSqlQuery Where<TEntity, TParameter>(this MultiTableSqlQuery multiTable, Expression<Func<TEntity, TParameter, bool>> query)
    {
        TableVisitor.Where(multiTable._filter._complex, query, multiTable);
        return multiTable;
    }
    #endregion
    #endregion
    #region IDataQuery
    #region And
    /// <summary>
    /// 指定表查询
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="multiTable">多表(联表)</param>
    /// <param name="table">表</param>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public static JoinTableQuery And<TEntity>(this JoinTableQuery multiTable, string table, Expression<Func<TEntity, bool>> query)
        => And<JoinTableQuery, TEntity>(multiTable, table, query);
    /// <summary>
    /// 指定表查询
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="multiTable">多表(联表)</param>
    /// <param name="table">表</param>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public static MultiTableQuery And<TEntity>(this MultiTableQuery multiTable, string table, Expression<Func<TEntity, bool>> query)
        => And<MultiTableQuery, TEntity>(multiTable, table, query);
    /// <summary>
    /// 直接查询(不建议对重名列查询)
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="multiTable">多表(联表)</param>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public static JoinTableQuery And<TEntity>(this JoinTableQuery multiTable, Expression<Func<TEntity, bool>> query)
        => And<JoinTableQuery, TEntity>(multiTable, query);
    /// <summary>
    /// 直接查询(不建议对重名列查询)
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="multiTable">多表(联表)</param>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public static MultiTableQuery And<TEntity>(this MultiTableQuery multiTable, Expression<Func<TEntity, bool>> query)
        => And<MultiTableQuery, TEntity>(multiTable, query);
    /// <summary>
    /// 指定表查询
    /// </summary>
    /// <typeparam name="TMultiTable"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="multiTable">多表(联表)</param>
    /// <param name="table">表</param>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public static TMultiTable And<TMultiTable, TEntity>(this TMultiTable multiTable, string table, Expression<Func<TEntity, bool>> query)
         where TMultiTable : MultiTableBase, IDataQuery
    {
        var visitor = TableVisitor.Where(multiTable.From(table), multiTable.Logic.ToAnd(), query);
        multiTable.Logic = visitor.Logic;
        return multiTable;
    }
    /// <summary>
    /// 直接查询(不建议对重名列查询)
    /// </summary>
    /// <typeparam name="TMultiTable"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="multiTable">多表(联表)</param>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public static TMultiTable And<TMultiTable, TEntity>(this TMultiTable multiTable, Expression<Func<TEntity, bool>> query)
         where TMultiTable : MultiTableBase, IDataQuery
    {
        var visitor = TableVisitor.Where(multiTable.Logic.ToAnd(), query, multiTable);
        multiTable.Logic = visitor.Logic;
        return multiTable;
    }
    #endregion
    #region Or
    /// <summary>
    /// 指定表查询
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="multiTable">多表(联表)</param>
    /// <param name="table">表</param>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public static JoinTableQuery Or<TEntity>(this JoinTableQuery multiTable, string table, Expression<Func<TEntity, bool>> query)
        => Or<JoinTableQuery, TEntity>(multiTable, table, query);
    /// <summary>
    /// 指定表查询
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="multiTable">多表(联表)</param>
    /// <param name="table">表</param>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public static MultiTableQuery Or<TEntity>(this MultiTableQuery multiTable, string table, Expression<Func<TEntity, bool>> query)
        => Or<MultiTableQuery, TEntity>(multiTable, table, query);
    /// <summary>
    /// 直接查询(不建议对重名列查询)
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="multiTable">多表(联表)</param>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public static MultiTableQuery Or<TEntity>(this MultiTableQuery multiTable, Expression<Func<TEntity, bool>> query)
        => Or<MultiTableQuery, TEntity>(multiTable, query);
    /// <summary>
    /// 指定表查询
    /// </summary>
    /// <typeparam name="TMultiTable"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="multiTable">多表(联表)</param>
    /// <param name="table">表</param>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public static TMultiTable Or<TMultiTable, TEntity>(this TMultiTable multiTable, string table, Expression<Func<TEntity, bool>> query)
         where TMultiTable : MultiTableBase, IDataQuery
    {
        var visitor = TableVisitor.Where(multiTable.From(table), multiTable.Logic.ToOr(), query);
        multiTable.Logic = visitor.Logic;
        return multiTable;
    }
    /// <summary>
    /// 直接查询(不建议对重名列查询)
    /// </summary>
    /// <typeparam name="TMultiTable"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="multiTable">多表(联表)</param>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public static TMultiTable Or<TMultiTable, TEntity>(this TMultiTable multiTable, Expression<Func<TEntity, bool>> query)
         where TMultiTable : MultiTableBase, IDataQuery
    {
        var visitor = TableVisitor.Where(multiTable.Logic.ToOr(), query, multiTable);
        multiTable.Logic = visitor.Logic;
        return multiTable;
    }
    #endregion
    #endregion
}

using ShadowSql.Expressions.AliasTables;
using ShadowSql.Expressions.GroupBy;
using ShadowSql.Expressions.Join;
using ShadowSql.Expressions.Tables;
using ShadowSql.Expressions.VisitSource;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Tables;
using System;
using System.Linq.Expressions;

namespace ShadowSql.Expressions;

/// <summary>
/// 分组扩展方法
/// </summary>
public static partial class ShadowSqlServices
{
    #region GroupBy
    #region GroupByTableQuery
    #region table
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="table"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public static GroupByTableQuery<TKey, TEntity> GroupBy<TKey, TEntity>(this ITable table, Expression<Func<TEntity, TKey>> select)
        => new(table, EmptyLogic.Instance, [.. TableVisitor.GetFieldsByExpression(table, select)]);
    #endregion
    #region table & where
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="table"></param>
    /// <param name="where"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public static GroupByTableQuery<TKey, TEntity> GroupBy<TKey, TEntity>(this ITable table, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TKey>> select)
        => new(table, TableVisitor.Where(table, new AndLogic(), where).Logic, [.. TableVisitor.GetFieldsByExpression(table, select)]);
    #endregion
    #region TableQuery
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="query"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public static GroupByTableQuery<TKey, TEntity> GroupBy<TKey, TEntity>(this TableQuery<TEntity> query, Expression<Func<TEntity, TKey>> select)
        => new(query.Source, query._filter, [.. TableVisitor.GetFieldsByExpression(query.Source, select)]);
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="query"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public static GroupByTableQuery<TKey, TEntity> GroupBy<TKey, TEntity>(this TableQuery query, Expression<Func<TEntity, TKey>> select)
    {
        var table = (ITable)query.Source;
        return new(table, query._filter, [.. TableVisitor.GetFieldsByExpression(table, select)]);
    }
    #endregion
    #region AliasTable
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="table"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public static GroupByTableQuery<TKey, TEntity> GroupBy<TKey, TEntity>(this IAliasTable table, Expression<Func<TEntity, TKey>> select)
        => new(table, EmptyLogic.Instance, [.. TableVisitor.GetFieldsByExpression(table, select)]);
    #endregion
    #region AliasTable & where
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="table"></param>
    /// <param name="where"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public static GroupByTableQuery<TKey, TEntity> GroupBy<TKey, TEntity>(this IAliasTable table, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TKey>> select)
        => new(table, TableVisitor.Where(table, new AndLogic(), where).Logic, [.. TableVisitor.GetFieldsByExpression(table, select)]);
    #endregion    
    #region AliasTableQuery
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="query"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public static GroupByTableQuery<TKey, TEntity> GroupBy<TKey, TEntity>(this AliasTableQuery<TEntity> query, Expression<Func<TEntity, TKey>> select)
        => new(query.Source, query._filter, [.. TableVisitor.GetFieldsByExpression(query.Source, select)]);
    #endregion
    #endregion
    #region GroupByMultiQuery
    //#region MultiTableQuery
    ///// <summary>
    ///// 分组查询
    ///// </summary>
    ///// <param name="multiTable"></param>
    ///// <param name="fields"></param>
    ///// <returns></returns>
    //public static GroupByMultiQuery<Key> GroupBy<Key>(this MultiTableQuery multiTable, params IField[] fields)
    //    => new(multiTable, fields);
    //#endregion
    //#region JoinTableQuery
    ///// <summary>
    ///// 分组查询
    ///// </summary>
    ///// <param name="multiTable"></param>
    ///// <param name="fields"></param>
    ///// <returns></returns>
    //public static GroupByMultiQuery<Key> GroupBy<Key>(this JoinTableQuery multiTable, params IField[] fields)
    //    => new(multiTable, fields);
    //#endregion
    #region JoinOnQuery<LTable, RTable>
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <typeparam name="TLeft"></typeparam>
    /// <typeparam name="TRight"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="joinOn"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public static GroupByMultiQuery<TKey> GroupBy<TLeft, TRight, TKey>(this JoinOnQuery<TLeft, TRight> joinOn, Expression<Func<TLeft, TRight, TKey>> select)
        => new(joinOn.Root, [.. JoinOnVisitor.GetFieldsByExpression(joinOn, select)]);
    #endregion
    #endregion
    #endregion
    #region SqlGroupBy
    #region GroupByTableSqlQuery
    #region TTable
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="table"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public static GroupByTableSqlQuery<TKey, TEntity> SqlGroupBy<TKey, TEntity>(this ITable table, Expression<Func<TEntity, TKey>> select)
        => new(table, EmptyLogic.Instance, [.. TableVisitor.GetFieldsByExpression(table, select)]);
    #endregion
    #region TTable & where
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="table"></param>
    /// <param name="where"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public static GroupByTableSqlQuery<TKey, TEntity> SqlGroupBy<TKey, TEntity>(this ITable table, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TKey>> select)
        => new(table, TableVisitor.Where(table, new AndLogic(), where).Logic, [.. TableVisitor.GetFieldsByExpression(table, select)]);
    #endregion
    #region TableSqlQuery
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="SqlQuery"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public static GroupByTableSqlQuery<TKey, TEntity> SqlGroupBy<TKey, TEntity>(this TableSqlQuery<TEntity> SqlQuery, Expression<Func<TEntity, TKey>> select)
        => new(SqlQuery.Source, SqlQuery._filter, [.. TableVisitor.GetFieldsByExpression(SqlQuery.Source, select)]);
    #endregion
    #region TableSqlQuery
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="SqlQuery"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public static GroupByTableSqlQuery<TKey, TEntity> SqlGroupBy<TKey, TEntity>(this TableSqlQuery SqlQuery, Expression<Func<TEntity, TKey>> select)
    {
        var table = (ITable)SqlQuery.Source;
        return new(table, SqlQuery._filter, [.. TableVisitor.GetFieldsByExpression(table, select)]);
    }
    #endregion
    #region AliasTable
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="table"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public static GroupByTableQuery<TKey, TEntity> SqlGroupBy<TKey, TEntity>(this IAliasTable table, Expression<Func<TEntity, TKey>> select)
        => new(table, EmptyLogic.Instance, [.. TableVisitor.GetFieldsByExpression(table, select)]);
    #endregion
    #region AliasTable & where
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="table"></param>
    /// <param name="where"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public static GroupByTableQuery<TKey, TEntity> SqlGroupBy<TKey, TEntity>(this IAliasTable table, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TKey>> select)
        => new(table, TableVisitor.Where(table, new AndLogic(), where).Logic, [.. TableVisitor.GetFieldsByExpression(table, select)]);
    #endregion    
    #region AliasTableSqlQuery
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <param name="query"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public static GroupByTableQuery<TKey, TEntity> SqlGroupBy<TKey, TEntity>(this AliasTableSqlQuery<TEntity> query, Expression<Func<TEntity, TKey>> select)
        => new(query.Source, query._filter, [.. TableVisitor.GetFieldsByExpression(query.Source, select)]);
    #endregion
    #endregion
    #region GroupByMultiQuery
    //#region MultiTableQuery
    ///// <summary>
    ///// 分组查询
    ///// </summary>
    ///// <param name="multiTable"></param>
    ///// <param name="fields"></param>
    ///// <returns></returns>
    //public static GroupByMultiQuery<Key> SqlGroupBy<Key>(this MultiTableQuery multiTable, params IField[] fields)
    //    => new(multiTable, fields);
    //#endregion
    //#region JoinTableQuery
    ///// <summary>
    ///// 分组查询
    ///// </summary>
    ///// <param name="multiTable"></param>
    ///// <param name="fields"></param>
    ///// <returns></returns>
    //public static GroupByMultiQuery<Key> SqlGroupBy<Key>(this JoinTableQuery multiTable, params IField[] fields)
    //    => new(multiTable, fields);
    //#endregion
    #region JoinOnSqlQuery<LTable, RTable>
    /// <summary>
    /// 分组查询
    /// </summary>
    /// <typeparam name="TLeft"></typeparam>
    /// <typeparam name="TRight"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="joinOn"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public static GroupByMultiQuery<TKey> SqlGroupBy<TLeft, TRight, TKey>(this JoinOnSqlQuery<TLeft, TRight> joinOn, Expression<Func<TLeft, TRight, TKey>> select)
        => new(joinOn.Root, [.. JoinOnVisitor.GetFieldsByExpression(joinOn, select)]);
    #endregion
    #endregion
    #endregion
}

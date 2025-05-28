using ShadowSql.Expressions.Filters;
using ShadowSql.Expressions.VisitSource;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Queries;
using ShadowSql.Tables;
using System;
using System.Linq.Expressions;

namespace ShadowSql.Expressions.Tables;

/// <summary>
/// 逻辑查询表
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public class TableQuery<TEntity>
    : DataFilterBase<ITable, Logic>, IDataQuery
{
    /// <summary>
    /// 逻辑查询表
    /// </summary>
    /// <param name="table"></param>
    /// <param name="filter"></param>
    public TableQuery(ITable table, Logic filter)
        : base(table, filter)
    {
    }
    /// <summary>
    /// 逻辑查询表
    /// </summary>
    /// <param name="table"></param>
    public TableQuery(ITable table)
        : base(table, new AndLogic())
    {
    }
    /// <summary>
    /// 逻辑查询表
    /// </summary>
    /// <param name="tableName"></param>
    public TableQuery(string tableName)
        : base(EmptyTable.Use(tableName), new AndLogic())
    {
    }
    /// <summary>
    /// 逻辑查询表
    /// </summary>
    public TableQuery()
        : base(EmptyTable.Use(typeof(TEntity).Name), new AndLogic())
    {
    }
    #region TEntity
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public TableQuery<TEntity> And(Expression<Func<TEntity, bool>> query)
    {
        var visitor = TableVisitor.Where(_source, _filter.ToAnd(), query);
        _filter = visitor.Logic;
        return this;
    }
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public TableQuery<TEntity> Or(Expression<Func<TEntity, bool>> query)
    {
        var visitor = TableVisitor.Where(_source, _filter.ToOr(), query);
        _filter = visitor.Logic;
        return this;
    }
    #endregion
    #region TParameter
    /// <summary>
    /// 与逻辑
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public TableQuery<TEntity> And<TParameter>(Expression<Func<TEntity, TParameter, bool>> query)
    {
        var visitor = TableVisitor.Where(_source, _filter.ToAnd(), query);
        _filter = visitor.Logic;
        return this;
    }
    /// <summary>
    /// 或逻辑
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public TableQuery<TEntity> Or<TParameter>(Expression<Func<TEntity, TParameter, bool>> query)
    {
        var visitor = TableVisitor.Where(_source, _filter.ToOr(), query);
        _filter = visitor.Logic;
        return this;
    }
    #endregion
    #region IDataQuery
    Logic IDataQuery.Logic
    {
        get => _filter;
        set => _filter = value;
    }
    #endregion
}

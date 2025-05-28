using ShadowSql.Expressions.Filters;
using ShadowSql.Expressions.VisitSource;
using ShadowSql.Filters;
using ShadowSql.Identifiers;
using ShadowSql.Queries;
using System;
using System.Linq.Expressions;

namespace ShadowSql.Expressions.AliasTables;

/// <summary>
/// sql查询别名表
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <param name="table"></param>
/// <param name="query"></param>
public class AliasTableSqlQuery<TEntity>(IAliasTable table, SqlQuery query)
    : DataFilterBase<IAliasTable, SqlQuery>(table, query), IDataSqlQuery, IWhere
{
    #region 扩展查询功能
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public AliasTableSqlQuery<TEntity> Where(Expression<Func<TEntity, bool>> query)
    {
        TableVisitor.Where(_source, _filter._complex, query);
        return this;
    }
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public AliasTableSqlQuery<TEntity> Where<TParameter>(Expression<Func<TEntity, TParameter, bool>> query)
    {
        TableVisitor.Where(_source, _filter._complex, query);
        return this;
    }
    #endregion
    #region IDataQuery
    SqlQuery IDataSqlQuery.Query
    {
        get => _filter;
        set => _filter = value;
    }
    #endregion
}
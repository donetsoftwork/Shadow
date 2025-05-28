using ShadowSql.Expressions.Filters;
using ShadowSql.Expressions.VisitSource;
using ShadowSql.Filters;
using ShadowSql.Identifiers;
using ShadowSql.Queries;
using ShadowSql.Tables;
using System;
using System.Linq.Expressions;

namespace ShadowSql.Expressions.Tables;

/// <summary>
/// sql查询表
/// </summary>
public class TableSqlQuery<TEntity>
    : DataFilterBase<ITable, SqlQuery>, IDataSqlQuery, IWhere
{
    #region Table
    /// <summary>
    /// sql查询表
    /// </summary>
    /// <param name="table"></param>
    /// <param name="query"></param>
    public TableSqlQuery(ITable table, SqlQuery query)
        : base(table, query)
    { 
    }
    /// <summary>
    /// sql查询表
    /// </summary>
    /// <param name="table"></param>
    public TableSqlQuery(ITable table)
        : base(table, SqlQuery.CreateAndQuery())
    {
    }
    /// <summary>
    /// sql查询表
    /// </summary>
    /// <param name="tableName"></param>
    public TableSqlQuery(string tableName)
        : base(EmptyTable.Use(tableName), SqlQuery.CreateAndQuery())
    {
    }
    /// <summary>
    /// sql查询表
    /// </summary>
    public TableSqlQuery()
        : base(EmptyTable.Use(typeof(TEntity).Name), SqlQuery.CreateAndQuery())
    {
    }
    #endregion
    #region 扩展查询功能
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public TableSqlQuery<TEntity> Where(Expression<Func<TEntity, bool>> query)
    {
        TableVisitor.Where(_source, _filter._complex, query);
        return this;
    }
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public TableSqlQuery<TEntity> Where<TParameter>(Expression<Func<TEntity, TParameter, bool>> query)
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
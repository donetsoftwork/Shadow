using ShadowSql.Engines;
using ShadowSql.Expressions.VisitSource;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Queries;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ShadowSql.Expressions.GroupBy;

/// <summary>
/// 分组查询
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TEntity"></typeparam>
public class GroupByTableSqlQuery<TKey, TEntity>
    : GroupBySqlQueryBase<ITableView>
{
    internal GroupByTableSqlQuery(ITableView table, ISqlLogic where, IField[] fields, SqlQuery having)
        : base(table, fields, having)
    {
        _where = where;
    }
    /// <summary>
    /// 对表进行分组查询
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="where">查询条件</param>
    /// <param name="fields">字段</param>
    public GroupByTableSqlQuery(ITable table, ISqlLogic where, IField[] fields)
        :this(table, where, fields, SqlQuery.CreateAndQuery())
    {
    }
    /// <summary>
    /// 对别名表进行分组查询
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="where">查询条件</param>
    /// <param name="fields">字段</param>
    public GroupByTableSqlQuery(IAliasTable table, ISqlLogic where, IField[] fields)
        : this(table, where, fields, SqlQuery.CreateAndQuery())
    {
    }
    #region 配置
    private readonly ISqlLogic _where;
    /// <summary>
    /// where查询条件
    /// </summary>
    public ISqlLogic Where
        => _where;
    #endregion
    #region 查询扩展
    /// <summary>
    /// 聚合查询
    /// </summary>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public GroupByTableSqlQuery<TKey, TEntity> Having(Expression<Func<IGrouping<TKey, TEntity>, bool>> query)
    {
        GroupByVisitor.Having(this, _filter._complex, query);
        return this;
    }
    /// <summary>
    /// 聚合查询
    /// </summary>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public GroupByTableSqlQuery<TKey, TEntity> Having<TParameter>(Expression<Func<IGrouping<TKey, TEntity>, TParameter, bool>> query)
    {
        GroupByVisitor.Having(this, _filter._complex, query);
        return this;
    }
    #endregion
    #region ISqlEntity
    /// <summary>
    /// 分组数据源拼写(+WHERE)
    /// </summary>
    /// <param name="engine">数据库引擎</param>
    /// <param name="sql">sql</param>
    /// <returns></returns>
    protected override void WriteGroupBySource(ISqlEngine engine, StringBuilder sql)
    {
        _source.Write(engine, sql);
        var point = sql.Length;
        //可选的WHERE
        engine.WherePrefix(sql);
        if (!_where.TryWrite(engine, sql))
        {
            //回滚
            sql.Length = point;
        }
    }
    #endregion
}

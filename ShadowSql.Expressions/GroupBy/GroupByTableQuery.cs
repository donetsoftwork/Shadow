using ShadowSql.Engines;
using ShadowSql.Expressions.VisitSource;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
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
public class GroupByTableQuery<TKey, TEntity>
    : GroupByQueryBase<ITableView>
{
    internal GroupByTableQuery(ITableView table, ISqlLogic where, IField[] fields, Logic having)
        : base(table, fields, having)
    {
        _where = where;
    }
    /// <summary>
    /// 对表进行分组查询
    /// </summary>
    /// <param name="table"></param>
    /// <param name="where"></param>
    /// <param name="fields"></param>
    public GroupByTableQuery(ITable table, ISqlLogic where, IField[] fields)
        : this(table, where, fields, new AndLogic())
    {
    }
    /// <summary>
    /// 对别名表进行分组查询
    /// </summary>
    /// <param name="table"></param>
    /// <param name="where"></param>
    /// <param name="fields"></param>
    public GroupByTableQuery(IAliasTable table, ISqlLogic where, IField[] fields)
        : this(table, where, fields, new AndLogic())
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
    /// 按逻辑查询
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public GroupByTableQuery<TKey, TEntity> And(Expression<Func<IGrouping<TKey, TEntity>, bool>> query)
    {
        var visitor = GroupByVisitor.Having(this, _filter.ToAnd(), query);
        _filter = visitor.Logic;
        return this;
    }
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <typeparam name="TParameter">参数类型</typeparam>
    /// <param name="query"></param>
    /// <returns></returns>
    public GroupByTableQuery<TKey, TEntity> And<TParameter>(Expression<Func<IGrouping<TKey, TEntity>, TParameter, bool>> query)
    {
        var visitor = GroupByVisitor.Having(this, _filter.ToAnd(), query);
        _filter = visitor.Logic;
        return this;
    }
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public GroupByTableQuery<TKey, TEntity> Or(Expression<Func<IGrouping<TKey, TEntity>, bool>> query)
    {
        var visitor = GroupByVisitor.Having(this, _filter.ToOr(), query);
        _filter = visitor.Logic;
        return this;
    }
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <typeparam name="TParameter">参数类型</typeparam>
    /// <param name="query"></param>
    /// <returns></returns>
    public GroupByTableQuery<TKey, TEntity> Or<TParameter>(Expression<Func<IGrouping<TKey, TEntity>, TParameter, bool>> query)
    {
        var visitor = GroupByVisitor.Having(this, _filter.ToOr(), query);
        _filter = visitor.Logic;
        return this;
    }
    #endregion
    #region ISqlEntity
    /// <summary>
    /// 数据源拼写(+WHERE)
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
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

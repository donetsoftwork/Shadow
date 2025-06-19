using ShadowSql.Aggregates;
using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Queries;
using System;
using System.Text;

namespace ShadowSql.GroupBy;

/// <summary>
/// 对别名表分组
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="aliasTable">别名表</param>
/// <param name="where">查询条件</param>
/// <param name="fields">字段</param>
/// <param name="having">分组查询条件</param>
public class GroupByAliasTableSqlQuery<TTable>(IAliasTable<TTable> aliasTable, ISqlLogic where, IField[] fields, SqlQuery having)
    : GroupBySqlQueryBase<IAliasTable<TTable>>(aliasTable, fields, having)
    where TTable : ITable
{
    /// <summary>
    /// 对TableQuery进行分组查询
    /// </summary>
    /// <param name="aliasTable">别名表</param>
    /// <param name="where">查询条件</param>
    /// <param name="fields">字段</param>
    public GroupByAliasTableSqlQuery(IAliasTable<TTable> aliasTable, ISqlLogic where, IField[] fields)
        : this(aliasTable, where, fields, SqlQuery.CreateAndQuery())
    {
    }
    #region 配置
    //private readonly TTable _table = source.Target;
    ///// <summary>
    ///// 原始表
    ///// </summary>
    //public TTable Table
    //    => _table;
    private readonly ISqlLogic _where = where;
    /// <summary>
    /// where查询条件
    /// </summary>
    public ISqlLogic Where
        => _where;
    #endregion
    #region HavingAggregate
    /// <summary>
    /// 按聚合逻辑查询
    /// </summary>
    /// <param name="select">筛选</param>
    /// <param name="aggregate">聚合</param>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public GroupByAliasTableSqlQuery<TTable> HavingAggregate(Func<TTable, IColumn> select, Func<IPrefixField, IAggregateField> aggregate, Func<IAggregateField, AtomicLogic> query)
    {
        _filter.AddLogic(query(aggregate(_source.Prefix(select(_source.Target)))));
        return this;
    }
    #endregion
    #region ISqlEntity
    /// <summary>
    /// 数据源拼写(+WHERE)
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

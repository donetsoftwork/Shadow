﻿using ShadowSql.Aggregates;
using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Variants;
using System;
using System.Text;

namespace ShadowSql.GroupBy;

/// <summary>
/// 对别名表分组
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="source"></param>
/// <param name="where"></param>
/// <param name="fields"></param>
/// <param name="having"></param>
public class GroupByAliasTableQuery<TTable>(TableAlias<TTable> source, ISqlLogic where, IFieldView[] fields, Logic having)
    : GroupByQueryBase<TableAlias<TTable>>(source, fields, having)
    where TTable : ITable
{
    /// <summary>
    /// 对TableQuery进行分组查询
    /// </summary>
    /// <param name="table"></param>
    /// <param name="where"></param>
    /// <param name="fields"></param>
    public GroupByAliasTableQuery(TableAlias<TTable> table, ISqlLogic where, IFieldView[] fields)
        : this(table, where, fields, new AndLogic())
    {
    }
    #region 配置
    private readonly ISqlLogic _where = where;
    /// <summary>
    /// where查询条件
    /// </summary>
    public ISqlLogic Where
        => _where;
    #endregion
    /// <summary>
    /// 按聚合逻辑查询
    /// </summary>
    /// <param name="select"></param>
    /// <param name="aggregate"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public GroupByAliasTableQuery<TTable> And(Func<TTable, IColumn> select, Func<IColumn, IAggregateField> aggregate, Func<IAggregateField, AtomicLogic> query)
    {
        _filter = _filter.And(query(_source.Aggregate(select, aggregate)));
        return this;
    }
    /// <summary>
    /// 按聚合逻辑查询
    /// </summary>
    /// <param name="select"></param>
    /// <param name="aggregate"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public GroupByAliasTableQuery<TTable> Or(Func<TTable, IColumn> select, Func<IColumn, IAggregateField> aggregate, Func<IAggregateField, AtomicLogic> query)
    {
        _filter = _filter.Or(query(_source.Aggregate(select, aggregate)));
        return this;
    }
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

using ShadowSql.Aggregates;
using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using System;
using System.Text;

namespace ShadowSql.GroupBy;

/// <summary>
/// 对Table进行分组查询
/// </summary>
/// <param name="table"></param>
/// <param name="where"></param>
/// <param name="fields"></param>
/// <param name="having"></param>
public class GroupByTableQuery<TTable>(TTable table, ISqlLogic where, IFieldView[] fields, Logic having)
    : GroupByQueryBase<TTable>(table, fields, having)
    where TTable : ITable
{
    /// <summary>
    /// 对TableQuery进行分组查询
    /// </summary>
    /// <param name="table"></param>
    /// <param name="where"></param>
    /// <param name="fields"></param>
    public GroupByTableQuery(TTable table, ISqlLogic where, IFieldView[] fields)
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
    #region 查询扩展
    /// <summary>
    /// 逻辑与
    /// </summary>
    /// <param name="aggregate"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public GroupByTableQuery<TTable> And(Func<TTable, IAggregateField> aggregate, Func<IAggregateField, AtomicLogic> query)
    {
        _filter = _filter.And(query(aggregate(_source)));
        return this;
    }
    /// <summary>
    /// 逻辑或
    /// </summary>
    /// <param name="aggregate"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public GroupByTableQuery<TTable> Or(Func<TTable, IAggregateField> aggregate, Func<IAggregateField, AtomicLogic> query)
    {
        _filter = _filter.Or(query(aggregate(_source)));
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

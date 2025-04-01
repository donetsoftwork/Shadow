using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShadowSql.GroupBy;

/// <summary>
/// 分组sql查询基类
/// </summary>
/// <typeparam name="TSource"></typeparam>
/// <param name="source"></param>
/// <param name="fields"></param>
/// <param name="having"></param>
public abstract class GroupBySqlQueryBase<TSource>(TSource source, IFieldView[] fields, SqlQuery having)
    : GroupByQueryBase(fields, having)
    where TSource : ITableView
{
    #region 配置
    /// <summary>
    /// 数据源表
    /// </summary>
    protected readonly TSource _source = source;
    /// <summary>
    /// 数据源表
    /// </summary>
    public TSource Source
        => _source;
    #endregion
    #region ISqlEntity
    /// <summary>
    /// 输出数据源
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    protected override void WriteGroupBySource(ISqlEngine engine, StringBuilder sql)
        => _source.Write(engine, sql);
    #endregion
}
/// <summary>
/// 分组sql查询基类
/// </summary>
/// <param name="fields"></param>
/// <param name="having"></param>
public abstract class GroupByQueryBase(IFieldView[] fields, SqlQuery having)
    : GroupByBase<SqlQuery>(fields, having)
{
    #region FilterBase
    /// <summary>
    /// 增加sql条件
    /// </summary>
    /// <param name="conditions"></param>
    internal void AddConditions(params IEnumerable<string> conditions)
        => _having.AddConditions(conditions);
    /// <summary>
    /// 增加逻辑
    /// </summary>
    /// <param name="condition"></param>
    internal override void AddLogic(AtomicLogic condition)
        => _having.AddLogic(condition);
    /// <summary>
    /// And查询
    /// </summary>
    internal override void ToAndCore()
        => _having = _having.ToAnd();
    /// <summary>
    /// Or查询
    /// </summary>
    internal override void ToOrCore()
        => _having = _having.ToOr();
    #endregion
}

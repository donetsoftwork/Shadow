using ShadowSql.Filters;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using System;

namespace ShadowSql.Queries;

/// <summary>
/// 数据逻辑过滤
/// </summary>
public abstract class DataQuery<TSource>(TSource source, Logic filter) 
    : DataFilterBase<TSource, Logic>(source, filter)
    , IDataQuery
    where TSource : ITableView
{
    #region FilterBase
    /// <summary>
    /// 添加查询
    /// </summary>
    /// <param name="condition"></param>
    internal override void AddLogic(AtomicLogic condition)
    {
        _filter.AddLogic(condition);
    }
    /// <summary>
    /// 切换为And
    /// </summary>
    internal override void ToAndCore()
    {
        _filter = _filter.ToAnd();
    }
    /// <summary>
    /// 切换为Or
    /// </summary>
    internal override void ToOrCore()
    {
        _filter = _filter.ToOr();
    }
    #region Logic
    /// <summary>
    /// 与运算
    /// </summary>
    /// <param name="condition"></param>
    internal override void AndCore(AtomicLogic condition)
        => _filter = _filter.And(condition);
    /// <summary>
    /// 与运算
    /// </summary>
    /// <param name="condition"></param>
    internal override void AndCore(AndLogic condition)
        => _filter = _filter.And(condition);
    /// <summary>
    /// 与运算
    /// </summary>
    /// <param name="condition"></param>
    internal override void AndCore(ComplexAndLogic condition)
        => _filter = _filter.And(condition);
    /// <summary>
    /// 与运算
    /// </summary>
    /// <param name="condition"></param>
    internal override void AndCore(OrLogic condition)
        => _filter = _filter.And(condition);
    /// <summary>
    /// 与运算
    /// </summary>
    /// <param name="condition"></param>
    internal override void AndCore(ComplexOrLogic condition)
        => _filter = _filter.And(condition);
    /// <summary>
    /// 与运算
    /// </summary>
    /// <param name="condition"></param>
    internal override void AndCore(Logic condition)
        => _filter = _filter.And(condition);
    /// <summary>
    /// 或运算
    /// </summary>
    /// <param name="condition"></param>
    internal override void OrCore(AtomicLogic condition)
        => _filter = _filter.Or(condition);
    /// <summary>
    /// 或运算
    /// </summary>
    /// <param name="condition"></param>
    internal override void OrCore(AndLogic condition)
        => _filter = _filter.Or(condition);
    /// <summary>
    /// 或运算
    /// </summary>
    /// <param name="condition"></param>
    internal override void OrCore(ComplexAndLogic condition)
        => _filter = _filter.Or(condition);
    /// <summary>
    /// 或运算
    /// </summary>
    /// <param name="condition"></param>
    internal override void OrCore(OrLogic condition)
        => _filter = _filter.Or(condition);
    /// <summary>
    /// 或运算
    /// </summary>
    /// <param name="condition"></param>
    internal override void OrCore(ComplexOrLogic condition)
        => _filter = _filter.Or(condition);
    /// <summary>
    /// 或运算
    /// </summary>
    /// <param name="condition"></param>
    internal override void OrCore(Logic condition)
        => _filter = _filter.Or(condition);
    #endregion
    #endregion
    #region IDataLogic
    void IDataQuery.ApplyFilter(Func<Logic, Logic> filter)
        => ApplyFilter(filter);
    #endregion
}

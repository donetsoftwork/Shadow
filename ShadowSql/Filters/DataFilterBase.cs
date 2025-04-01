using ShadowSql.Logics;
using System;

namespace ShadowSql.Filters;

/// <summary>
/// 逻辑过滤基类
/// </summary>
public abstract class DataFilterBase(Logic filter) 
    : FilterBase
{
    /// <summary>
    /// 逻辑过滤器
    /// </summary>
    protected Logic _filter = filter;
    #region 过滤逻辑
    /// <summary>
    /// 添加查询
    /// </summary>
    /// <param name="condition"></param>
    internal override void AddLogic(AtomicLogic condition)
    {
        _filter.AddLogic(condition);
    }
    ///// <summary>
    ///// 切换为And
    ///// </summary>
    //internal void ToAndCore()
    //{
    //    _filter = _filter.ToAnd();
    //}
    ///// <summary>
    ///// 切换为Or
    ///// </summary>
    //internal void ToOrCore()
    //{
    //    _filter = _filter.ToOr();
    //}
    /// <summary>
    /// 应用过滤
    /// </summary>
    /// <param name="filter"></param>
    internal void ApplyFilter(Func<Logic, Logic> filter)
        => _filter = filter(_filter);
    #endregion
}

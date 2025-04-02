using System;
using ShadowSql.Identifiers;
using ShadowSql.Logics;

namespace ShadowSql.Filters;

/// <summary>
/// 逻辑过滤基类
/// </summary>
public abstract class SqlLogicFilterBase<TSource>(TSource source, ISqlLogic filter)
    : DataFilterBase<TSource, ISqlLogic>(source, filter)
    where TSource : ITableView
{
    //#region 过滤逻辑
    ///// <summary>
    ///// 添加查询
    ///// </summary>
    ///// <param name="condition"></param>
    //internal override void AddLogic(AtomicLogic condition)
    //    => throw new InvalidOperationException("不支持修改");
    ///// <summary>
    ///// 切换为And
    ///// </summary>
    //internal override void ToAndCore()
    //    => throw new InvalidOperationException("不支持修改");
    ///// <summary>
    ///// 切换为Or
    ///// </summary>
    //internal override void ToOrCore()
    //    => throw new InvalidOperationException("不支持修改");
    //#endregion
}

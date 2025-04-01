using ShadowSql.Generators;
using ShadowSql.Logics;
using ShadowSql.Queries;
using System;

namespace ShadowSql.Join;

/// <summary>
/// 多表查询
/// </summary>
public class MultiTableQuery(IIdentifierGenerator aliasGenerator, Logic query)
    : MultiTableBase<Logic>(aliasGenerator, query), IDataQuery
{
    /// <summary>
    /// 多表视图
    /// </summary>
    public MultiTableQuery()
        : this(new IdIncrementGenerator("t"), new AndLogic())
    {
    }
    #region FilterBase
    /// <summary>
    /// 增加逻辑
    /// </summary>
    /// <param name="condition"></param>
    internal override void AddLogic(AtomicLogic condition)
        => _filter.AddLogic(condition);
    /// <summary>
    /// And查询
    /// </summary>
    internal override void ToAndCore()
        => _filter = _filter.ToAnd();
    /// <summary>
    /// Or查询
    /// </summary>
    internal override void ToOrCore()
        => _filter = _filter.ToOr();
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
    #region IDataQuery
    void IDataQuery.ApplyFilter(Func<Logic, Logic> query)
        => ApplyFilter(query);
    #endregion
}

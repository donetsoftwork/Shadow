using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Queries;
using ShadowSql.Variants;
using System;

namespace ShadowSql.Join;

/// <summary>
/// 联表俩俩关联查询
/// </summary>
/// <remarks>
/// 联表俩俩关联查询
/// </remarks>
public class JoinOnQuery<LTable, RTable>(JoinTableQuery root, TableAlias<LTable> left, TableAlias<RTable> right, Logic onQuery)
    : JoinOnBase<LTable, RTable, Logic>(root, left, right, onQuery), IDataQuery
    where LTable : ITable
    where RTable : ITable
{
    /// <summary>
    /// 联表俩俩关联查询
    /// </summary>
    /// <param name="root"></param>
    /// <param name="left"></param>
    /// <param name="right"></param>
    public JoinOnQuery(JoinTableQuery root, TableAlias<LTable> left, TableAlias<RTable> right)
        : this(root, left, right, new AndLogic())
    {
    }
    #region 配置
    private readonly JoinTableQuery _root = root;
    /// <summary>
    /// 联表
    /// </summary>
    public new JoinTableQuery Root
        => _root;
    #endregion
    #region FilterBase
    /// <summary>
    /// 增加逻辑
    /// </summary>
    /// <param name="condition"></param>
    internal override void AddLogic(AtomicLogic condition)
        => _onQuery.AddLogic(condition);
    /// <summary>
    /// And查询
    /// </summary>
    internal override void ToAndCore()
        => _onQuery = _onQuery.ToAnd();
    /// <summary>
    /// Or查询
    /// </summary>
    internal override void ToOrCore()
        => _onQuery = _onQuery.ToOr();
    #region Logic
    /// <summary>
    /// 与运算
    /// </summary>
    /// <param name="condition"></param>
    internal override void AndCore(AtomicLogic condition)
        => _onQuery = _onQuery.And(condition);
    /// <summary>
    /// 与运算
    /// </summary>
    /// <param name="condition"></param>
    internal override void AndCore(AndLogic condition)
        => _onQuery = _onQuery.And(condition);
    /// <summary>
    /// 与运算
    /// </summary>
    /// <param name="condition"></param>
    internal override void AndCore(ComplexAndLogic condition)
        => _onQuery = _onQuery.And(condition);
    /// <summary>
    /// 与运算
    /// </summary>
    /// <param name="condition"></param>
    internal override void AndCore(OrLogic condition)
        => _onQuery = _onQuery.And(condition);
    /// <summary>
    /// 与运算
    /// </summary>
    /// <param name="condition"></param>
    internal override void AndCore(ComplexOrLogic condition)
        => _onQuery = _onQuery.And(condition);
    /// <summary>
    /// 与运算
    /// </summary>
    /// <param name="condition"></param>
    internal override void AndCore(Logic condition)
        => _onQuery = _onQuery.And(condition);
    /// <summary>
    /// 或运算
    /// </summary>
    /// <param name="condition"></param>
    internal override void OrCore(AtomicLogic condition)
        => _onQuery = _onQuery.Or(condition);
    /// <summary>
    /// 或运算
    /// </summary>
    /// <param name="condition"></param>
    internal override void OrCore(AndLogic condition)
        => _onQuery = _onQuery.Or(condition);
    /// <summary>
    /// 或运算
    /// </summary>
    /// <param name="condition"></param>
    internal override void OrCore(ComplexAndLogic condition)
        => _onQuery = _onQuery.Or(condition);
    /// <summary>
    /// 或运算
    /// </summary>
    /// <param name="condition"></param>
    internal override void OrCore(OrLogic condition)
        => _onQuery = _onQuery.Or(condition);
    /// <summary>
    /// 或运算
    /// </summary>
    /// <param name="condition"></param>
    internal override void OrCore(ComplexOrLogic condition)
        => _onQuery = _onQuery.Or(condition);
    /// <summary>
    /// 或运算
    /// </summary>
    /// <param name="condition"></param>
    internal override void OrCore(Logic condition)
        => _onQuery = _onQuery.Or(condition);
    #endregion
    #endregion
    #region IDataQuery
    void IDataQuery.ApplyFilter(Func<Logic, Logic> query)
        => ApplyFilter(query);
    #endregion
}

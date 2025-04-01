using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Queries;
using ShadowSql.Variants;
using System;
using System.Collections.Generic;

namespace ShadowSql.Join;

/// <summary>
/// 联表俩俩关联查询
/// </summary>
/// <remarks>
/// 联表俩俩关联查询
/// </remarks>
public class JoinOnSqlQuery<LTable, RTable>(JoinTableSqlQuery root, TableAlias<LTable> left, TableAlias<RTable> right, SqlQuery onQuery)
    : JoinOnBase<LTable, RTable, SqlQuery>(root, left, right, onQuery), IDataSqlQuery
    where LTable : ITable
    where RTable : ITable
{
    /// <summary>
    /// 联表俩俩关联查询
    /// </summary>
    /// <param name="root"></param>
    /// <param name="left"></param>
    /// <param name="right"></param>
    public JoinOnSqlQuery(JoinTableSqlQuery root, TableAlias<LTable> left, TableAlias<RTable> right)
        : this(root, left, right, SqlQuery.CreateAndQuery())
    { 
    }
    #region 配置
    private readonly JoinTableSqlQuery _root = root;
    /// <summary>
    /// 联表
    /// </summary>
    public new JoinTableSqlQuery Root
        => _root;
    #endregion
    #region FilterBase
    /// <summary>
    /// 增加sql条件
    /// </summary>
    /// <param name="conditions"></param>
    internal void AddConditions(params IEnumerable<string> conditions)
        => _onQuery.AddConditions(conditions);
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
    #endregion
    #region IDataQuery
    void IDataSqlQuery.AddConditions(IEnumerable<string> conditions)
    => _onQuery.AddConditions(conditions);
    void IDataSqlQuery.ApplyFilter(Func<SqlQuery, SqlQuery> query)
        => ApplyFilter(query);
    #endregion
}

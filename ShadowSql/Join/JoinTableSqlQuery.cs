using ShadowSql.Generators;
using ShadowSql.Logics;
using ShadowSql.Queries;
using System;
using System.Collections.Generic;

namespace ShadowSql.Join;

/// <summary>
/// 联表查询
/// </summary>
/// <param name="aliasGenerator"></param>
/// <param name="filter"></param>
public class JoinTableSqlQuery(IIdentifierGenerator aliasGenerator, SqlQuery filter)
    : JoinTableBase<SqlQuery>(aliasGenerator, filter), IDataSqlQuery
{
    /// <summary>
    /// 联表查询
    /// </summary>
    public JoinTableSqlQuery()
        : this(new IdIncrementGenerator("t"), SqlQuery.CreateAndQuery())
    {
    }
    #region FilterBase
    /// <summary>
    /// 增加sql条件
    /// </summary>
    /// <param name="conditions"></param>
    internal void AddConditions(params IEnumerable<string> conditions)
        => _filter.AddConditions(conditions);
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
    #endregion
    #region IDataQuery
    void IDataSqlQuery.AddConditions(IEnumerable<string> conditions)
    => _filter.AddConditions(conditions);
    void IDataSqlQuery.ApplyFilter(Func<SqlQuery, SqlQuery> query)
        => ApplyFilter(query);
    #endregion
}

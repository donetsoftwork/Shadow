using ShadowSql.Aggregates;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Queries;
using System;

namespace ShadowSql.GroupBy;

/// <summary>
/// 对MultiQuery进行分组查询
/// </summary>
/// <param name="multiTable"></param>
/// <param name="fields"></param>
/// <param name="filter"></param>
public class GroupByMultiSqlQuery(IMultiView multiTable, IFieldView[] fields, SqlQuery filter)
    : GroupBySqlQueryBase<IMultiView>(multiTable, fields, filter)
{
    /// <summary>
    /// 对多表进行分组查询
    /// </summary>
    /// <param name="multiTable"></param>
    /// <param name="fields"></param>
    public GroupByMultiSqlQuery(IMultiView multiTable, IFieldView[] fields)
        : this(multiTable, fields, SqlQuery.CreateAndQuery())
    {
    }
}

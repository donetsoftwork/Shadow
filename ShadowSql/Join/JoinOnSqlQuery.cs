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
    #region IDataQuery
    SqlQuery IDataSqlQuery.Query
    {
        get => _filter;
        set => _filter = value;
    }
    #endregion
}

using ShadowSql.Filters;
using ShadowSql.Identifiers;
using ShadowSql.Queries;
using System;

namespace ShadowSql.Join;

/// <summary>
/// 多表查询
/// </summary>
public class MultiTableSqlQuery(SqlQuery query)
    : MultiTableBase<SqlQuery>(query), IDataSqlQuery, IWhere
{
    /// <summary>
    /// 多表查询
    /// </summary>
    public MultiTableSqlQuery()
        : this(SqlQuery.CreateAndQuery())
    {
    }
    /// <summary>
    /// 指定表查询
    /// </summary>
    /// <typeparam name="TAliasTable"></typeparam>
    /// <param name="tableName"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public MultiTableSqlQuery Apply<TAliasTable>(string tableName, Func<SqlQuery, TAliasTable, SqlQuery> query)
        where TAliasTable : IAliasTable
    {
        _filter = query(_filter, this.From<TAliasTable>(tableName));
        return this;
    }
    #region IDataQuery
    SqlQuery IDataSqlQuery.Query
    {
        get => _filter;
        set => _filter = value;
    }
    #endregion
}

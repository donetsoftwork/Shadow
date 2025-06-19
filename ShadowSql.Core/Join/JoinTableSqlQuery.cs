using ShadowSql.Filters;
using ShadowSql.Generators;
using ShadowSql.Queries;

namespace ShadowSql.Join;

/// <summary>
/// 联表查询
/// </summary>
/// <param name="filter">过滤条件</param>
public class JoinTableSqlQuery(SqlQuery filter)
    : JoinTableBase<SqlQuery>(filter), IDataSqlQuery, IWhere
{
    /// <summary>
    /// 联表查询
    /// </summary>
    public JoinTableSqlQuery()
        : this(SqlQuery.CreateAndQuery())
    {
    }
    #region IDataQuery
    SqlQuery IDataSqlQuery.Query
    {
        get => _filter;
        set => _filter = value;
    }
    #endregion
}

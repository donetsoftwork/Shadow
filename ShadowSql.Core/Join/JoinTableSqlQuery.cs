using ShadowSql.Generators;
using ShadowSql.Queries;

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
    #region IDataQuery
    SqlQuery IDataSqlQuery.Query
    {
        get => _filter;
        set => _filter = value;
    }
    #endregion
}

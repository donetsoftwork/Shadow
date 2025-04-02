using ShadowSql.Generators;
using ShadowSql.Queries;

namespace ShadowSql.Join;

/// <summary>
/// 多表查询
/// </summary>
public class MultiTableSqlQuery(IIdentifierGenerator aliasGenerator, SqlQuery query)
    : MultiTableBase<SqlQuery>(aliasGenerator, query), IDataSqlQuery
{
    /// <summary>
    /// 多表视图
    /// </summary>
    public MultiTableSqlQuery()
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

using ShadowSql.Generators;
using ShadowSql.Logics;
using ShadowSql.Queries;

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
    #region IDataQuery
    Logic IDataQuery.Logic
    {
        get => _filter;
        set => _filter = value;
    }
    #endregion
}

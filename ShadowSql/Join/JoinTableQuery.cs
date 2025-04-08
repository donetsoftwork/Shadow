using ShadowSql.Generators;
using ShadowSql.Logics;
using ShadowSql.Queries;

namespace ShadowSql.Join;

/// <summary>
/// 联表查询
/// </summary>
/// <param name="aliasGenerator"></param>
/// <param name="filter"></param>
public class JoinTableQuery(IIdentifierGenerator aliasGenerator, Logic filter)
    : JoinTableBase<Logic>(aliasGenerator, filter), IDataQuery
{
    /// <summary>
    /// 联表查询
    /// </summary>
    public JoinTableQuery()
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

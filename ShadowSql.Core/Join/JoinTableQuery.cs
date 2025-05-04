using ShadowSql.Logics;
using ShadowSql.Queries;

namespace ShadowSql.Join;

/// <summary>
/// 联表查询
/// </summary>
/// <param name="filter"></param>
public class JoinTableQuery(Logic filter)
    : JoinTableBase<Logic>(filter), IDataQuery
{
    /// <summary>
    /// 联表查询
    /// </summary>
    public JoinTableQuery()
        : this(new AndLogic())
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

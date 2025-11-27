using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Queries;
using System;

namespace ShadowSql.Join;

/// <summary>
/// 多表查询
/// </summary>
public class MultiTableQuery(Logic logic)
    : MultiTableBase<Logic>(logic), IDataQuery
{
    /// <summary>
    /// 多表查询
    /// </summary>
    public MultiTableQuery()
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

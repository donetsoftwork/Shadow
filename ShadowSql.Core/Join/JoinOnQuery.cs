using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Queries;

namespace ShadowSql.Join;

/// <summary>
/// 联表俩俩关联查询
/// </summary>
/// <param name="root"></param>
/// <param name="left"></param>
/// <param name="right"></param>
/// <param name="onQuery"></param>
public class JoinOnQuery(JoinTableQuery root, IAliasTable left, IAliasTable right, Logic onQuery)
    : JoinOnCoreBase<Logic>(root, left, right, onQuery), IDataQuery
{
    /// <summary>
    /// 联表俩俩关联查询
    /// </summary>
    /// <param name="root"></param>
    /// <param name="left"></param>
    /// <param name="right"></param>
    public JoinOnQuery(JoinTableQuery root, IAliasTable left, IAliasTable right)
        : this(root, left, right, new AndLogic())
    {
    }
    #region 配置
    private readonly JoinTableQuery _root = root;
    /// <summary>
    /// 联表
    /// </summary>
    public new JoinTableQuery Root
        => _root;
    #endregion
    #region IDataQuery
    Logic IDataQuery.Logic
    {
        get => _filter;
        set => _filter = value;
    }
    #endregion
}

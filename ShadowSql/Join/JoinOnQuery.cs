using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Queries;
using ShadowSql.Variants;

namespace ShadowSql.Join;

/// <summary>
/// 联表俩俩关联查询
/// </summary>
/// <typeparam name="LTable"></typeparam>
/// <typeparam name="RTable"></typeparam>
/// <param name="root"></param>
/// <param name="left"></param>
/// <param name="right"></param>
/// <param name="onQuery"></param>
public class JoinOnQuery<LTable, RTable>(JoinTableQuery root, TableAlias<LTable> left, TableAlias<RTable> right, Logic onQuery)
    : JoinOnBase<LTable, RTable, Logic>(root, left, right, onQuery), IDataQuery
    where LTable : ITable
    where RTable : ITable
{
    /// <summary>
    /// 联表俩俩关联查询
    /// </summary>
    /// <param name="root"></param>
    /// <param name="left"></param>
    /// <param name="right"></param>
    public JoinOnQuery(JoinTableQuery root, TableAlias<LTable> left, TableAlias<RTable> right)
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

using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Queries;
using ShadowSql.Simples;

namespace ShadowSql.Join;

/// <summary>
/// 联表俩俩关联查询
/// </summary>
/// <param name="root"></param>
/// <param name="left"></param>
/// <param name="right"></param>
/// <param name="onQuery"></param>
public class JoinOnQuery(JoinTableQuery root, IAliasTable left, IAliasTable right, Logic onQuery)
    : JoinOnCoreBase<JoinTableQuery, Logic>(root, left, right, onQuery), IDataQuery
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
    #region Create
    /// <summary>
    /// 联表查询
    /// </summary>
    /// <param name="t1"></param>
    /// <param name="t2"></param>
    /// <returns></returns>
    public static JoinOnQuery Create(string t1, string t2)
        => Create(SimpleDB.From(t1), SimpleDB.From(t2));
    /// <summary>
    /// 联表查询
    /// </summary>
    /// <param name="t1"></param>
    /// <param name="t2"></param>
    /// <returns></returns>
    public static JoinOnQuery Create(ITable t1, ITable t2)
    {
        var joinTable = new JoinTableQuery();
        var a1 = joinTable.CreateMember(t1);
        var a2 = joinTable.CreateMember(t2);
        var joinOn = new JoinOnQuery(joinTable, a1, a2);
        joinTable.AddJoinOn(joinOn);
        return joinOn;
    }
    /// <summary>
    /// 联表查询
    /// </summary>
    /// <param name="t1"></param>
    /// <param name="t2"></param>
    /// <returns></returns>
    public static JoinOnQuery Create(IAliasTable t1, IAliasTable t2)
    {
        var joinTable = new JoinTableQuery();
        joinTable.AddMemberCore(t1);
        joinTable.AddMemberCore(t2);
        var joinOn = new JoinOnQuery(joinTable, t1, t2);
        joinTable.AddJoinOn(joinOn);
        return joinOn;
    }
    #endregion
    #region IDataQuery
    Logic IDataQuery.Logic
    {
        get => _filter;
        set => _filter = value;
    }
    #endregion
}

using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Queries;
using ShadowSql.Tables;

namespace ShadowSql.Join;

/// <summary>
/// 联表俩俩关联查询
/// </summary>
/// <param name="joinTable">联表</param>
/// <param name="left">左</param>
/// <param name="right">右</param>
/// <param name="onQuery">联表逻辑</param>
public class JoinOnQuery(JoinTableQuery joinTable, IAliasTable left, IAliasTable right, Logic onQuery)
    : JoinOnCoreBase<JoinTableQuery, Logic>(joinTable, left, right, onQuery), IDataQuery
{
    /// <summary>
    /// 联表俩俩关联查询
    /// </summary>
    /// <param name="joinTable">联表</param>
    /// <param name="left">左</param>
    /// <param name="right">右</param>
    public JoinOnQuery(JoinTableQuery joinTable, IAliasTable left, IAliasTable right)
        : this(joinTable, left, right, new AndLogic())
    {
    }
    #region Create
    /// <summary>
    /// 联表查询
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static JoinOnQuery Create(string left, string right)
        => Create(EmptyTable.Use(left), EmptyTable.Use(right));
    /// <summary>
    /// 联表查询
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static JoinOnQuery Create<TTable>(TTable left, TTable right)
        where TTable : ITable
    {
        var joinTable = new JoinTableQuery();
        var a1 = joinTable.CreateMember(left);
        var a2 = joinTable.CreateMember(right);
        var joinOn = new JoinOnQuery(joinTable, a1, a2);
        joinTable.AddJoinOn(joinOn);
        return joinOn;
    }
    /// <summary>
    /// 联表查询
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static JoinOnQuery Create(IAliasTable left, IAliasTable right)
    {
        var joinTable = new JoinTableQuery();
        joinTable.AddMemberCore(left);
        joinTable.AddMemberCore(right);
        var joinOn = new JoinOnQuery(joinTable, left, right);
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

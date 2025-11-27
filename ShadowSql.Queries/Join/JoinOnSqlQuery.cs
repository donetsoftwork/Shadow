using ShadowSql.Identifiers;
using ShadowSql.Queries;
using ShadowSql.Tables;

namespace ShadowSql.Join;

/// <summary>
/// 联表俩俩关联查询
/// </summary>
/// <param name="joinTable">联表</param>
/// <param name="left">左</param>
/// <param name="right">右</param>
/// <param name="onQuery">联表查询</param>
public class JoinOnSqlQuery(JoinTableSqlQuery joinTable, IAliasTable left, IAliasTable right, SqlQuery onQuery)
    : JoinOnCoreBase<JoinTableSqlQuery, SqlQuery>(joinTable, left, right, onQuery), IDataSqlQuery
{
    /// <summary>
    /// 联表俩俩关联查询
    /// </summary>
    /// <param name="joinTable">联表</param>
    /// <param name="left">左</param>
    /// <param name="right">右</param>
    public JoinOnSqlQuery(JoinTableSqlQuery joinTable, IAliasTable left, IAliasTable right)
        : this(joinTable, left, right, SqlQuery.CreateAndQuery())
    { 
    }
    #region Create
    /// <summary>
    /// 联表查询
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static JoinOnSqlQuery Create(string left, string right)
        => Create(EmptyTable.Use(left), EmptyTable.Use(right));
    /// <summary>
    /// 联表查询
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static JoinOnSqlQuery Create<TTable>(TTable left, TTable right)
        where TTable : ITable
    {
        var joinTable = new JoinTableSqlQuery();
        var a1 = joinTable.CreateMember(left);
        var a2 = joinTable.CreateMember(right);
        var joinOn = new JoinOnSqlQuery(joinTable, a1, a2);
        joinTable.AddJoinOn(joinOn);
        return joinOn;
    }
    /// <summary>
    /// 联表查询
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static JoinOnSqlQuery Create(IAliasTable left, IAliasTable right)
    {
        var joinTable = new JoinTableSqlQuery();
        joinTable.AddMemberCore(left);
        joinTable.AddMemberCore(right);
        var joinOn = new JoinOnSqlQuery(joinTable, left, right);
        joinTable.AddJoinOn(joinOn);
        return joinOn;
    }
    #endregion
    #region IDataQuery
    SqlQuery IDataSqlQuery.Query
    {
        get => _filter;
        set => _filter = value;
    }
    #endregion
}

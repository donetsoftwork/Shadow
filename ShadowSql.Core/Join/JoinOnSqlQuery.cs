using ShadowSql.Identifiers;
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
public class JoinOnSqlQuery(JoinTableSqlQuery root, IAliasTable left, IAliasTable right, SqlQuery onQuery)
    : JoinOnCoreBase<JoinTableSqlQuery, SqlQuery>(root, left, right, onQuery), IDataSqlQuery
{
    /// <summary>
    /// 联表俩俩关联查询
    /// </summary>
    /// <param name="root"></param>
    /// <param name="left"></param>
    /// <param name="right"></param>
    public JoinOnSqlQuery(JoinTableSqlQuery root, IAliasTable left, IAliasTable right)
        : this(root, left, right, SqlQuery.CreateAndQuery())
    { 
    }
    #region Create
    /// <summary>
    /// 联表查询
    /// </summary>
    /// <param name="t1"></param>
    /// <param name="t2"></param>
    /// <returns></returns>
    public static JoinOnSqlQuery Create(string t1, string t2)
        => Create(SimpleDB.From(t1), SimpleDB.From(t2));
    /// <summary>
    /// 联表查询
    /// </summary>
    /// <param name="t1"></param>
    /// <param name="t2"></param>
    /// <returns></returns>
    public static JoinOnSqlQuery Create(ITable t1, ITable t2)
    {
        var joinTable = new JoinTableSqlQuery();
        var a1 = joinTable.CreateMember(t1);
        var a2 = joinTable.CreateMember(t2);
        var joinOn = new JoinOnSqlQuery(joinTable, a1, a2);
        joinTable.AddJoinOn(joinOn);
        return joinOn;
    }
    /// <summary>
    /// 联表查询
    /// </summary>
    /// <param name="t1"></param>
    /// <param name="t2"></param>
    /// <returns></returns>
    public static JoinOnSqlQuery Create(IAliasTable t1, IAliasTable t2)
    {
        var joinTable = new JoinTableSqlQuery();
        joinTable.AddMemberCore(t1);
        joinTable.AddMemberCore(t2);
        var joinOn = new JoinOnSqlQuery(joinTable, t1, t2);
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

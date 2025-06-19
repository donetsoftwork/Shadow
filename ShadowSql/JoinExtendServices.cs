using ShadowSql.Identifiers;
using ShadowSql.Join;

namespace ShadowSql;

/// <summary>
/// 联表扩展方法
/// </summary>
public static class ShadowSqlExtendServices
{
    #region IDataQuery
    #region Join
    /// <summary>
    /// 联表(创建新联表)
    /// </summary>
    /// <typeparam name="TLeft"></typeparam>
    /// <typeparam name="TRight"></typeparam>
    /// <param name="left">左</param>
    /// <param name="right">右</param>
    /// <returns></returns>
    public static AliasJoinOnQuery<TLeft, TRight> Join<TLeft, TRight>(this TLeft left, TRight right)
        where TLeft : IAliasTable<ITable>
        where TRight : IAliasTable<ITable>
    {
        JoinTableQuery joinTable = new();
        joinTable.AddMemberCore(left);
        joinTable.AddMemberCore(right);
        AliasJoinOnQuery<TLeft, TRight> joinOn = new(joinTable, left, right);
        joinTable.AddJoinOn(joinOn);
        return joinOn;
    }
    #endregion
    /// <summary>
    /// 用左表联新表
    /// </summary>
    /// <typeparam name="TLeft"></typeparam>
    /// <typeparam name="TRight"></typeparam>
    /// <typeparam name="TAliasTable"></typeparam>
    /// <param name="joinOn">联接</param>
    /// <param name="aliasTable">别名表</param>
    /// <returns></returns>
    public static AliasJoinOnQuery<TLeft, TAliasTable> LeftTableJoin<TLeft, TRight, TAliasTable>(this AliasJoinOnQuery<TLeft, TRight> joinOn, TAliasTable aliasTable)
        where TLeft : IAliasTable<ITable>
        where TRight : IAliasTable<ITable>
        where TAliasTable : IAliasTable<ITable>
    {
        var root = joinOn.Root;
        var joinOnNew = new AliasJoinOnQuery<TLeft, TAliasTable>(root, joinOn.Left, aliasTable);
        root.AddJoinOn(joinOnNew);
        return joinOnNew;
    }
    /// <summary>
    /// 用右表联新表
    /// </summary>
    /// <typeparam name="TLeft"></typeparam>
    /// <typeparam name="TRight"></typeparam>
    /// <typeparam name="TAliasTable"></typeparam>
    /// <param name="joinOn">联接</param>
    /// <param name="aliasTable">别名表</param>
    /// <returns></returns>
    public static AliasJoinOnQuery<TRight, TAliasTable> RightTableJoin<TLeft, TRight, TAliasTable>(this AliasJoinOnQuery<TLeft, TRight> joinOn, TAliasTable aliasTable)
        where TLeft : IAliasTable<ITable>
        where TRight : IAliasTable<ITable>
        where TAliasTable : IAliasTable<ITable>
    {
        var root = joinOn.Root;
        var joinOnNew = new AliasJoinOnQuery<TRight, TAliasTable>(root, joinOn.Source, aliasTable);
        root.AddJoinOn(joinOnNew);
        return joinOnNew;
    }
    #endregion
    #region IDataSqlQuery
    #region SqlJoin
    /// <summary>
    /// 联表(创建新联表)
    /// </summary>
    /// <typeparam name="TLeft"></typeparam>
    /// <typeparam name="TRight"></typeparam>
    /// <param name="left">左</param>
    /// <param name="right">右</param>
    /// <returns></returns>
    public static AliasJoinOnSqlQuery<TLeft, TRight> SqlJoin<TLeft, TRight>(this TLeft left, TRight right)
        where TLeft : IAliasTable<ITable>
        where TRight : IAliasTable<ITable>
    {
        JoinTableSqlQuery joinTable = new();
        joinTable.AddMemberCore(left);
        joinTable.AddMemberCore(right);
        AliasJoinOnSqlQuery<TLeft, TRight> joinOn = new(joinTable, left, right);
        joinTable.AddJoinOn(joinOn);
        return joinOn;
    }
    #endregion
    /// <summary>
    /// 用左表联新表
    /// </summary>
    /// <typeparam name="TLeft"></typeparam>
    /// <typeparam name="TRight"></typeparam>
    /// <typeparam name="TAliasTable"></typeparam>
    /// <param name="joinOn">联接</param>
    /// <param name="aliasTable">别名表</param>
    /// <returns></returns>
    public static AliasJoinOnSqlQuery<TLeft, TAliasTable> LeftTableJoin<TLeft, TRight, TAliasTable>(this AliasJoinOnSqlQuery<TLeft, TRight> joinOn, TAliasTable aliasTable)
        where TLeft : IAliasTable<ITable>
        where TRight : IAliasTable<ITable>
        where TAliasTable : IAliasTable<ITable>
    {
        var root = joinOn.Root;
        var joinOnNew = new AliasJoinOnSqlQuery<TLeft, TAliasTable>(root, joinOn.Left, aliasTable);
        root.AddJoinOn(joinOnNew);
        return joinOnNew;
    }
    /// <summary>
    /// 用右表联新表
    /// </summary>
    /// <typeparam name="TLeft"></typeparam>
    /// <typeparam name="TRight"></typeparam>
    /// <typeparam name="TAliasTable"></typeparam>
    /// <param name="joinOn">联接</param>
    /// <param name="aliasTable">别名表</param>
    /// <returns></returns>
    public static AliasJoinOnSqlQuery<TRight, TAliasTable> RightTableJoin<TLeft, TRight, TAliasTable>(this AliasJoinOnSqlQuery<TLeft, TRight> joinOn, TAliasTable aliasTable)
        where TLeft : IAliasTable<ITable>
        where TRight : IAliasTable<ITable>
        where TAliasTable : IAliasTable<ITable>
    {
        var root = joinOn.Root;
        var joinOnNew = new AliasJoinOnSqlQuery<TRight, TAliasTable>(root, joinOn.Source, aliasTable);
        root.AddJoinOn(joinOnNew);
        return joinOnNew;
    }
    #endregion
}

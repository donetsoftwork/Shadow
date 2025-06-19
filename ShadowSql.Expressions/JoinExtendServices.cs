using ShadowSql.Expressions.Join;
using ShadowSql.Identifiers;
using ShadowSql.Join;

namespace ShadowSql.Expressions;

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
    public static JoinOnQuery<TLeft, TRight> Join<TLeft, TRight>(this IAliasTable left, IAliasTable right)
    {
        JoinTableQuery joinTable = new();
        joinTable.AddMemberCore(left);
        joinTable.AddMemberCore(right);
        JoinOnQuery<TLeft, TRight> joinOn = new(joinTable, left, right);
        joinTable.AddJoinOn(joinOn);
        return joinOn;
    }
    #endregion
    /// <summary>
    /// 用左表联新表
    /// </summary>
    /// <typeparam name="TLeft"></typeparam>
    /// <typeparam name="TRight"></typeparam>
    /// <typeparam name="TOther"></typeparam>
    /// <param name="joinOn">联接</param>
    /// <param name="table">表</param>
    /// <returns></returns>
    public static JoinOnQuery<TLeft, TOther> LeftTableJoin<TLeft, TRight, TOther>(this JoinOnQuery<TLeft, TRight> joinOn, IAliasTable table)
    {
        var root = joinOn.Root;
        var joinOnNew = new JoinOnQuery<TLeft, TOther>(root, joinOn.Left, table);
        root.AddJoinOn(joinOnNew);
        return joinOnNew;
    }
    /// <summary>
    /// 用右表联新表
    /// </summary>
    /// <typeparam name="TLeft"></typeparam>
    /// <typeparam name="TRight"></typeparam>
    /// <typeparam name="TOther"></typeparam>
    /// <param name="joinOn">联接</param>
    /// <param name="table">表</param>
    /// <returns></returns>
    public static JoinOnQuery<TRight, TOther> RightTableJoin<TLeft, TRight, TOther>(this JoinOnQuery<TLeft, TRight> joinOn, IAliasTable table)
    {
        var root = joinOn.Root;
        var joinOnNew = new JoinOnQuery<TRight, TOther>(root, joinOn.Source, table);
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
    public static JoinOnSqlQuery<TLeft, TRight> SqlJoin<TLeft, TRight>(this IAliasTable left, IAliasTable right)
    {
        JoinTableSqlQuery joinTable = new();
        joinTable.AddMemberCore(left);
        joinTable.AddMemberCore(right);
        JoinOnSqlQuery<TLeft, TRight> joinOn = new(joinTable, left, right);
        joinTable.AddJoinOn(joinOn);
        return joinOn;
    }
    #endregion
    /// <summary>
    /// 用左表联新表
    /// </summary>
    /// <typeparam name="TLeft"></typeparam>
    /// <typeparam name="TRight"></typeparam>
    /// <typeparam name="TOther"></typeparam>
    /// <param name="joinOn">联接</param>
    /// <param name="table">表</param>
    /// <returns></returns>
    public static JoinOnSqlQuery<TLeft, TOther> LeftTableJoin<TLeft, TRight, TOther>(this JoinOnSqlQuery<TLeft, TRight> joinOn, IAliasTable table)
    {
        var root = joinOn.Root;
        var joinOnNew = new JoinOnSqlQuery<TLeft, TOther>(root, joinOn.Left, table);
        root.AddJoinOn(joinOnNew);
        return joinOnNew;
    }
    /// <summary>
    /// 用右表联新表
    /// </summary>
    /// <typeparam name="TLeft"></typeparam>
    /// <typeparam name="TRight"></typeparam>
    /// <typeparam name="TOther"></typeparam>
    /// <param name="joinOn">联接</param>
    /// <param name="table">表</param>
    /// <returns></returns>
    public static JoinOnSqlQuery<TRight, TOther> RightTableJoin<TLeft, TRight, TOther>(this JoinOnSqlQuery<TLeft, TRight> joinOn, IAliasTable table)
    {
        var root = joinOn.Root;
        var joinOnNew = new JoinOnSqlQuery<TRight, TOther>(root, joinOn.Source, table);
        root.AddJoinOn(joinOnNew);
        return joinOnNew;
    }
    #endregion
}

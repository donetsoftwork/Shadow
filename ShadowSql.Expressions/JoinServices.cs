using ShadowSql.Expressions.Join;
using ShadowSql.Identifiers;
using ShadowSql.Join;

namespace ShadowSql.Expressions;

/// <summary>
/// 联表扩展方法
/// </summary>
public static partial class ShadowSqlServices
{
    #region IDataQuery
    /// <summary>
    /// 联表(创建新联表)
    /// </summary>
    /// <param name="main"></param>
    /// <param name="table"></param>
    /// <returns></returns>
    public static JoinOnQuery<TLeft, TRight> Join<TLeft, TRight>(this ITable main, ITable table)
    {
        JoinTableQuery joinTable = new();
        var leftTable = joinTable.CreateMember(main);
        var rightTable = joinTable.CreateMember(table);
        JoinOnQuery<TLeft, TRight> joinOn = new(joinTable, leftTable, rightTable);
        joinTable.AddJoinOn(joinOn);
        return joinOn;
    }
    /// <summary>
    /// 用左表联新表
    /// </summary>
    /// <typeparam name="TLeft"></typeparam>
    /// <typeparam name="TRight"></typeparam>
    /// <typeparam name="TOther"></typeparam>
    /// <param name="joinOn"></param>
    /// <param name="table"></param>
    /// <returns></returns>
    public static JoinOnQuery<TLeft, TOther> LeftTableJoin<TLeft, TRight, TOther>(this JoinOnQuery<TLeft, TRight> joinOn, ITable table)
    {
        var root = joinOn.Root;
        var rightNew = root.CreateMember(table);
        var joinOnNew = new JoinOnQuery<TLeft, TOther>(root, joinOn.Left, rightNew);
        root.AddJoinOn(joinOnNew);
        return joinOnNew;
    }

    /// <summary>
    /// 用右表联新表
    /// </summary>
    /// <typeparam name="TLeft"></typeparam>
    /// <typeparam name="TRight"></typeparam>
    /// <typeparam name="TOther"></typeparam>
    /// <param name="joinOn"></param>
    /// <param name="table"></param>
    /// <returns></returns>
    public static JoinOnQuery<TRight, TOther> RightTableJoin<TLeft, TRight, TOther>(this JoinOnQuery<TLeft, TRight> joinOn, ITable table)
    {
        var root = joinOn.Root;
        var rightNew = root.CreateMember(table);
        var joinOnNew = new JoinOnQuery<TRight, TOther>(root, joinOn.Source, rightNew);
        root.AddJoinOn(joinOnNew);
        return joinOnNew;
    }
    #endregion
    #region IDataSqlQuery
    /// <summary>
    /// 联表(创建新联表)
    /// </summary>
    /// <param name="main"></param>
    /// <param name="table"></param>
    /// <returns></returns>
    public static JoinOnSqlQuery<TLeft, TRight> SqlJoin<TLeft, TRight>(this ITable main, ITable table)
    {
        JoinTableSqlQuery joinTable = new();
        var leftTable = joinTable.CreateMember(main);
        var rightTable = joinTable.CreateMember(table);
        JoinOnSqlQuery<TLeft, TRight> joinOn = new(joinTable, leftTable, rightTable);
        joinTable.AddJoinOn(joinOn);
        return joinOn;
    }
    /// <summary>
    /// 用左表联新表
    /// </summary>
    /// <typeparam name="TLeft"></typeparam>
    /// <typeparam name="TRight"></typeparam>
    /// <typeparam name="TOther"></typeparam>
    /// <param name="joinOn"></param>
    /// <param name="table"></param>
    /// <returns></returns>
    public static JoinOnSqlQuery<TLeft, TOther> LeftTableJoin<TLeft, TRight, TOther>(this JoinOnSqlQuery<TLeft, TRight> joinOn, ITable table)
    {
        var root = joinOn.Root;
        var rightNew = root.CreateMember(table);
        var joinOnNew = new JoinOnSqlQuery<TLeft, TOther>(root, joinOn.Left, rightNew);
        root.AddJoinOn(joinOnNew);
        return joinOnNew;
    }

    /// <summary>
    /// 用右表联新表
    /// </summary>
    /// <typeparam name="TLeft"></typeparam>
    /// <typeparam name="TRight"></typeparam>
    /// <typeparam name="TOther"></typeparam>
    /// <param name="joinOn"></param>
    /// <param name="table"></param>
    /// <returns></returns>
    public static JoinOnSqlQuery<TRight, TOther> RightTableJoin<TLeft, TRight, TOther>(this JoinOnSqlQuery<TLeft, TRight> joinOn, ITable table)
    {
        var root = joinOn.Root;
        var rightNew = root.CreateMember(table);
        var joinOnNew = new JoinOnSqlQuery<TRight, TOther>(root, joinOn.Source, rightNew);
        root.AddJoinOn(joinOnNew);
        return joinOnNew;
    }    
    #endregion  
}
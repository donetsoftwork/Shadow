using ShadowSql.Identifiers;
using ShadowSql.Join;
using ShadowSql.Variants;
using System.Reflection.Emit;

namespace ShadowSql;

/// <summary>
/// 联表扩展方法
/// </summary>
public static partial class ShadowSqlServices
{
    #region Join
    /// <summary>
    /// 联表(创建新联表)
    /// </summary>
    /// <param name="main"></param>
    /// <param name="table"></param>
    /// <returns></returns>
    public static JoinOnQuery<LTable, RTable> Join<LTable, RTable>(this LTable main, RTable table)
        where LTable : ITable
        where RTable : ITable
    {
        JoinTableQuery joinTable = new();
        var leftTable = joinTable.CreateMember(main);
        var rightTable = joinTable.CreateMember(table);
        JoinOnQuery<LTable, RTable> joinOn = new(joinTable, leftTable, rightTable);
        joinTable.AddJoinOn(joinOn);
        return joinOn;
    }
    /// <summary>
    /// 联表(创建新联表)
    /// </summary>
    /// <typeparam name="LTable"></typeparam>
    /// <typeparam name="RTable"></typeparam>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static JoinOnQuery<LTable, RTable> Join<LTable, RTable>(this TableAlias<LTable> left, TableAlias<RTable> right)
        where LTable : ITable
        where RTable : ITable
    {
        JoinTableQuery joinTable = new();
        joinTable.AddMember(left);
        joinTable.AddMember(right);
        JoinOnQuery<LTable, RTable> joinOn = new(joinTable, left, right);
        joinTable.AddJoinOn(joinOn);
        return joinOn;
    }
    /// <summary>
    /// 联表(用左表查询联右表)
    /// </summary>
    /// <typeparam name="LTable"></typeparam>
    /// <typeparam name="RTable"></typeparam>
    /// <param name="left"></param>
    /// <param name="table"></param>
    /// <returns></returns>
    public static JoinOnQuery<LTable, RTable> Join<LTable, RTable>(this MemberQuery<LTable> left, RTable table)
        where LTable : ITable
        where RTable : ITable
    {
        if (left.InnerQuery.Source is not JoinTableQuery root)
            return Join(left.Source.Target, table);
        var right = root.CreateMember(table);
        var joinOn = new JoinOnQuery<LTable, RTable>(root, left.Source, right);
        root.AddJoinOn(joinOn);
        return joinOn;
    }
    /// <summary>
    /// 用左表联新表
    /// </summary>
    /// <typeparam name="LTable"></typeparam>
    /// <typeparam name="RTable"></typeparam>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="joinOn"></param>
    /// <param name="table"></param>
    /// <returns></returns>
    public static JoinOnQuery<LTable, TTable> JoinLeftTable<LTable, RTable, TTable>(this JoinOnQuery<LTable, RTable> joinOn, TTable table)
        where LTable : ITable
        where RTable : ITable
        where TTable : ITable
    {
        var root = joinOn.Root;
        var rightNew = root.CreateMember(table);
        var joinOnNew = new JoinOnQuery<LTable, TTable>(root, joinOn.Left, rightNew);
        root.AddJoinOn(joinOnNew);
        return joinOnNew;
    }
    /// <summary>
    /// 用右表联新表
    /// </summary>
    /// <typeparam name="LTable"></typeparam>
    /// <typeparam name="RTable"></typeparam>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="joinOn"></param>
    /// <param name="table"></param>
    /// <returns></returns>
    public static JoinOnQuery<RTable, TTable> JoinRightTable<LTable, RTable, TTable>(this JoinOnQuery<LTable, RTable> joinOn, TTable table)
        where LTable : ITable
        where RTable : ITable
        where TTable : ITable
    {
        var root = joinOn.Root;
        var rightNew = root.CreateMember(table);
        var joinOnNew = new JoinOnQuery<RTable, TTable>(root, joinOn.Source, rightNew);
        root.AddJoinOn(joinOnNew);
        return joinOnNew;
    }
    #endregion
    #region PairMemberQuery
    /// <summary>
    /// 两表查询
    /// </summary>
    /// <typeparam name="LTable"></typeparam>
    /// <typeparam name="RTable"></typeparam>
    /// <param name="multiTable"></param>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static PairMemberQuery<LTable, RTable> ToPairQuery<LTable, RTable>(this MultiTableBase multiTable, string left, string right)
        where LTable : ITable
        where RTable : ITable
        => new(multiTable.Table<LTable>(left), multiTable.Table<RTable>(right), multiTable.InnerQuery);
    #endregion
    #region MemberQuery
    /// <summary>
    /// 单独查询左表
    /// </summary>
    /// <typeparam name="LTable"></typeparam>
    /// <typeparam name="RTable"></typeparam>
    /// <param name="joinOn"></param>
    /// <returns></returns>
    public static MemberQuery<LTable> ToLeftQuery<LTable, RTable>(this JoinOnQuery<LTable, RTable> joinOn)
        where LTable : ITable
        where RTable : ITable
        => new(joinOn.Left, joinOn.Root.InnerQuery);
    /// <summary>
    /// 单独查询右表
    /// </summary>
    /// <typeparam name="LTable"></typeparam>
    /// <typeparam name="RTable"></typeparam>
    /// <param name="joinOn"></param>
    /// <returns></returns>
    public static MemberQuery<RTable> ToRightQuery<LTable, RTable>(this JoinOnQuery<LTable, RTable> joinOn)
        where LTable : ITable
        where RTable : ITable
        => new(joinOn.Source, joinOn.Root.InnerQuery);
    /// <summary>
    /// 构造表成员
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="multiTable"></param>
    /// <param name="table"></param>
    /// <returns></returns>
    public static MemberQuery<TTable> CreateMemberQuery<TTable>(this MultiTableBase multiTable, TTable table)
        where TTable : ITable
        => new(multiTable.CreateMember(table), multiTable.InnerQuery);
    #endregion
    /// <summary>
    /// 添加表成员
    /// </summary>
    /// <typeparam name="MultiQuery"></typeparam>
    /// <param name="multiQuery"></param>
    /// <param name="aliasTable"></param>
    /// <returns></returns>
    public static MultiQuery AddMember<MultiQuery>(this MultiQuery multiQuery, IAliasTable aliasTable)
        where MultiQuery : MultiTableBase
    {
        multiQuery.AddMemberCore(aliasTable);
        return multiQuery;
    }
}
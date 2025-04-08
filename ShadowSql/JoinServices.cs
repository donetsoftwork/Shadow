using ShadowSql.Identifiers;
using ShadowSql.Join;
using ShadowSql.Variants;

namespace ShadowSql;

/// <summary>
/// 联表扩展方法
/// </summary>
public static partial class ShadowSqlServices
{
    #region Join
    #region JoinTableQuery
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
    #region JoinTableSqlQuery
    /// <summary>
    /// 联表(创建新联表)
    /// </summary>
    /// <param name="main"></param>
    /// <param name="table"></param>
    /// <returns></returns>
    public static JoinOnSqlQuery<LTable, RTable> SqlJoin<LTable, RTable>(this LTable main, RTable table)
        where LTable : ITable
        where RTable : ITable
    {
        JoinTableSqlQuery joinTable = new();
        var leftTable = joinTable.CreateMember(main);
        var rightTable = joinTable.CreateMember(table);
        JoinOnSqlQuery<LTable, RTable> joinOn = new(joinTable, leftTable, rightTable);
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
    public static JoinOnSqlQuery<LTable, RTable> SqlJoin<LTable, RTable>(this TableAlias<LTable> left, TableAlias<RTable> right)
        where LTable : ITable
        where RTable : ITable
    {
        JoinTableSqlQuery joinTable = new();
        joinTable.AddMember(left);
        joinTable.AddMember(right);
        JoinOnSqlQuery<LTable, RTable> joinOn = new(joinTable, left, right);
        joinTable.AddJoinOn(joinOn);
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
    public static JoinOnSqlQuery<LTable, TTable> JoinLeftTable<LTable, RTable, TTable>(this JoinOnSqlQuery<LTable, RTable> joinOn, TTable table)
        where LTable : ITable
        where RTable : ITable
        where TTable : ITable
    {
        var root = joinOn.Root;
        var rightNew = root.CreateMember(table);
        var joinOnNew = new JoinOnSqlQuery<LTable, TTable>(root, joinOn.Left, rightNew);
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
    public static JoinOnSqlQuery<RTable, TTable> JoinRightTable<LTable, RTable, TTable>(this JoinOnSqlQuery<LTable, RTable> joinOn, TTable table)
        where LTable : ITable
        where RTable : ITable
        where TTable : ITable
    {
        var root = joinOn.Root;
        var rightNew = root.CreateMember(table);
        var joinOnNew = new JoinOnSqlQuery<RTable, TTable>(root, joinOn.Source, rightNew);
        root.AddJoinOn(joinOnNew);
        return joinOnNew;
    }
    #endregion
    #endregion
    /// <summary>
    /// 添加表成员
    /// </summary>
    /// <typeparam name="MultiTable"></typeparam>
    /// <param name="multiTable"></param>
    /// <param name="aliasTable"></param>
    /// <returns></returns>
    public static MultiTable AddMember<MultiTable>(this MultiTable multiTable, IAliasTable aliasTable)
        where MultiTable : MultiTableBase
    {
        multiTable.AddMemberCore(aliasTable);
        return multiTable;
    }
    /// <summary>
    /// 构造表成员
    /// </summary>
    /// <typeparam name="MultiTable"></typeparam>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="multiTable"></param>
    /// <param name="table"></param>
    /// <returns></returns>
    public static MultiTable AddMember<MultiTable, TTable>(this MultiTable multiTable, TTable table)
        where MultiTable : MultiTableBase, IMultiTable
        where TTable : ITable
    {
        multiTable.AddMemberCore(table.As(multiTable.CreateMemberName()));
        return multiTable;
    }
    /// <summary>
    /// 构造表成员
    /// </summary>
    /// <typeparam name="MultiTable"></typeparam>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="multiTable"></param>
    /// <param name="table"></param>
    /// <returns></returns>
    public static TableAlias<TTable> CreateMember<MultiTable, TTable>(this MultiTable multiTable, TTable table)
        where MultiTable : MultiTableBase, IMultiTable
        where TTable : ITable
    {
        var aliasTable = table.As(multiTable.CreateMemberName());
        multiTable.AddMemberCore(aliasTable);
        return aliasTable;
    }
}
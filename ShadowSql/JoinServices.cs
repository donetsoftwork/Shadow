using ShadowSql.Identifiers;
using ShadowSql.Join;

namespace ShadowSql;

/// <summary>
/// 联表扩展方法
/// </summary>
public static partial class ShadowSqlServices
{
    #region IDataQuery
    /// <summary>
    /// 联表(创建新联表)
    /// </summary>
    /// <param name="main">主表</param>
    /// <param name="table">表</param>
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
    /// 用左表联新表
    /// </summary>
    /// <typeparam name="LTable"></typeparam>
    /// <typeparam name="RTable"></typeparam>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="joinOn">联接</param>
    /// <param name="table">表</param>
    /// <returns></returns>
    public static JoinOnQuery<LTable, TTable> LeftTableJoin<LTable, RTable, TTable>(this JoinOnQuery<LTable, RTable> joinOn, TTable table)
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
    /// <param name="joinOn">联接</param>
    /// <param name="table">表</param>
    /// <returns></returns>
    public static JoinOnQuery<RTable, TTable> RightTableJoin<LTable, RTable, TTable>(this JoinOnQuery<LTable, RTable> joinOn, TTable table)
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
    #region Multi
    /// <summary>
    /// 多表(创建新多表)
    /// </summary>
    /// <param name="aliasTable">别名表</param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static MultiTableQuery Multi(this IAliasTable aliasTable, IAliasTable other)
    {
        var multiTable = new MultiTableQuery();
        multiTable.AddMemberCore(aliasTable);
        multiTable.AddMemberCore(other);
        return multiTable;
    }
    /// <summary>
    /// 多表(创建新多表)
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static MultiTableQuery Multi(this ITable table, ITable other)
    {
        var multiTable = new MultiTableQuery();
        multiTable.CreateMember(table);
        multiTable.CreateMember(other);
        return multiTable;
    }
    #endregion
    #endregion
    #region IDataSqlQuery
    /// <summary>
    /// 联表(创建新联表)
    /// </summary>
    /// <param name="main">主表</param>
    /// <param name="table">表</param>
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
    #region SqlMulti
    /// <summary>
    /// 多表(创建新多表)
    /// </summary>
    /// <param name="aliasTable">别名表</param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static MultiTableSqlQuery SqlMulti(this IAliasTable aliasTable, IAliasTable other)
    {
        var multiTable = new MultiTableSqlQuery();
        multiTable.AddMemberCore(aliasTable);
        multiTable.AddMemberCore(other);
        return multiTable;
    }
    /// <summary>
    /// 多表(创建新多表)
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="other"></param>
    /// <returns></returns>
    public static MultiTableSqlQuery SqlMulti(this ITable table, ITable other)
    {
        var multiTable = new MultiTableSqlQuery();
        multiTable.CreateMember(table);
        multiTable.CreateMember(other);
        return multiTable;
    }
    #endregion
    /// <summary>
    /// 用左表联新表
    /// </summary>
    /// <typeparam name="LTable"></typeparam>
    /// <typeparam name="RTable"></typeparam>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="joinOn">联接</param>
    /// <param name="table">表</param>
    /// <returns></returns>
    public static JoinOnSqlQuery<LTable, TTable> LeftTableJoin<LTable, RTable, TTable>(this JoinOnSqlQuery<LTable, RTable> joinOn, TTable table)
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
    /// <param name="joinOn">联接</param>
    /// <param name="table">表</param>
    /// <returns></returns>
    public static JoinOnSqlQuery<RTable, TTable> RightTableJoin<LTable, RTable, TTable>(this JoinOnSqlQuery<LTable, RTable> joinOn, TTable table)
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
}
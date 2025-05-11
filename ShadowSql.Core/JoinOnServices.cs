using ShadowSql.CompareLogics;
using ShadowSql.Compares;
using ShadowSql.Identifiers;
using ShadowSql.Join;
using ShadowSql.Logics;
using ShadowSql.Queries;
using System;
using System.Collections.Generic;

namespace ShadowSql;

/// <summary>
/// 联表扩展方法
/// </summary>
public static partial class ShadowSqlCoreServices
{
    #region JoinType
    /// <summary>
    /// 内联
    /// </summary>
    /// <typeparam name="TjoinOn"></typeparam>
    /// <param name="joinOn"></param>
    /// <returns></returns>
    public static TjoinOn AsInnerJoin<TjoinOn>(this TjoinOn joinOn)
        where TjoinOn : JoinOnBase
    {
        joinOn.AsInnerJoinCore();
        return joinOn;
    }
    /// <summary>
    /// 外联
    /// </summary>
    /// <typeparam name="TjoinOn"></typeparam>
    /// <param name="joinOn"></param>
    /// <returns></returns>
    public static TjoinOn AsOuterJoin<TjoinOn>(this TjoinOn joinOn)
        where TjoinOn : JoinOnBase
    {
        joinOn.AsOuterJoinCore();
        return joinOn;
    }
    /// <summary>
    /// 左联
    /// </summary>
    /// <typeparam name="TjoinOn"></typeparam>
    /// <param name="joinOn"></param>
    /// <returns></returns>
    public static TjoinOn AsLeftJoin<TjoinOn>(this TjoinOn joinOn)
        where TjoinOn : JoinOnBase
    {
        joinOn.AsLeftJoinCore();
        return joinOn;
    }
    /// <summary>
    /// 右联
    /// </summary>
    /// <typeparam name="TjoinOn"></typeparam>
    /// <param name="joinOn"></param>
    /// <returns></returns>
    public static TjoinOn AsRightJoin<TjoinOn>(this TjoinOn joinOn)
        where TjoinOn : JoinOnBase
    {
        joinOn.AsRightJoinCore();
        return joinOn;
    }
    #endregion
    #region JoinOnQuery
    #region LeftTableJoin
    /// <summary>
    /// 用左表联新表
    /// </summary>
    /// <param name="joinOn"></param>
    /// <param name="table"></param>
    /// <returns></returns>
    public static JoinOnQuery LeftTableJoin(this JoinOnQuery joinOn, IAliasTable table)
    {
        var root = joinOn.Root;
        root.AddMemberCore(table);
        var joinOnNew = new JoinOnQuery(root, joinOn.Left, table);
        root.AddJoinOn(joinOnNew);
        return joinOnNew;
    }
    /// <summary>
    /// 用左表联新表
    /// </summary>
    /// <param name="joinOn"></param>
    /// <param name="table"></param>
    /// <returns></returns>
    public static JoinOnQuery LeftTableJoin(this JoinOnQuery joinOn, ITable table)
    {
        var root = joinOn.Root;
        var newRight = root.CreateMember(table);
        var joinOnNew = new JoinOnQuery(root, joinOn.Left, newRight);
        root.AddJoinOn(joinOnNew);
        return joinOnNew;
    }
    /// <summary>
    /// 用左表联新表
    /// </summary>
    /// <param name="joinOn"></param>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public static JoinOnQuery LeftTableJoin(this JoinOnQuery joinOn, string tableName)
        => LeftTableJoin(joinOn, new Table(tableName));
    #endregion
    #region RightTableJoin
    /// <summary>
    /// 用右表联新表
    /// </summary>
    /// <param name="joinOn"></param>
    /// <param name="table"></param>
    /// <returns></returns>
    public static JoinOnQuery RightTableJoin(this JoinOnQuery joinOn, IAliasTable table)
    {
        var root = joinOn.Root;
        root.AddMemberCore(table);
        var joinOnNew = new JoinOnQuery(root, joinOn.Source, table);
        root.AddJoinOn(joinOnNew);
        return joinOnNew;
    }
    /// <summary>
    /// 用右表联新表
    /// </summary>
    /// <param name="joinOn"></param>
    /// <param name="table"></param>
    /// <returns></returns>
    public static JoinOnQuery RightTableJoin(this JoinOnQuery joinOn, ITable table)
    {
        var root = joinOn.Root;
        var newRight = root.CreateMember(table);
        var joinOnNew = new JoinOnQuery(root, joinOn.Source, newRight);
        root.AddJoinOn(joinOnNew);
        return joinOnNew;
    }
    /// <summary>
    /// 用右表联新表
    /// </summary>
    /// <param name="joinOn"></param>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public static JoinOnQuery RightTableJoin(this JoinOnQuery joinOn, string tableName)
        => RightTableJoin(joinOn, new Table(tableName));
    #endregion
    #region 扩展逻辑
    /// <summary>
    /// 按列查询
    /// </summary>
    /// <typeparam name="TJoinOn"></typeparam>
    /// <param name="joinOn"></param>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <param name="logic"></param>
    /// <returns></returns>
    public static TJoinOn Apply<TJoinOn>(this TJoinOn joinOn, string left, string right, Func<Logic, ICompareView, ICompareView, Logic> logic)
        where TJoinOn: JoinOnBase, IDataQuery
    {
        joinOn.Logic = logic(joinOn.Logic, joinOn.GetLeftCompareField(left), joinOn.GetRightCompareField(right));
        return joinOn;
    }
    #endregion
    #endregion
    #region JoinOnSqlQuery
    #region 基础查询功能
    #region conditions
    /// <summary>
    /// 按原始sql查询
    /// </summary>
    /// <typeparam name="TJoinOn"></typeparam>
    /// <param name="joinOn"></param>
    /// <param name="conditions"></param>
    /// <returns></returns>
    public static TJoinOn On<TJoinOn>(this TJoinOn joinOn, params IEnumerable<string> conditions)
        where TJoinOn : JoinOnBase, IDataSqlQuery
    {
        joinOn.Query.AddConditions(conditions);
        return joinOn;
    }
    #endregion
    #region AtomicLogic
    /// <summary>
    /// 按逻辑查询
    /// </summary>
    /// <typeparam name="TjoinOn"></typeparam>
    /// <param name="joinOn"></param>
    /// <param name="logic"></param>
    /// <returns></returns>
    public static TjoinOn On<TjoinOn>(this TjoinOn joinOn, AtomicLogic logic)
        where TjoinOn : JoinOnBase, IDataSqlQuery
    {
        joinOn.Query.AddLogic(logic);
        return joinOn;
    }
    /// <summary>
    /// On
    /// </summary>
    /// <typeparam name="TjoinOn"></typeparam>
    /// <param name="joinOn"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static TjoinOn On<TjoinOn>(this TjoinOn joinOn, Func<IJoinOn, AtomicLogic> query)
        where TjoinOn : JoinOnBase, IJoinOn, IDataSqlQuery
    {
        joinOn.Query.AddLogic(query(joinOn));
        return joinOn;
    }
    /// <summary>
    /// On
    /// </summary>
    /// <typeparam name="TjoinOn"></typeparam>
    /// <param name="joinOn"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static TjoinOn On<TjoinOn>(this TjoinOn joinOn, Func<IAliasTable, IAliasTable, AtomicLogic> query)
        where TjoinOn : JoinOnBase, IJoinOn, IDataSqlQuery
    {
        joinOn.Query.AddLogic(query(joinOn.Left, joinOn.JoinSource));
        return joinOn;
    }
    #endregion
    #region SqlQuery
    /// <summary>
    /// 按SqlQuery查询
    /// </summary>
    /// <typeparam name="TJoinOn"></typeparam>
    /// <param name="joinOn"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static TJoinOn Apply<TJoinOn>(this TJoinOn joinOn, Func<SqlQuery, TJoinOn, SqlQuery> query)
        where TJoinOn : JoinOnBase, IDataSqlQuery, IJoinOn
    {
        joinOn.Query = query(joinOn.Query, joinOn);
        return joinOn;
    }
    #endregion
    #endregion
    #region OnColumn
    /// <summary>
    /// 按列联表
    /// </summary>
    /// <typeparam name="TJoinOn"></typeparam>
    /// <param name="joinOn"></param>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static TJoinOn On<TJoinOn>(this TJoinOn joinOn, IColumn left, IColumn right)
        where TJoinOn : JoinOnBase, IJoinOn, IDataSqlQuery
        => On(joinOn, left, CompareSymbol.Equal, right);
    /// <summary>
    /// 按列联表
    /// </summary>
    /// <typeparam name="TJoinOn"></typeparam>
    /// <param name="joinOn"></param>
    /// <param name="left"></param>
    /// <param name="compare"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static TJoinOn On<TJoinOn>(this TJoinOn joinOn, IColumn left, CompareSymbol compare, IColumn right)
        where TJoinOn : JoinOnBase, IJoinOn, IDataSqlQuery
    {
        var leftColumn = joinOn.Left.GetPrefixField(left);
        var rightColumn = joinOn.JoinSource.GetPrefixField(right);
        if (leftColumn != null && rightColumn != null)
            joinOn.Query.AddLogic(new CompareLogic(leftColumn, compare, rightColumn));
        return joinOn;
    }
    /// <summary>
    /// 按列联表
    /// </summary>
    /// <typeparam name="TJoinOn"></typeparam>
    /// <param name="joinOn"></param>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static TJoinOn On<TJoinOn>(this TJoinOn joinOn, IPrefixField left, IPrefixField right)
        where TJoinOn : JoinOnBase, IJoinOn, IDataSqlQuery
        => On(joinOn, left, CompareSymbol.Equal, right);
    /// <summary>
    /// 按列联表
    /// </summary>
    /// <typeparam name="TJoinOn"></typeparam>
    /// <param name="joinOn"></param>
    /// <param name="left"></param>
    /// <param name="compare"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static TJoinOn On<TJoinOn>(this TJoinOn joinOn, IPrefixField left, CompareSymbol compare, IPrefixField right)
        where TJoinOn : JoinOnBase, IJoinOn, IDataSqlQuery
    {
        joinOn.Query.AddLogic(new CompareLogic(left, compare, right));
        return joinOn;
    }
    /// <summary>
    /// 对列进行联表查询
    /// </summary>
    /// <param name="joinOn"></param>
    /// <param name="leftColumn"></param>
    /// <param name="rightColumn"></param>
    /// <returns></returns>
    public static TJoinOn OnColumn<TJoinOn>(this TJoinOn joinOn, string leftColumn, string rightColumn)
        where TJoinOn : JoinOnBase, IJoinOn, IDataSqlQuery
        => joinOn.On(new CompareLogic(joinOn.GetLeftCompareField(leftColumn), CompareSymbol.Equal, joinOn.GetRightCompareField(rightColumn)));
    /// <summary>
    /// 对列进行联表查询
    /// </summary>
    /// <param name="joinOn"></param>
    /// <param name="leftColumn"></param>
    /// <param name="op"></param>
    /// <param name="rightColumn"></param>
    /// <returns></returns>
    public static TJoinOn OnColumn<TJoinOn>(this TJoinOn joinOn, string leftColumn, string op, string rightColumn)
        where TJoinOn : JoinOnBase, IJoinOn, IDataSqlQuery
        => joinOn.On(new CompareLogic(joinOn.GetLeftCompareField(leftColumn), CompareSymbol.Get(op), joinOn.GetRightCompareField(rightColumn)));
    #endregion
    #region LeftTableJoin
    /// <summary>
    /// 用左表联新表
    /// </summary>
    /// <param name="joinOn"></param>
    /// <param name="table"></param>
    /// <returns></returns>
    public static JoinOnSqlQuery LeftTableJoin(this JoinOnSqlQuery joinOn, IAliasTable table)
    {
        var root = joinOn.Root;
        root.AddMemberCore(table);
        var joinOnNew = new JoinOnSqlQuery(root, joinOn.Left, table);
        root.AddJoinOn(joinOnNew);
        return joinOnNew;
    }
    /// <summary>
    /// 用左表联新表
    /// </summary>
    /// <param name="joinOn"></param>
    /// <param name="table"></param>
    /// <returns></returns>
    public static JoinOnSqlQuery LeftTableJoin(this JoinOnSqlQuery joinOn, ITable table)
    {
        var root = joinOn.Root;
        var newRight = root.CreateMember(table);
        var joinOnNew = new JoinOnSqlQuery(root, joinOn.Left, newRight);
        root.AddJoinOn(joinOnNew);
        return joinOnNew;
    }
    /// <summary>
    /// 用左表联新表
    /// </summary>
    /// <param name="joinOn"></param>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public static JoinOnSqlQuery LeftTableJoin(this JoinOnSqlQuery joinOn, string tableName)
        => LeftTableJoin(joinOn, new Table(tableName));
    #endregion
    #region RightTableJoin
    /// <summary>
    /// 用右表联新表
    /// </summary>
    /// <param name="joinOn"></param>
    /// <param name="table"></param>
    /// <returns></returns>
    public static JoinOnSqlQuery RightTableJoin(this JoinOnSqlQuery joinOn, IAliasTable table)
    {
        var root = joinOn.Root;
        root.AddMemberCore(table);
        var joinOnNew = new JoinOnSqlQuery(root, joinOn.Source, table);
        root.AddJoinOn(joinOnNew);
        return joinOnNew;
    }
    /// <summary>
    /// 用右表联新表
    /// </summary>
    /// <param name="joinOn"></param>
    /// <param name="table"></param>
    /// <returns></returns>
    public static JoinOnSqlQuery RightTableJoin(this JoinOnSqlQuery joinOn, ITable table)
    {
        var root = joinOn.Root;
        var newRight = root.CreateMember(table);
        var joinOnNew = new JoinOnSqlQuery(root, joinOn.Source, newRight);
        root.AddJoinOn(joinOnNew);
        return joinOnNew;
    }
    /// <summary>
    /// 用右表联新表
    /// </summary>
    /// <param name="joinOn"></param>
    /// <param name="tableName"></param>
    /// <returns></returns>
    public static JoinOnSqlQuery RightTableJoin(this JoinOnSqlQuery joinOn, string tableName)
        => RightTableJoin(joinOn, new Table(tableName));
    #endregion
    #region 扩展逻辑
    /// <summary>
    /// 应用sql查询
    /// </summary>
    /// <param name="query"></param>
    /// <param name="on"></param>
    /// <returns></returns>
    public static JoinOnSqlQuery Apply(this JoinOnSqlQuery query, Func<SqlQuery, IAliasTable, IAliasTable,  SqlQuery> on)
    {
        query._filter = on(query._filter, query.Left, query.Source);
        return query;
    }
    #endregion
    #endregion
}

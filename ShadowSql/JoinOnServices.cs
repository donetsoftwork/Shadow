﻿using ShadowSql.CompareLogics;
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
public static partial class ShadowSqlServices
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
        joinOn.AddConditions(conditions);
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
        where TjoinOn : JoinOnBase
    {
        joinOn.AddLogic(logic);
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
        where TjoinOn : JoinOnBase, IJoinOn
    {
        joinOn.AddLogic(query(joinOn));
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
        where TjoinOn : JoinOnBase, IJoinOn
    {
        joinOn.AddLogic(query(joinOn.Left, joinOn.Source));
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
    public static TJoinOn On<TJoinOn>(this TJoinOn joinOn, Func<SqlQuery, SqlQuery> query)
        where TJoinOn : JoinOnBase, IDataSqlQuery
    {
        joinOn.ApplyFilter(query);
        return joinOn;
    }
    /// <summary>
    /// 按SqlQuery查询
    /// </summary>
    /// <typeparam name="TJoinOn"></typeparam>
    /// <param name="joinOn"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static TJoinOn On<TJoinOn>(this TJoinOn joinOn, Func<IJoinOn, SqlQuery, SqlQuery> query)
        where TJoinOn : JoinOnBase, IDataSqlQuery, IJoinOn
    {
        joinOn.ApplyFilter(sqlQuery => query(joinOn, sqlQuery));
        return joinOn;
    }
    #endregion
    #region Logic
    /// <summary>
    /// 按Logic查询
    /// </summary>
    /// <typeparam name="TJoinOn"></typeparam>
    /// <param name="joinOn"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static TJoinOn On<TJoinOn>(this TJoinOn joinOn, Func<Logic, Logic> query)
        where TJoinOn : JoinOnBase, IDataQuery
    {
        joinOn.ApplyFilter(query);
        return joinOn;
    }
    /// <summary>
    /// 按Logic查询
    /// </summary>
    /// <typeparam name="TJoinOn"></typeparam>
    /// <param name="joinOn"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static TJoinOn On<TJoinOn>(this TJoinOn joinOn, Func<IMultiView, Logic, Logic> query)
        where TJoinOn : JoinOnBase, IDataQuery
    {
        joinOn.ApplyFilter(logic => query(joinOn, logic));
        return joinOn;
    }
    #endregion
    #endregion
    #region OnColumn
    #region JoinOnSqlQuery
    /// <summary>
    /// 按列联表
    /// </summary>
    /// <typeparam name="LTable"></typeparam>
    /// <typeparam name="RTable"></typeparam>
    /// <param name="joinOn"></param>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static JoinOnSqlQuery<LTable, RTable> On<LTable, RTable>(this JoinOnSqlQuery<LTable, RTable> joinOn, Func<LTable, IColumn> left, Func<RTable, IColumn> right)
        where LTable : ITable
        where RTable : ITable
        => On(joinOn, left, CompareSymbol.Equal, right);
    /// <summary>
    /// 按列联表
    /// </summary>
    /// <typeparam name="LTable"></typeparam>
    /// <typeparam name="RTable"></typeparam>
    /// <param name="joinOn"></param>
    /// <param name="left"></param>
    /// <param name="compare"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static JoinOnSqlQuery<LTable, RTable> On<LTable, RTable>(this JoinOnSqlQuery<LTable, RTable> joinOn, Func<LTable, IColumn> left, CompareSymbol compare, Func<RTable, IColumn> right)
        where LTable : ITable
        where RTable : ITable
    {
        var leftColumn = joinOn.Left.SelectPrefixColumn(left);
        var rightColumn = joinOn.Source.SelectPrefixColumn(right);
        if (leftColumn != null && rightColumn != null)
            joinOn.AddLogic(new CompareLogic(leftColumn, compare, rightColumn));

        return joinOn;
    }
    #endregion
    /// <summary>
    /// 按列联表
    /// </summary>
    /// <typeparam name="TJoinOn"></typeparam>
    /// <param name="joinOn"></param>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static TJoinOn On<TJoinOn>(this TJoinOn joinOn, IColumn left, IColumn right)
        where TJoinOn : JoinOnBase, IJoinOn
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
        where TJoinOn : JoinOnBase, IJoinOn
    {
        var leftColumn = joinOn.Left.GetPrefixColumn(left);
        var rightColumn = joinOn.Source.GetPrefixColumn(right);
        if (leftColumn != null && rightColumn != null)
            joinOn.AddLogic(new CompareLogic(leftColumn, compare, rightColumn));
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
        where TJoinOn : JoinOnBase, IJoinOn
        => OnColumn(joinOn, leftColumn, CompareSymbol.Equal, rightColumn);
    /// <summary>
    /// 对列进行联表查询
    /// </summary>
    /// <param name="joinOn"></param>
    /// <param name="leftColumn"></param>
    /// <param name="op"></param>
    /// <param name="rightColumn"></param>
    /// <returns></returns>
    public static TJoinOn OnColumn<TJoinOn>(this TJoinOn joinOn, string leftColumn, CompareSymbol op, string rightColumn)
        where TJoinOn : JoinOnBase, IJoinOn
        => joinOn.On(new CompareLogic(joinOn.GetLeftCompareField(leftColumn), op, joinOn.GetRightCompareField(rightColumn)));
    #endregion
}

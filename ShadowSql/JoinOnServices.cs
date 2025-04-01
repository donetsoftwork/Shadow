using ShadowSql.CompareLogics;
using ShadowSql.Compares;
using ShadowSql.Identifiers;
using ShadowSql.Join;
using System;
using System.Reflection.Emit;

namespace ShadowSql;

/// <summary>
/// 联表扩展方法
/// </summary>
public static partial class ShadowSqlServices
{
    #region OnColumn
    /// <summary>
    /// 按列联表
    /// </summary>
    /// <typeparam name="LTable"></typeparam>
    /// <typeparam name="RTable"></typeparam>
    /// <param name="joinOn"></param>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static JoinOnQuery<LTable, RTable> On<LTable, RTable>(this JoinOnQuery<LTable, RTable> joinOn, Func<LTable, IColumn> left, Func<RTable, IColumn> right)
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
    public static JoinOnQuery<LTable, RTable> On<LTable, RTable>(this JoinOnQuery<LTable, RTable> joinOn, Func<LTable, IColumn> left, CompareSymbol compare, Func<RTable, IColumn> right)
        where LTable : ITable
        where RTable : ITable
    {
        var leftColumn = joinOn.Left.SelectPrefixColumn(left);
        var rightColumn = joinOn.Source.SelectPrefixColumn(right);
        if (leftColumn != null && rightColumn != null)
            joinOn.InnerQuery.AddLogic(new CompareLogic(leftColumn, compare, rightColumn));

        return joinOn;
    }
    /// <summary>
    /// 按列联表
    /// </summary>
    /// <typeparam name="LTable"></typeparam>
    /// <typeparam name="RTable"></typeparam>
    /// <param name="joinOn"></param>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static JoinOnQuery<LTable, RTable> On<LTable, RTable>(this JoinOnQuery<LTable, RTable> joinOn, IColumn left, IColumn right)
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
    public static JoinOnQuery<LTable, RTable> On<LTable, RTable>(this JoinOnQuery<LTable, RTable> joinOn, IColumn left, CompareSymbol compare, IColumn right)
        where LTable : ITable
        where RTable : ITable
    {
        var leftColumn = joinOn.Left.GetPrefixColumn(left);
        var rightColumn = joinOn.Source.GetPrefixColumn(right);
        if (leftColumn != null && rightColumn != null)
            joinOn.InnerQuery.AddLogic(new CompareLogic(leftColumn, compare, rightColumn));

        return joinOn;
    }
    /// <summary>
    /// 对列进行联表查询
    /// </summary>
    /// <param name="joinOn"></param>
    /// <param name="leftColumn"></param>
    /// <param name="rightColumn"></param>
    /// <returns></returns>
    public static JoinOnQuery<LTable, RTable> OnColumn<LTable, RTable>(this JoinOnQuery<LTable, RTable> joinOn, string leftColumn, string rightColumn)
        where LTable : ITable
        where RTable : ITable
        => OnColumn(joinOn, leftColumn, CompareSymbol.Equal, rightColumn);
    /// <summary>
    /// 对列进行联表查询
    /// </summary>
    /// <param name="joinOn"></param>
    /// <param name="leftColumn"></param>
    /// <param name="op"></param>
    /// <param name="rightColumn"></param>
    /// <returns></returns>
    public static JoinOnQuery<LTable, RTable> OnColumn<LTable, RTable>(this JoinOnQuery<LTable, RTable> joinOn, string leftColumn, CompareSymbol op, string rightColumn)
        where LTable : ITable
        where RTable : ITable
        => joinOn.On(new CompareLogic(joinOn.GetLeftCompareField(leftColumn), op, joinOn.GetRightCompareField(rightColumn)));
    #endregion
}

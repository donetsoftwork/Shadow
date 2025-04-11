using ShadowSql.CompareLogics;
using ShadowSql.Compares;
using ShadowSql.Identifiers;
using ShadowSql.Join;
using System;

namespace ShadowSql;

/// <summary>
/// 联表扩展方法
/// </summary>
public static partial class ShadowSqlServices
{    
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
            joinOn._filter.AddLogic(new CompareLogic(leftColumn, compare, rightColumn));

        return joinOn;
    }
    #endregion    
    #endregion
}

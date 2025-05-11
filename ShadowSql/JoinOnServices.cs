using ShadowSql.CompareLogics;
using ShadowSql.Compares;
using ShadowSql.Identifiers;
using ShadowSql.Join;
using ShadowSql.Logics;
using ShadowSql.Queries;
using System;

namespace ShadowSql;

/// <summary>
/// 联表扩展方法
/// </summary>
public static partial class ShadowSqlServices
{
    #region JoinOnSqlQuery
    #region On
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
        var leftColumn = joinOn.Left.Prefix(left);
        var rightColumn = joinOn.Source.Prefix(right);
        if (leftColumn != null && rightColumn != null)
            joinOn._filter.AddLogic(new CompareLogic(leftColumn, compare, rightColumn));

        return joinOn;
    }
    /// <summary>
    /// 按列查询
    /// </summary>
    /// <typeparam name="LTable"></typeparam>
    /// <typeparam name="RTable"></typeparam>
    /// <param name="joinOn"></param>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static JoinOnSqlQuery<LTable, RTable> On<LTable, RTable>(this JoinOnSqlQuery<LTable, RTable> joinOn, Func<LTable, IColumn> left, Func<RTable, IColumn> right,  Func<IPrefixField, IPrefixField, AtomicLogic> query)
        where LTable : ITable
        where RTable : ITable
    {
        var lTable = joinOn.Left;
        var rTable = joinOn.Source;
        return joinOn.On(query(lTable.Prefix(left(lTable.Target)), rTable.Prefix(right(rTable.Target))));
    }
    #endregion
    #endregion
    #region AliasJoinOnSqlQuery
    #region On
    /// <summary>
    /// 按列联表
    /// </summary>
    /// <typeparam name="TLeft"></typeparam>
    /// <typeparam name="TRight"></typeparam>
    /// <param name="joinOn"></param>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static AliasJoinOnSqlQuery<TLeft, TRight> On<TLeft, TRight>(this AliasJoinOnSqlQuery<TLeft, TRight> joinOn, Func<TLeft, IPrefixField> left, Func<TRight, IPrefixField> right)
        where TLeft : IAliasTable<ITable>
        where TRight : IAliasTable<ITable>
        => On(joinOn, left, CompareSymbol.Equal, right);
    /// <summary>
    /// 按列联表
    /// </summary>
    /// <typeparam name="TLeft"></typeparam>
    /// <typeparam name="TRight"></typeparam>
    /// <param name="joinOn"></param>
    /// <param name="left"></param>
    /// <param name="compare"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static AliasJoinOnSqlQuery<TLeft, TRight> On<TLeft, TRight>(this AliasJoinOnSqlQuery<TLeft, TRight> joinOn, Func<TLeft, IPrefixField> left, CompareSymbol compare, Func<TRight, IPrefixField> right)
        where TLeft : IAliasTable<ITable>
        where TRight : IAliasTable<ITable>
        => joinOn.On(new CompareLogic(left(joinOn.Left), compare, right(joinOn.Source)));
    /// <summary>
    /// 按列查询
    /// </summary>
    /// <typeparam name="TLeft"></typeparam>
    /// <typeparam name="TRight"></typeparam>
    /// <param name="joinOn"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static AliasJoinOnSqlQuery<TLeft, TRight> On<TLeft, TRight>(this AliasJoinOnSqlQuery<TLeft, TRight> joinOn, Func<TLeft, TRight, AtomicLogic> query)
        where TLeft : IAliasTable<ITable>
        where TRight : IAliasTable<ITable>
        => joinOn.On(query(joinOn.Left, joinOn.Source));
    #endregion
    /// <summary>
    /// 按表查询
    /// </summary>
    /// <typeparam name="TLeft"></typeparam>
    /// <typeparam name="TRight"></typeparam>
    /// <param name="joinOn"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static AliasJoinOnSqlQuery<TLeft, TRight> Apply<TLeft, TRight>(this AliasJoinOnSqlQuery<TLeft, TRight> joinOn, Func<SqlQuery, TLeft, TRight, SqlQuery> query)
        where TLeft : IAliasTable<ITable>
        where TRight : IAliasTable<ITable>
    {
        joinOn._filter = query(joinOn._filter, joinOn.Left, joinOn.Source);
        return joinOn;
    }
    #endregion
    #region IDataSqlQuery
    /// <summary>
    /// 查询左表
    /// </summary>
    /// <typeparam name="TJoinOn"></typeparam>
    /// <param name="joinOn"></param>
    /// <param name="columnName"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static TJoinOn WhereLeft<TJoinOn>(this TJoinOn joinOn, string columnName, Func<ICompareView, AtomicLogic> query)
        where TJoinOn : JoinOnBase, IJoinOn, IDataSqlQuery
    {
        if (joinOn.Root is IDataSqlQuery root)
            root.Query.AddLogic(query(joinOn.GetLeftCompareField(columnName)));
        return joinOn;
    }
    /// <summary>
    /// 查询右表
    /// </summary>
    /// <typeparam name="TJoinOn"></typeparam>
    /// <param name="joinOn"></param>
    /// <param name="columnName"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static TJoinOn WhereRight<TJoinOn>(this TJoinOn joinOn, string columnName, Func<ICompareView, AtomicLogic> query)
        where TJoinOn : JoinOnBase, IJoinOn, IDataSqlQuery
    {
        if (joinOn.Root is IDataSqlQuery root)
            root.Query.AddLogic(query(joinOn.GetRightCompareField(columnName)));
        return joinOn;
    }
    #endregion
    #region IDataQuery
    /// <summary>
    /// 查询左表
    /// </summary>
    /// <typeparam name="TJoinOn"></typeparam>
    /// <param name="joinOn"></param>
    /// <param name="columnName"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static TJoinOn ApplyLeft<TJoinOn>(this TJoinOn joinOn, string columnName, Func<Logic, ICompareView, Logic> query)
        where TJoinOn : JoinOnBase, IJoinOn, IDataQuery
    {
        if (joinOn.Root is IDataQuery root)
            root.Logic = query(root.Logic, joinOn.GetLeftCompareField(columnName));
        return joinOn;
    }
    /// <summary>
    /// 查询右表
    /// </summary>
    /// <typeparam name="TJoinOn"></typeparam>
    /// <param name="joinOn"></param>
    /// <param name="columnName"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static TJoinOn ApplyRight<TJoinOn>(this TJoinOn joinOn, string columnName, Func<Logic, ICompareView, Logic> query)
        where TJoinOn : JoinOnBase, IJoinOn, IDataQuery
    {
        if (joinOn.Root is IDataQuery root)
            root.Logic = query(root.Logic, joinOn.GetRightCompareField(columnName));
        return joinOn;
    }
    #endregion
    #region JoinOnQuery
    /// <summary>
    /// 按列查询
    /// </summary>
    /// <typeparam name="LTable"></typeparam>
    /// <typeparam name="RTable"></typeparam>
    /// <param name="joinOn"></param>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <param name="logic"></param>
    /// <returns></returns>
    public static JoinOnQuery<LTable, RTable> Apply<LTable, RTable>(this JoinOnQuery<LTable, RTable> joinOn, Func<LTable, IColumn> left, Func<RTable, IColumn> right, Func<Logic, IPrefixField, IPrefixField, Logic> logic)
        where LTable : ITable
        where RTable : ITable
    {
        var lTable = joinOn.Left;
        var rTable = joinOn.Source;
        joinOn._filter = logic(joinOn._filter, lTable.Prefix(left(lTable.Target)), rTable.Prefix(right(rTable.Target)));
        return joinOn;
    }
    #endregion
    #region AliasJoinOnQuery
    /// <summary>
    /// 按表查询
    /// </summary>
    /// <typeparam name="TLeft"></typeparam>
    /// <typeparam name="TRight"></typeparam>
    /// <param name="joinOn"></param>
    /// <param name="logic"></param>
    /// <returns></returns>
    public static AliasJoinOnQuery<TLeft, TRight> Apply<TLeft, TRight>(this AliasJoinOnQuery<TLeft, TRight> joinOn, Func<Logic, TLeft, TRight, Logic> logic)
        where TLeft : IAliasTable<ITable>
        where TRight : IAliasTable<ITable>
    {
        joinOn._filter = logic(joinOn._filter, joinOn.Left, joinOn.Source);
        return joinOn;
    }
    #endregion
}

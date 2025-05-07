using ShadowSql.Aggregates;
using ShadowSql.AliasTables;
using ShadowSql.Cursors;
using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using ShadowSql.SingleSelect;
using ShadowSql.Tables;
using System;

namespace ShadowSql;

/// <summary>
/// 构造筛选单列扩展方法
/// </summary>
public static partial class ShadowSqlServices
{
    #region Table
    /// <summary>
    /// 表筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public static TableSingleSelect ToSingle<TTable>(this TableSqlQuery<TTable> query, Func<TTable, IFieldView> select)
        where TTable : ITable
        => new(query, select(query.Source));
    /// <summary>
    /// 表筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public static TableSingleSelect ToSingle<TTable>(this TableQuery<TTable> query, Func<TTable, IFieldView> select)
    where TTable : ITable
        => new(query, select(query.Source));
    /// <summary>
    /// 表范围筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="cursor"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public static CursorSingleSelect ToSingle<TTable>(this TableCursor<TTable> cursor, Func<TTable, IFieldView> select)
        where TTable : ITable
        => new(cursor, select(cursor.Source));
    #endregion
    #region AliasTable    
    /// <summary>
    /// 别名表筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public static TableSingleSelect ToSingle<TTable>(this AliasTableSqlQuery<TTable> query, Func<TTable, IColumn> select)
        where TTable : ITable
        => new(query, query.Source.Prefix(select(query.Source.Target)));
    /// <summary>
    /// 别名表筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="query"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public static TableSingleSelect ToSingle<TTable>(this AliasTableQuery<TTable> query, Func<TTable, IColumn> select)
        where TTable : ITable
    => new(query, query.Source.Prefix(select(query.Source.Target)));
    /// <summary>
    /// 别名表范围筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="cursor"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public static CursorSingleSelect ToSingle<TTable>(this AliasTableCursor<TTable> cursor, Func<TTable, IColumn> select)
        where TTable : ITable
        => new(cursor, cursor.Source.Prefix(select(cursor.Source.Target)));
    #endregion
    #region MultiTableFields
    /// <summary>
    /// 多(联)表筛选单列
    /// </summary>
    /// <param name="cursor"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public static CursorSingleSelect ToSingle(this MultiTableCursor cursor, Func<IMultiView, IFieldView> select)
        => new(cursor, select(cursor.Source));
    #endregion
    #region GroupByTableFields
    /// <summary>
    /// GroupBy后再筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="source"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public static TableSingleSelect ToSingle<TTable>(this GroupByTableSqlQuery<TTable> source, Func<TTable, IAggregateFieldAlias> select)
        where TTable : ITable
        => new(source, select(source._source));
    /// <summary>
    /// GroupBy后再筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="source"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public static TableSingleSelect ToSingle<TTable>(this GroupByTableQuery<TTable> source, Func<TTable, IAggregateFieldAlias> select)
        where TTable : ITable
        => new(source, select(source._source));
    /// <summary>
    /// GroupBy后再范围(分页)及列筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="cursor"></param>
    /// <param name="select"></param>
    /// <returns></returns>
    public static CursorSingleSelect ToSingle<TTable>(this GroupByTableCursor<TTable> cursor, Func<TTable, IAggregateFieldAlias> select)
        where TTable : ITable
        => new(cursor, select(cursor.Table));
    #endregion
    #region GroupByAliasTableFields
    /// <summary>
    /// GroupBy别名表后再筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="source"></param>
    /// <param name="select"></param>
    /// <param name="aggregate"></param>
    /// <returns></returns>
    public static TableSingleSelect ToSingle<TTable>(this GroupByAliasTableSqlQuery<TTable> source, Func<TTable, IColumn> select, Func<IPrefixField, IAggregateFieldAlias> aggregate)
        where TTable : ITable
        => new(source, aggregate(source._source.Prefix(select(source._source.Target))));
    /// <summary>
    /// GroupBy别名表后再筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="source"></param>
    /// <param name="select"></param>
    /// <param name="aggregate"></param>
    /// <returns></returns>
    public static TableSingleSelect ToSingle<TTable>(this GroupByAliasTableQuery<TTable> source, Func<TTable, IColumn> select, Func<IPrefixField, IAggregateFieldAlias> aggregate)
        where TTable : ITable
        => new(source, aggregate(source._source.Prefix(select(source._source.Target))));
    /// <summary>
    /// GroupBy别名表后再范围(分页)及列筛选单列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="cursor"></param>
    /// <param name="select"></param>
    /// <param name="aggregate"></param>
    /// <returns></returns>

    public static CursorSingleSelect ToSingle<TTable>(this GroupByAliasTableCursor<TTable> cursor, Func<TTable, IColumn> select, Func<IPrefixField, IAggregateFieldAlias> aggregate)
        where TTable : ITable
        => new(cursor, aggregate(cursor.AliasTable.Prefix(select(cursor.Table))));
    #endregion
}

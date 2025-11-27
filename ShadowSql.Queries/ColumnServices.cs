using ShadowSql.Identifiers;
using ShadowSql.Join;
using ShadowSql.Orders;
using ShadowSql.SingleSelect;
using ShadowSql.Variants;
using System;

namespace ShadowSql;

/// <summary>
/// 字段扩展
/// </summary>
public static partial class ShadowSqlQueryServices
{
    /// <summary>
    /// 字段别名
    /// </summary>
    /// <param name="field">字段</param>
    /// <param name="aliasName">别名</param>
    /// <returns></returns>
    public static AliasFieldInfo As(this ICompareView field, string aliasName)
        => new(field, aliasName);
    #region 子查询
    /// <summary>
    /// 子查询作为字段
    /// </summary>
    /// <param name="select">筛选</param>
    /// <returns></returns>
    public static SingleField ToField(this ISingleSelect select)
        => new(select);
    /// <summary>
    /// 子查询作为字段别名
    /// </summary>
    /// <param name="select">筛选</param>
    /// <param name="aliasName">别名</param>
    /// <returns></returns>
    public static SingleFieldAlias ToField(this ISingleSelect select, string aliasName)
        => new(select, aliasName);
    #endregion
    /// <summary>
    /// 获取指定表比较字段
    /// </summary>
    /// <param name="multi"></param>
    /// <param name="tableName">表名</param>
    /// <param name="fieldName">字段名</param>
    /// <returns></returns>
    public static ICompareField TableCompare(this IMultiView multi, string tableName, string fieldName)
    {
        var member = multi.From(tableName);
        if (member.GetPrefixField(fieldName) is IPrefixField prefixField)
            return prefixField;
        return member.NewField(fieldName);
    }
    #region Prefix
    /// <summary>
    /// 增加前缀
    /// </summary>
    /// <param name="column">列</param>
    /// <param name="tableName">表名</param>
    /// <returns></returns>
    public static PrefixField Prefix(this IColumn column, string tableName)
        => new(column, tableName, ".");
    /// <summary>
    /// 增加前缀
    /// </summary>
    /// <param name="column">列</param>
    /// <param name="table">表</param>
    /// <returns></returns>
    public static IPrefixField Prefix(this IColumn column, IAliasTable table)
        => table.GetPrefixField(column) ?? table.NewPrefixField(column);
    /// <summary>
    /// 定位到前缀字段
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="columnName">列名</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static IPrefixField Prefix(this IAliasTable table, string columnName)
        => table.GetPrefixField(columnName)
        ?? throw new ArgumentException(columnName + "字段不存在", nameof(columnName));
    /// <summary>
    /// 增加前缀
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="column">列</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static IPrefixField Prefix(this IAliasTable table, IColumn column)
        => table.GetPrefixField(column)
        ?? throw new ArgumentException("字段不存在", nameof(column));
    /// <summary>
    /// 增加前缀
    /// </summary>
    /// <param name="joinOn"></param>
    /// <param name="column">列</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static IPrefixField Prefix(this IJoinOn joinOn, IColumn column)
    {
        if(column is IPrefixField prefixField)
            return prefixField;
        var left = joinOn.Left;
        var right = joinOn.JoinSource;
        foreach (var c in right.Original.Columns)
            if (c == column)
                return right.Prefix(c);
        foreach (var c in left.Original.Columns)
            if (c == column)
                return left.Prefix(c);
        return right.GetPrefixField(column)
            ?? left.GetPrefixField(column)
            ?? throw new ArgumentException("字段不存在", nameof(column));
    }
    /// <summary>
    /// 增加前缀
    /// </summary>
    /// <param name="joinOn"></param>
    /// <param name="columnName">列名</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static IPrefixField Prefix(this IJoinOn joinOn, string columnName)
        => joinOn.GetRightField(columnName)
        ?? joinOn.GetLeftField(columnName)
        ?? throw new ArgumentException(columnName + "字段不存在", nameof(columnName));
    /// <summary>
    /// 选择列
    /// </summary>
    /// <typeparam name="TTable"></typeparam>
    /// <param name="table">表</param>
    /// <param name="query">查询</param>
    /// <returns></returns>
    public static IPrefixField Prefix<TTable>(this IAliasTable<TTable> table, Func<TTable, IColumn> query)
        where TTable : ITable
        => table.GetPrefixField(query(table.Original))
        ?? throw new ArgumentException("字段不存在", nameof(query));
    #endregion
    #region 排序
    /// <summary>
    /// 倒序
    /// </summary>
    /// <param name="field">字段</param>
    /// <returns></returns>
    public static IOrderDesc Desc(this IOrderAsc field)
        => new DescView(field);
    #endregion
}

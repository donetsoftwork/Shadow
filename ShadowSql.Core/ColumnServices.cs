using ShadowSql.FieldInfos;
using ShadowSql.Identifiers;
using ShadowSql.Insert;
using ShadowSql.Join;
using ShadowSql.Orders;
using ShadowSql.SingleSelect;
using ShadowSql.SqlVales;
using ShadowSql.Variants;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShadowSql;

/// <summary>
/// 字段扩展
/// </summary>
public static partial class ShadowSqlCoreServices
{
    /// <summary>
    /// 定位到列
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="columnName">列名</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static IColumn Column(this ITable table, string columnName)
        => table.GetColumn(columnName)
        ?? throw new ArgumentException(columnName + "列不存在", nameof(columnName));
    /// <summary>
    /// 定位到字段(严格校验)
    /// </summary>
    /// <param name="view"></param>
    /// <param name="fieldName">字段名</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static IField Strict(this ITableView view, string fieldName)
        => view.GetField(fieldName)
        ?? throw new ArgumentException(fieldName + "字段不存在", nameof(fieldName));
    /// <summary>
    /// 定位到字段(宽松不校验)
    /// </summary>
    /// <param name="view"></param>
    /// <param name="fieldName">字段名</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static IField Field(this ITableView view, string fieldName)
        => view.GetField(fieldName) ?? view.NewField(fieldName);
    /// <summary>
    /// 选择列
    /// </summary>
    /// <param name="table">表</param>
    /// <param name="columnNames">列名</param>
    /// <returns></returns>
    public static IEnumerable<IField> Fields(this ITableView table, params IEnumerable<string> columnNames)
    {
        foreach (var name in columnNames)
            yield return table.Field(name);
    }
    /// <summary>
    /// 字段别名
    /// </summary>
    /// <param name="field">字段</param>
    /// <param name="aliasName">别名</param>
    /// <returns></returns>
    public static AliasFieldInfo As(this ICompareView field, string aliasName)
        => new(field, aliasName);
    ///// <summary>
    ///// 获取比较字段
    ///// </summary>
    ///// <param name="filter">过滤条件</param>
    ///// <param name="fieldName">字段名</param>
    ///// <returns></returns>
    //public static ICompareField Compare(this ITableView filter, string fieldName)
    //    => filter.GetCompareField(fieldName);
    ///// <summary>
    ///// 获取比较字段
    ///// </summary>
    ///// <param name="table">表</param>
    ///// <param name="fieldName">字段名</param>
    ///// <returns></returns>
    //public static ICompareField GetCompareField(this ITableView table, string fieldName)
    //{
    //    if (table.GetColumn(fieldName) is IColumn column)
    //        return column;
    //    return table.GetField(fieldName);
    //}
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
        foreach (var c in right.Target.Columns)
            if (c == column)
                return right.Prefix(c);
        foreach (var c in left.Target.Columns)
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
        => table.GetPrefixField(query(table.Target))
        ?? throw new ArgumentException("字段不存在", nameof(query));
    #endregion
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
    #region 插入
    #region 插入值
    /// <summary>
    /// 插入单值
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="column">列</param>
    /// <param name="value">值</param>
    /// <returns></returns>
    public static InsertValue InsertValue<TValue>(this IColumn column, TValue value)
        => new(column, SqlValue.From(value));

    /// <summary>
    /// 插入多值
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="column">列</param>
    /// <param name="values">值</param>
    /// <returns></returns>
    public static InsertValues InsertValues<TValue>(this IColumn column, params IEnumerable<TValue> values)
        => new(column, [.. values.Select(value => SqlValue.From(value))]);
    #endregion
    #region 插入参数
    /// <summary>
    /// 插入参数
    /// </summary>
    /// <param name="column">列</param>
    /// <param name="parameterName"></param>
    /// <returns></returns>
    public static InsertValue Insert(this IColumn column, string parameterName = "")
         => new(column, Parameter.Use(parameterName, column));
    #endregion
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

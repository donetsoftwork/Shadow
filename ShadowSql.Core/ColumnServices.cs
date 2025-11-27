using ShadowSql.FieldInfos;
using ShadowSql.Identifiers;
using ShadowSql.Insert;
using ShadowSql.SingleSelect;
using ShadowSql.Values;
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
}

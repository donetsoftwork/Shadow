using ShadowSql.Fragments;
using System.Collections.Generic;

namespace ShadowSql.Identifiers;

/// <summary>
/// 原始表标识
/// </summary>
public interface ITable : IIdentifier, ITableView, IInsertTable, IUpdateTable
{
    /// <summary>
    /// 所有列
    /// </summary>
    IEnumerable<IColumn> Columns { get; }
    /// <summary>
    /// 获取单个列
    /// </summary>
    /// <param name="columName">列名</param>
    /// <returns></returns>
    IColumn? GetColumn(string columName);
}
/// <summary>
/// 插入表
/// </summary>
public interface IInsertTable : IIdentifier, ISqlEntity
{
    /// <summary>
    /// 所有插入列
    /// </summary>
    IEnumerable<IColumn> InsertColumns { get; }
    /// <summary>
    /// 获取插入列
    /// </summary>
    /// <param name="columnName">列名</param>
    /// <returns></returns>
    IColumn? GetInsertColumn(string columnName);
}
/// <summary>
/// 更新表
/// </summary>
public interface IUpdateTable : ISqlEntity
{
    /// <summary>
    /// 所有更新字段
    /// </summary>
    IEnumerable<IAssignView> AssignFields { get; }
    /// <summary>
    /// 获取更新字段
    /// </summary>
    /// <param name="fieldName">字段名</param>
    /// <returns></returns>
    IAssignView? GetAssignField(string fieldName);
}
/// <summary>
/// 表视图
/// </summary>
public interface ITableView : ISqlEntity
{
    /// <summary>
    /// 所有字段
    /// </summary>
    IEnumerable<IField> Fields { get; }
    /// <summary>
    /// 构造字段
    /// </summary>
    /// <param name="fieldName">字段名</param>
    /// <returns></returns>
    IField? GetField(string fieldName);
    /// <summary>
    /// 获取比较字段
    /// </summary>
    /// <param name="fieldName">字段名</param>
    /// <returns></returns>
    ICompareField GetCompareField(string fieldName);
    /// <summary>
    /// 构造新字段
    /// </summary>
    /// <param name="fieldName">字段名</param>
    /// <returns></returns>
    IField NewField(string fieldName);
}

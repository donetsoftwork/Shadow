using ShadowSql.Fragments;
using ShadowSql.Join;
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
    /// <param name="columName"></param>
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
    /// <param name="columnName"></param>
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
    /// <param name="fieldName"></param>
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
    /// <param name="fieldName"></param>
    /// <returns></returns>
    IField? GetField(string fieldName);
    /// <summary>
    /// 获取比较字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    ICompareField GetCompareField(string fieldName);
    /// <summary>
    /// 构造新字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    IField NewField(string fieldName);
}
/// <summary>
/// 多表视图
/// </summary>
public interface IMultiView : ITableView
{
    /// <summary>
    /// 表
    /// </summary>
    IEnumerable<IAliasTable> Tables { get; }
    /// <summary>
    /// 获取成员表
    /// </summary>
    /// <param name="tableName"></param>
    /// <returns></returns>
    IAliasTable? GetMember(string tableName);
}
/// <summary>
/// 联表
/// </summary>
public interface IJoinTable : IMultiView
{
    /// <summary>
    /// 连接
    /// </summary>
    IEnumerable<IJoinOn> JoinOns { get;  }
    /// <summary>
    /// 添加连接
    /// </summary>
    /// <param name="joinOn"></param>
    void AddJoinOn(IJoinOn joinOn);
}
/// <summary>
/// 分组视图
/// </summary>
public interface IGroupByView : ITableView
{
    /// <summary>
    /// 分组数据源表
    /// </summary>
    ITableView Source { get; }
    /// <summary>
    /// 分组字段
    /// </summary>
    IField[] GroupByFields { get; }
}

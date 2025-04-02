using ShadowSql.Fragments;
using ShadowSql.Join;
using ShadowSql.Queries;
using System.Collections.Generic;

namespace ShadowSql.Identifiers;

/// <summary>
/// 原始表标识
/// </summary>
public interface ITable : IIdentifier, IInsertTable, IUpdateTable, ISelectView, ITableView
{
}
/// <summary>
/// Select筛选视图
/// </summary>
public interface ISelectView : ISqlEntity
{
}
/// <summary>
/// 插入表
/// </summary>
public interface IInsertTable : IIdentifier, ISqlEntity
{
    /// <summary>
    /// 插入列
    /// </summary>
    IEnumerable<IColumn> InsertColumns { get; }
}
/// <summary>
/// 修改表
/// </summary>
public interface IUpdateTable : IView, ISqlEntity
{
    /// <summary>
    /// 修改列
    /// </summary>
    IEnumerable<IColumn> UpdateColumns { get; }
}
/// <summary>
/// 表视图
/// </summary>
public interface ITableView : ISqlEntity
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
    /// <summary>
    /// 构造字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    IField Field(string fieldName);
    /// <summary>
    /// 获取比较字段
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    ICompareField GetCompareField(string fieldName);
}
///// <summary>
///// 多表(联表)查询
///// </summary>
//public interface IMultiTableQuery : IDataSqlQuery, IMultiTable
//{
//}
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
/// 多表(联表)
/// </summary>
public interface IMultiTable : IMultiView
{
    /// <summary>
    /// 构造新成员名
    /// </summary>
    /// <returns></returns>
    string CreateMemberName();
    /// <summary>
    /// 添加表成员
    /// </summary>
    /// <param name="aliasTable"></param>
    void AddMember(IAliasTable aliasTable);
}

/// <summary>
/// 联表
/// </summary>
public interface IJoinTable : IMultiTable
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
    ///// <summary>
    ///// 数据源表
    ///// </summary>
    //ITableView Source { get; }
}
/// <summary>
/// 表别名
/// </summary>
public interface IAliasTable : IView, ITableView
{
    /// <summary>
    /// 别名
    /// </summary>
    string Alias { get; }
    /// <summary>
    /// 原始表
    /// </summary>
    public ITableView Target { get; }
    /// <summary>
    /// 前缀列
    /// </summary>
    IEnumerable<IPrefixColumn> PrefixColumns { get; }
    /// <summary>
    /// 获取前缀列
    /// </summary>
    /// <param name="columName"></param>
    /// <returns></returns>
    IPrefixColumn? GetPrefixColumn(string columName);
    /// <summary>
    /// 获取前缀列
    /// </summary>
    /// <param name="column"></param>
    /// <returns></returns>
    IPrefixColumn? GetPrefixColumn(IColumn column);
}

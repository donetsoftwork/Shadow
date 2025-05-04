using System.Collections.Generic;

namespace ShadowSql.Identifiers;

/// <summary>
/// 表别名
/// </summary>
/// <typeparam name="TTable"></typeparam>
public interface IAliasTable<out TTable> : IAliasTable
{
    /// <summary>
    /// 原始表
    /// </summary>
    new public TTable Target { get; }
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
    public ITable Target { get; }
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

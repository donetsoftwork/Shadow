using System.Collections.Generic;

namespace ShadowSql.Identifiers;

/// <summary>
/// 表别名
/// </summary>
/// <typeparam name="TTable"></typeparam>
public interface IAliasTable<out TTable> : IAliasTable
    where TTable : ITable
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
    /// 前缀字段
    /// </summary>
    IEnumerable<IPrefixField> PrefixFields { get; }
    /// <summary>
    /// 获取前缀字段
    /// </summary>
    /// <param name="fieldName">字段名</param>
    /// <returns></returns>
    IPrefixField? GetPrefixField(string fieldName);
    /// <summary>
    /// 获取前缀字段
    /// </summary>
    /// <param name="field">字段</param>
    /// <returns></returns>
    IPrefixField? GetPrefixField(IField field);
    /// <summary>
    /// 构造新前缀字段
    /// </summary>
    /// <param name="column">列</param>
    /// <returns></returns>
    IPrefixField NewPrefixField(IColumn column);
}

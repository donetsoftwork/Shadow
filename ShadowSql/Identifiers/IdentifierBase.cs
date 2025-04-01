using ShadowSql.Engines;
using ShadowSql.Fragments;
using System.Text;

namespace ShadowSql.Identifiers;

/// <summary>
/// 标识符基类
/// </summary>
/// <param name="name"></param>
public class IdentifierBase(string name)
    : IMatch, ISqlEntity
{
    /// <summary>
    /// 标识符名
    /// </summary>
    protected readonly string _name = name;
    /// <summary>
    /// 标识符名
    /// </summary>
    public string Name
        => _name;

    #region IMatch
    /// <summary>
    /// 是否匹配
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    internal virtual bool IsMatch(string name)
        => Identifier.Match(name, _name);
    bool IMatch.IsMatch(string name)
        => IsMatch(name);
    #endregion
    #region ISqlEntity
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    internal virtual void Write(ISqlEngine engine, StringBuilder sql)
        => engine.Identifier(sql, _name);
    void ISqlEntity.Write(ISqlEngine engine, StringBuilder sql)
        => Write(engine, sql);
    #endregion
}

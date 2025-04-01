using ShadowSql.Engines;
using System.Text;

namespace ShadowSql.Identifiers;

/// <summary>
/// 标识符基类
/// </summary>
/// <param name="name"></param>
public class IdentifierBase(string name)
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
    /// <summary>
    /// 是否匹配
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public virtual bool IsMatch(string name)
        => Identifier.Match(name, _name);
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    public virtual void Write(ISqlEngine engine, StringBuilder sql)
    {
        engine.Identifier(sql, _name);
        //return true;
    }
}

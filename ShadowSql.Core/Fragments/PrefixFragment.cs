using ShadowSql.Engines;
using System.Text;

namespace ShadowSql.Fragments;

/// <summary>
/// 前缀片段
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <param name="target"></param>
/// <param name="prefix">前缀</param>
public class PrefixFragment<TEntity>(TEntity target, params string[] prefix)
   : ISqlEntity
   where TEntity : ISqlEntity
{
    /// <summary>
    /// 被包裹片段
    /// </summary>
    protected readonly TEntity _target = target;
    /// <summary>
    /// 前缀
    /// </summary>
    protected readonly string[] _prefix = prefix;
    /// <summary>
    /// 被包裹片段
    /// </summary>
    public TEntity Target
        => _target;
    /// <summary>
    /// 前缀
    /// </summary>
    public string[] Prefix
        => _prefix;
    /// <inheritdoc/>
    public virtual void Write(ISqlEngine engine, StringBuilder sql)
    {
        engine.ConcatPrefix(_target, sql, _prefix);
    }
}

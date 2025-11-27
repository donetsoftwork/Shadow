using Hand.Structural;
using ShadowSql.Engines;
using System.Text;

namespace ShadowSql.Fragments;

/// <summary>
/// 前缀实体
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <param name="original">原始实体</param>
/// <param name="prefix">前缀</param>
public class PrefixEntity<TEntity>(TEntity original, params string[] prefix)
    : ISqlEntity
    , IWrapper<TEntity>
   where TEntity : ISqlEntity
{
    /// <summary>
    /// 原始实体
    /// </summary>
    protected readonly TEntity _original = original;
    /// <summary>
    /// 前缀
    /// </summary>
    protected readonly string[] _prefix = prefix;
    /// <summary>
    /// 原始实体
    /// </summary>
    public TEntity Original
        => _original;
    /// <summary>
    /// 前缀
    /// </summary>
    public string[] Prefix
        => _prefix;
    /// <inheritdoc/>
    public virtual void Write(ISqlEngine engine, StringBuilder sql)
    {
        engine.ConcatPrefix(_original, sql, _prefix);
    }
}

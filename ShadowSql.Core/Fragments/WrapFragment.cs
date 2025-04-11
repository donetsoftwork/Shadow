using ShadowSql.Engines;
using System.Text;

namespace ShadowSql.Fragments;

/// <summary>
/// 包裹片段
/// </summary>
/// <param name="prefix"></param>
/// <param name="target"></param>
/// <param name="suffix"></param>
public class WrapFragment<TEntity>(TEntity target, string[] prefix, params string[] suffix)
    : PrefixFragment<TEntity>(target, prefix), ISqlEntity
    where TEntity : ISqlEntity
{
    /// <summary>
    /// 后缀
    /// </summary>
    protected readonly string[] _suffix = suffix;
    /// <summary>
    /// 后缀
    /// </summary>
    public string[] Suffix
        => _suffix;
    /// <summary>
    /// 字符拼接
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    public override void Write(ISqlEngine engine, StringBuilder sql)
    {
        engine.ConcatPrefix(_target, sql, _prefix);
        foreach (string suffix in _suffix)
            sql.Append(suffix);
        //return true;
        //if (engine.ConcatPrefix(_target, sql, _prefix))
        //{
        //    foreach (string suffix in _suffix)
        //        sql.Append(suffix);
        //    return true;
        //}
        //return false;
    }
}

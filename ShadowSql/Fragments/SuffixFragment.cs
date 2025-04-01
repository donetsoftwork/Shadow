using ShadowSql.Engines;
using System.Text;

namespace ShadowSql.Fragments;

/// <summary>
/// 后缀片段
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <param name="target"></param>
/// <param name="suffix"></param>
public class SuffixFragment<TEntity>(TEntity target, params string[] suffix)
    : ISqlEntity
    where TEntity : ISqlEntity
{
    #region 配置
    /// <summary>
    /// 被包裹片段
    /// </summary>
    protected readonly TEntity _target = target;
    /// <summary>
    /// 后缀
    /// </summary>
    protected readonly string[] _suffix = suffix;
    /// <summary>
    /// 被包裹片段
    /// </summary>
    public TEntity Target
        => _target;
    /// <summary>
    /// 后缀
    /// </summary>
    public string[] Suffix
        => _suffix;
    #endregion
    /// <summary>
    /// 字符拼接
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    public virtual void Write(ISqlEngine engine, StringBuilder sql)
    {
        _target.Write(engine, sql);
        foreach (string suffix in _suffix)
            sql.Append(suffix);
        //if (_target.Write(engine, sql))
        //{
        //    foreach (string suffix in _suffix)
        //        sql.Append(suffix);
        //    return true;
        //}
        //return false;
    }
}

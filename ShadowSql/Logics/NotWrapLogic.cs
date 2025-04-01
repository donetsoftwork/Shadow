using ShadowSql.Engines;
using System.Text;

namespace ShadowSql.Logics;

/// <summary>
/// 否定包装逻辑
/// </summary>
/// <param name="target"></param>
public class NotWrapLogic(AtomicLogic target)
    : AtomicLogic
{
    #region 配置
    /// <summary>
    /// 被包裹片段
    /// </summary>
    private readonly AtomicLogic _target = target;
    /// <summary>
    /// 被包裹片段
    /// </summary>
    public AtomicLogic Target
        => _target;
    #endregion
    #region AtomicLogic
    /// <summary>
    /// 拼写sql
    /// 复合条件被否定需要加小括号
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    public override bool TryWrite(ISqlEngine engine, StringBuilder sql)
    {
        var point = sql.Length;
        engine.LogicNot(sql);
        sql.Append('(');
        if (_target.TryWrite(engine, sql))
        {
            sql.Append(')');
            return true;
        }
        //回滚
        sql.Length = point;
        return false;
    }
    /// <summary>
    /// 负负得正,返回被否定逻辑
    /// </summary>
    /// <returns></returns>
    public override AtomicLogic Not()
        => _target;
    #endregion
}

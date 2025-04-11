using ShadowSql.Engines;
using System.Text;

namespace ShadowSql.Logics;

/// <summary>
/// 否定逻辑
/// </summary>
/// <param name="target"></param>
public class NotLogic(AtomicLogic target)
    : AtomicLogic
{
    /// <summary>
    /// 被否定逻辑
    /// </summary>
    protected readonly AtomicLogic _target = target;
    /// <summary>
    /// 被否定逻辑
    /// </summary>
    public ISqlLogic Target 
        => _target;
    #region AtomicLogic
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    public override bool TryWrite(ISqlEngine engine, StringBuilder sql)
    {
        var point = sql.Length;
        engine.LogicNot(sql);
        if(_target.TryWrite(engine, sql))
            return true;
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

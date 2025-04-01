using ShadowSql.Compares;
using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.SqlVales;
using System.Text;

namespace ShadowSql.CompareLogics;

/// <summary>
/// 比较逻辑
/// </summary>
/// <param name="field"></param>
/// <param name="op"></param>
/// <param name="value"></param>
public class CompareLogic(ICompareView field, CompareSymbol op, ISqlValue value)
    : CompareLogicBase(field, op)
{
    /// <summary>
    /// 比较参数(或字段)
    /// </summary>
    protected readonly ISqlValue _value = value;

    /// <summary>
    /// 比较参数(或字段)
    /// </summary>
    public ISqlValue Value
        => _value;
    /// <summary>
    /// 否定逻辑
    /// </summary>
    /// <returns></returns>
    public override AtomicLogic Not()
        => new CompareLogic(_field, _operation.Not(), _value);
    /// <summary>
    /// 拼写sql
    /// </summary>
    /// <param name="engine"></param>
    /// <param name="sql"></param>
    /// <returns></returns>
    public override bool TryWrite(ISqlEngine engine, StringBuilder sql)
    {
        _field.Write(engine, sql);
        _operation.Write(engine, sql);
        _value.Write(engine, sql);
        return true;
        //var point = sql.Length;
        //if (_field.Write(engine, sql))
        //{
        //    _operation.Write(engine, sql);
        //    if (_value.Write(engine, sql))
        //        return true;
        //}
        ////回滚
        //sql.Length = point;
        //return false;
    }
}

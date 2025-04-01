using ShadowSql.Compares;
using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using System.Text;

namespace ShadowSql.CompareLogics;

/// <summary>
/// IS NOT NULL判断
/// </summary>
/// <param name="field"></param>
public class NotNullLogic(ICompareView field)
    : CompareLogicBase(field, CompareSymbol.NotNull)
{
    /// <summary>
    /// 否定逻辑
    /// </summary>
    /// <returns></returns>
    public override AtomicLogic Not()
        => new IsNullLogic(_field);
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
        return true;
        //if (_field.Write(engine, sql))
        //{
        //    _operation.Write(engine, sql);
        //    return true;
        //}
        //return false;
    }
}

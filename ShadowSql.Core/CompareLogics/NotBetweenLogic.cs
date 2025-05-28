using ShadowSql.Compares;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.SqlVales;

namespace ShadowSql.CompareLogics;

/// <summary>
/// NOT BETWEEN(AND)条件
/// </summary>
/// <param name="field"></param>
/// <param name="begin"></param>
/// <param name="end"></param>
public class NotBetweenLogic(ICompareView field, ICompareView begin, ICompareView end)
    : BetweenLogic(field, CompareSymbol.NotBetween, begin, end)
{
    /// <summary>
    /// 否定逻辑
    /// </summary>
    /// <returns></returns>
    public override AtomicLogic Not()
    {
        return new BetweenLogic(_field, _value, _end);
    }
}

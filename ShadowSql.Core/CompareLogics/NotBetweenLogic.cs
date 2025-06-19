using ShadowSql.Compares;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.SqlVales;

namespace ShadowSql.CompareLogics;

/// <summary>
/// NOT BETWEEN(AND)条件
/// </summary>
/// <param name="field">字段</param>
/// <param name="begin">范围开始</param>
/// <param name="end">范围结束</param>
public class NotBetweenLogic(ICompareView field, ICompareView begin, ICompareView end)
    : BetweenLogic(field, CompareSymbol.NotBetween, begin, end)
{
    /// <inheritdoc/>
    public override AtomicLogic Not()
    {
        return new BetweenLogic(_field, _value, _end);
    }
}

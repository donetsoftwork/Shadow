using ShadowSql.Compares;
using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using System.Text;

namespace ShadowSql.CompareLogics;

/// <summary>
/// IS NOT NULL判断
/// </summary>
/// <param name="field">字段</param>
public class NotNullLogic(ICompareView field)
    : CompareLogicBase(field, CompareSymbol.NotNull)
{
    /// <inheritdoc/>
    public override AtomicLogic Not()
        => new IsNullLogic(_field);
    /// <inheritdoc/>
    public override bool TryWrite(ISqlEngine engine, StringBuilder sql)
    {
        _field.Write(engine, sql);
        _operation.Write(engine, sql);
        return true;
    }
}

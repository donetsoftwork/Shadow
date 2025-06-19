using ShadowSql.Compares;
using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using System.Text;

namespace ShadowSql.CompareLogics;

/// <summary>
/// IS NULL判断
/// </summary>
/// <param name="field">字段</param>
public class IsNullLogic(ICompareView field)
    : CompareLogicBase(field, CompareSymbol.IsNull)
{
    /// <inheritdoc/>
    public override AtomicLogic Not()
    {
        return new NotNullLogic(_field);
    }
    /// <inheritdoc/>
    public override bool TryWrite(ISqlEngine engine, StringBuilder sql)
    {
        _field.Write(engine, sql);
        _operation.Write(engine, sql);
        return true;;
    }
}

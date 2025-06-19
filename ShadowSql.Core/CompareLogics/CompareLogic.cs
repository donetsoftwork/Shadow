using ShadowSql.Compares;
using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using System.Text;

namespace ShadowSql.CompareLogics;

/// <summary>
/// 比较逻辑
/// </summary>
/// <param name="field">字段</param>
/// <param name="op">操作</param>
/// <param name="value">值</param>
public class CompareLogic(ICompareView field, CompareSymbol op, ICompareView value)
    : CompareLogicBase(field, op)
{
    /// <summary>
    /// 比较参数(或字段)
    /// </summary>
    protected readonly ICompareView _value = value;

    /// <summary>
    /// 比较参数(或字段)
    /// </summary>
    public ICompareView Value
        => _value;
    /// <inheritdoc/>
    public override AtomicLogic Not()
        => new CompareLogic(_field, _operation.Not(), _value);
    /// <inheritdoc/>
    public override bool TryWrite(ISqlEngine engine, StringBuilder sql)
    {
        _field.Write(engine, sql);
        _operation.Write(engine, sql);
        _value.Write(engine, sql);
        return true;
    }
}

using ShadowSql.Compares;
using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using System.Text;

namespace ShadowSql.CompareLogics;

/// <summary>
/// BETWEEN(AND)条件
/// </summary>
public class BetweenLogic : CompareLogic
{
    /// <summary>
    /// BETWEEN(AND)条件
    /// </summary>
    /// <param name="field">字段</param>
    /// <param name="begin">范围开始</param>
    /// <param name="end">范围结束</param>
    public BetweenLogic(ICompareView field, ICompareView begin, ICompareView end)
        : this(field, CompareSymbol.Between, begin, end)
    {
    }
    /// <summary>
    /// BETWEEN(AND)条件
    /// </summary>
    /// <param name="field">字段</param>
    /// <param name="op">操作</param>
    /// <param name="begin">范围开始</param>
    /// <param name="end">范围结束</param>
    protected BetweenLogic(ICompareView field, CompareSymbol op, ICompareView begin, ICompareView end)
        : base(field, op, begin)
    {
        _end = end;
    }
    /// <summary>
    /// 下限
    /// </summary>
    protected readonly ICompareView _end;
    /// <summary>
    /// 下限
    /// </summary>
    public ICompareView End 
        => _end;
    /// <inheritdoc/>
    public override bool TryWrite(ISqlEngine engine, StringBuilder sql)
    {
        _field.Write(engine, sql);
        _operation.Write(engine, sql);
        _value.Write(engine, sql);
        sql.Append(" AND ");
        _end.Write(engine, sql);
        return true;
    }
    /// <inheritdoc/>
    public override AtomicLogic Not()
    {
        return new NotBetweenLogic(_field, _value, _end);
    }
}

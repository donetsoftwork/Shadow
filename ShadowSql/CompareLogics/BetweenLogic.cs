using ShadowSql.Compares;
using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.SqlVales;
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
    /// <param name="field"></param>
    /// <param name="begin"></param>
    /// <param name="end"></param>
    public BetweenLogic(ICompareView field, ISqlValue begin, ISqlValue end)
        : this(field, CompareSymbol.Between, begin, end)
    {
    }
    /// <summary>
    /// BETWEEN(AND)条件
    /// </summary>
    /// <param name="field"></param>
    /// <param name="op"></param>
    /// <param name="begin"></param>
    /// <param name="end"></param>
    protected BetweenLogic(ICompareView field, CompareSymbol op, ISqlValue begin, ISqlValue end)
        : base(field, op, begin)
    {
        _end = end;
    }
    /// <summary>
    /// 下限
    /// </summary>
    protected readonly ISqlValue _end;
    /// <summary>
    /// 下限
    /// </summary>
    public ISqlValue End 
        => _end;
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
        sql.Append(" AND ");
        _end.Write(engine, sql);
        return true;
    }
    /// <summary>
    /// 否定逻辑
    /// </summary>
    /// <returns></returns>
    public override AtomicLogic Not()
    {
        return new NotBetweenLogic(_field, _value, _end);
    }
}

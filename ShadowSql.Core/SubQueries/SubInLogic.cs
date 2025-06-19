using ShadowSql.Compares;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.SingleSelect;

namespace ShadowSql.SubQueries;

/// <summary>
/// IN子查询逻辑
/// </summary>
/// <param name="field">字段</param>
/// <param name="select">筛选</param>
public class SubInLogic(ICompareView field, ISingleSelect select)
    : FieldSubLogicBase(field, CompareSymbol.In, select)
{
    /// <inheritdoc/>
    public override AtomicLogic Not()
        => new SubNotInLogic(_field, _singleSelect);
}

/// <summary>
/// NOT IN子查询逻辑
/// </summary>
/// <param name="field">字段</param>
/// <param name="select">筛选</param>
public class SubNotInLogic(ICompareView field, ISingleSelect select)
    : FieldSubLogicBase(field, CompareSymbol.NotIn, select)
{
    /// <inheritdoc/>
    public override AtomicLogic Not()
        => new SubInLogic(_field, _singleSelect);
}

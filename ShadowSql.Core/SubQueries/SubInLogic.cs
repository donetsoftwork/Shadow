using ShadowSql.Compares;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.SingleSelect;

namespace ShadowSql.SubQueries;

/// <summary>
/// IN子查询逻辑
/// </summary>
/// <param name="field"></param>
/// <param name="select"></param>
public class SubInLogic(ICompareView field, ISingleSelect select)
    : FieldSubLogicBase(field, CompareSymbol.In, select)
{
    /// <summary>
    /// 反逻辑
    /// </summary>
    /// <returns></returns>
    public override AtomicLogic Not()
        => new SubNotInLogic(_field, _singleSelect);
}

/// <summary>
/// NOT IN子查询逻辑
/// </summary>
/// <param name="field"></param>
/// <param name="select"></param>
public class SubNotInLogic(ICompareView field, ISingleSelect select)
    : FieldSubLogicBase(field, CompareSymbol.NotIn, select)
{
    /// <summary>
    /// 反逻辑
    /// </summary>
    /// <returns></returns>
    public override AtomicLogic Not()
        => new SubInLogic(_field, _singleSelect);
}

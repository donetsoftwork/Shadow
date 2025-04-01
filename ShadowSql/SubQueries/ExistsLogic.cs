using ShadowSql.Compares;
using ShadowSql.Identifiers;
using ShadowSql.Logics;

namespace ShadowSql.SubQueries;

/// <summary>
/// EXISTS子查询逻辑
/// </summary>
/// <param name="source"></param>
public class ExistsLogic(ITableView source)
    : ExistsLogicBase(source, CompareSymbol.Exists)
{
    /// <summary>
    /// 反逻辑
    /// </summary>
    /// <returns></returns>
    public override AtomicLogic Not()
        => new NotExistsLogic(_source);
}

/// <summary>
/// NOT EXISTS子查询逻辑
/// </summary>
/// <param name="source"></param>
public class NotExistsLogic(ITableView source)
    : ExistsLogicBase(source, CompareSymbol.NotExists)
{
    /// <summary>
    /// 反逻辑
    /// </summary>
    /// <returns></returns>
    public override AtomicLogic Not()
        => new ExistsLogic(_source);
}

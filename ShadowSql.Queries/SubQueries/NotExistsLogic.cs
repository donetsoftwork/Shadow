using ShadowSql.Compares;
using ShadowSql.Identifiers;
using ShadowSql.Logics;

namespace ShadowSql.SubQueries;

/// <summary>
/// NOT EXISTS子查询逻辑
/// </summary>
/// <param name="view"></param>
public class NotExistsLogic(ITableView view)
    : ExistsLogicBase(view, CompareSymbol.NotExists)
{
    /// <inheritdoc/>
    public override AtomicLogic Not()
        => new ExistsLogic(_source);
}

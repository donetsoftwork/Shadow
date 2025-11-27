using ShadowSql.Compares;
using ShadowSql.Identifiers;
using ShadowSql.Logics;

namespace ShadowSql.SubQueries;

/// <summary>
/// EXISTS子查询逻辑
/// </summary>
/// <param name="view"></param>
public class ExistsLogic(ITableView view)
    : ExistsLogicBase(view, CompareSymbol.Exists)
{
    /// <inheritdoc/>
    public override AtomicLogic Not()
        => new NotExistsLogic(_source);
}

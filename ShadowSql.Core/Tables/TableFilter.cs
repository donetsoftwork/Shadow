using ShadowSql.Filters;
using ShadowSql.Identifiers;
using ShadowSql.Logics;

namespace ShadowSql.Tables;

/// <summary>
/// 表数据过滤
/// </summary>
/// <param name="view"></param>
/// <param name="filter">过滤条件</param>
public class TableFilter(ITableView view, ISqlLogic filter)
    : DataFilterBase<ISqlLogic>(view, filter)
{
}

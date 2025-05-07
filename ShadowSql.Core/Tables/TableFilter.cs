using ShadowSql.Filters;
using ShadowSql.Identifiers;
using ShadowSql.Logics;

namespace ShadowSql.Tables;

/// <summary>
/// 表数据过滤
/// </summary>
/// <param name="source"></param>
/// <param name="filter"></param>
public class TableFilter(ITableView source, ISqlLogic filter)
    : DataFilterBase<ISqlLogic>(source, filter)
{
}

using ShadowSql.Filters;
using ShadowSql.Identifiers;
using ShadowSql.Logics;

namespace ShadowSql.Tables;

/// <summary>
/// 表数据过滤
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="source"></param>
/// <param name="filter"></param>
public class TableFilter<TTable>(TTable source, ISqlLogic filter)
    : SqlLogicFilterBase<TTable>(source, filter)
    where TTable : ITableView
{
}

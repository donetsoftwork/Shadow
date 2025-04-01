using ShadowSql.Logics;

namespace ShadowSql.Identifiers;

/// <summary>
/// 表数据过滤
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="source"></param>
/// <param name="filter"></param>
public class TableFilter<TTable>(TTable source, ISqlLogic filter)
    : DataFilterBase<TTable, ISqlLogic>(source, filter)
    where TTable : ITableView
{
}

using ShadowSql.Identifiers;

namespace ShadowSql.Select;

/// <summary>
/// 联表分组后再筛选列基类
/// </summary>
/// <typeparam name="TSource"></typeparam>
public abstract class GroupByMultiSelectBase<TSource>(TSource source, IGroupByView groupBy, IMultiView multiView)
    : GroupBySelectBase<TSource, IMultiView>(source, groupBy, multiView)
    where TSource : ITableView
{

}

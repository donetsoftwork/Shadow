using ShadowSql.Identifiers;
using ShadowSql.Logics;

namespace ShadowSql.GroupBy;

/// <summary>
/// 对MultiQuery进行分组查询
/// </summary>
/// <param name="multiTable"></param>
/// <param name="fields"></param>
/// <param name="filter"></param>
public class GroupByMultiQuery(IMultiView multiTable, IFieldView[] fields, Logic filter)
    : GroupByQueryBase<IMultiView>(multiTable, fields, filter)
{
    /// <summary>
    /// 对多表进行分组查询
    /// </summary>
    /// <param name="multiTable"></param>
    /// <param name="fields"></param>
    public GroupByMultiQuery(IMultiView multiTable, IFieldView[] fields)
        : this(multiTable, fields, new AndLogic())
    {
    }
}

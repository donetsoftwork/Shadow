using ShadowSql.Identifiers;

namespace ShadowSql.Expressions.Cursors;

/// <summary>
/// 分组游标基类
/// </summary>
/// <param name="groupBy">分组查询</param>
/// <param name="limit">筛选数量</param>
/// <param name="offset">跳过数量</param>
public abstract class GroupByCursorBase(IGroupByView groupBy, int limit, int offset)
    : CursorBase<IGroupByView>(groupBy, limit, offset)
{
}

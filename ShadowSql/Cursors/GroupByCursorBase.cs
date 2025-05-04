using ShadowSql.Identifiers;

namespace ShadowSql.Cursors;

/// <summary>
/// 分组游标基类
/// </summary>
/// <param name="groupBy"></param>
/// <param name="limit"></param>
/// <param name="offset"></param>
public abstract class GroupByCursorBase(IGroupByView groupBy, int limit, int offset)
    : CursorBase<IGroupByView>(groupBy, limit, offset)
{
}

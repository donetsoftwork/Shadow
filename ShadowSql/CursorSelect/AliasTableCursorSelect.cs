using ShadowSql.Cursors;
using ShadowSql.Identifiers;
using ShadowSql.Variants;

namespace ShadowSql.CursorSelect;

/// <summary>
/// 别名表范围(分页)及列筛选
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="cursor"></param>
public sealed class AliasTableCursorSelect<TTable>(AliasTableCursor<TTable> cursor)
    : CursorSelectBase<TableAlias<TTable>>(cursor, cursor.Source)
    where TTable : ITable
{
}

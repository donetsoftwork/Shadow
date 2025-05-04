using ShadowSql.Cursors;
using ShadowSql.Identifiers;
using ShadowSql.Variants;

namespace ShadowSql.CursorSelect;

/// <summary>
/// GroupBy别名表后再范围(分页)及列筛选
/// </summary>
/// <typeparam name="TTable"></typeparam>
/// <param name="cursor"></param>
public sealed class GroupByAliasTableCursorSelect<TTable>(GroupByAliasTableCursor<TTable> cursor)
    : GroupCursorBySelectBase<IAliasTable<TTable>>(cursor, cursor.Source, cursor.AliasTable)
    where TTable : ITable
{
}

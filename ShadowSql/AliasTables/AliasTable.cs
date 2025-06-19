using ShadowSql.Identifiers;
using ShadowSql.Variants;

namespace ShadowSql.AliasTables;

/// <inheritdoc />
public class AliasTable(Table target, string tableAlias)
    : TableAlias<Table>(target, tableAlias)
{
}

using ShadowSql.Identifiers;
using ShadowSql.Variants;

namespace ShadowSql.AliasTables;

/// <summary>
/// 别名表
/// </summary>
public class AliasTable(Table target, string tableAlias)
    : TableAlias<Table>(target, tableAlias)
{
}

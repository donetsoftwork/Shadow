using ShadowSql.Identifiers;
using ShadowSql.Variants;

namespace ShadowSqlTest;

internal class CommentAliasTable : TableAlias<Table>
{
    public CommentAliasTable(string tableAlias)
        : this(new Table("Comments"), tableAlias)
    {
    }
    private CommentAliasTable(Table table, string tableAlias)
        : base(table, tableAlias)
    {
        Id = AddColumn(Column.Use(nameof(Id)));
        PostId = AddColumn(Column.Use(nameof(PostId)));
        Content = AddColumn(Column.Use(nameof(Content)));
        Pick = AddColumn(Column.Use(nameof(Pick)));
    }
    public readonly IPrefixColumn Id;
    public readonly IPrefixColumn PostId;
    public readonly IPrefixColumn Content;
    public readonly IPrefixColumn Pick;
}

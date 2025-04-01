using ShadowSql.Identifiers;
using ShadowSql.Variants;

namespace ShadowSqlTest;

internal class PostAliasTable : TableAlias<Table>
{
    public PostAliasTable(string tableAlias)
        : this(new Table("Posts"), tableAlias)
    {
    }
    private PostAliasTable(Table table, string tableAlias)
        : base(table, tableAlias)
    {
        Id = AddColumn(Column.Use(nameof(Id)));
        Title = AddColumn(Column.Use(nameof(Title)));
        Author = AddColumn(Column.Use(nameof(Author)));
    }
    //Id, Title, Author
    public readonly IPrefixColumn Id;
    public readonly IPrefixColumn Title;
    public readonly IPrefixColumn Author;
}

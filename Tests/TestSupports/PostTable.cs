using ShadowSql.Identifiers;
using ShadowSql.Variants;

namespace TestSupports;

public class PostTable : Table
{
    public PostTable(string tableName = "Posts")
        : base(tableName)
    {
        Id = DefineColumn(nameof(Id));
        Title = DefineColumn(nameof(Title));
        Author = DefineColumn(nameof(Author));
        AuthorId = DefineColumn(nameof(AuthorId));
    }
    //Id, Title, Author
    public readonly IColumn Id;
    public readonly IColumn Title;
    public readonly IColumn Author;
    public readonly IColumn AuthorId;
}

public class PostAliasTable : TableAlias<PostTable>
{
    public PostAliasTable(string tableAlias)
        : this(new PostTable("Posts"), tableAlias)
    {
    }
    public PostAliasTable(PostTable table, string tableAlias)
        : base(table, tableAlias)
    {
        Id = AddColumn(table.Id);
        Title = AddColumn(table.Title);
        Author = AddColumn(table.Author);
        AuthorId = AddColumn(table.AuthorId);
    }
    //Id, Title, Author
    public readonly IPrefixField Id;
    public readonly IPrefixField Title;
    public readonly IPrefixField Author;
    public readonly IPrefixField AuthorId;
}

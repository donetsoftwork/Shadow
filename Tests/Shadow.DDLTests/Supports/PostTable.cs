using Shadow.DDL.Schemas;
using ShadowSql.Identifiers;
using ShadowSql.Variants;

namespace Shadow.DDLTests.Supports;

public class PostTable(string tableName = "Posts", string schema = "")
    : TableSchema(tableName, [Defines.Id, Defines.Title, Defines.Author, Defines.AuthorId], schema)
{ 
    //Id, Title, Author
    public readonly ColumnSchema Id = Defines.Id;
    public readonly ColumnSchema Title = Defines.Title;
    public readonly ColumnSchema Author = Defines.Author;
    public readonly ColumnSchema AuthorId = Defines.AuthorId;

    class Defines
    {
        public static readonly ColumnSchema Id = new("Id") { ColumnType = ColumnType.Key };
        public static readonly ColumnSchema Title = new("Title");
        public static readonly ColumnSchema Author = new("Author");
        public static readonly ColumnSchema AuthorId = new("AuthorId");
    }
}

public class PostAliasTable : TableAlias<PostTable>
{
    public PostAliasTable(string tableAlias, string schema = "")
        : this(new PostTable("Posts", schema), tableAlias)
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

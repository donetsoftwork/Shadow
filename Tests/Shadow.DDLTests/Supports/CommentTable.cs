using Shadow.DDL.Schemas;
using ShadowSql.Identifiers;
using ShadowSql.Variants;

namespace Shadow.DDLTests.Supports;


public class CommentTable(string tableName = "Comments", string schema = "")
    : TableSchema(tableName, [Defines.Id, Defines.UserId, Defines.PostId, Defines.Content, Defines.Pick], schema)
{
    public readonly ColumnSchema Id = Defines.Id;
    public readonly ColumnSchema UserId = Defines.UserId;
    public readonly ColumnSchema PostId = Defines.PostId;
    public readonly ColumnSchema Content = Defines.Content;
    public readonly ColumnSchema Pick = Defines.Pick;

    class Defines
    {
        public static readonly ColumnSchema Id = new("Id") { ColumnType = ColumnType.Key };
        public static readonly ColumnSchema UserId = new("UserId");
        public static readonly ColumnSchema PostId = new("PostId");
        public static readonly ColumnSchema Content = new("Content");
        public static readonly ColumnSchema Pick = new("Pick");
    }
}

public class CommentAliasTable : TableAlias<CommentTable>
{
    public CommentAliasTable(string tableAlias, string schema = "")
        : this(new CommentTable("Comments", schema), tableAlias)
    {
    }
    public CommentAliasTable(CommentTable table, string tableAlias)
        : base(table, tableAlias)
    {
        Id = AddColumn(table.Id);
        UserId = AddColumn(table.UserId);
        PostId = AddColumn(table.PostId);
        Content = AddColumn(table.Content);
        Pick = AddColumn(table.Pick);
    }
    public readonly IPrefixField Id;
    public readonly IPrefixField UserId;
    public readonly IPrefixField PostId;
    public readonly IPrefixField Content;
    public readonly IPrefixField Pick;
}
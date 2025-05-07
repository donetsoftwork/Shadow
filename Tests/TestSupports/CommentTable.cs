using ShadowSql.Identifiers;
using ShadowSql.Variants;

namespace TestSupports;


public class CommentTable : Table
{
    public CommentTable(string tableName = "Comments")
        : base(tableName)
    {
        Id = DefineColumn(nameof(Id));
        UserId = DefineColumn(nameof(UserId));
        PostId = DefineColumn(nameof(PostId));
        Content = DefineColumn(nameof(Content));
        Pick = DefineColumn(nameof(Pick));
    }
    public readonly IColumn Id;
    public readonly IColumn UserId;
    public readonly IColumn PostId;
    public readonly IColumn Content;
    public readonly IColumn Pick;
}

public class CommentAliasTable : TableAlias<CommentTable>
{
    public CommentAliasTable(string tableAlias)
        : this(new CommentTable("Comments"), tableAlias)
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
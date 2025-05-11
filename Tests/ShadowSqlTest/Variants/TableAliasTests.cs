using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.FieldQueries;
using ShadowSql.Identifiers;
using TestSupports;

namespace ShadowSqlTest.Variants;

public class TableAliasTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void JoinSelect()
    {
        var c = new Table("Comments")
            .As("c");
        var p = new Table("Posts")
            .As("p");
        var select = c.SqlJoin(p)
            .OnColumn("PostId", "Id")
            .ToSelect()
            .SelectTable(c);
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT c.* FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id]", sql);
    }
    [Fact]
    public void JoinUpdate()
    {
        var c = new Table("Comments")
            .As("c");
        var p = new Table("Posts")
            .As("p");
        var update = c.SqlJoin(p)
            .OnColumn("PostId", "Id")
            .Root
            .Where(p.Field("Author").LikeValue("%专家"))
            .ToUpdate()
            .Update(c)
            .SetEqualToValue("Pick", true);
        var sql = _engine.Sql(update);
        Assert.Equal("UPDATE c SET c.[Pick]=1 FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] WHERE p.[Author] LIKE '%专家'", sql);
    }
    [Fact]
    public void JoinDelete()
    {
        var c = new Table("Comments")
            .As("c");
        var p = new Table("Posts")
            .As("p");
        var delete = c.SqlJoin(p)
            .OnColumn("PostId", "Id")
            .Root
            .TableFieldEqualValue("p", "Author", "王二")
            .ToDelete()
            .Delete(c);
        var sql = _engine.Sql(delete);
        Assert.Equal("DELETE c FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] WHERE p.[Author]='王二'", sql);
    }
    [Fact]
    public void Define()
    {
        CommentAliasTable c = new("c");
        PostAliasTable p = new("p");
        var delete = c.SqlJoin(p)
            .On(c.PostId, p.Id)
            .Root
            .Where(p.Author.EqualValue("王二"))
            .ToDelete()
            .Delete(c);
        var sql = _engine.Sql(delete);
        Assert.Equal("DELETE c FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] WHERE p.[Author]='王二'", sql);
    }
}

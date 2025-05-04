using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Join;
using TestSupports;


namespace ShadowSqlTest.Delete;

public class MultiTableDeleteTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void Query()
    {
        CommentAliasTable c = new("c");
        PostAliasTable p = new("p");
        var delete = c.Join(p)
            .And(c.PostId.Equal(p.Id))
            .Root
            .And(p.Author.EqualValue("张三"))
            .ToDelete();
        var sql = _engine.Sql(delete);
        Assert.Equal("DELETE c FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] WHERE p.[Author]='张三'", sql);
    }
    [Fact]
    public void SqlQuery()
    {
        CommentAliasTable c = new("c");
        PostAliasTable p = new("p");
        var delete = c.SqlJoin(p)
            .On(c.PostId, p.Id)
            .Root
            .Where(p.Author.EqualValue("张三"))
            .ToDelete();
        var sql = _engine.Sql(delete);
        Assert.Equal("DELETE c FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] WHERE p.[Author]='张三'", sql);
    }
    [Fact]
    public void Delete()
    {
        CommentAliasTable c = new("c");
        PostAliasTable p = new("p");
        var delete = c.SqlJoin(p)
            .On(c.PostId, p.Id)
            .Root
            .Where(p.Author.EqualValue("张三"))
            .ToDelete()
            .Delete(c);
        var sql = _engine.Sql(delete);
        Assert.Equal("DELETE c FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] WHERE p.[Author]='张三'", sql);
    }
    [Fact]
    public void Delete2()
    {
        var joinOn = JoinOnSqlQuery.Create("Comments", "Posts")
            .OnColumn("PostId", "Id");
        var query = joinOn.Root
            .Where("Posts", p => p.Field("Author").EqualValue("张三"));
        var delete = query.ToDelete()
            .Delete("Comments");
        var sql = _engine.Sql(delete);
        Assert.Equal("DELETE t1 FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] WHERE t2.[Author]='张三'", sql);
    }
}

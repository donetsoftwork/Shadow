using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using TestSupports;

namespace ShadowSqlTest.Join;

public class AliasJoinOnQueryTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly DB _db = DB.Use("MyDb");

    [Fact]
    public void Join()
    {
        CommentAliasTable c = new("c");
        PostAliasTable p = new("p");
        var query = c.Join(p)
            .And(c.PostId.Equal(p.Id));
        var sql = _engine.Sql(query.Root);
        Assert.Equal("[Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id]", sql);
    }
    [Fact]
    public void Join2()
    {
        var query = new CommentAliasTable("c")
            .Join(new PostAliasTable("p"))
            .Apply((q, c, p) => q.And(c.PostId.Equal(p.Id)));
        var sql = _engine.Sql(query.Root);
        Assert.Equal("[Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id]", sql);
    }
    [Fact]
    public void LeftTableJoin()
    {
        CommentAliasTable c = new("c");
        PostAliasTable p = new("p");
        UserAliasTable u = new("u");
        var query = c.Join(p)
            .And(c.PostId.Equal(p.Id))
            .LeftTableJoin(u)
            .And(c.UserId.Equal(u.Id));
        var sql = _engine.Sql(query.Root);
        Assert.Equal("[Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] INNER JOIN [Users] AS u ON c.[UserId]=u.[Id]", sql);
    }
    [Fact]
    public void LeftTableJoin2()
    {
        var query = new CommentAliasTable("c")
            .Join(new PostAliasTable("p"))
            .Apply((q, c, p) => q.And(c.PostId.Equal(p.Id)))
            .LeftTableJoin(new UserAliasTable("u"))
            .Apply((q, c, u) => q.And(c.UserId.Equal(u.Id)));
        var sql = _engine.Sql(query.Root);
        Assert.Equal("[Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] INNER JOIN [Users] AS u ON c.[UserId]=u.[Id]", sql);
    }
    [Fact]
    public void RightTableJoin()
    {
        PostAliasTable p = new("p");
        CommentAliasTable c = new("c");
        UserAliasTable u = new("u");
        var query = p.Join(c)
            .And(p.Id.Equal(c.PostId))
            .RightTableJoin(u)
            .And(c.UserId.Equal(u.Id));
        var sql = _engine.Sql(query.Root);
        Assert.Equal("[Posts] AS p INNER JOIN [Comments] AS c ON p.[Id]=c.[PostId] INNER JOIN [Users] AS u ON c.[UserId]=u.[Id]", sql);
    }
    [Fact]
    public void RightTableJoin2()
    {
        var query = new PostAliasTable("p")
            .Join(new CommentAliasTable("c"))
            .Apply((q, p, c) => q.And(p.Id.Equal(c.PostId)))
            .RightTableJoin(new UserAliasTable("u"))
            .Apply((q, c, u) => q.And(c.UserId.Equal(u.Id)));
        var sql = _engine.Sql(query.Root);
        Assert.Equal("[Posts] AS p INNER JOIN [Comments] AS c ON p.[Id]=c.[PostId] INNER JOIN [Users] AS u ON c.[UserId]=u.[Id]", sql);
    }
    [Fact]
    public void ApplyLeft()
    {
        var query = new CommentAliasTable("c")
            .Join(new PostAliasTable("p"))
            .Apply((q, c, p) => q.And(c.PostId.Equal(p.Id)))
            .ApplyLeft((q, c) => q.And(c.Pick.EqualValue(true)));
        var sql = _engine.Sql(query.Root);
        Assert.Equal("[Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] WHERE c.[Pick]=1", sql);
    }
    [Fact]
    public void ApplyRight()
    {
        var query = new PostAliasTable("p")
            .Join(new CommentAliasTable("c"))
            .Apply((q, p, c) => q.And(p.Id.Equal(c.PostId)))            
            .ApplyRight((q, c) => q.And(c.Pick.EqualValue(true)));
        var sql = _engine.Sql(query.Root);
        Assert.Equal("[Posts] AS p INNER JOIN [Comments] AS c ON p.[Id]=c.[PostId] WHERE c.[Pick]=1", sql);
    }
}

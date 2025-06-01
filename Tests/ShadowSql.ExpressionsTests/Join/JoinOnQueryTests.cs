using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Expressions;
using ShadowSql.ExpressionsTests.Supports;
using ShadowSql.Tables;

namespace ShadowSql.ExpressionsTests.Join;

public class JoinOnQueryTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void Join()
    {
        var query = EmptyTable.Use("Users")
            .Join<User, UserRole>(EmptyTable.Use("UserRoles"))
            .And((u, r) => u.Id == r.UserId);
        var sql = _engine.Sql(query.Root);
        Assert.Equal("[Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId]", sql);
    }
    [Fact]
    public void Join2()
    {
        var query = EmptyTable.Use("Users")
            .As("u")
            .Join<User, UserRole>(EmptyTable.Use("UserRoles").As("r"))
            .And((u, r) => u.Id == r.UserId);
        var sql = _engine.Sql(query.Root);
        Assert.Equal("[Users] AS u INNER JOIN [UserRoles] AS r ON u.[Id]=r.[UserId]", sql);
    }
    [Fact]
    public void And()
    {
        var query = EmptyTable.Use("Users")
            .Join<User, UserRole>(EmptyTable.Use("UserRoles"))
            .And((u, r) => u.Id == r.UserId);
        var sql = _engine.Sql(query.Root);
        Assert.Equal("[Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId]", sql);
    }
    [Fact]
    public void And2()
    {
        var query = EmptyTable.Use("Users")
            .As("u")
            .Join<User, UserRole>(EmptyTable.Use("UserRoles").As("r"))
            .And((u, r) => u.Id == r.UserId);
        var sql = _engine.Sql(query.Root);
        Assert.Equal("[Users] AS u INNER JOIN [UserRoles] AS r ON u.[Id]=r.[UserId]", sql);
    }
    [Fact]
    public void LeftTableJoin()
    {
        var query = EmptyTable.Use("Comments")
            .Join<Comment, Post>(EmptyTable.Use("Posts"))
            .And((c, p) => c.PostId == p.Id)
            .LeftTableJoin<Comment, Post, User>(EmptyTable.Use("Users"))
            .And((c, u) => c.UserId == u.Id);
        var sql = _engine.Sql(query.Root);
        Assert.Equal("[Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] INNER JOIN [Users] AS t3 ON t1.[UserId]=t3.[Id]", sql);
    }
    [Fact]
    public void LeftTableJoin2()
    {
        var query = EmptyTable.Use("Comments")
            .As("c")
            .Join<Comment, Post>(EmptyTable.Use("Posts").As("p"))
            .And((c, p) => c.PostId == p.Id)
            .LeftTableJoin<Comment, Post, User>(EmptyTable.Use("Users").As("u"))
            .And((c, u) => c.UserId == u.Id);
        var sql = _engine.Sql(query.Root);
        Assert.Equal("[Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] INNER JOIN [Users] AS u ON c.[UserId]=u.[Id]", sql);
    }
    [Fact]
    public void RightTableJoin()
    {
        var query = EmptyTable.Use("Posts")
            .Join<Post, Comment>(EmptyTable.Use("Comments"))
            .And((c, p) => c.Id == p.PostId)
            .RightTableJoin<Post, Comment, User>(EmptyTable.Use("Users"))
            .And((c, u) => c.UserId == u.Id);
        var sql = _engine.Sql(query.Root);
        Assert.Equal("[Posts] AS t1 INNER JOIN [Comments] AS t2 ON t1.[Id]=t2.[PostId] INNER JOIN [Users] AS t3 ON t2.[UserId]=t3.[Id]", sql);
    }
    [Fact]
    public void RightTableJoin2()
    {
        var query = EmptyTable.Use("Posts")
            .As("p")
            .Join<Post, Comment>(EmptyTable.Use("Comments").As("c"))
            .And((c, p) => c.Id == p.PostId)
            .RightTableJoin<Post, Comment, User>(EmptyTable.Use("Users").As("u"))
            .And((c, u) => c.UserId == u.Id);
        var sql = _engine.Sql(query.Root);
        Assert.Equal("[Posts] AS p INNER JOIN [Comments] AS c ON p.[Id]=c.[PostId] INNER JOIN [Users] AS u ON c.[UserId]=u.[Id]", sql);
    }
}

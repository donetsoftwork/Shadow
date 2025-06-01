using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Expressions;
using ShadowSql.ExpressionsTests.Supports;
using ShadowSql.Tables;

namespace ShadowSql.ExpressionsTests.Join;

public class JoinOnSqlQueryTest
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void OnKey()
    {
        var query = EmptyTable.Use("Users")
            .SqlJoin<User, UserRole>(EmptyTable.Use("UserRoles"))
            .On(u => u.Id, r => r.UserId);
        var sql = _engine.Sql(query.Root);
        Assert.Equal("[Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId]", sql);
    }
    [Fact]
    public void On()
    {
        var query = EmptyTable.Use("Users")
            .SqlJoin<User, UserRole>(EmptyTable.Use("UserRoles"))
            .On((u, r) => u.Id == r.UserId);
        var sql = _engine.Sql(query.Root);
        Assert.Equal("[Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId]", sql);
    }
    [Fact]
    public void On2()
    {
        var query = EmptyTable.Use("Users")
            .As("u")
            .SqlJoin<User, UserRole>(EmptyTable.Use("UserRoles").As("r"))
            .On((u, r) => u.Id == r.UserId);
        var sql = _engine.Sql(query.Root);
        Assert.Equal("[Users] AS u INNER JOIN [UserRoles] AS r ON u.[Id]=r.[UserId]", sql);
    }
    [Fact]
    public void On3()
    {
        var query = EmptyTable.Use("Users")
            .SqlJoin<User, UserRole>(EmptyTable.Use("UserRoles"))
            .On((u, r) => u.Id == r.UserId + 1);
        var sql = _engine.Sql(query.Root);
        Assert.Equal("[Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=(t2.[UserId]+1)", sql);
    }
    [Fact]
    public void LeftTableJoin()
    {
        var query = EmptyTable.Use("Comments")
            .SqlJoin<Comment, Post>(EmptyTable.Use("Posts"))
            .On((c, p) => c.PostId == p.Id)
            .LeftTableJoin<Comment, Post, User>(EmptyTable.Use("Users"))
            .On((c, u) => c.UserId == u.Id);
        var sql = _engine.Sql(query.Root);
        Assert.Equal("[Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] INNER JOIN [Users] AS t3 ON t1.[UserId]=t3.[Id]", sql);
    }
    [Fact]
    public void LeftTableJoin2()
    {
        var query = EmptyTable.Use("Comments")
            .As("c")
            .SqlJoin<Comment, Post>(EmptyTable.Use("Posts").As("p"))
            .On((c, p) => c.PostId == p.Id)
            .LeftTableJoin<Comment, Post, User>(EmptyTable.Use("Users").As("u"))
            .On((c, u) => c.UserId == u.Id);
        var sql = _engine.Sql(query.Root);
        Assert.Equal("[Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] INNER JOIN [Users] AS u ON c.[UserId]=u.[Id]", sql);
    }
    [Fact]
    public void RightTableJoin()
    {
        var query = EmptyTable.Use("Posts")
            .SqlJoin<Post, Comment>(EmptyTable.Use("Comments"))
            .On((c, p) => c.Id == p.PostId)
            .RightTableJoin<Post, Comment, User>(EmptyTable.Use("Users"))
            .On((c, u) => c.UserId == u.Id);
        var sql = _engine.Sql(query.Root);
        Assert.Equal("[Posts] AS t1 INNER JOIN [Comments] AS t2 ON t1.[Id]=t2.[PostId] INNER JOIN [Users] AS t3 ON t2.[UserId]=t3.[Id]", sql);
    }
    [Fact]
    public void RightTableJoin2()
    {
        var query = EmptyTable.Use("Posts")
            .As("p")
            .SqlJoin<Post, Comment>(EmptyTable.Use("Comments").As("c"))
            .On((c, p) => c.Id == p.PostId)
            .RightTableJoin<Post, Comment, User>(EmptyTable.Use("Users").As("u"))
            .On((c, u) => c.UserId == u.Id);
        var sql = _engine.Sql(query.Root);
        Assert.Equal("[Posts] AS p INNER JOIN [Comments] AS c ON p.[Id]=c.[PostId] INNER JOIN [Users] AS u ON c.[UserId]=u.[Id]", sql);
    }
    [Fact]
    public void OnLeft()
    {
        var query = EmptyTable.Use("Users")
            .SqlJoin<User, UserRole>(EmptyTable.Use("UserRoles"))
            .AsRightJoin()
            .On((u, r) => u.Id == r.UserId)
            .OnLeft(u => u.Status);
        var sql = _engine.Sql(query.Root);
        Assert.Equal("[Users] AS t1 RIGHT JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId] AND t1.[Status]=1", sql);
    }
    [Fact]
    public void OnRight()
    {
        var query = EmptyTable.Use("Users")
            .SqlJoin<User, UserRole>(EmptyTable.Use("UserRoles"))
            .AsLeftJoin()
            .On((u, r) => u.Id == r.UserId)
            .OnRight(r => r.Score >= 60);
        var sql = _engine.Sql(query.Root);
        Assert.Equal("[Users] AS t1 LEFT JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId] AND t2.[Score]>=60", sql);
    }
}

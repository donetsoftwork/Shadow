using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using TestSupports;

namespace ShadowSqlTest.Join;

public class AliasJoinOnSqlQueryTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly DB _db = DB.Use("MyDb");

    [Fact]
    public void SqlJoin()
    {
        var joinOn = _db.From("Employees")
            .As("e")
            .SqlJoin(_db.From("Departments").As("d"))
            .OnColumn("DepartmentId", "Id");
        var sql = _engine.Sql(joinOn.Root);
        Assert.Equal("[Employees] AS e INNER JOIN [Departments] AS d ON e.[DepartmentId]=d.[Id]", sql);
    }
    [Fact]
    public void LeftTableJoin()
    {
        CommentAliasTable c = new("c");
        PostAliasTable p = new("p");
        UserAliasTable u = new("u");
        var query = c.SqlJoin(p)
            .On(c.PostId, p.Id)
            .LeftTableJoin(u)
            .On(c.UserId, u.Id);
        var sql = _engine.Sql(query.Root);
        Assert.Equal("[Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] INNER JOIN [Users] AS u ON c.[UserId]=u.[Id]", sql);
    }
    [Fact]
    public void LeftTableJoin2()
    {
        CommentAliasTable c = new("c");
        PostAliasTable p = new("p");
        UserAliasTable u = new("u");
        var query = new CommentAliasTable("c")
            .SqlJoin(new PostAliasTable("p"))
            .On(c => c.PostId, p => p.Id)
            .LeftTableJoin(new UserAliasTable("u"))
            .On(c.UserId, u.Id);
        var sql = _engine.Sql(query.Root);
        Assert.Equal("[Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] INNER JOIN [Users] AS u ON c.[UserId]=u.[Id]", sql);
    }
    [Fact]
    public void RightTableJoin()
    {
        PostAliasTable p = new("p");
        CommentAliasTable c = new("c");
        UserAliasTable u = new("u");
        var query = p.SqlJoin(c)
            .On(p.Id, c.PostId)
            .RightTableJoin(u)
            .On(c.UserId, u.Id);
        var sql = _engine.Sql(query.Root);
        Assert.Equal("[Posts] AS p INNER JOIN [Comments] AS c ON p.[Id]=c.[PostId] INNER JOIN [Users] AS u ON c.[UserId]=u.[Id]", sql);
    }
    [Fact]
    public void RightTableJoin2()
    {
        var query = new PostAliasTable("p")
            .SqlJoin(new CommentAliasTable("c"))
            .On(p => p.Id, c => c.PostId)
            .RightTableJoin(new UserAliasTable("u"))
            .On(c => c.UserId, u => u.Id);
        var sql = _engine.Sql(query.Root);
        Assert.Equal("[Posts] AS p INNER JOIN [Comments] AS c ON p.[Id]=c.[PostId] INNER JOIN [Users] AS u ON c.[UserId]=u.[Id]", sql);
    }
    [Fact]
    public void On()
    {
        var query = new PostAliasTable("p")
            .SqlJoin(new CommentAliasTable("c"))
            .On(p => p.Id, c => c.PostId);
        var sql = _engine.Sql(query.Root);
        Assert.Equal("[Posts] AS p INNER JOIN [Comments] AS c ON p.[Id]=c.[PostId]", sql);
    }
    [Fact]
    public void On2()
    {
        var query = new PostAliasTable("p")
            .SqlJoin(new CommentAliasTable("c"))
            .On((p, c) => p.Id.Equal(c.PostId));
        var sql = _engine.Sql(query.Root);
        Assert.Equal("[Posts] AS p INNER JOIN [Comments] AS c ON p.[Id]=c.[PostId]", sql);
    }
    [Fact]
    public void OnLeft()
    {
        var query = new CommentAliasTable("c")
            .SqlJoin(new PostAliasTable("p"))
            .AsRightJoin()
            .On(c => c.PostId, p => p.Id)
            .OnLeft(c => c.Pick.EqualValue(true));
        var sql = _engine.Sql(query.Root);
        Assert.Equal("[Comments] AS c RIGHT JOIN [Posts] AS p ON c.[PostId]=p.[Id] AND c.[Pick]=1", sql);
    }
    [Fact]
    public void OnRight()
    {
        var query = new PostAliasTable("p")
            .SqlJoin(new CommentAliasTable("c"))
            .AsLeftJoin()
            .On(p => p.Id, c => c.PostId)
            .OnRight(c => c.Pick.EqualValue(true));
        var sql = _engine.Sql(query.Root);
        Assert.Equal("[Posts] AS p LEFT JOIN [Comments] AS c ON p.[Id]=c.[PostId] AND c.[Pick]=1", sql);
    }
    [Fact]
    public void Apply()
    {
        var query = new CommentAliasTable("c")
            .SqlJoin(new PostAliasTable("p"))
            .AsRightJoin()
            .Apply((q, c, p) => q
                .And(c.PostId.Equal(p.Id))
                .And(c.Pick.EqualValue(true))
            );
        var sql = _engine.Sql(query.Root);
        Assert.Equal("[Comments] AS c RIGHT JOIN [Posts] AS p ON c.[PostId]=p.[Id] AND c.[Pick]=1", sql);
    }
}

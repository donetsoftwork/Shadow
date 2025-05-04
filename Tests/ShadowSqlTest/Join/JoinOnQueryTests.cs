using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Simples;
using TestSupports;

namespace ShadowSqlTest.Join;

public class JoinOnQueryTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly DB _db = DB.Use("MyDb");

    [Fact]
    public void Apply()
    {
        var query = new CommentTable()
            .Join(new PostTable())
            .Apply(
                c => c.PostId,
                p => p.Id,
                (q, PostId, Id) => q.And(PostId.Equal(Id))
            );
        var sql = _engine.Sql(query.Root);
        Assert.Equal("[Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id]", sql);
    }

    [Fact]
    public void LeftTableJoin()
    {
        var query = new CommentTable()
            .Join(new PostTable())
            .Apply(
                c => c.PostId,
                p => p.Id,
                (q, PostId, Id) => q.And(PostId.Equal(Id))
            )
            .LeftTableJoin(new UserTable())
            .Apply(
                c => c.UserId,
                u => u.Id,
                (q, UserId, Id) => q.And(UserId.Equal(Id))
            );
        var sql = _engine.Sql(query.Root);
        Assert.Equal("[Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] INNER JOIN [Users] AS t3 ON t1.[UserId]=t3.[Id]", sql);
    }
    [Fact]
    public void RightTableJoin()
    {
        var query = new PostTable()
            .Join(new CommentTable())
            .Apply(
                p => p.Id,
                c => c.PostId,
                (q, Id, PostId) => q.And(Id.Equal(PostId))
            )
            .RightTableJoin(new UserTable())
            .Apply(
                c => c.UserId,
                u => u.Id,
                (q, UserId, Id) => q.And(UserId.Equal(Id))
            );
        var sql = _engine.Sql(query.Root);
        Assert.Equal("[Posts] AS t1 INNER JOIN [Comments] AS t2 ON t1.[Id]=t2.[PostId] INNER JOIN [Users] AS t3 ON t2.[UserId]=t3.[Id]", sql);
    }
    [Fact]
    public void ApplyLeft()
    {
        var query = new CommentTable()
            .Join(new PostTable())
            .Apply(
                c => c.PostId,
                p => p.Id,
                (q, PostId, Id) => q.And(PostId.Equal(Id))
            )
            .ApplyLeft(c => c.Pick, (q, Pick) => q.And(Pick.EqualValue(true)));
        var sql = _engine.Sql(query.Root);
        Assert.Equal("[Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] WHERE t1.[Pick]=1", sql);
    }
    [Fact]
    public void ApplyRight()
    {
        var query = new PostTable()
            .Join(new CommentTable())
            .Apply(
                p => p.Id,
                c => c.PostId,
                (q, Id, PostId) => q.And(Id.Equal(PostId))
            )
            .ApplyRight(c => c.Pick, (q, Pick) => q.And(Pick.EqualValue(true)));
        var sql = _engine.Sql(query.Root);
        Assert.Equal("[Posts] AS t1 INNER JOIN [Comments] AS t2 ON t1.[Id]=t2.[PostId] WHERE t2.[Pick]=1", sql);
    }
}

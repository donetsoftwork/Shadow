using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using TestSupports;

namespace ShadowSqlTest.GroupBy;

public class GroupByMultiQueryTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void GroupBy0()
    {
        var query = new CommentAliasTable("c")
            .Join(new PostAliasTable("p"))
            .Apply(c => c.PostId, p => p.Id, (q, PostId, Id) => q.And(PostId.Equal(Id)))
            .GroupBy((c, p) => [c.PostId]);
        var sql = _engine.Sql(query);
        Assert.Equal("[Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] GROUP BY c.[PostId]", sql);
    }
    [Fact]
    public void GroupBy()
    {
        var query = new CommentTable()
            .Join(new PostTable())
            .Apply(c => c.PostId, p => p.Id, (q, PostId, Id) => q.And(PostId.Equal(Id)))
            .GroupBy((c, p) => [p.Id]);
        var sql = _engine.Sql(query);
        Assert.Equal("[Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] GROUP BY t2.[Id]", sql);
    }
    [Fact]
    public void GroupBy2()
    {
        CommentAliasTable c = new("c");
        PostAliasTable p = new("p");
        var query = c.Join(p)
            .And(c.PostId.Equal(p.Id))
            .Root
            .GroupBy(p.Id);
        var sql = _engine.Sql(query);
        Assert.Equal("[Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] GROUP BY p.[Id]", sql);
    }
    [Fact]
    public void GroupBy3()
    {
        CommentAliasTable c = new("c");
        PostAliasTable p = new("p");
        var query = c.Multi(p)
            .And(c.PostId.Equal(p.Id))
            .GroupBy(p.Id);
        var sql = _engine.Sql(query);
        Assert.Equal("[Comments] AS c,[Posts] AS p WHERE c.[PostId]=p.[Id] GROUP BY p.[Id]", sql);
    }
    [Fact]
    public void Apply()
    {
        var query = new CommentAliasTable("c")
            .Join(new PostAliasTable("p"))
            .Apply((q, c, p) => q.And(c.PostId.Equal(p.Id)))
            .GroupBy((c, p) => [c.PostId])
            .Apply<CommentAliasTable>("c", c => c.Pick.Max(), (q, Pick) => q.And(Pick.GreaterValue(10)));
        var sql = _engine.Sql(query);
        Assert.Equal("[Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] GROUP BY c.[PostId] HAVING MAX(c.[Pick])>10", sql);
    }
    [Fact]
    public void Apply2()
    {
        var query = new CommentTable()
            .Join(new PostTable())
            .Apply(c => c.PostId, p => p.Id, (q, PostId, Id) => q.And(PostId.Equal(Id)))
            .GroupBy((c, p) => [c.PostId])
            .Apply<CommentTable>("Comments", c => c.Pick, Pick => Pick.Max(), (q, Pick) => q.And(Pick.GreaterValue(10)));
        var sql = _engine.Sql(query);
        Assert.Equal("[Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] GROUP BY t1.[PostId] HAVING MAX(t1.[Pick])>10", sql);
    }
}

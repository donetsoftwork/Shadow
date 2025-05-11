using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Tables;
using TestSupports;

namespace ShadowSqlTest.Cursors;

public class GroupByMultiCursorTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void ToCursor()
    {
        var cursor = SimpleTable.Use("Employees")
            .SqlJoin(SimpleTable.Use("Departments"))
            .OnColumn("DepartmentId", "Id")
            .Root
            .SqlGroupBy("Manager")
            .ToCursor()
	        .CountAsc();
        var sql = _engine.Sql(cursor);
        Assert.Equal("[Employees] AS t1 INNER JOIN [Departments] AS t2 ON t1.[DepartmentId]=t2.[Id] GROUP BY [Manager] ORDER BY COUNT(*)", sql);
    }
    [Fact]
    public void ToCursor2()
    {
        var cursor = new CommentTable()
            .Join(new PostTable())
            .Apply(c => c.PostId, p => p.Id, (q, PostId, Id) => q.And(PostId.Equal(Id)))
            .GroupBy((c, p) => [p.Id])
            .ToCursor()
            .CountDesc();
        var sql = _engine.Sql(cursor);
        Assert.Equal("[Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] GROUP BY t2.[Id] ORDER BY COUNT(*) DESC", sql);
    }
    [Fact]
    public void ToCursor3()
    {
        CommentAliasTable c = new("c");
        PostAliasTable p = new("p");
        var cursor = c.Join(p)
            .And(c.PostId.Equal(p.Id))
            .Root
            .GroupBy(p.Id)
            .ToCursor()
            .CountDesc();
        var sql = _engine.Sql(cursor);
        Assert.Equal("[Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] GROUP BY p.[Id] ORDER BY COUNT(*) DESC", sql);
    }
    [Fact]
    public void AggregateAsc()
    {
        var cursor = new CommentTable()
            .Join(new PostTable())
            .Apply(c => c.PostId, p => p.Id, (q, PostId, Id) => q.And(PostId.Equal(Id)))
            .GroupBy((c, p) => [p.Id])
            .ToCursor()
            .AggregateAsc<CommentTable>("t1", c => c.Pick, Pick => Pick.Sum());
        var sql = _engine.Sql(cursor);
        Assert.Equal("[Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] GROUP BY t2.[Id] ORDER BY SUM(t1.[Pick])", sql);
    }
    [Fact]
    public void AggregateAsc2()
    {
        CommentAliasTable c = new("c");
        PostAliasTable p = new("p");
        var cursor = c.SqlJoin(p)
            .On(c.PostId, p.Id)
            .Root
            .SqlGroupBy(p.Id)
            .ToCursor()
            .AggregateAsc<CommentTable>("c", c => c.Pick, Pick => Pick.Sum());
        var sql = _engine.Sql(cursor);
        Assert.Equal("[Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] GROUP BY p.[Id] ORDER BY SUM(c.[Pick])", sql);
    }

    [Fact]
    public void AggregateDesc()
    {
        var cursor = new CommentTable()
            .Join(new PostTable())
            .Apply(c => c.PostId, p => p.Id, (q, PostId, Id) => q.And(PostId.Equal(Id)))
            .GroupBy((c, p) => [p.Id])
            .ToCursor()
            .AggregateDesc<CommentTable>("t1", c => c.Pick, Pick => Pick.Sum());
        var sql = _engine.Sql(cursor);
        Assert.Equal("[Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] GROUP BY t2.[Id] ORDER BY SUM(t1.[Pick]) DESC", sql);
    }
    [Fact]
    public void AggregateDesc2()
    {
        CommentAliasTable c = new("c");
        PostAliasTable p = new("p");
        var cursor = c.SqlJoin(p)
            .On(c.PostId, p.Id)
            .Root
            .SqlGroupBy(p.Id)
            .ToCursor()
            .AggregateDesc<CommentTable>("c", c => c.Pick, Pick => Pick.Sum());
        var sql = _engine.Sql(cursor);
        Assert.Equal("[Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] GROUP BY p.[Id] ORDER BY SUM(c.[Pick]) DESC", sql);
    }
}

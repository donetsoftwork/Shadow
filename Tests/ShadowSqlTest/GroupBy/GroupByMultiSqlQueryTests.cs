using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using TestSupports;

namespace ShadowSqlTest.GroupBy;

public class GroupByMultiSqlQueryTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly IDB _db = new DB("MyDb");

    [Fact]
    public void SqlGroupBy()
    {
        var query = _db.From("Employees")
            .SqlJoin(_db.From("Departments"))
            .OnColumn("DepartmentId", "Id")
            .Root
            .SqlGroupBy("Manager");
        var sql = _engine.Sql(query);
        Assert.Equal("[Employees] AS t1 INNER JOIN [Departments] AS t2 ON t1.[DepartmentId]=t2.[Id] GROUP BY [Manager]", sql);
    }
    [Fact]
    public void SqlGroupBy2()
    {
        CommentAliasTable c = new("c");
        PostAliasTable p = new("p");
        var query = c.SqlJoin(p)
            .On(c.PostId, p.Id)
            .Root
            .SqlGroupBy(p.Id);
        var sql = _engine.Sql(query);
        Assert.Equal("[Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] GROUP BY p.[Id]", sql);
    }
    [Fact]
    public void SqlGroupBy3()
    {
        var query = _db.From("Employees")
            .SqlMulti(_db.From("Departments"))
            .Where("t1.DepartmentId=t2.Id")
            .SqlGroupBy("Manager");
        var sql = _engine.Sql(query);
        Assert.Equal("[Employees] AS t1,[Departments] AS t2 WHERE t1.DepartmentId=t2.Id GROUP BY [Manager]", sql);
    }
    [Fact]
    public void SqlGroupBy4()
    {
        CommentAliasTable c = new("c");
        PostAliasTable p = new("p");
        var query = c.SqlMulti(p)
            .Where(c.PostId.Equal(p.Id))
            .SqlGroupBy(p.Id);
        var sql = _engine.Sql(query);
        Assert.Equal("[Comments] AS c,[Posts] AS p WHERE c.[PostId]=p.[Id] GROUP BY p.[Id]", sql);
    }
    [Fact]
    public void SqlGroupBy5()
    {
        var query = new CommentAliasTable("c")
            .SqlJoin(new PostAliasTable("p"))
            .On(c => c.PostId, p => p.Id)
            .SqlGroupBy((c, p) => [c.PostId]);
        var sql = _engine.Sql(query);
        Assert.Equal("[Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] GROUP BY c.[PostId]", sql);
    }
    [Fact]
    public void SqlGroupBy6()
    {
        var query = new CommentTable()
            .SqlJoin(new PostTable())
            .On(c => c.PostId, p => p.Id)
            .SqlGroupBy((c, p) => [c.PostId]);
        var sql = _engine.Sql(query);
        Assert.Equal("[Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] GROUP BY t1.[PostId]", sql);
    }
    [Fact]
    public void HavingAggregate()
    {
        var query = new CommentAliasTable("c")
            .SqlJoin(new PostAliasTable("p"))
            .On(c => c.PostId, p => p.Id)
            .SqlGroupBy((c, p) => [c.PostId])
            .HavingAggregate<CommentAliasTable>("c", c => c.Pick.Max(), Pick => Pick.GreaterValue(40));
        var sql = _engine.Sql(query);
        Assert.Equal("[Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] GROUP BY c.[PostId] HAVING MAX(c.[Pick])>40", sql);
    }
    [Fact]
    public void HavingAggregate2()
    {
        var query = new CommentTable()
            .SqlJoin(new PostTable())
            .On(c => c.PostId, p => p.Id)
            .SqlGroupBy((c, p) => [c.PostId])
            .HavingAggregate<CommentTable>("Comments", c => c.Pick, Pick => Pick.Sum(), Pick => Pick.GreaterValue(10));
        var sql = _engine.Sql(query);
        Assert.Equal("[Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] GROUP BY t1.[PostId] HAVING SUM(t1.[Pick])>10", sql);
    }
}
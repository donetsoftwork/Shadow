using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using TestSupports;

namespace ShadowSqlTest.Cursors;

public class GroupByTableCursorTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly DB _db = DB.Use("MyDb");

    [Fact]
    public void ToCursor()
    {
        var cursor = _db.From("Employees")
            .SqlGroupBy("DepartmentId")
            .ToCursor()
            .CountAsc();
        var sql = _engine.Sql(cursor);
        Assert.Equal("[Employees] GROUP BY [DepartmentId] ORDER BY COUNT(*)", sql);
    }
    [Fact]
    public void ToCursor2()
    {
        var cursor = _db.From("Employees")
            .GroupBy("DepartmentId")
            .ToCursor()
            .AggregateDesc(g => g.Max("Age"));
        var sql = _engine.Sql(cursor);
        Assert.Equal("[Employees] GROUP BY [DepartmentId] ORDER BY MAX([Age]) DESC", sql);
    }
    [Fact]
    public void Asc()
    {
        var cursor = _db.From("Employees")
            .SqlGroupBy("DepartmentId")
            .ToCursor(10, 20)
            .CountAsc();
        var sql = _engine.Sql(cursor);
        Assert.Equal("[Employees] GROUP BY [DepartmentId] ORDER BY COUNT(*)", sql);
    }
    [Fact]
    public void AggregateAsc()
    {
        var cursor = new CommentTable()
            .SqlGroupBy(c => [c.PostId])
            .ToCursor()
            .AggregateAsc(c => c.Pick.Sum());
        var sql = _engine.Sql(cursor);
        Assert.Equal("[Comments] GROUP BY [PostId] ORDER BY SUM([Pick])", sql);
    }
    [Fact]
    public void AggregateDesc()
    {
        var cursor = new CommentTable()
            .GroupBy(c => [c.PostId])
            .ToCursor()
            .AggregateDesc(c => c.Pick.Sum());
        var sql = _engine.Sql(cursor);
        Assert.Equal("[Comments] GROUP BY [PostId] ORDER BY SUM([Pick]) DESC", sql);
    }
}

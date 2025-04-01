using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Simples;

namespace ShadowSqlTest.Fetches;

public class TableFetchTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly IDB _db = SimpleDB.Use("MyDb");

    [Fact]
    public void ToFetch()
    {
        int limit = 10;
        int offset = 10;
        var fetch = _db.From("Users")
            .ToFetch(limit, offset);
        Assert.Equal(limit, fetch.Limit);
        Assert.Equal(offset, fetch.Offset);
    }
    [Fact]
    public void Cursor()
    {
        int limit = 10;
        int offset = 10;
        var fetch = _db.From("Users")
            .ToFetch()
            .Skip(offset)
            .Take(limit);
        Assert.Equal(limit, fetch.Limit);
        Assert.Equal(offset, fetch.Offset);
    }
    [Fact]
    public void Where()
    {
        var age = Column.Use("Age");
        var where = age.GreaterValue(30);
        var fetch = _db.From("Users")
            .ToFetch(where);
        var sql = _engine.Sql(fetch);
        Assert.Equal("[Users] WHERE [Age]>30", sql);
    }
    [Fact]
    public void OrderBy()
    {
        var fetch = _db.From("Users")
            .ToFetch()
            .OrderBy("Age DESC");
        var sql = _engine.Sql(fetch);
        Assert.Equal("[Users] ORDER BY Age DESC", sql);
    }
    [Fact]
    public void Desc()
    {
        var fetch = _db.From("Users")
            .ToFetch()
            .Desc("Age");
        var sql = _engine.Sql(fetch);
        Assert.Equal("[Users] ORDER BY [Age] DESC", sql);
    }
}

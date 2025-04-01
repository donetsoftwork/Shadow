using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Simples;

namespace ShadowSqlTest.Fetches;

public class AliasTableSelectTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly IDB _db = SimpleDB.Use("MyDb");

    [Fact]
    public void ToFetch()
    {
        int limit = 10;
        int offset = 10;
        var fetch = _db.From("Users")
            .As("u")
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
            .As("u")
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
            .As("u")
            .ToFetch(where);
        var sql = _engine.Sql(fetch);
        Assert.Equal("[Users] AS u WHERE [Age]>30", sql);
    }
    [Fact]
    public void Asc()
    {
        var fetch = _db.From("Users")
            .As("u")
            .ToFetch()
            .Asc("Age");
        var sql = _engine.Sql(fetch);
        Assert.Equal("[Users] AS u ORDER BY [Age]", sql);
    }
    [Fact]
    public void AscField()
    {
        var age = Column.Use("Age");
        var fetch = _db.From("Users")
            .As("u")
            .ToFetch()
            .Asc(age);
        var sql = _engine.Sql(fetch);
        Assert.Equal("[Users] AS u ORDER BY [Age]", sql);
    }
}

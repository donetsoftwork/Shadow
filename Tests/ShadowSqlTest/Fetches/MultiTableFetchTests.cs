using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Join;

namespace ShadowSqlTest.Fetches;

public class MultiTableFetchTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly DB _db = DB.Use("MyDb");

    [Fact]
    public void ToFetch()
    {
        int limit = 10;
        int offset = 10;
        var fetch = new MultiTableQuery()
            .AddMember(_db.From("Employees").As("e"))
            .AddMember(_db.From("Departments").As("d"))
            .ToFetch(limit, offset);
        Assert.Equal(limit, fetch.Limit);
        Assert.Equal(offset, fetch.Offset);
    }

    [Fact]
    public void AscMuti()
    {
        var fetch = new MultiTableQuery()
            .AddMember(_db.From("Employees").As("e"))
            .AddMember(_db.From("Departments").As("d"))
            .ToFetch()
            .Asc(muti => muti.From("Employees").Field("Age"));
        var sql = _engine.Sql(fetch);
        Assert.Equal("[Employees] AS e,[Departments] AS d ORDER BY e.[Age]", sql);
    }

    [Fact]
    public void DescTable()
    {
        var fetch = new MultiTableQuery()
            .AddMember(_db.From("Employees").As("e"))
            .AddMember(_db.From("Departments").As("d"))
            .ToFetch()
            .Desc("e", e => e.Field("Id"));
        var sql = _engine.Sql(fetch);
        Assert.Equal("[Employees] AS e,[Departments] AS d ORDER BY e.[Id] DESC", sql);
    }
}

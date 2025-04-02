using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Simples;

namespace ShadowSqlTest.Fetches;

public class AliasTableCursorTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly IDB _db = SimpleDB.Use("MyDb");

    [Fact]
    public void ToCursor()
    {
        int limit = 10;
        int offset = 10;
        var cursor = _db.From("Users")
            .As("u")
            .ToCursor(limit, offset);
        Assert.Equal(limit, cursor.Limit);
        Assert.Equal(offset, cursor.Offset);
    }
    [Fact]
    public void Cursor()
    {
        int limit = 10;
        int offset = 10;
        var cursor = _db.From("Users")
            .As("u")
            .ToCursor()
            .Skip(offset)
            .Take(limit);
        Assert.Equal(limit, cursor.Limit);
        Assert.Equal(offset, cursor.Offset);
    }
    [Fact]
    public void Where()
    {
        var age = Column.Use("Age");
        var where = age.GreaterValue(30);
        var cursor = _db.From("Users")
            .As("u")
            .ToCursor(where);
        var sql = _engine.Sql(cursor);
        Assert.Equal("[Users] AS u WHERE [Age]>30", sql);
    }
    [Fact]
    public void Asc()
    {
        var cursor = _db.From("Users")
            .As("u")
            .ToCursor()
            .Asc("Age");
        var sql = _engine.Sql(cursor);
        Assert.Equal("[Users] AS u ORDER BY [Age]", sql);
    }
    [Fact]
    public void AscField()
    {
        var age = Column.Use("Age");
        var cursor = _db.From("Users")
            .As("u")
            .ToCursor()
            .Asc(age);
        var sql = _engine.Sql(cursor);
        Assert.Equal("[Users] AS u ORDER BY [Age]", sql);
    }
}

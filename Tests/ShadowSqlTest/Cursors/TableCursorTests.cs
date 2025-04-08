using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Simples;

namespace ShadowSqlTest.Cursors;

public class TableCursorTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly IDB _db = SimpleDB.Use("MyDb");

    [Fact]
    public void ToCursor()
    {
        int limit = 10;
        int offset = 10;
        var cursor = _db.From("Users")
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
            .ToCursor(where);
        var sql = _engine.Sql(cursor);
        Assert.Equal("[Users] WHERE [Age]>30", sql);
    }
    [Fact]
    public void OrderBy()
    {
        var cursor = _db.From("Users")
            .ToCursor()
            .OrderBy("Age DESC");
        var sql = _engine.Sql(cursor);
        Assert.Equal("[Users] ORDER BY Age DESC", sql);
    }
    [Fact]
    public void Desc()
    {
        var cursor = _db.From("Users")
            .ToCursor()
            .Desc("Age");
        var sql = _engine.Sql(cursor);
        Assert.Equal("[Users] ORDER BY [Age] DESC", sql);
    }
}

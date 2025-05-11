using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Tables;
using TestSupports;

namespace ShadowSqlTest.Cursors;

public class TableCursorTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly IDB _db = new DB("MyDb");

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
        var cursor = _db.From("Users")
            .ToCursor(age.GreaterValue(30), 10, 20);
        var sql = _engine.Sql(cursor);
        Assert.Equal("[Users] WHERE [Age]>30", sql);
    }
    [Fact]
    public void TableSqlQueryTest()
    {
        var cursor = _db.From("Users")
            .ToSqlQuery()
            .Where("Age>30")
            .ToCursor(10, 20);
        var sql = _engine.Sql(cursor);
        Assert.Equal("[Users] WHERE Age>30", sql);
    }
    [Fact]
    public void TableSqlQueryTest2()
    {
        var cursor = new TableSqlQuery("Users")
            .Where("Age>30")
            .ToCursor(10, 20);
        var sql = _engine.Sql(cursor);
        Assert.Equal("[Users] WHERE Age>30", sql);
    }
    [Fact]
    public void TableQueryTest()
    {
        var age = Column.Use("Age");
        var cursor = _db.From("Users")
            .ToQuery()
            .And(age.GreaterValue(30))
            .ToCursor(10, 20);
        var sql = _engine.Sql(cursor);
        Assert.Equal("[Users] WHERE [Age]>30", sql);
    }
    [Fact]
    public void TableQueryTest2()
    {
        var age = Column.Use("Age");
        var cursor = new TableQuery("Users")
            .And(age.GreaterValue(30))
            .ToCursor(10, 20);
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
    public void Asc()
    {
        var select = new UserTable()
            .ToCursor(10, 20)
            .Asc(table => table.Id);
        var sql = _engine.Sql(select);
        Assert.Equal("[Users] ORDER BY [Id]", sql);
    }
    [Fact]
    public void Desc()
    {
        var select = new UserTable()
            .ToCursor(10, 20)
            .Desc(table => table.Id);
        var sql = _engine.Sql(select);
        Assert.Equal("[Users] ORDER BY [Id] DESC", sql);
    }
    [Fact]
    public void Desc2()
    {
        var cursor = _db.From("Users")
            .ToCursor()
            .Desc("Age");
        var sql = _engine.Sql(cursor);
        Assert.Equal("[Users] ORDER BY [Age] DESC", sql);
    }
}

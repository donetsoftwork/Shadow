using ShadowSql;
using ShadowSql.Cursors;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Select;
using ShadowSql.Simples;
using ShadowSql.Tables;

namespace ShadowSqlCoreTest.Select;

public class TableSelectTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly IDB _db = SimpleDB.Use("MyDb");

    [Fact]
    public void Select()
    {
        var table = _db.From("Users");
        var select = new TableSelect(table);
        select.Select("Id", "Name");
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [Id],[Name] FROM [Users]", sql);
    }
    [Fact]
    public void Cursor()
    {
        int limit = 10;
        int offset = 10;
        var table = _db.From("Users");
        var cursor = new TableCursor(table)
            .Skip(offset)
            .Take(limit);
        var select = new CursorSelect(cursor);
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT * FROM [Users] OFFSET 10 ROWS FETCH NEXT 10 ROWS ONLY", sql);
    }
    [Fact]
    public void Desc()
    {
        var table = _db.From("Users");
        var cursor = new TableCursor(table)
            .Desc(u => u.Field("Age"))
            .Asc(u => u.Field("Id"));
        var select = new CursorSelect(cursor);
        select.Select("Id", "Name");
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [Id],[Name] FROM [Users] ORDER BY [Age] DESC,[Id]", sql);
    }

    [Fact]
    public void Logic()
    {
        var table = new UserTable();
        var query = new TableSqlQuery(table)
             .Where(table.Id.Less("LastId"))
             .Where(table.Status.EqualValue(true));
        var cursor = new TableCursor(query)
            .Desc(table.Id)
            .Skip(10)
            .Take(10);
        var select = new CursorSelect(cursor);

        var sql = _engine.Sql(select);
        Assert.Equal("SELECT * FROM [Users] WHERE [Id]<@LastId AND [Status]=1 ORDER BY [Id] DESC OFFSET 10 ROWS FETCH NEXT 10 ROWS ONLY", sql);
    }
}

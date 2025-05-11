using ShadowSql;
using ShadowSql.Cursors;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using ShadowSql.Select;
using ShadowSql.Tables;
using TestSupports;

namespace ShadowSqlCoreTest.Select;

public class CursorSelectTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly IDB _db = new DB("MyDb");

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

    [Fact]
    public void Table()
    {
        var table = _db.From("Users");
        var cursor = new TableCursor(table)
            .OrderBy("Id DESC")
            .Skip(20)
            .Take(10);
        var select = new CursorSelect(cursor)
            .Select("Id", "Name");
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [Id],[Name] FROM [Users] ORDER BY Id DESC OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY", sql);
    }
    [Fact]
    public void SqlQuery()
    {
        var table = new UserTable();
        var query = new TableSqlQuery(table)
            .Where(table.Status.EqualValue(true));
        var cursor = new TableCursor(query, 10, 20)
            .Desc(table.Id);
        var select = new CursorSelect(cursor)
            .Select(table.Id, table.Name);
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [Id],[Name] FROM [Users] WHERE [Status]=1 ORDER BY [Id] DESC OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY", sql);
    }
    [Fact]
    public void Query()
    {
        var table = new UserTable();
        var query = new TableQuery(table)
            .And(table.Status.EqualValue(true));
        var cursor = new TableCursor(query, 10, 20)
            .Desc(table.Id);
        var select = new CursorSelect(cursor)
            .Select(table.Id, table.Name);
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [Id],[Name] FROM [Users] WHERE [Status]=1 ORDER BY [Id] DESC OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY", sql);
    }
    [Fact]
    public void GroupBySqlQueryTest()
    {
        var query = new TableSqlQuery("Users")
            .Where("Age<20");
        var groupBy = GroupBySqlQuery.Create(query, "City");
        var cursor = new TableCursor(groupBy, 10, 20)
            .Desc(groupBy.Count());
        var select = new CursorSelect(cursor)
            .Select("City")
            .Select(groupBy.CountAs("CityCount"));
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [City],COUNT(*) AS CityCount FROM [Users] WHERE Age<20 GROUP BY [City] ORDER BY COUNT(*) DESC OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY", sql);
    }
    [Fact]
    public void GroupByQueryTest()
    {
        var table = new CommentTable();
        var query = new TableQuery(table)
            .And(table.Pick.EqualValue(true));
        var groupBy = GroupByQuery.Create(query, table.PostId);
        var cursor = new TableCursor(groupBy, 10, 20)
            .Desc(groupBy.Count());
        var select = new CursorSelect(cursor)
            .Select(table.PostId, groupBy.CountAs("CommentCount"));
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [PostId],COUNT(*) AS CommentCount FROM [Comments] WHERE [Pick]=1 GROUP BY [PostId] ORDER BY COUNT(*) DESC OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY", sql);
    }
}

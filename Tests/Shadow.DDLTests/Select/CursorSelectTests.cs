using Shadow.DDL;
using Shadow.DDL.Schemas;
using Shadow.DDLTests.Supports;
using ShadowSql;
using ShadowSql.Cursors;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using ShadowSql.Select;
using ShadowSql.Tables;

namespace Shadow.DDLTests.Select;

public class CursorSelectTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void Cursor()
    {
        int limit = 10;
        int offset = 10;
        var table = new TableSchema("Users", [], "tenant1");
        var cursor = new TableCursor(table)
            .Skip(offset)
            .Take(limit);
        var select = new CursorSelect(cursor);
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT * FROM [tenant1].[Users] OFFSET 10 ROWS FETCH NEXT 10 ROWS ONLY", sql);
    }
    [Fact]
    public void Desc()
    {
        var builder = new TableSchemaBuilder("Users", "tenant1")
            .DefinColumns("TEXT", "Id", "Name", "Age");
        var cursor = new TableCursor(builder.Build())
            .Desc(u => u.Field("Age"))
            .Asc(u => u.Field("Id"));
        var select = new CursorSelect(cursor);
        select.Select("Id", "Name");
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [Id],[Name] FROM [tenant1].[Users] ORDER BY [Age] DESC,[Id]", sql);
    }

    [Fact]
    public void Logic()
    {
        var table = new UserTable("Users", "tenant1");
        var query = new TableSqlQuery(table)
             .Where(table.Id.Less("LastId"))
             .Where(table.Status.EqualValue(true));
        var cursor = new TableCursor(query)
            .Desc(table.Id)
            .Skip(10)
            .Take(10);
        var select = new CursorSelect(cursor);

        var sql = _engine.Sql(select);
        Assert.Equal("SELECT * FROM [tenant1].[Users] WHERE [Id]<@LastId AND [Status]=1 ORDER BY [Id] DESC OFFSET 10 ROWS FETCH NEXT 10 ROWS ONLY", sql);
    }

    [Fact]
    public void Table()
    {
        var builder = new TableSchemaBuilder("Users", "tenant1")
            .DefinColumns("TEXT", "Id", "Name");
        var cursor = new TableCursor(builder.Build())
            .OrderBy("Id DESC")
            .Skip(20)
            .Take(10);
        var select = new CursorSelect(cursor)
            .Select("Id", "Name");
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [Id],[Name] FROM [tenant1].[Users] ORDER BY Id DESC OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY", sql);
    }
    [Fact]
    public void SqlQuery()
    {
        var table = new UserTable("Users", "tenant1");
        var query = new TableSqlQuery(table)
            .Where(table.Status.EqualValue(true));
        var cursor = new TableCursor(query, 10, 20)
            .Desc(table.Id);
        var select = new CursorSelect(cursor)
            .Select(table.Id, table.Name);
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [Id],[Name] FROM [tenant1].[Users] WHERE [Status]=1 ORDER BY [Id] DESC OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY", sql);
    }
    [Fact]
    public void Query()
    {
        UserTable table = new("Users", "tenant1");
        var query = new TableQuery(table)
            .And(table.Status.EqualValue(true));
        var cursor = new TableCursor(query, 10, 20)
            .Desc(table.Id);
        var select = new CursorSelect(cursor)
            .Select(table.Id, table.Name);
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [Id],[Name] FROM [tenant1].[Users] WHERE [Status]=1 ORDER BY [Id] DESC OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY", sql);
    }
    [Fact]
    public void GroupBySqlQueryTest()
    {
        var builder = new TableSchemaBuilder("Users", "tenant1")
            .DefinColumns("TEXT", "City");
        var query = new TableSqlQuery(builder.Build())
            .Where("Age<20");
        var groupBy = GroupBySqlQuery.Create(query, "City");
        var cursor = new TableCursor(groupBy, 10, 20)
            .Desc(groupBy.Count());
        var select = new CursorSelect(cursor)
            .Select("City")
            .Select(groupBy.CountAs("CityCount"));
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [City],COUNT(*) AS CityCount FROM [tenant1].[Users] WHERE Age<20 GROUP BY [City] ORDER BY COUNT(*) DESC OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY", sql);
    }
    [Fact]
    public void GroupByQueryTest()
    {
        var table = new CommentTable("Comments", "tenant1");
        var query = new TableQuery(table)
            .And(table.Pick.EqualValue(true));
        var groupBy = GroupByQuery.Create(query, table.PostId);
        var cursor = new TableCursor(groupBy, 10, 20)
            .Desc(groupBy.Count());
        var select = new CursorSelect(cursor)
            .Select(table.PostId, groupBy.CountAs("CommentCount"));
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [PostId],COUNT(*) AS CommentCount FROM [tenant1].[Comments] WHERE [Pick]=1 GROUP BY [PostId] ORDER BY COUNT(*) DESC OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY", sql);
    }
}

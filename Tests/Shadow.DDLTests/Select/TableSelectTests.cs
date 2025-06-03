using Shadow.DDL;
using Shadow.DDLTests.Supports;
using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.GroupBy;
using ShadowSql.Select;
using ShadowSql.Tables;

namespace Shadow.DDLTests.Select;

public class TableSelectTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void Table()
    {
        var builder = new TableSchemaBuilder("Users", "tenant1")
            .DefinColumns("TEXT", "Id", "Name");
        var select = new TableSelect(builder.Build())
            .Select("Id", "Name");
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [Id],[Name] FROM [tenant1].[Users]", sql);
    }
    [Fact]
    public void SqlQuery()
    {
        var table = new UserTable("Users", "tenant1");
        var query = new TableSqlQuery(table)
            .Where(table.Status.EqualValue(true));
        var select = new TableSelect(query)
            .Select(table.Id, table.Name);
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [Id],[Name] FROM [tenant1].[Users] WHERE [Status]=1", sql);
    }
    [Fact]
    public void Query()
    {
        var table = new UserTable("Users", "tenant1");
        var query = new TableQuery(table)
            .And(table.Status.EqualValue(true));
        var select = new TableSelect(query)
            .Select(table.Id, table.Name);
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [Id],[Name] FROM [tenant1].[Users] WHERE [Status]=1", sql);
    }
    [Fact]
    public void GroupBySqlQueryTest()
    {
        var builder = new TableSchemaBuilder("Users", "tenant1")
            .DefinColumns("TEXT", "City");
        var query = new TableSqlQuery(builder.Build())
            .Where("Age<20");
        var groupBy = GroupBySqlQuery.Create(query, "City");
        var select = new TableSelect(groupBy)
            .Select("City")
            .Alias("Count(*)", "CityCount");
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [City],Count(*) AS CityCount FROM [tenant1].[Users] WHERE Age<20 GROUP BY [City]", sql);
    }
    [Fact]
    public void GroupByQueryTest()
    {
        var table = new CommentTable("Comments", "tenant1");
        var query = new TableQuery(table)
            .And(table.Pick.EqualValue(true));
        var groupBy = GroupByQuery.Create(query, table.PostId);
        var select = new TableSelect(groupBy)
            .Select(table.PostId, groupBy.CountAs("CommentCount"));
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [PostId],COUNT(*) AS CommentCount FROM [tenant1].[Comments] WHERE [Pick]=1 GROUP BY [PostId]", sql);
    }
}

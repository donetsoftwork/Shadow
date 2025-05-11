using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using ShadowSql.Select;
using ShadowSql.Tables;
using TestSupports;

namespace ShadowSqlCoreTest.Select;

public class TableSelectTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly IDB _db = new DB("MyDb");

    [Fact]
    public void Table()
    {
        var select = new TableSelect(_db.From("Users"))
            .Select("Id", "Name");
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [Id],[Name] FROM [Users]", sql);
    }
    [Fact]
    public void SqlQuery()
    {
        var table = new UserTable();
        var query = new TableSqlQuery(table)
            .Where(table.Status.EqualValue(true));
        var select = new TableSelect(query)
            .Select(table.Id, table.Name);
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [Id],[Name] FROM [Users] WHERE [Status]=1", sql);
    }
    [Fact]
    public void Query()
    {
        var table = new UserTable();
        var query = new TableQuery(table)
            .And(table.Status.EqualValue(true));
        var select = new TableSelect(query)
            .Select(table.Id, table.Name);
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [Id],[Name] FROM [Users] WHERE [Status]=1", sql);
    }
    [Fact]
    public void GroupBySqlQueryTest()
    {
        var query = new TableSqlQuery("Users")
            .Where("Age<20");
        var groupBy = GroupBySqlQuery.Create(query, "City");
        var select = new TableSelect(groupBy)
            .Select("City")
            .Alias("Count(*)", "CityCount");
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [City],Count(*) AS CityCount FROM [Users] WHERE Age<20 GROUP BY [City]", sql);
    }
    [Fact]
    public void GroupByQueryTest()
    {
        var table = new CommentTable();
        var query = new TableQuery(table)
            .And(table.Pick.EqualValue(true));
        var groupBy = GroupByQuery.Create(query, table.PostId);
        var select = new TableSelect(groupBy)
            .Select(table.PostId, groupBy.CountAs("CommentCount"));
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [PostId],COUNT(*) AS CommentCount FROM [Comments] WHERE [Pick]=1 GROUP BY [PostId]", sql);
    }
}

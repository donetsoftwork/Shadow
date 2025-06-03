using Shadow.DDL;
using Shadow.DDL.Schemas;
using Shadow.DDLTests.Supports;
using ShadowSql;
using ShadowSql.Cursors;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using ShadowSql.Tables;

namespace Shadow.DDLTests.Cursors;

public class TableCursorTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void Cursor()
    {
        var table = new TableSchema("Users", [], "tenant1");
        int limit = 10;
        int offset = 10;
        var cursor = new TableCursor(table)
            .Skip(offset)
            .Take(limit);
        Assert.Equal(limit, cursor.Limit);
        Assert.Equal(offset, cursor.Offset);
    }
    [Fact]
    public void Where()
    {
        var builder = new TableSchemaBuilder("Users", "tenant1");
        var age = builder.DefineColumn("Age");
        var query = new TableSqlQuery(builder.Build())
            .Where(age.GreaterValue(30));
        var cursor = new TableCursor(query);
        var sql = _engine.Sql(cursor);
        Assert.Equal("[tenant1].[Users] WHERE [Age]>30", sql);
    }
    [Fact]
    public void OrderBy()
    {
        var table = new TableSchema("Users", [], "tenant1");
        var cursor = new TableCursor(table)
            .OrderBy("Age DESC");
        var sql = _engine.Sql(cursor);
        Assert.Equal("[tenant1].[Users] ORDER BY Age DESC", sql);
    }
    [Fact]
    public void Desc()
    {
        var builder = new TableSchemaBuilder("Users", "tenant1")
            .DefinColumns("TEXT", "Age");
        var cursor = new TableCursor(builder.Build())
            .Desc("Age");
        var sql = _engine.Sql(cursor);
        Assert.Equal("[tenant1].[Users] ORDER BY [Age] DESC", sql);
    }
    [Fact]
    public void Table()
    {
        var builder = new TableSchemaBuilder("Users", "tenant1")
            .DefinColumns("TEXT", "Age");
        var cursor = new TableCursor(builder.Build())
            .Desc("Age")
            .Skip(20)
            .Take(10);
        var sql = _engine.Sql(cursor);
        Assert.Equal("[tenant1].[Users] ORDER BY [Age] DESC", sql);
    }
    [Fact]
    public void SqlQuery()
    {
        var table = new TableSchema("Users", [], "tenant1");
        var query = new TableSqlQuery(table)
            .Where("Age>30");
        var cursor = new TableCursor(query)
            .OrderBy("Id DESC")
            .Skip(20)
            .Take(10);
        var sql = _engine.Sql(cursor);
        Assert.Equal("[tenant1].[Users] WHERE Age>30 ORDER BY Id DESC", sql);
    }
    [Fact]
    public void Query()
    {
        var table = new PostTable("Posts", "tenant1");
        var query = new TableQuery(table)
            .And(table.Author.EqualValue("张三"));
        var cursor = new TableCursor(query)
            .Desc(table.Id)
            .Skip(20)
            .Take(10);
        var sql = _engine.Sql(cursor);
        Assert.Equal("[tenant1].[Posts] WHERE [Author]='张三' ORDER BY [Id] DESC", sql);
    }
    [Fact]
    public void GroupBySqlQueryTest()
    {
        CommentTable table = new("Comments", "tenant1");
        var query = new TableSqlQuery(table)
            .Where(table.Pick.EqualValue(true));
        var groupBy = GroupBySqlQuery.Create(query, table.PostId)
            .Having(g => g.Count().GreaterValue(10));
        var cursor = new TableCursor(groupBy)
            .Asc(table.PostId)
            .Skip(20)
            .Take(10);
        var sql = _engine.Sql(cursor);
        Assert.Equal("[tenant1].[Comments] WHERE [Pick]=1 GROUP BY [PostId] HAVING COUNT(*)>10 ORDER BY [PostId]", sql);
    }
    [Fact]
    public void GroupByQueryTest()
    {
        CommentTable table = new("Comments", "tenant1");
        var query = new TableQuery(table)
            .And(table.Pick.EqualValue(true));
        var groupBy = GroupByQuery.Create(query, table.PostId)
            .And(g => g.Count().GreaterValue(10));
        var cursor = new TableCursor(groupBy)
            .Asc(table.PostId)
            .Skip(20)
            .Take(10);
        var sql = _engine.Sql(cursor);
        Assert.Equal("[tenant1].[Comments] WHERE [Pick]=1 GROUP BY [PostId] HAVING COUNT(*)>10 ORDER BY [PostId]", sql);
    }
}

using ShadowSql;
using ShadowSql.Cursors;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.GroupBy;
using ShadowSql.Identifiers;
using ShadowSql.Simples;
using ShadowSql.Tables;
using TestSupports;

namespace ShadowSqlCoreTest.Cursors;

public class TableCursorTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly IDB _db = SimpleDB.Use("MyDb");

    [Fact]
    public void Cursor()
    {
        int limit = 10;
        int offset = 10;
        var cursor = new TableCursor(_db.From("Users"))
            .Skip(offset)
            .Take(limit);
        Assert.Equal(limit, cursor.Limit);
        Assert.Equal(offset, cursor.Offset);
    }
    [Fact]
    public void Where()
    {
        var age = Column.Use("Age");
        var query = new TableSqlQuery("Users")
            .Where(age.GreaterValue(30));
        var cursor = new TableCursor(query);
        var sql = _engine.Sql(cursor);
        Assert.Equal("[Users] WHERE [Age]>30", sql);
    }
    [Fact]
    public void OrderBy()
    {
        var cursor = new TableCursor(_db.From("Users"))
            .OrderBy("Age DESC");
        var sql = _engine.Sql(cursor);
        Assert.Equal("[Users] ORDER BY Age DESC", sql);
    }
    [Fact]
    public void Desc()
    {
        var cursor = new TableCursor(_db.From("Users"))
            .Desc("Age");
        var sql = _engine.Sql(cursor);
        Assert.Equal("[Users] ORDER BY [Age] DESC", sql);
    }
    [Fact]
    public void Table()
    {
        var cursor = new TableCursor(_db.From("Users"))
            .Desc("Age")
            .Skip(20)
            .Take(10);
        var sql = _engine.Sql(cursor);
        Assert.Equal("[Users] ORDER BY [Age] DESC", sql);
    }
    [Fact]
    public void SqlQuery()
    {
        var query = new TableSqlQuery("Users")
            .Where("Age>30");
        var cursor = new TableCursor(query)
            .OrderBy("Id DESC")
            .Skip(20)
            .Take(10);
        var sql = _engine.Sql(cursor);
        Assert.Equal("[Users] WHERE Age>30 ORDER BY Id DESC", sql);
    }
    [Fact]
    public void Query()
    {
        var table = new PostTable();
        var query = new TableQuery(table)
            .And(table.Author.EqualValue("张三"));
        var cursor = new TableCursor(query)
            .Desc(table.Id)
            .Skip(20)
            .Take(10);
        var sql = _engine.Sql(cursor);
        Assert.Equal("[Posts] WHERE [Author]='张三' ORDER BY [Id] DESC", sql);
    }
    [Fact]
    public void GroupBySqlQueryTest()
    {
        CommentTable table = new();
        var query = new TableSqlQuery(table)
            .Where(table.Pick.EqualValue(true));
        var groupBy = GroupBySqlQuery.Create(query, table.PostId)
            .Having(g => g.Count().GreaterValue(10));
        var cursor = new TableCursor(groupBy)
            .Asc(table.PostId)
            .Skip(20)
            .Take(10);
        var sql = _engine.Sql(cursor);
        Assert.Equal("[Comments] WHERE [Pick]=1 GROUP BY [PostId] HAVING COUNT(*)>10 ORDER BY [PostId]", sql);
    }
    [Fact]
    public void GroupByQueryTest()
    {
        CommentTable table = new();
        var query = new TableQuery(table)
            .And(table.Pick.EqualValue(true));
        var groupBy = GroupByQuery.Create(query, table.PostId)
            .And(g => g.Count().GreaterValue(10));
        var cursor = new TableCursor(groupBy)
            .Asc(table.PostId)
            .Skip(20)
            .Take(10);
        var sql = _engine.Sql(cursor);
        Assert.Equal("[Comments] WHERE [Pick]=1 GROUP BY [PostId] HAVING COUNT(*)>10 ORDER BY [PostId]", sql);
    }
}

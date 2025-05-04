using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using TestSupports;

namespace ShadowSqlTest.CursorSelect;

public class JoinTableCursorSelectTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly DB _db = DB.Use("MyDb");

    [Fact]
    public void SqlJoin()
    {
        var select = _db.From("Employees")
            .SqlJoin(_db.From("Departments"))
            .OnColumn("DepartmentId", "Id")
            .Root
            .ToCursor(10, 20)
            .OrderBy("t1.Id DESC")
            .ToSelect();
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT * FROM [Employees] AS t1 INNER JOIN [Departments] AS t2 ON t1.[DepartmentId]=t2.[Id] ORDER BY t1.Id DESC OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY", sql);
    }
    [Theory]
    [InlineData(SqlEngineNames.MsSql, "SELECT c.[Content] FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] ORDER BY c.[Id] DESC OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY")]
    [InlineData(SqlEngineNames.MySql, "SELECT c.`Content` FROM `Comments` AS c INNER JOIN `Posts` AS p ON c.`PostId`=p.`Id` ORDER BY c.`Id` DESC LIMIT 20,10")]
    [InlineData(SqlEngineNames.Sqlite, "SELECT c.\"Content\" FROM \"Comments\" AS c INNER JOIN \"Posts\" AS p ON c.\"PostId\"=p.\"Id\" ORDER BY c.\"Id\" DESC LIMIT 10 OFFSET 20")]
    [InlineData(SqlEngineNames.Postgres, "SELECT c.\"Content\" FROM \"Comments\" AS c INNER JOIN \"Posts\" AS p ON c.\"PostId\"=p.\"Id\" ORDER BY c.\"Id\" DESC LIMIT 10 OFFSET 20")]
    public void Select(SqlEngineNames engineName, string expected)
    {
        ISqlEngine engine = SqlEngines.Get(engineName);
        var select = new CommentAliasTable("c")
            .SqlJoin(new PostAliasTable("p"))
            .On(c => c.PostId, p => p.Id)
            .Root
            .ToCursor(10, 20)
            .Desc<CommentAliasTable>("c", c => c.Id)
            .ToSelect()
            .Select<CommentAliasTable>("Comments", c => c.Content);
        var sql = engine.Sql(select);
        Assert.Equal(expected, sql);
    }
    [Theory]
    [InlineData(SqlEngineNames.MsSql, "SELECT c.[Id],c.[Content] FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] ORDER BY c.[Id] DESC OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY")]
    [InlineData(SqlEngineNames.MySql, "SELECT c.`Id`,c.`Content` FROM `Comments` AS c INNER JOIN `Posts` AS p ON c.`PostId`=p.`Id` ORDER BY c.`Id` DESC LIMIT 20,10")]
    [InlineData(SqlEngineNames.Sqlite, "SELECT c.\"Id\",c.\"Content\" FROM \"Comments\" AS c INNER JOIN \"Posts\" AS p ON c.\"PostId\"=p.\"Id\" ORDER BY c.\"Id\" DESC LIMIT 10 OFFSET 20")]
    [InlineData(SqlEngineNames.Postgres, "SELECT c.\"Id\",c.\"Content\" FROM \"Comments\" AS c INNER JOIN \"Posts\" AS p ON c.\"PostId\"=p.\"Id\" ORDER BY c.\"Id\" DESC LIMIT 10 OFFSET 20")]
    public void Select2(SqlEngineNames engineName, string expected)
    {
        ISqlEngine engine = SqlEngines.Get(engineName);
        CommentAliasTable c = new("c");
        PostAliasTable p = new("p");
        var select = c.SqlJoin(p)
            .On(c.PostId, p.Id)
            .Root
            .ToCursor(10, 20)
            .Desc<CommentAliasTable>("c", c => c.Id)
            .ToSelect()
            .Select<CommentAliasTable>("Comments", c => [c.Id, c.Content]);
        var sql = engine.Sql(select);
        Assert.Equal(expected, sql);
    }
    [Theory]
    [InlineData(SqlEngineNames.MsSql, "SELECT t1.[Content] FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] ORDER BY t1.[Id] DESC OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY")]
    [InlineData(SqlEngineNames.MySql, "SELECT t1.`Content` FROM `Comments` AS t1 INNER JOIN `Posts` AS t2 ON t1.`PostId`=t2.`Id` ORDER BY t1.`Id` DESC LIMIT 20,10")]
    [InlineData(SqlEngineNames.Sqlite, "SELECT t1.\"Content\" FROM \"Comments\" AS t1 INNER JOIN \"Posts\" AS t2 ON t1.\"PostId\"=t2.\"Id\" ORDER BY t1.\"Id\" DESC LIMIT 10 OFFSET 20")]
    [InlineData(SqlEngineNames.Postgres, "SELECT t1.\"Content\" FROM \"Comments\" AS t1 INNER JOIN \"Posts\" AS t2 ON t1.\"PostId\"=t2.\"Id\" ORDER BY t1.\"Id\" DESC LIMIT 10 OFFSET 20")]
    public void Select3(SqlEngineNames engineName, string expected)
    {
        ISqlEngine engine = SqlEngines.Get(engineName);
        var select = new CommentTable()
            .SqlJoin(new PostTable())
            .On(t1 => t1.PostId, t2 => t2.Id)
            .Root
            .ToCursor(10, 20)
            .Desc<CommentTable>("Comments", c => c.Id)
            .ToSelect()
            .Select<CommentTable>("Comments", t1 => t1.Content);
        var sql = engine.Sql(select);
        Assert.Equal(expected, sql);
    }
    [Theory]
    [InlineData(SqlEngineNames.MsSql, "SELECT t1.[Id],t1.[Content] FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] ORDER BY t1.[Id] DESC OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY")]
    [InlineData(SqlEngineNames.MySql, "SELECT t1.`Id`,t1.`Content` FROM `Comments` AS t1 INNER JOIN `Posts` AS t2 ON t1.`PostId`=t2.`Id` ORDER BY t1.`Id` DESC LIMIT 20,10")]
    [InlineData(SqlEngineNames.Sqlite, "SELECT t1.\"Id\",t1.\"Content\" FROM \"Comments\" AS t1 INNER JOIN \"Posts\" AS t2 ON t1.\"PostId\"=t2.\"Id\" ORDER BY t1.\"Id\" DESC LIMIT 10 OFFSET 20")]
    [InlineData(SqlEngineNames.Postgres, "SELECT t1.\"Id\",t1.\"Content\" FROM \"Comments\" AS t1 INNER JOIN \"Posts\" AS t2 ON t1.\"PostId\"=t2.\"Id\" ORDER BY t1.\"Id\" DESC LIMIT 10 OFFSET 20")]
    public void Select4(SqlEngineNames engineName, string expected)
    {
        ISqlEngine engine = SqlEngines.Get(engineName);
        var select = new CommentTable()
            .SqlJoin(new PostTable())
            .On(t1 => t1.PostId, t2 => t2.Id)
            .Root
            .ToCursor(10, 20)
            .Desc<CommentTable>("Comments", c => c.Id)
            .ToSelect()
            .Select<CommentTable>("Comments", t1 => [t1.Id, t1.Content]);
        var sql = engine.Sql(select);
        Assert.Equal(expected, sql);
    }
    [Theory]
    [InlineData(SqlEngineNames.MsSql, "SELECT c.* FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] ORDER BY c.[Id] DESC OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY")]
    [InlineData(SqlEngineNames.MySql, "SELECT c.* FROM `Comments` AS c INNER JOIN `Posts` AS p ON c.`PostId`=p.`Id` ORDER BY c.`Id` DESC LIMIT 20,10")]
    [InlineData(SqlEngineNames.Sqlite, "SELECT c.* FROM \"Comments\" AS c INNER JOIN \"Posts\" AS p ON c.\"PostId\"=p.\"Id\" ORDER BY c.\"Id\" DESC LIMIT 10 OFFSET 20")]
    [InlineData(SqlEngineNames.Postgres, "SELECT c.* FROM \"Comments\" AS c INNER JOIN \"Posts\" AS p ON c.\"PostId\"=p.\"Id\" ORDER BY c.\"Id\" DESC LIMIT 10 OFFSET 20")]
    public void SelectTable(SqlEngineNames engineName, string expected)
    {
        ISqlEngine engine = SqlEngines.Get(engineName);
        CommentAliasTable c = new("c");
        PostAliasTable p = new("p");
        var select = c.SqlJoin(p)
            .On(c.PostId, p.Id)
            .Root
            .ToCursor(10, 20)
            .Desc<CommentAliasTable>("c", c => c.Id)
            .ToSelect()
            .SelectTable(c);
        var sql = engine.Sql(select);
        Assert.Equal(expected, sql);
    }
    [Theory]
    [InlineData(SqlEngineNames.MsSql, "SELECT t1.* FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] ORDER BY t1.Id DESC OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY")]
    [InlineData(SqlEngineNames.MySql, "SELECT t1.* FROM `Comments` AS t1 INNER JOIN `Posts` AS t2 ON t1.`PostId`=t2.`Id` ORDER BY t1.Id DESC LIMIT 20,10")]
    [InlineData(SqlEngineNames.Sqlite, "SELECT t1.* FROM \"Comments\" AS t1 INNER JOIN \"Posts\" AS t2 ON t1.\"PostId\"=t2.\"Id\" ORDER BY t1.Id DESC LIMIT 10 OFFSET 20")]
    [InlineData(SqlEngineNames.Postgres, "SELECT t1.* FROM \"Comments\" AS t1 INNER JOIN \"Posts\" AS t2 ON t1.\"PostId\"=t2.\"Id\" ORDER BY t1.Id DESC LIMIT 10 OFFSET 20")]
    public void SelectTable2(SqlEngineNames engineName, string expected)
    {
        ISqlEngine engine = SqlEngines.Get(engineName);
        var select = new Table("Comments")
            .SqlJoin(new Table("Posts"))
            .OnColumn("PostId", "Id")
            .Root
            .ToCursor(10, 20)
            .OrderBy("t1.Id DESC")
            .ToSelect()
            .SelectTable("Comments");
        var sql = engine.Sql(select);
        Assert.Equal(expected, sql);
    }
}

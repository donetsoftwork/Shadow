using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MySql;
using ShadowSql.Identifiers;
using TestSupports;

namespace ShadowSqlTest.Select;

public class JoinTableSelectTests
{
    private readonly static ISqlEngine _engine = new MySqlEngine();
    private readonly static CommentAliasTable c = new("c");
    private readonly static PostAliasTable p = new("p");

    [Fact]
    public void ByFieldName()
    {
        var Comments = new Table("Comments").As("c");
        var Posts = new Table("Posts").As("p");
        var joinOn = Comments
            .Join(Posts)
            .And(Comments.Field("PostId").Equal(Posts.Field("Id")));

        var query = joinOn.Root
            .And(Comments.Field("Pick").EqualValue(true))
            .And(Posts.Field("Author").EqualValue("jxj"))
            .ToCursor()
            .Desc<IAliasTable>("c", c => c.Field("Id"))
            .ToSelect()
            .Select<IAliasTable>("c", c => [c.Field("Id"), c.Field("Content")]);
        var sql = _engine.Sql(query);
        Assert.Equal("SELECT c.`Id`,c.`Content` FROM `Comments` AS c INNER JOIN `Posts` AS p ON c.`PostId`=p.`Id` WHERE c.`Pick`=1 AND p.`Author`='jxj' ORDER BY c.`Id` DESC", sql);
    }

    [Fact]
    public void ByLogic()
    {
        var joinOn = c.Join(p)
            .And(c.PostId.Equal(p.Id));
        var query = joinOn.Root
            .And(c.Pick.Equal())
            .And(p.Author.Equal())
            .ToCursor()
            .Desc(c.Id)
            .ToSelect()
            .Select(c.Id, c.Content);
        var sql = _engine.Sql(query);
        //Console.WriteLine(sql);
        Assert.Equal("SELECT c.`Id`,c.`Content` FROM `Comments` AS c INNER JOIN `Posts` AS p ON c.`PostId`=p.`Id` WHERE c.`Pick`=@Pick AND p.`Author`=@Author ORDER BY c.`Id` DESC", sql);
    }

    
    [Theory]
    [InlineData(SqlEngineNames.MsSql, "SELECT * FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id]")]
    [InlineData(SqlEngineNames.MySql, "SELECT * FROM `Comments` AS c INNER JOIN `Posts` AS p ON c.`PostId`=p.`Id`")]
    [InlineData(SqlEngineNames.Sqlite, "SELECT * FROM \"Comments\" AS c INNER JOIN \"Posts\" AS p ON c.\"PostId\"=p.\"Id\"")]
    [InlineData(SqlEngineNames.Postgres, "SELECT * FROM \"Comments\" AS c INNER JOIN \"Posts\" AS p ON c.\"PostId\"=p.\"Id\"")]
    public void SqlJoin(SqlEngineNames engineName, string expected)
    {
        ISqlEngine engine = SqlEngines.Get(engineName);
        var select = c.SqlJoin(p)
            .On(c.PostId, p.Id)
            .ToSelect();
        var sql = engine.Sql(select);
        Assert.Equal(expected, sql);
    }
    [Theory]
    [InlineData(SqlEngineNames.MsSql, "SELECT * FROM [Comments] AS c,[Posts] AS p WHERE c.[PostId]=p.[Id]")]
    [InlineData(SqlEngineNames.MySql, "SELECT * FROM `Comments` AS c,`Posts` AS p WHERE c.`PostId`=p.`Id`")]
    [InlineData(SqlEngineNames.Sqlite, "SELECT * FROM \"Comments\" AS c,\"Posts\" AS p WHERE c.\"PostId\"=p.\"Id\"")]
    [InlineData(SqlEngineNames.Postgres, "SELECT * FROM \"Comments\" AS c,\"Posts\" AS p WHERE c.\"PostId\"=p.\"Id\"")]
    public void SqlMulti(SqlEngineNames engineName, string expected)
    {
        ISqlEngine engine = SqlEngines.Get(engineName);
        var select = c.SqlMulti(p)
            .Where(c.PostId.Equal(p.Id))
            .ToSelect();
        var sql = engine.Sql(select);
        Assert.Equal(expected, sql);
    }
    [Theory]
    [InlineData(SqlEngineNames.MsSql, "SELECT * FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id]")]
    [InlineData(SqlEngineNames.MySql, "SELECT * FROM `Comments` AS c INNER JOIN `Posts` AS p ON c.`PostId`=p.`Id`")]
    [InlineData(SqlEngineNames.Sqlite, "SELECT * FROM \"Comments\" AS c INNER JOIN \"Posts\" AS p ON c.\"PostId\"=p.\"Id\"")]
    [InlineData(SqlEngineNames.Postgres, "SELECT * FROM \"Comments\" AS c INNER JOIN \"Posts\" AS p ON c.\"PostId\"=p.\"Id\"")]
    public void Join(SqlEngineNames engineName, string expected)
    {
        ISqlEngine engine = SqlEngines.Get(engineName);
        var select = c.Join(p)
            .And(c.PostId.Equal(p.Id))
            .ToSelect();
        var sql = engine.Sql(select);
        Assert.Equal(expected, sql);
    }
    [Theory]
    [InlineData(SqlEngineNames.MsSql, "SELECT * FROM [Comments] AS c,[Posts] AS p WHERE c.[PostId]=p.[Id]")]
    [InlineData(SqlEngineNames.MySql, "SELECT * FROM `Comments` AS c,`Posts` AS p WHERE c.`PostId`=p.`Id`")]
    [InlineData(SqlEngineNames.Sqlite, "SELECT * FROM \"Comments\" AS c,\"Posts\" AS p WHERE c.\"PostId\"=p.\"Id\"")]
    [InlineData(SqlEngineNames.Postgres, "SELECT * FROM \"Comments\" AS c,\"Posts\" AS p WHERE c.\"PostId\"=p.\"Id\"")]
    public void Multi(SqlEngineNames engineName, string expected)
    {
        ISqlEngine engine = SqlEngines.Get(engineName);
        var select = c.Multi(p)
            .And(c.PostId.Equal(p.Id))
            .ToSelect();
        var sql = engine.Sql(select);
        Assert.Equal(expected, sql);
    }
    [Theory]
    [InlineData(SqlEngineNames.MsSql, "SELECT c.[Content] FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id]")]
    [InlineData(SqlEngineNames.MySql, "SELECT c.`Content` FROM `Comments` AS c INNER JOIN `Posts` AS p ON c.`PostId`=p.`Id`")]
    [InlineData(SqlEngineNames.Sqlite, "SELECT c.\"Content\" FROM \"Comments\" AS c INNER JOIN \"Posts\" AS p ON c.\"PostId\"=p.\"Id\"")]
    [InlineData(SqlEngineNames.Postgres, "SELECT c.\"Content\" FROM \"Comments\" AS c INNER JOIN \"Posts\" AS p ON c.\"PostId\"=p.\"Id\"")]
    public void Select(SqlEngineNames engineName, string expected)
    {
        ISqlEngine engine = SqlEngines.Get(engineName);
        var select = new CommentAliasTable("c")
            .SqlJoin(new PostAliasTable("p"))
            .On(c => c.PostId, p => p.Id)
            .ToSelect()
            .Select<CommentAliasTable>("Comments", c => c.Content);
        var sql = engine.Sql(select);
        Assert.Equal(expected, sql);
    }
    [Theory]
    [InlineData(SqlEngineNames.MsSql, "SELECT c.[Id],c.[Content] FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id]")]
    [InlineData(SqlEngineNames.MySql, "SELECT c.`Id`,c.`Content` FROM `Comments` AS c INNER JOIN `Posts` AS p ON c.`PostId`=p.`Id`")]
    [InlineData(SqlEngineNames.Sqlite, "SELECT c.\"Id\",c.\"Content\" FROM \"Comments\" AS c INNER JOIN \"Posts\" AS p ON c.\"PostId\"=p.\"Id\"")]
    [InlineData(SqlEngineNames.Postgres, "SELECT c.\"Id\",c.\"Content\" FROM \"Comments\" AS c INNER JOIN \"Posts\" AS p ON c.\"PostId\"=p.\"Id\"")]
    public void Select2(SqlEngineNames engineName, string expected)
    {
        ISqlEngine engine = SqlEngines.Get(engineName);
        var select = c.SqlJoin(p)
            .On(c.PostId, p.Id)
            .ToSelect()
            .Select<CommentAliasTable>("Comments", c => [c.Id, c.Content]);
        var sql = engine.Sql(select);
        Assert.Equal(expected, sql);
    }
    [Theory]
    [InlineData(SqlEngineNames.MsSql, "SELECT t1.[Content] FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id]")]
    [InlineData(SqlEngineNames.MySql, "SELECT t1.`Content` FROM `Comments` AS t1 INNER JOIN `Posts` AS t2 ON t1.`PostId`=t2.`Id`")]
    [InlineData(SqlEngineNames.Sqlite, "SELECT t1.\"Content\" FROM \"Comments\" AS t1 INNER JOIN \"Posts\" AS t2 ON t1.\"PostId\"=t2.\"Id\"")]
    [InlineData(SqlEngineNames.Postgres, "SELECT t1.\"Content\" FROM \"Comments\" AS t1 INNER JOIN \"Posts\" AS t2 ON t1.\"PostId\"=t2.\"Id\"")]
    public void Select3(SqlEngineNames engineName, string expected)
    {
        ISqlEngine engine = SqlEngines.Get(engineName);
        var select = new CommentTable()
            .SqlJoin(new PostTable())
            .On(t1 => t1.PostId, t2 => t2.Id)
            .ToSelect()
            .Select<CommentTable>("Comments", t1 => t1.Content);
        var sql = engine.Sql(select);
        Assert.Equal(expected, sql);
    }
    [Theory]
    [InlineData(SqlEngineNames.MsSql, "SELECT t1.[Id],t1.[Content] FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id]")]
    [InlineData(SqlEngineNames.MySql, "SELECT t1.`Id`,t1.`Content` FROM `Comments` AS t1 INNER JOIN `Posts` AS t2 ON t1.`PostId`=t2.`Id`")]
    [InlineData(SqlEngineNames.Sqlite, "SELECT t1.\"Id\",t1.\"Content\" FROM \"Comments\" AS t1 INNER JOIN \"Posts\" AS t2 ON t1.\"PostId\"=t2.\"Id\"")]
    [InlineData(SqlEngineNames.Postgres, "SELECT t1.\"Id\",t1.\"Content\" FROM \"Comments\" AS t1 INNER JOIN \"Posts\" AS t2 ON t1.\"PostId\"=t2.\"Id\"")]
    public void Select4(SqlEngineNames engineName, string expected)
    {
        ISqlEngine engine = SqlEngines.Get(engineName);
        var select = new CommentTable()
            .SqlJoin(new PostTable())
            .On(t1 => t1.PostId, t2 => t2.Id)
            .ToSelect()
            .Select<CommentTable>("Comments", t1 => [t1.Id, t1.Content]);
        var sql = engine.Sql(select);
        Assert.Equal(expected, sql);
    }
    [Theory]
    [InlineData(SqlEngineNames.MsSql, "SELECT c.* FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id]")]
    [InlineData(SqlEngineNames.MySql, "SELECT c.* FROM `Comments` AS c INNER JOIN `Posts` AS p ON c.`PostId`=p.`Id`")]
    [InlineData(SqlEngineNames.Sqlite, "SELECT c.* FROM \"Comments\" AS c INNER JOIN \"Posts\" AS p ON c.\"PostId\"=p.\"Id\"")]
    [InlineData(SqlEngineNames.Postgres, "SELECT c.* FROM \"Comments\" AS c INNER JOIN \"Posts\" AS p ON c.\"PostId\"=p.\"Id\"")]
    public void SelectTable(SqlEngineNames engineName, string expected)
    {
        ISqlEngine engine = SqlEngines.Get(engineName);
        var select = c.SqlJoin(p)
            .On(c.PostId, p.Id)
            .ToSelect()
            .SelectTable(c);
        var sql = engine.Sql(select);
        Assert.Equal(expected, sql);
    }
    [Theory]
    [InlineData(SqlEngineNames.MsSql, "SELECT t1.* FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id]")]
    [InlineData(SqlEngineNames.MySql, "SELECT t1.* FROM `Comments` AS t1 INNER JOIN `Posts` AS t2 ON t1.`PostId`=t2.`Id`")]
    [InlineData(SqlEngineNames.Sqlite, "SELECT t1.* FROM \"Comments\" AS t1 INNER JOIN \"Posts\" AS t2 ON t1.\"PostId\"=t2.\"Id\"")]
    [InlineData(SqlEngineNames.Postgres, "SELECT t1.* FROM \"Comments\" AS t1 INNER JOIN \"Posts\" AS t2 ON t1.\"PostId\"=t2.\"Id\"")]
    public void SelectTable2(SqlEngineNames engineName, string expected)
    {
        ISqlEngine engine = SqlEngines.Get(engineName);
        var select = new Table("Comments")
            .SqlJoin(new Table("Posts"))
            .OnColumn("PostId", "Id")
            .ToSelect()
            .SelectTable("Comments");
        var sql = engine.Sql(select);
        Assert.Equal(expected, sql);
    }
}

using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Identifiers;
using TestSupports;

namespace ShadowSqlTest.Select;

public class GroupByMultiSelectTests
{
    static readonly IDB _db = new DB("MyDb");

    [Theory]
    [InlineData(SqlEngineNames.MsSql, "SELECT [Manager] FROM [Employees] AS t1 INNER JOIN [Departments] AS t2 ON t1.[DepartmentId]=t2.[Id] GROUP BY [Manager]")]
    [InlineData(SqlEngineNames.MySql, "SELECT `Manager` FROM `Employees` AS t1 INNER JOIN `Departments` AS t2 ON t1.`DepartmentId`=t2.`Id` GROUP BY `Manager`")]
    [InlineData(SqlEngineNames.Sqlite, "SELECT \"Manager\" FROM \"Employees\" AS t1 INNER JOIN \"Departments\" AS t2 ON t1.\"DepartmentId\"=t2.\"Id\" GROUP BY \"Manager\"")]
    [InlineData(SqlEngineNames.Postgres, "SELECT \"Manager\" FROM \"Employees\" AS t1 INNER JOIN \"Departments\" AS t2 ON t1.\"DepartmentId\"=t2.\"Id\" GROUP BY \"Manager\"")]
    public void SqlGroupBy(SqlEngineNames engineName, string expected)
    {
        ISqlEngine engine = SqlEngines.Get(engineName);
        var select = _db.From("Employees")
            .SqlJoin(_db.From("Departments"))
            .OnColumn("DepartmentId", "Id")
            .Root
            .SqlGroupBy("Manager")
            .ToSelect();
        var sql = engine.Sql(select);
        Assert.Equal(expected, sql);
    }
    [Theory]
    [InlineData(SqlEngineNames.MsSql, "SELECT p.[Id] FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] GROUP BY p.[Id]")]
    [InlineData(SqlEngineNames.MySql, "SELECT p.`Id` FROM `Comments` AS c INNER JOIN `Posts` AS p ON c.`PostId`=p.`Id` GROUP BY p.`Id`")]
    [InlineData(SqlEngineNames.Sqlite, "SELECT p.\"Id\" FROM \"Comments\" AS c INNER JOIN \"Posts\" AS p ON c.\"PostId\"=p.\"Id\" GROUP BY p.\"Id\"")]
    [InlineData(SqlEngineNames.Postgres, "SELECT p.\"Id\" FROM \"Comments\" AS c INNER JOIN \"Posts\" AS p ON c.\"PostId\"=p.\"Id\" GROUP BY p.\"Id\"")]
    public void GroupBy(SqlEngineNames engineName, string expected)
    {
        ISqlEngine engine = SqlEngines.Get(engineName);
        CommentAliasTable c = new("c");
        PostAliasTable p = new("p");
        var select = c.SqlJoin(p)
            .On(c.PostId, p.Id)
            .Root
            .SqlGroupBy(p.Id)
            .ToSelect();
        var sql = engine.Sql(select);
        Assert.Equal(expected, sql);
    }
    [Theory]
    [InlineData(SqlEngineNames.MsSql, "SELECT c.[PostId],SUM(c.[Pick]) AS PickTotal FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] GROUP BY c.[PostId]")]
    [InlineData(SqlEngineNames.MySql, "SELECT c.`PostId`,SUM(c.`Pick`) AS PickTotal FROM `Comments` AS c INNER JOIN `Posts` AS p ON c.`PostId`=p.`Id` GROUP BY c.`PostId`")]
    [InlineData(SqlEngineNames.Sqlite, "SELECT c.\"PostId\",SUM(c.\"Pick\") AS PickTotal FROM \"Comments\" AS c INNER JOIN \"Posts\" AS p ON c.\"PostId\"=p.\"Id\" GROUP BY c.\"PostId\"")]
    [InlineData(SqlEngineNames.Postgres, "SELECT c.\"PostId\",SUM(c.\"Pick\") AS PickTotal FROM \"Comments\" AS c INNER JOIN \"Posts\" AS p ON c.\"PostId\"=p.\"Id\" GROUP BY c.\"PostId\"")]
    public void SelectAggregate(SqlEngineNames engineName, string expected)
    {
        ISqlEngine engine = SqlEngines.Get(engineName);
        var select = new CommentAliasTable("c")
            .SqlJoin(new PostAliasTable("p"))
            .On(c => c.PostId, p => p.Id)
            .SqlGroupBy((c, p) => [c.PostId])
            .ToSelect()
            .SelectGroupBy()
            .SelectAggregate<CommentAliasTable>("c", c => c.Pick.SumAs("PickTotal"));
        var sql = engine.Sql(select);
        Assert.Equal(expected, sql);
    }
    [Theory]
    [InlineData(SqlEngineNames.MsSql, "SELECT t1.[PostId],SUM(t1.[Pick]) AS PickTotal FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] GROUP BY t1.[PostId]")]
    [InlineData(SqlEngineNames.MySql, "SELECT t1.`PostId`,SUM(t1.`Pick`) AS PickTotal FROM `Comments` AS t1 INNER JOIN `Posts` AS t2 ON t1.`PostId`=t2.`Id` GROUP BY t1.`PostId`")]
    [InlineData(SqlEngineNames.Sqlite, "SELECT t1.\"PostId\",SUM(t1.\"Pick\") AS PickTotal FROM \"Comments\" AS t1 INNER JOIN \"Posts\" AS t2 ON t1.\"PostId\"=t2.\"Id\" GROUP BY t1.\"PostId\"")]
    [InlineData(SqlEngineNames.Postgres, "SELECT t1.\"PostId\",SUM(t1.\"Pick\") AS PickTotal FROM \"Comments\" AS t1 INNER JOIN \"Posts\" AS t2 ON t1.\"PostId\"=t2.\"Id\" GROUP BY t1.\"PostId\"")]
    public void SelectAggregate2(SqlEngineNames engineName, string expected)
    {
        ISqlEngine engine = SqlEngines.Get(engineName);
        var select = new CommentTable()
            .SqlJoin(new PostTable())
            .On(c => c.PostId, p => p.Id)
            .SqlGroupBy((c, p) => [c.PostId])
            .ToSelect()
            .SelectGroupBy()
            .SelectAggregate<CommentTable>("Comments", c => c.Pick, Pick => Pick.SumAs("PickTotal"));
        var sql = engine.Sql(select);
        Assert.Equal(expected, sql);
    }
}

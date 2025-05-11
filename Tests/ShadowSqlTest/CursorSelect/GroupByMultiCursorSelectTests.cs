using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Tables;
using TestSupports;

namespace ShadowSqlTest.CursorSelect;

public class GroupByMultiCursorSelectTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void ToCursor()
    {
        var select = SimpleTable.Use("Employees")
            .SqlJoin(SimpleTable.Use("Departments"))
            .OnColumn("DepartmentId", "Id")
            .Root
            .SqlGroupBy("Manager")
            .ToCursor()
            .CountAsc()
            .ToSelect()
            .SelectGroupBy()
            .SelectCount("ManagerCount");

        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [Manager],COUNT(*) AS ManagerCount FROM [Employees] AS t1 INNER JOIN [Departments] AS t2 ON t1.[DepartmentId]=t2.[Id] GROUP BY [Manager] ORDER BY COUNT(*)", sql);
    }
    [Theory]
    [InlineData(SqlEngineNames.MsSql, "SELECT c.[PostId],SUM(c.[Pick]) AS PickTotal FROM [Comments] AS c INNER JOIN [Posts] AS p ON c.[PostId]=p.[Id] GROUP BY c.[PostId] ORDER BY COUNT(*) OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY")]
    [InlineData(SqlEngineNames.MySql, "SELECT c.`PostId`,SUM(c.`Pick`) AS PickTotal FROM `Comments` AS c INNER JOIN `Posts` AS p ON c.`PostId`=p.`Id` GROUP BY c.`PostId` ORDER BY COUNT(*) LIMIT 20,10")]
    [InlineData(SqlEngineNames.Sqlite, "SELECT c.\"PostId\",SUM(c.\"Pick\") AS PickTotal FROM \"Comments\" AS c INNER JOIN \"Posts\" AS p ON c.\"PostId\"=p.\"Id\" GROUP BY c.\"PostId\" ORDER BY COUNT(*) LIMIT 10 OFFSET 20")]
    [InlineData(SqlEngineNames.Postgres, "SELECT c.\"PostId\",SUM(c.\"Pick\") AS PickTotal FROM \"Comments\" AS c INNER JOIN \"Posts\" AS p ON c.\"PostId\"=p.\"Id\" GROUP BY c.\"PostId\" ORDER BY COUNT(*) LIMIT 10 OFFSET 20")]
    public void SelectAggregate(SqlEngineNames engineName, string expected)
    {
        ISqlEngine engine = SqlEngines.Get(engineName);
        var select = new CommentAliasTable("c")
            .SqlJoin(new PostAliasTable("p"))
            .On(c => c.PostId, p => p.Id)
            .SqlGroupBy((c, p) => [c.PostId])
            .ToCursor(10, 20)
            .CountAsc()
            .ToSelect()
            .SelectGroupBy()
            .SelectAggregate<CommentAliasTable>("c", c => c.Pick.SumAs("PickTotal"));
        var sql = engine.Sql(select);
        Assert.Equal(expected, sql);
    }
    [Theory]
    [InlineData(SqlEngineNames.MsSql, "SELECT t1.[PostId],SUM(t1.[Pick]) AS PickTotal FROM [Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] GROUP BY t1.[PostId] ORDER BY COUNT(*) OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY")]
    [InlineData(SqlEngineNames.MySql, "SELECT t1.`PostId`,SUM(t1.`Pick`) AS PickTotal FROM `Comments` AS t1 INNER JOIN `Posts` AS t2 ON t1.`PostId`=t2.`Id` GROUP BY t1.`PostId` ORDER BY COUNT(*) LIMIT 20,10")]
    [InlineData(SqlEngineNames.Sqlite, "SELECT t1.\"PostId\",SUM(t1.\"Pick\") AS PickTotal FROM \"Comments\" AS t1 INNER JOIN \"Posts\" AS t2 ON t1.\"PostId\"=t2.\"Id\" GROUP BY t1.\"PostId\" ORDER BY COUNT(*) LIMIT 10 OFFSET 20")]
    [InlineData(SqlEngineNames.Postgres, "SELECT t1.\"PostId\",SUM(t1.\"Pick\") AS PickTotal FROM \"Comments\" AS t1 INNER JOIN \"Posts\" AS t2 ON t1.\"PostId\"=t2.\"Id\" GROUP BY t1.\"PostId\" ORDER BY COUNT(*) LIMIT 10 OFFSET 20")]
    public void SelectAggregate2(SqlEngineNames engineName, string expected)
    {
        ISqlEngine engine = SqlEngines.Get(engineName);
        var select = new CommentTable()
            .SqlJoin(new PostTable())
            .On(c => c.PostId, p => p.Id)
            .SqlGroupBy((c, p) => [c.PostId])
            .ToCursor(10, 20)
            .CountAsc()
            .ToSelect()
            .SelectGroupBy()
            .SelectAggregate<CommentTable>("Comments", c => c.Pick, Pick => Pick.SumAs("PickTotal"));
        var sql = engine.Sql(select);
        Assert.Equal(expected, sql);
    }
}

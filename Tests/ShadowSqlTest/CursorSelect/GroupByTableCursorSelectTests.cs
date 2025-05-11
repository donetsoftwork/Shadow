using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Identifiers;
using TestSupports;

namespace ShadowSqlTest.CursorSelect;

public class GroupByTableCursorSelectTests
{
    static readonly IDB _db = new DB("MyDb");
    [Theory]
    [InlineData(SqlEngineNames.MsSql, "SELECT [City] FROM [Users] GROUP BY [City] ORDER BY COUNT(*) OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY")]
    [InlineData(SqlEngineNames.MySql, "SELECT `City` FROM `Users` GROUP BY `City` ORDER BY COUNT(*) LIMIT 20,10")]
    [InlineData(SqlEngineNames.Sqlite, "SELECT \"City\" FROM \"Users\" GROUP BY \"City\" ORDER BY COUNT(*) LIMIT 10 OFFSET 20")]
    [InlineData(SqlEngineNames.Postgres, "SELECT \"City\" FROM \"Users\" GROUP BY \"City\" ORDER BY COUNT(*) LIMIT 10 OFFSET 20")]
    public void GroupBy(SqlEngineNames engineName, string expected)
    {
        ISqlEngine engine = SqlEngines.Get(engineName);
        var select = _db.From("Users")
            .GroupBy("City")
            .ToCursor(10, 20)
            .CountAsc()
            .ToSelect();
        var sql = engine.Sql(select);
        Assert.Equal(expected, sql);
    }
    [Theory]
    [InlineData(SqlEngineNames.MsSql, "SELECT [City] FROM [Users] WHERE Status=1 GROUP BY [City] ORDER BY COUNT(*) OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY")]
    [InlineData(SqlEngineNames.MySql, "SELECT `City` FROM `Users` WHERE Status=1 GROUP BY `City` ORDER BY COUNT(*) LIMIT 20,10")]
    [InlineData(SqlEngineNames.Sqlite, "SELECT \"City\" FROM \"Users\" WHERE Status=1 GROUP BY \"City\" ORDER BY COUNT(*) LIMIT 10 OFFSET 20")]
    [InlineData(SqlEngineNames.Postgres, "SELECT \"City\" FROM \"Users\" WHERE Status=1 GROUP BY \"City\" ORDER BY COUNT(*) LIMIT 10 OFFSET 20")]
    public void SqlGroupBy(SqlEngineNames engineName, string expected)
    {
        ISqlEngine engine = SqlEngines.Get(engineName);
        var select = _db.From("Users")
            .ToSqlQuery()
            .Where("Status=1")
            .SqlGroupBy("City")
            .ToCursor(10, 20)
            .CountAsc()
            .ToSelect();
        var sql = engine.Sql(select);
        Assert.Equal(expected, sql);
    }
    [Theory]
    [InlineData(SqlEngineNames.MsSql, "SELECT [PostId],SUM([Pick]) AS PickTotal FROM [Comments] GROUP BY [PostId] ORDER BY COUNT(*) OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY")]
    [InlineData(SqlEngineNames.MySql, "SELECT `PostId`,SUM(`Pick`) AS PickTotal FROM `Comments` GROUP BY `PostId` ORDER BY COUNT(*) LIMIT 20,10")]
    [InlineData(SqlEngineNames.Sqlite, "SELECT \"PostId\",SUM(\"Pick\") AS PickTotal FROM \"Comments\" GROUP BY \"PostId\" ORDER BY COUNT(*) LIMIT 10 OFFSET 20")]
    [InlineData(SqlEngineNames.Postgres, "SELECT \"PostId\",SUM(\"Pick\") AS PickTotal FROM \"Comments\" GROUP BY \"PostId\" ORDER BY COUNT(*) LIMIT 10 OFFSET 20")]
    public void SelectAggregate(SqlEngineNames engineName, string expected)
    {
        ISqlEngine engine = SqlEngines.Get(engineName);
        var select = new CommentTable()
            .GroupBy(c => [c.PostId])
            .ToCursor(10, 20)
            .CountAsc()
            .ToSelect()
            .SelectGroupBy()
            .SelectAggregate(c => c.Pick.SumAs("PickTotal"));
        var sql = engine.Sql(select);
        Assert.Equal(expected, sql);
    }
}

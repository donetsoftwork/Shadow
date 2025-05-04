using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Identifiers;
using ShadowSql.Simples;
using TestSupports;

namespace ShadowSqlTest.Select;

public class GroupByTableSelectTests
{
    static readonly IDB _db = SimpleDB.Use("MyDb");
    [Theory]
    [InlineData(SqlEngineNames.MsSql, "SELECT [City] FROM [Users] GROUP BY [City]")]
    [InlineData(SqlEngineNames.MySql, "SELECT `City` FROM `Users` GROUP BY `City`")]
    [InlineData(SqlEngineNames.Sqlite, "SELECT \"City\" FROM \"Users\" GROUP BY \"City\"")]
    [InlineData(SqlEngineNames.Postgres, "SELECT \"City\" FROM \"Users\" GROUP BY \"City\"")]
    public void GroupBy(SqlEngineNames engineName, string expected)
    {
        ISqlEngine engine = SqlEngines.Get(engineName);
        var select = _db.From("Users")
            .GroupBy("City")
            .ToSelect();
        var sql = engine.Sql(select);
        Assert.Equal(expected, sql);
    }
    [Theory]
    [InlineData(SqlEngineNames.MsSql, "SELECT [City] FROM [Users] WHERE Status=1 GROUP BY [City]")]
    [InlineData(SqlEngineNames.MySql, "SELECT `City` FROM `Users` WHERE Status=1 GROUP BY `City`")]
    [InlineData(SqlEngineNames.Sqlite, "SELECT \"City\" FROM \"Users\" WHERE Status=1 GROUP BY \"City\"")]
    [InlineData(SqlEngineNames.Postgres, "SELECT \"City\" FROM \"Users\" WHERE Status=1 GROUP BY \"City\"")]
    public void SqlGroupBy(SqlEngineNames engineName, string expected)
    {
        ISqlEngine engine = SqlEngines.Get(engineName);
        var select = _db.From("Users")
            .ToSqlQuery()
            .Where("Status=1")
            .SqlGroupBy("City")
            .ToSelect();
        var sql = engine.Sql(select);
        Assert.Equal(expected, sql);
    }
    [Theory]
    [InlineData(SqlEngineNames.MsSql, "SELECT [PostId],SUM([Pick]) AS PickTotal FROM [Comments] GROUP BY [PostId]")]
    [InlineData(SqlEngineNames.MySql, "SELECT `PostId`,SUM(`Pick`) AS PickTotal FROM `Comments` GROUP BY `PostId`")]
    [InlineData(SqlEngineNames.Sqlite, "SELECT \"PostId\",SUM(\"Pick\") AS PickTotal FROM \"Comments\" GROUP BY \"PostId\"")]
    [InlineData(SqlEngineNames.Postgres, "SELECT \"PostId\",SUM(\"Pick\") AS PickTotal FROM \"Comments\" GROUP BY \"PostId\"")]
    public void SelectAggregate(SqlEngineNames engineName, string expected)
    {
        ISqlEngine engine = SqlEngines.Get(engineName);
        var select = new CommentTable()
            .GroupBy(c => [c.PostId])
            .ToSelect()
            .SelectGroupBy()
            .SelectAggregate(c => c.Pick.SumAs("PickTotal"));
        var sql = engine.Sql(select);
        Assert.Equal(expected, sql);
    }
}

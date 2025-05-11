using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;

namespace ShadowSqlTest.SingleSelect;

public class SingleFieldTest
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly IDB _db = new DB("MyDb");

    [Fact]
    public void SingleField()
    {
        var u = _db.From("Users")
            .As("u");
        var count = _db.From("Orders")
            .ToSqlQuery()
            .Where("UserId=u.Id")
            .SqlGroupBy("UserId")
            .ToSingle(order => order.CountAs("Count"));

        var cursor = u.ToSelect()
            .Select("Id", "Name")
            .Select(count);
        var sql = _engine.Sql(cursor);
        //取最后一个字段
        Assert.Equal("SELECT u.[Id],u.[Name],(SELECT COUNT(*) AS Count FROM [Orders] WHERE UserId=u.Id GROUP BY [UserId]) FROM [Users] AS u", sql);
    }

    [Fact]
    public void SingleFieldAlias()
    {
        var u = _db.From("Users")
            .As("u");
        var count = _db.From("Orders")
            .ToSqlQuery()
            .Where("UserId=u.Id")
            .SqlGroupBy("UserId")
            .ToSingle(order => order.DistinctCountAs("Id", "Count"));

        var cursor = u.ToSelect()
            .Select("Id", "Name")
            .Alias(count, "OrderCount");
        var sql = _engine.Sql(cursor);
        //取最后一个字段
        Assert.Equal("SELECT u.[Id],u.[Name],(SELECT COUNT(DISTINCT [Id]) AS Count FROM [Orders] WHERE UserId=u.Id GROUP BY [UserId]) AS OrderCount FROM [Users] AS u", sql);
    }
}

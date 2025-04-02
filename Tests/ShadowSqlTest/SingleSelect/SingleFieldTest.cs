using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Simples;

namespace ShadowSqlTest.SingleSelect;

public class SingleFieldTest
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly IDB _db = SimpleDB.Use("MyDb");

    [Fact]
    public void SingleField()
    {
        var u = _db.From("Users")
            .As("u");
        var count = _db.From("Orders")
            .ToSqlQuery()
            .Where("UserId=u.Id")
            .GroupBy("UserId")
            .ToSingle();
        count.Fields.Select(order => order.CountAs("Count"));

        var cursor = u.ToSelect();
        cursor.Fields.Select("Id", "Name");
        cursor.Fields.Select(count);
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
            .GroupBy("UserId")
            .ToSingle();
        count.Fields.Select(order => order.CountAs("Id", "Count"));

        var cursor = u.ToSelect();
        cursor.Fields.Select("Id", "Name");
        cursor.Fields.Select(count, "OrderCount");
        var sql = _engine.Sql(cursor);
        //取最后一个字段
        Assert.Equal("SELECT u.[Id],u.[Name],(SELECT COUNT(DISTINCT [Id]) AS Count FROM [Orders] WHERE UserId=u.Id GROUP BY [UserId]) AS OrderCount FROM [Users] AS u", sql);
    }
}

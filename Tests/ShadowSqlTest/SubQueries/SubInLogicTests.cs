using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;

namespace ShadowSqlTest.SubQueries;

public class SubInLogicTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly IDB _db = new DB("MyDb");

    [Fact]
    public void In()
    {
        var userIds = _db.From("Orders")
            .ToSingle("UserId");
        var select = _db.From("Users")
            .ToSqlQuery()
            .Where(u => u.Field("Id").In(userIds))
            .ToSelect()
            .Select("Id", "Name");
        var sql = _engine.Sql(select);
        //取最后一个字段
        Assert.Equal("SELECT [Id],[Name] FROM [Users] WHERE [Id] IN (SELECT [UserId] FROM [Orders])", sql);
    }
    [Fact]
    public void NotIn()
    {
        var userIds = _db.From("Orders")
            .ToSingle("UserId");
        var select = _db.From("Users")
            .ToSqlQuery()
            .Where(u => u.Field("Id").NotIn(userIds))
            .ToSelect()
            .Select("Id", "Name");
        var sql = _engine.Sql(select);
        //取最后一个字段
        Assert.Equal("SELECT [Id],[Name] FROM [Users] WHERE [Id] NOT IN (SELECT [UserId] FROM [Orders])", sql);
    }
}

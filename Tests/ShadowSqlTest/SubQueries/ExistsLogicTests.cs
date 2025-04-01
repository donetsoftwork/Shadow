using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Simples;

namespace ShadowSqlTest.SubQueries;

public class ExistsLogicTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly IDB _db = SimpleDB.Use("MyDb");

    [Fact]
    public void Exists()
    {
        var u = _db.From("Users")
            .As("u");
        var orders = _db.From("Orders")
            .ToSqlQuery()
            .Where("UserId=u.Id");
        var select = u.ToSqlQuery()
            .Exists(orders)
            .ToSelect();
        var sql = _engine.Sql(select);
        //取最后一个字段
        Assert.Equal("SELECT * FROM [Users] AS u WHERE EXISTS(SELECT * FROM [Orders] WHERE UserId=u.Id)", sql);
    }

    [Fact]
    public void NotExists()
    {
        var u = _db.From("Users")
            .As("u");
        var orders = _db.From("Orders")
            .ToSqlQuery()
            .Where("UserId=u.Id");
        var select = u.ToSqlQuery()
            .NotExists(orders)
            .ToSelect();
        var sql = _engine.Sql(select);
        //取最后一个字段
        Assert.Equal("SELECT * FROM [Users] AS u WHERE NOT EXISTS(SELECT * FROM [Orders] WHERE UserId=u.Id)", sql);
    }
}

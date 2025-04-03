using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Simples;

namespace ShadowSqlTest.Simples;

public class SimpleTableTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void From()
    {
        var users = SimpleDB.From("Users");
        var tableName = _engine.Sql(users);
        Assert.Equal("[Users]", tableName);
    }
    [Fact]
    public void SqlQuery()
    {
        var users = SimpleDB.From("Users");
        var query = users.ToSqlQuery()
            .Where(users.Field("Id").LessValue(100));
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]<100", sql);
    }
    [Fact]
    public void SqlQueryFunc()
    {
        var query = SimpleDB.From("Users")
            .ToSqlQuery()
            .Where(users => users.Field("Id").LessValue(100));
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]<100", sql);
    }
    [Fact]
    public void Query()
    {
        var users = SimpleDB.From("Users");
        var query = users.ToQuery()
            .And(users.Field("Id").LessValue(100));
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]<100", sql);
    }
    [Fact]
    public void QueryFunc()
    {
        var query = SimpleDB.From("Users")
            .ToQuery()
            .And(users => users.Field("Id").LessValue(100));
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]<100", sql);
    }
}

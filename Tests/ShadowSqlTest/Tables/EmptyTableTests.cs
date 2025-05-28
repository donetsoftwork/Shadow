using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Tables;
using ShadowSql.FieldQueries;

namespace ShadowSqlTest.Tables;

public class EmptyTableTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void SqlQuery()
    {
        var userTable = EmptyTable.Use("Users");
        var query = userTable.ToSqlQuery()
            .Where(userTable.Field("Id").LessValue(100));
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]<100", sql);
    }
    [Fact]
    public void SqlQueryFunc()
    {
        var query = EmptyTable.Use("Users")
            .ToSqlQuery()
            .Where(users => users.Field("Id").LessValue(100));
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]<100", sql);
    }
    [Fact]
    public void Query()
    {
        var users = EmptyTable.Use("Users");
        var query = users.ToQuery()
            .And(users.Field("Id").LessValue(100));
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]<100", sql);
    }
    [Fact]
    public void QueryFunc()
    {
        var query = EmptyTable.Use("Users")
            .ToQuery()
            .And(users => users.Field("Id").LessValue(100));
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]<100", sql);
    }
    [Fact]
    public void Select()
    {
        var query = EmptyTable.Use("Users")
            .ToSqlQuery()
            .FieldEqualValue("Id", 1)
            .ToSelect()
            .Select("Id", "Name");
        var sql = _engine.Sql(query);
        Assert.Equal("SELECT [Id],[Name] FROM [Users] WHERE [Id]=1", sql);

    }
    [Fact]
    public void Delete()
    {
        var query = EmptyTable.Use("Users")
            .ToSqlQuery()
            .FieldEqualValue("Id", 1)
            .ToDelete();
        var sql = _engine.Sql(query);
        Assert.Equal("DELETE FROM [Users] WHERE [Id]=1", sql);
    }
}

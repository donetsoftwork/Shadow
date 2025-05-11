using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.FieldQueries;
using ShadowSql.Identifiers;
using TestSupports;

namespace ShadowSqlTest.Tables;

public class TableSqlQueryTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly IDB _db = new DB("MyDb");

    [Fact]
    public void AndQuery()
    {
        var query = _db.From("Users")
            .ToSqlQuery()
            .Where("Id=@Id", "Status=@Status");
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE Id=@Id AND Status=@Status", sql);
    }
    [Fact]
    public void OrQuery()
    {
        var query = _db.From("Users")
            .ToSqlOrQuery()
            .Where("Id=@Id", "Status=@Status");
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE Id=@Id OR Status=@Status", sql);
    }
    [Fact]
    public void Where()
    {
        var query = _db.From("Users")
            .ToSqlQuery()
            .Where("Id=@Id", "Status=@Status");
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE Id=@Id AND Status=@Status", sql);
    }
    [Fact]
    public void WhereAnd()
    {
        var query = _db.From("Users")
            .ToSqlQuery()
            .Apply(q => q
                .And("Id=@Id")
                .And("Status=@Status")
            );
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE Id=@Id AND Status=@Status", sql);
    }
    [Fact]
    public void WhereOr()
    {
        var query = _db.From("Users")
            .ToSqlOrQuery()
            .Apply(q => q
                .Or("Id=@Id")
                .Or("Status=@Status")
            );
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE Id=@Id OR Status=@Status", sql);
    }
    [Fact]
    public void FieldCompare()
    {
        var query = _db.From("Users")
            .ToSqlQuery()
            .FieldEqualValue("Id", 100);
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]=100", sql);
    }
    [Fact]
    public void Logic()
    {
        var users = new UserTable();
        var query = users.ToSqlQuery()
            .Where(users.Id.LessValue(100));
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]<100", sql);
    }
    [Fact]
    public void TableLogic()
    {
        var query = new UserTable()
            .ToSqlQuery()
            .Where(user => user.Id.Less("LastId"));
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]<@LastId", sql);
    }
    [Fact]
    public void Apply()
    {
        var query = new UserTable()
            .ToSqlQuery()
            .Apply(static (q, u) => q
                .And(u.Id.Less("LastId"))
                .And(u.Status.EqualValue(true))
            );
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]<@LastId AND [Status]=1", sql);
    }
}

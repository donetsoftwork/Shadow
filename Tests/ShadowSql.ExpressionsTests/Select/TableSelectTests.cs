using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Expressions;
using ShadowSql.ExpressionsTests.Supports;
using ShadowSql.Identifiers;

namespace ShadowSql.ExpressionsTests.Select;

public class TableSelectTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly IDB _db = new DB("MyDb");

    [Fact]
    public void Select()
    {
        var select = _db.From("Users")
            .ToSelect<User>()
            .Select(u => new { u.Id, u.Name });
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [Id],[Name] FROM [Users]", sql);
    }
    [Fact]
    public void Filter()
    {
        var select = _db.From("Users")
            .ToSelect<User>(u => u.Status)
            .Select(u => u.Id);
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [Id] FROM [Users] WHERE [Status]=1", sql);
    }
    [Fact]
    public void Arithmetic()
    {
        var select = _db.From("Users")
            .ToSelect<User>()
            .Select(u => new { u.Id, u.Name, Age = u.Age + u.Id });
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [Id],[Name],([Age]+[Id]) AS Age FROM [Users]", sql);
    }
    [Fact]
    public void SqlQuery()
    {
        var select = _db.From("Users")
            .ToSqlQuery<User>()
            .Where(u => u.Status)
            .ToSelect()
            .Select(u => new { u.Id, u.Name });
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [Id],[Name] FROM [Users] WHERE [Status]=1", sql);
    }
}

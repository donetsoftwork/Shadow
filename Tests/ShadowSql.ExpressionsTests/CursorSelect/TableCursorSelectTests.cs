using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Expressions;
using ShadowSql.ExpressionsTests.Supports;
using ShadowSql.Expressions.Tables;

namespace ShadowSql.ExpressionsTests.CursorSelect;

public class TableCursorSelectTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly IDB _db = new DB("MyDb");
    [Fact]
    public void ToSelect()
    {
        var select = _db.From("Users")
            .ToCursor<User>(10, 20)
            .Desc(u => u.Id)
            .ToSelect();
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT * FROM [Users] ORDER BY [Id] DESC OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY", sql);
    }
    [Fact]
    public void Select()
    {
        var select = _db.From("Users")
            .Take<User>(10, 20)
            .Desc(u => u.Id)
            .ToSelect()
            .Select(u => new { u.Id, u.Name });
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [Id],[Name] FROM [Users] ORDER BY [Id] DESC OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY", sql);
    }
    [Fact]
    public void Select2()
    {
        var select = _db.From("Users")
            .Take<User>(10, 20)
            .Desc(u => u.Id)
            .ToSelect()
            .Select(u => u.Id);
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [Id] FROM [Users] ORDER BY [Id] DESC OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY", sql);
    }
    [Fact]
    public void Filter()
    {
        var select = new TableSqlQuery<User>("Users")
            .Where(u => u.Status)
            .Take(10, 20)
            .Desc(u => u.Id)
            .ToSelect();
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT * FROM [Users] WHERE [Status]=1 ORDER BY [Id] DESC OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY", sql);
    }    
}

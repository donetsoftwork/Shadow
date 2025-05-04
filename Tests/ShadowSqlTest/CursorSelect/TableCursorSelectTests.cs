using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Simples;
using TestSupports;
namespace ShadowSqlTest.CursorSelect;

public class TableCursorSelectTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly IDB _db = SimpleDB.Use("MyDb");
    [Fact]
    public void ToSelect()
    {
        var select = _db.From("Users")
            .ToCursor(10, 20)
            .Desc("Id")
            .ToSelect()
            .Select("Id", "Name");
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [Id],[Name] FROM [Users] ORDER BY [Id] DESC OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY", sql);
    }
    [Fact]
    public void Filter()
    {
        var select = new UserTable()
            .ToSqlQuery()
            .Where(table => table.Status.EqualValue(true))
            .ToCursor(10, 20)
            .Asc(table => table.Id)
            .ToSelect()
            .Select(table => [table.Id, table.Name]);
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [Id],[Name] FROM [Users] WHERE [Status]=1 ORDER BY [Id] OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY", sql);
    }    
}

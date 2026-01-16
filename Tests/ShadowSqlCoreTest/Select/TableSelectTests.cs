using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Select;

namespace ShadowSqlCoreTest.Select;

public class TableSelectTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly IDB _db = new DB("MyDb");

    [Fact]
    public void Table()
    {
        var select = new TableSelect(_db.From("Users"));
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT * FROM [Users]", sql);
    }
    [Fact]
    public void Select()
    {
        var select = new TableSelect(_db.From("Users"))
            .Select("Id", "Name");
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [Id],[Name] FROM [Users]", sql);
    }
}

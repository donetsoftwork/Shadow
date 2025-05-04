using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Simples;

namespace ShadowSqlTest.SingleSelect;

public class TableSingleSelectTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly IDB _db = SimpleDB.Use("MyDb");

    [Fact]
    public void Select()
    {
        var cursor = _db.From("Users")
            .ToSingle("Name");
        var sql = _engine.Sql(cursor);
        //取最后一个字段
        Assert.Equal("SELECT [Name] FROM [Users]", sql);
    }
    [Fact]
    public void Desc()
    {
        var select = _db.From("Users")
            .ToCursor()
            .Desc(u => u.Field("Age"))
            .Asc(u => u.Field("Id"))
            .ToSingle("Name");
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [Name] FROM [Users] ORDER BY [Age] DESC,[Id]", sql);
    }
}

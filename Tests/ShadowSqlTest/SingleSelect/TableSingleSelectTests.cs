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
        var fetch = _db.From("Users")
            .ToSingle();
        fetch.Fields.Select("Id", "Name");
        var sql = _engine.Sql(fetch);
        //取最后一个字段
        Assert.Equal("SELECT [Name] FROM [Users]", sql);
    }
    [Fact]
    public void Cursor()
    {
        int limit = 10;
        int offset = 10;
        var select = _db.From("Users")
            .ToFetch()
            .Skip(offset)
            .Take(limit)
            .ToSingle();
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT 1 FROM [Users] OFFSET 10 ROWS FETCH NEXT 10 ROWS ONLY", sql);
    }
    [Fact]
    public void Desc()
    {
        var select = _db.From("Users")
            .ToFetch()
            .Desc(u => u.Field("Age"))
            .Asc(u => u.Field("Id"))
            .ToSingle();
        select.Fields.Select("Id", "Name");
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [Name] FROM [Users] ORDER BY [Age] DESC,[Id]", sql);
    }
}

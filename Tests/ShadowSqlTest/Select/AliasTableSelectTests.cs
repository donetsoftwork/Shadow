using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Simples;

namespace ShadowSqlTest.Select;

public class AliasTableSelectTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly IDB _db = SimpleDB.Use("MyDb");

    [Fact]
    public void Select()
    {
        var select = _db.From("Users")
            .As("u")
            .ToSelect();
        select.Fields.Select("Id", "Name");
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT u.[Id],u.[Name] FROM [Users] AS u", sql);
    }
    [Fact]
    public void Cursor()
    {
        int limit = 10;
        int offset = 10;
        var select = _db.From("Users")
            .As("u")
            .ToCursor()
            .Skip(offset)
            .Take(limit)
            .ToSelect();
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT * FROM [Users] AS u OFFSET 10 ROWS FETCH NEXT 10 ROWS ONLY", sql);
    }
    [Fact]
    public void Desc()
    {
        var select = _db.From("Users")
            .As("u")
            .ToCursor()
            .Desc(u => u.Field("Age"))
            .Asc(u => u.Field("Id"))
            .ToSelect();
        select.Fields.Select("Id", "Name");
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT u.[Id],u.[Name] FROM [Users] AS u ORDER BY u.[Age] DESC,u.[Id]", sql);
    }
}

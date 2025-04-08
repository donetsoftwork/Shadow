using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Join;

namespace ShadowSqlTest.Cursors;

public class MultiTableCursorTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly DB _db = DB.Use("MyDb");

    [Fact]
    public void ToCursor()
    {
        int limit = 10;
        int offset = 10;
        var cursor = new MultiTableSqlQuery()
            .AddMember(_db.From("Employees").As("e"))
            .AddMember(_db.From("Departments").As("d"))
            .ToCursor(limit, offset);
        Assert.Equal(limit, cursor.Limit);
        Assert.Equal(offset, cursor.Offset);
    }

    [Fact]
    public void AscMuti()
    {
        var cursor = new MultiTableSqlQuery()
            .AddMember(_db.From("Employees").As("e"))
            .AddMember(_db.From("Departments").As("d"))
            .ToCursor()
            .Asc(muti => muti.From("Employees").Field("Age"));
        var sql = _engine.Sql(cursor);
        Assert.Equal("[Employees] AS e,[Departments] AS d ORDER BY e.[Age]", sql);
    }

    [Fact]
    public void DescTable()
    {
        var cursor = new MultiTableSqlQuery()
            .AddMember(_db.From("Employees").As("e"))
            .AddMember(_db.From("Departments").As("d"))
            .ToCursor()
            .Desc("e", e => e.Field("Id"));
        var sql = _engine.Sql(cursor);
        Assert.Equal("[Employees] AS e,[Departments] AS d ORDER BY e.[Id] DESC", sql);
    }
}

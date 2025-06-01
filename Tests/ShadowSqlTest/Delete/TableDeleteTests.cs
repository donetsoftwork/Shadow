using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using TestSupports;

namespace ShadowSqlTest.Delete;

public class TableDeleteTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly IDB _db = DB.Use("MyDB");

    [Fact]
    public void Query()
    {
        var delete = new StudentTable()
            .ToSqlQuery()
            .Where(table => table.Score.LessValue(60))
            .ToDelete();
        var sql = _engine.Sql(delete);
        Assert.Equal("DELETE FROM [Students] WHERE [Score]<60", sql);
    }
    [Fact]
    public void TableQuery()
    {
        var delete = _db.From("Students")
            .ToQuery()
            .And(table => table.Field("Score").LessValue(60))
            .ToDelete();
        var sql = _engine.Sql(delete);
        Assert.Equal("DELETE FROM [Students] WHERE [Score]<60", sql);
    }
}

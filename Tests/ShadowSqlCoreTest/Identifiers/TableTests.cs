using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;

namespace ShadowSqlCoreTest.Identifiers;

public class TableTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly DB _db = DB.Use("MyDb");

    [Fact]
    public void From()
    {
        var table = _db.From("Users");
        var tableName = _engine.Sql(table);
        Assert.Equal("[Users]", tableName);
    }
}

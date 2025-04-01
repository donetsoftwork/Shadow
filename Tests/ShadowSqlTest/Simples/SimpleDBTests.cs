using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Simples;

namespace ShadowSqlTest.Simples;

public class SimpleDBTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void From()
    {
        IDB db = SimpleDB.Use("MyDb");
        var dbName = _engine.Sql(db);
        Assert.Equal("[MyDb]", dbName);
    }
}

using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;

namespace ShadowSqlTest.Identifiers;

public class DBTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void From()
    {
        IDB db = DB.Use("MyDb");
        var dbName = _engine.Sql(db);
        Assert.Equal("[MyDb]", dbName);
    }
}

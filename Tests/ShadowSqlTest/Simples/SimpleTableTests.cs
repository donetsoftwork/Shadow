using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Simples;

namespace ShadowSqlTest.Simples;

public class SimpleTableTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly SimpleDB _db = SimpleDB.Use("MyDb");

    [Fact]
    public void From()
    {
        var table = SimpleDB.From("Users");
        var tableName = _engine.Sql(table);
        Assert.Equal("[Users]", tableName);
    }
}

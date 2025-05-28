using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Tables;

namespace ShadowSqlCoreTest.Tables;

public class EmptyTableTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void From()
    {
        var users = EmptyTable.Use("Users");
        var tableName = _engine.Sql(users);
        Assert.Equal("[Users]", tableName);
    }    
}

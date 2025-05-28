using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;

namespace ShadowSqlCoreTest.Identifiers;

public class ColumnTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void Use()
    {
        Column column = Column.Use("Id");
        var columnName = _engine.Sql(column);
        Assert.Equal("[Id]", columnName);
    }
}

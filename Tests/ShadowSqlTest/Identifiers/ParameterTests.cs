using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;

namespace ShadowSqlTest.Identifiers;

public class ParameterTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void Use()
    {
        Parameter parameter = Parameter.Use("Id");
        var parameterName = _engine.Sql(parameter);
        Assert.Equal("@Id", parameterName);
    }
}

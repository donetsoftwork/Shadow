using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Logics;

namespace ShadowSqlCoreTest.Logics;

public class NotStatementLogicTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void Accept()
    {
        AtomicLogic logic = NotStatementLogic.CreateLogic("age > 25");
        var sql = _engine.Sql(logic);
        Assert.Equal("NOT age > 25", sql);
    }

    [Fact]
    public void Not()
    {
        AtomicLogic logic = NotStatementLogic.CreateLogic("age > 25");
        var not = logic.Not();
        var sql = _engine.Sql(not);
        Assert.Equal("age > 25", sql);
    }
    [Fact]
    public void Not2()
    {
        AtomicLogic logic = NotStatementLogic.CreateLogic("age > 25");
        var not2 = logic.Not().Not();
        var sql = _engine.Sql(not2);
        Assert.Equal("NOT age > 25", sql);
        Assert.StrictEqual(logic, not2);
    }
}

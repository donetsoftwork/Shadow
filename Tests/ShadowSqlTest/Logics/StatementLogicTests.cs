using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Logics;

namespace ShadowSqlTest.Logics;

public class StatementLogicTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void Accept()
    {
        AtomicLogic logic = StatementLogic.CreateLogic("age > 25");
        var sql = _engine.Sql(logic);
        Assert.Equal("age > 25", sql);
    }

    [Fact]
    public void Not()
    {
        AtomicLogic logic = StatementLogic.CreateLogic("age > 25");
        var not = logic.Not();
        var sql = _engine.Sql(not);
        Assert.Equal("NOT age > 25", sql);
    }
    [Fact]
    public void Not2()
    {
        AtomicLogic logic = StatementLogic.CreateLogic("age > 25");
        var not2 = logic.Not().Not();
        var sql = _engine.Sql(not2);
        Assert.Equal("age > 25", sql);
        Assert.StrictEqual(logic, not2);
    }
    [Fact]
    public void NotLogic()
    {
        AtomicLogic logic = StatementLogic.CreateLogic("not age > 25");
        AtomicLogic not = NotStatementLogic.CreateLogic("age > 25");
        Assert.StrictEqual(logic, not);
    }
}

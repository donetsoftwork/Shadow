using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Logics;
using System.Text;

namespace ShadowSqlCoreTest.Logics;

public class EmptyLogicTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void Accept()
    {
        EmptyLogic empty = EmptyLogic.Instance;
        var sql = new StringBuilder();
        var state = empty.TryWrite(_engine, sql);
        Assert.False(state);
        Assert.Empty(sql.ToString());
    }

    [Fact]
    public void Not()
    {
        EmptyLogic empty = EmptyLogic.Instance;
        var not = empty.Not();
        Assert.StrictEqual(empty, not);
    }
}

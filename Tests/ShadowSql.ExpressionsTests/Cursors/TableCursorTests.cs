using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Expressions;
using ShadowSql.ExpressionsTests.Supports;
using ShadowSql.Tables;

namespace ShadowSql.ExpressionsTests.Cursors;

public class TableCursorTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void Member()
    {
        var cursor = EmptyTable.Use("Users")
            .ToCursor<User>()
            .Asc(u => u.Id);
        var sql = _engine.Sql(cursor);
        Assert.Equal("[Users] ORDER BY [Id]", sql);
    }
    [Fact]
    public void Member2()
    {
        var cursor = EmptyTable.Use("Users")
            .ToCursor<User>()
            .Desc(u => new { u.Age, u.Id });
        var sql = _engine.Sql(cursor);
        Assert.Equal("[Users] ORDER BY [Age] DESC,[Id] DESC", sql);
    }
}

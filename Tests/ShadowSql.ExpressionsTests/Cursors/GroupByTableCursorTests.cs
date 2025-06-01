using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Expressions;
using ShadowSql.ExpressionsTests.Supports;
using ShadowSql.Tables;

namespace ShadowSql.ExpressionsTests.Cursors;

public class GroupByTableCursorTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void ToCursor()
    {
        var cursor = EmptyTable.Use("UserRoles")
            .GroupBy<int, UserRole>(u => u.UserId)
            .ToCursor()
            .CountAsc();
        var sql = _engine.Sql(cursor);
        Assert.Equal("[UserRoles] GROUP BY [UserId] ORDER BY COUNT(*)", sql);
    }
    [Fact]
    public void ToCursor2()
    {
        var cursor = EmptyTable.Use("UserRoles")
            .SqlGroupBy<int, UserRole>(u => u.UserId)
            .ToCursor()
            .CountDesc();
        var sql = _engine.Sql(cursor);
        Assert.Equal("[UserRoles] GROUP BY [UserId] ORDER BY COUNT(*) DESC", sql);
    }
    [Fact]
    public void Asc()
    {
        var cursor = EmptyTable.Use("UserRoles")
            .SqlGroupBy<int, UserRole>(u => u.UserId)
            .ToCursor()
            .Asc(g => g.Max(r => r.Score));
        var sql = _engine.Sql(cursor);
        Assert.Equal("[UserRoles] GROUP BY [UserId] ORDER BY MAX([Score])", sql);
    }
    [Fact]
    public void Desc()
    {
        var cursor = EmptyTable.Use("UserRoles")
            .SqlGroupBy<int, UserRole>(u => u.UserId)
            .ToCursor()
            .Desc(g => g.Max(r => r.Score));
        var sql = _engine.Sql(cursor);
        Assert.Equal("[UserRoles] GROUP BY [UserId] ORDER BY MAX([Score]) DESC", sql);
    }
}

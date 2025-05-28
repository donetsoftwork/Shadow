using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.ExpressionsTests.Supports;
using ShadowSql.Expressions;
using ShadowSql.Tables;

namespace ShadowSql.ExpressionsTests.CursorSelect;

public class GroupByTableCursorSelectTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void GroupBy()
    {
        var select = EmptyTable.Use("UserRoles")
            .GroupBy<int, UserRole>(u => u.UserId)
            .ToCursor(10, 20)
            .CountAsc()
            .ToSelect();
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [UserId] FROM [UserRoles] GROUP BY [UserId] ORDER BY COUNT(*) OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY", sql);
    }
    [Fact]
    public void SqlGroupBy()
    {
        var select = EmptyTable.Use("UserRoles")
            .SqlGroupBy<int, UserRole>(u => u.UserId)
            .ToCursor(10, 20)
            .CountDesc()
            .ToSelect();
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [UserId] FROM [UserRoles] GROUP BY [UserId] ORDER BY COUNT(*) DESC OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY", sql);
    }
    [Fact]
    public void SelectKey()
    {
        var select = EmptyTable.Use("UserRoles")
            .SqlGroupBy<int, UserRole>(u => u.UserId)
            .ToCursor(10, 20)
            .Desc(g => g.Sum(r => r.Score))
            .ToSelect()
            .SelectKey();
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [UserId] FROM [UserRoles] GROUP BY [UserId] ORDER BY SUM([Score]) DESC OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY", sql);
    }
    [Fact]
    public void Select()
    {
        var select = EmptyTable.Use("UserRoles")
            .SqlGroupBy<int, UserRole>(u => u.UserId)
            .ToCursor(10, 20)
            .CountAsc()
            .ToSelect()
            .SelectKey()
            .Select(g => new { Score = g.Max(u => u.Score) });
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [UserId],MAX([Score]) AS Score FROM [UserRoles] GROUP BY [UserId] ORDER BY COUNT(*) OFFSET 20 ROWS FETCH NEXT 10 ROWS ONLY", sql);
    }
}

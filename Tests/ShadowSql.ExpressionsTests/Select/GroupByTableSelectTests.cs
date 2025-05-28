using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.ExpressionsTests.Supports;
using ShadowSql.Expressions;
using ShadowSql.Tables;

namespace ShadowSql.ExpressionsTests.Select;

public class GroupByTableSelectTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void GroupBy()
    {
        var select = EmptyTable.Use("UserRoles")
            .GroupBy<int, UserRole>(u => u.UserId)
            .ToSelect();
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [UserId] FROM [UserRoles] GROUP BY [UserId]", sql);
    }
    [Fact]
    public void SqlGroupBy()
    {
        var select = EmptyTable.Use("UserRoles")
            .SqlGroupBy<int, UserRole>(u => u.UserId)
            .ToSelect();
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [UserId] FROM [UserRoles] GROUP BY [UserId]", sql);
    }
    [Fact]
    public void SelectKey()
    {
        var select = EmptyTable.Use("UserRoles")
            .SqlGroupBy<int, UserRole>(u => u.UserId)
            .ToSelect()
            .SelectKey();
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [UserId] FROM [UserRoles] GROUP BY [UserId]", sql);
    }
    [Fact]
    public void Select()
    {
        var select = EmptyTable.Use("UserRoles")
            .SqlGroupBy<int, UserRole>(u => u.UserId)
            .ToSelect()
            .SelectKey()
            .Select(g => new { Score = g.Max(u => u.Score) });
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [UserId],MAX([Score]) AS Score FROM [UserRoles] GROUP BY [UserId]", sql);
    }
}

using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Expressions;
using ShadowSql.ExpressionsTests.Supports;
using ShadowSql.Tables;

namespace ShadowSql.ExpressionsTests.GroupBy;

public class GroupByTableQueryTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void GroupBy()
    {
        var query = EmptyTable.Use("UserRoles")
            .GroupBy<int, UserRole>(u => u.UserId);
        var sql = _engine.Sql(query);
        Assert.Equal("[UserRoles] GROUP BY [UserId]", sql);
    }
    [Fact]
    public void GroupBy2()
    {
        var query = EmptyTable.Use("UserRoles")
            .GroupBy<int, UserRole>(u => u.Score >= 60, u => u.UserId);
        var sql = _engine.Sql(query);
        Assert.Equal("[UserRoles] WHERE [Score]>=60 GROUP BY [UserId]", sql);
    }
    [Fact]
    public void GroupBy3()
    {
        var query = EmptyTable.Use("UserRoles")
            .ToQuery<UserRole>()
            .And(u => u.Score >= 60)
            .GroupBy(u => u.UserId);
        var sql = _engine.Sql(query);
        Assert.Equal("[UserRoles] WHERE [Score]>=60 GROUP BY [UserId]", sql);
    }
    [Fact]
    public void And0()
    {
        var query = EmptyTable.Use("UserRoles")
            .GroupBy<int, UserRole>(u => u.UserId)
            .And(g => g.Key > 10);
        var sql = _engine.Sql(query);
        Assert.Equal("[UserRoles] GROUP BY [UserId] HAVING [UserId]>10", sql);
    }
    [Fact]
    public void And()
    {
        var query = EmptyTable.Use("UserRoles")
            .GroupBy<int, UserRole>(u => u.UserId)
            .And(g => g.Average(r => r.Score) > 60);
        var sql = _engine.Sql(query);
        Assert.Equal("[UserRoles] GROUP BY [UserId] HAVING AVG([Score])>60", sql);
    }
    [Fact]
    public void And2()
    {
        var query = EmptyTable.Use("UserRoles")
            .GroupBy<int, UserRole>(u => u.UserId)
            .And<UserRole>((g, p) => g.Average(r => r.Score) > p.Score);
        var sql = _engine.Sql(query);
        Assert.Equal("[UserRoles] GROUP BY [UserId] HAVING AVG([Score])>@Score", sql);
    }
    [Fact]
    public void Or()
    {
        var query = EmptyTable.Use("Users")
            .GroupBy<string?, User>(u => u.Belief)
            .Or(g => g.Min(r => r.Age) < 18 || g.Max(r => r.Age) > 60);
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] GROUP BY [Belief] HAVING MIN([Age])<18 OR MAX([Age])>60", sql);
    }
}

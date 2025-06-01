using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Expressions;
using ShadowSql.ExpressionsTests.Supports;
using ShadowSql.Tables;

namespace ShadowSql.ExpressionsTests.GroupBy;

public class GroupByTableSqlQueryTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void SqlGroupBy()
    {
        var query = EmptyTable.Use("UserRoles")
            .SqlGroupBy<int, UserRole>(u => u.UserId);
        var sql = _engine.Sql(query);
        Assert.Equal("[UserRoles] GROUP BY [UserId]", sql);
    }
    [Fact]
    public void SqlGroupBy2()
    {
        var query = EmptyTable.Use("UserRoles")
            .SqlGroupBy<int, UserRole>(u => u.Score >= 60, u => u.UserId);
        var sql = _engine.Sql(query);
        Assert.Equal("[UserRoles] WHERE [Score]>=60 GROUP BY [UserId]", sql);
    }
    [Fact]
    public void SqlGroupBy3()
    {
        var query = EmptyTable.Use("UserRoles")
            .ToSqlQuery<UserRole>()
            .Where(u => u.Score >= 60)
            .SqlGroupBy(u => u.UserId);
        var sql = _engine.Sql(query);
        Assert.Equal("[UserRoles] WHERE [Score]>=60 GROUP BY [UserId]", sql);
    }
    [Fact]
    public void Having0()
    {
        var query = EmptyTable.Use("UserRoles")
            .SqlGroupBy<int, UserRole>(u => u.UserId)
            .Having(g => g.Key > 10);
        var sql = _engine.Sql(query);
        Assert.Equal("[UserRoles] GROUP BY [UserId] HAVING [UserId]>10", sql);
    }
    [Fact]
    public void Having()
    {
        var query = EmptyTable.Use("UserRoles")
            .SqlGroupBy<int, UserRole>(u => u.UserId)
            .Having(g => g.Average(r => r.Score) > 60);
        var sql = _engine.Sql(query);
        Assert.Equal("[UserRoles] GROUP BY [UserId] HAVING AVG([Score])>60", sql);
    }
    [Fact]
    public void Having2()
    {
        var query = EmptyTable.Use("UserRoles")
            .SqlGroupBy<int, UserRole>(u => u.UserId)
            .Having<UserRole>((g, p) => g.Average(r => r.Score) > p.Score);
        var sql = _engine.Sql(query);
        Assert.Equal("[UserRoles] GROUP BY [UserId] HAVING AVG([Score])>@Score", sql);
    }
}

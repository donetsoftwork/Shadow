using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Expressions;
using ShadowSql.ExpressionsTests.Supports;
using ShadowSql.Tables;

namespace ShadowSql.ExpressionsTests.GroupBy;

public class GroupByMultiQueryTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void GroupBy()
    {
        var query = EmptyTable.Use("Users")
            .Join<User, UserRole>(EmptyTable.Use("UserRoles"))
            .And((u, r) => u.Id == r.UserId)
            .GroupBy((u, r) => r.UserId);
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId] GROUP BY t2.[UserId]", sql);
    }
    [Fact]
    public void And()
    {
        var query = EmptyTable.Use("Users")
            .Join<User, UserRole>(EmptyTable.Use("UserRoles"))
            .And((u, r) => u.Id == r.UserId)
            .GroupBy((u, r) => r.UserId)
            .And<User>("Users", g => g.Average(u => u.Age) > 18);
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId] GROUP BY t2.[UserId] HAVING AVG(t1.[Age])>18", sql);
    }
    [Fact]
    public void And2()
    {
        var query = EmptyTable.Use("Users")
            .Join<User, UserRole>(EmptyTable.Use("UserRoles"))
            .And((u, r) => u.Id == r.UserId)
            .GroupBy((u, r) => r.UserId)
            .And<User, User>("Users", (g, p) => g.Average(u => u.Age) > p.Age);
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId] GROUP BY t2.[UserId] HAVING AVG(t1.[Age])>@Age", sql);
    }
    [Fact]
    public void Or()
    {
        var query = EmptyTable.Use("Users")
            .Join<User, UserRole>(EmptyTable.Use("UserRoles"))
            .And((u, r) => u.Id == r.UserId)
            .GroupBy((u, r) => r.UserId)
            .Or<User>("Users", g => g.Average(u => u.Age) > 18)
            .Or<UserRole>("UserRoles", g => g.Average(u => u.Score) > 60);
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId] GROUP BY t2.[UserId] HAVING AVG(t1.[Age])>18 OR AVG(t2.[Score])>60", sql);
    }
}

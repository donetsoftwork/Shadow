using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Expressions;
using ShadowSql.ExpressionsTests.Supports;
using ShadowSql.Tables;

namespace ShadowSql.ExpressionsTests.GroupBy;

public class GroupByMultiSqlQueryTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void SqlGroupBy()
    {
        var query = EmptyTable.Use("Users")
            .SqlJoin<User, UserRole>(EmptyTable.Use("UserRoles"))
            .On(u => u.Id, r => r.UserId)
            .SqlGroupBy((u, r) => r.UserId);
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId] GROUP BY t2.[UserId]", sql);
    }
    [Fact]
    public void SqlGroupBy2()
    {
        var query = EmptyTable.Use("Users")
            .SqlJoin<User, UserRole>(EmptyTable.Use("UserRoles"))
            .On((u, r) => u.Id == r.UserId)
            .SqlGroupBy((u, r) => r.UserId);
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId] GROUP BY t2.[UserId]", sql);
    }
    [Fact]
    public void Having()
    {
        var query = EmptyTable.Use("Users")
            .SqlJoin<User, UserRole>(EmptyTable.Use("UserRoles"))
            .On((u, r) => u.Id == r.UserId)
            .SqlGroupBy((u, r) => r.UserId)
            .Having<User>("Users", g => g.Average(u => u.Age) > 18);
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId] GROUP BY t2.[UserId] HAVING AVG(t1.[Age])>18", sql);
    }
    [Fact]
    public void Having2()
    {
        var query = EmptyTable.Use("Users")
            .SqlJoin<User, UserRole>(EmptyTable.Use("UserRoles"))
            .On((u, r) => u.Id == r.UserId)
            .SqlGroupBy((u, r) => r.UserId)
            .Having<User, User>("Users", (g, p) => g.Average(u => u.Age) > p.Age);
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId] GROUP BY t2.[UserId] HAVING AVG(t1.[Age])>@Age", sql);
    }
}
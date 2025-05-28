using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Expressions;
using ShadowSql.ExpressionsTests.Supports;
using ShadowSql.Tables;

namespace ShadowSql.ExpressionsTests.Join;

public class JoinOnQueryTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void And()
    {
        var query = EmptyTable.Use("Users")
            .Join<User, UserRole>(EmptyTable.Use("UserRoles"))
            .And((u, r) => u.Id == r.UserId);
        var sql = _engine.Sql(query.Root);
        Assert.Equal("[Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId]", sql);
    }
    [Fact]
    public void And2()
    {
        var query = EmptyTable.Use("Users")
            .As("u")
            .Join<User, UserRole>(EmptyTable.Use("UserRoles").As("r"))
            .And((u, r) => u.Id == r.UserId);
        var sql = _engine.Sql(query.Root);
        Assert.Equal("[Users] AS u INNER JOIN [UserRoles] AS r ON u.[Id]=r.[UserId]", sql);
    }
}

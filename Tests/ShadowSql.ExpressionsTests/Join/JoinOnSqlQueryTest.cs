using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Expressions;
using ShadowSql.ExpressionsTests.Supports;
using ShadowSql.Tables;

namespace ShadowSql.ExpressionsTests.Join;

public class JoinOnSqlQueryTest
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void OnKey()
    {
        var query = EmptyTable.Use("Users")
            .SqlJoin<User, UserRole>(EmptyTable.Use("UserRoles"))
            .On(u => u.Id, r => r.UserId);
        var sql = _engine.Sql(query.Root);
        Assert.Equal("[Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId]", sql);
    }
    [Fact]
    public void On()
    {
        var query = EmptyTable.Use("Users")
            .SqlJoin<User, UserRole>(EmptyTable.Use("UserRoles"))
            .On((u, r) => u.Id == r.UserId);
        var sql = _engine.Sql(query.Root);
        Assert.Equal("[Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId]", sql);
    }
    [Fact]
    public void On2()
    {
        var query = EmptyTable.Use("Users")
            .As("u")
            .SqlJoin<User, UserRole>(EmptyTable.Use("UserRoles").As("r"))
            .On((u, r) => u.Id == r.UserId);
        var sql = _engine.Sql(query.Root);
        Assert.Equal("[Users] AS u INNER JOIN [UserRoles] AS r ON u.[Id]=r.[UserId]", sql);
    }
    [Fact]
    public void On3()
    {
        var query = EmptyTable.Use("Users")
            .SqlJoin<User, UserRole>(EmptyTable.Use("UserRoles"))
            .On((u, r) => u.Id == r.UserId + 1);
        var sql = _engine.Sql(query.Root);
        Assert.Equal("[Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=(t2.[UserId]+1)", sql);
    }
}

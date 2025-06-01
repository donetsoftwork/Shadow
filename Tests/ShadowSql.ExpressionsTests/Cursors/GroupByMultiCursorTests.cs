using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Expressions;
using ShadowSql.ExpressionsTests.Supports;
using ShadowSql.Tables;

namespace ShadowSql.ExpressionsTests.Cursors;

public class GroupByMultiCursorTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void ToCursor()
    {
        var cursor = EmptyTable.Use("Users")
            .SqlJoin<User, UserRole>(EmptyTable.Use("UserRoles"))
            .On(u => u.Id, r => r.UserId)
            .SqlGroupBy((u, r) => r.UserId)
            .ToCursor()
            .CountAsc();
        var sql = _engine.Sql(cursor);
        Assert.Equal("[Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId] GROUP BY t2.[UserId] ORDER BY COUNT(*)", sql);
    }
    [Fact]
    public void ToCursor2()
    {
        var cursor = EmptyTable.Use("Users")
            .SqlJoin<User, UserRole>(EmptyTable.Use("UserRoles"))
            .On((u, r) => u.Id == r.UserId)
            .SqlGroupBy((u, r) => r.UserId)
            .ToCursor()
            .CountDesc();
        var sql = _engine.Sql(cursor);
        Assert.Equal("[Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId] GROUP BY t2.[UserId] ORDER BY COUNT(*) DESC", sql);
    }
    [Fact]
    public void ToCursor3()
    {
        var cursor = EmptyTable.Use("Users")
            .Join<User, UserRole>(EmptyTable.Use("UserRoles"))
            .And((u, r) => u.Id == r.UserId)
            .GroupBy((u, r) => r.UserId)
            .ToCursor()
            .Asc<User, int>("Users", g => g.Sum(r => r.Age));
        var sql = _engine.Sql(cursor);
        Assert.Equal("[Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId] GROUP BY t2.[UserId] ORDER BY SUM(t1.[Age])", sql);
    }
    [Fact]
    public void Asc()
    {
        var cursor = EmptyTable.Use("Users")
            .Join<User, UserRole>(EmptyTable.Use("UserRoles"))
            .And((u, r) => u.Id == r.UserId)
            .GroupBy((u, r) => r.UserId)
            .ToCursor()
            .Asc<UserRole, int>("UserRoles", g => g.Sum(r => r.Score));
        var sql = _engine.Sql(cursor);
        Assert.Equal("[Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId] GROUP BY t2.[UserId] ORDER BY SUM(t2.[Score])", sql);
    }
    [Fact]
    public void Asc2()
    {
        var cursor = EmptyTable.Use("Users")
            .Join<User, UserRole>(EmptyTable.Use("UserRoles"))
            .And((u, r) => u.Id == r.UserId)
            .GroupBy((u, r) => r.UserId)
            .ToCursor()
            .Asc<UserRole, int>(g => g.Sum(r => r.Score));
        var sql = _engine.Sql(cursor);
        Assert.Equal("[Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId] GROUP BY t2.[UserId] ORDER BY SUM([Score])", sql);
    }
    [Fact]
    public void Desc()
    {
        var cursor = EmptyTable.Use("Users")
            .Join<User, UserRole>(EmptyTable.Use("UserRoles"))
            .And((u, r) => u.Id == r.UserId)
            .GroupBy((u, r) => r.UserId)
            .ToCursor()
            .Desc<UserRole, int>("UserRoles", g => g.Sum(r => r.Score));
        var sql = _engine.Sql(cursor);
        Assert.Equal("[Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId] GROUP BY t2.[UserId] ORDER BY SUM(t2.[Score]) DESC", sql);
    }
    [Fact]
    public void Desc2()
    {
        var cursor = EmptyTable.Use("Users")
            .Join<User, UserRole>(EmptyTable.Use("UserRoles"))
            .And((u, r) => u.Id == r.UserId)
            .GroupBy((u, r) => r.UserId)
            .ToCursor()
            .Desc<UserRole, int>(g => g.Sum(r => r.Score));
        var sql = _engine.Sql(cursor);
        Assert.Equal("[Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId] GROUP BY t2.[UserId] ORDER BY SUM([Score]) DESC", sql);
    }
}

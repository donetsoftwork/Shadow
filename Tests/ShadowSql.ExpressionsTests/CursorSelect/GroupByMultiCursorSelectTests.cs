using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Expressions;
using ShadowSql.ExpressionsTests.Supports;
using ShadowSql.Tables;

namespace ShadowSql.ExpressionsTests.CursorSelect;

public class GroupByMultiCursorSelectTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void ToSelect()
    {
        var select = EmptyTable.Use("Users")
            .SqlJoin<User, UserRole>(EmptyTable.Use("UserRoles"))
            .On(u => u.Id, r => r.UserId)
            .SqlGroupBy((u, r) => r.UserId)
            .ToCursor()
            .CountAsc()
            .ToSelect();

        var sql = _engine.Sql(select);
        Assert.Equal("SELECT t2.[UserId] FROM [Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId] GROUP BY t2.[UserId] ORDER BY COUNT(*)", sql);
    }

    [Fact]
    public void ToCursor()
    {
        var select = EmptyTable.Use("Users")
            .SqlJoin<User, UserRole>(EmptyTable.Use("UserRoles"))
            .On(u => u.Id, r => r.UserId)
            .SqlGroupBy((u, r) => r.UserId)
            .ToCursor()
            .CountAsc()
            .ToSelect()
            .SelectKey()
            .SelectCount("UserCount");

        var sql = _engine.Sql(select);
        Assert.Equal("SELECT t2.[UserId],COUNT(*) AS UserCount FROM [Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId] GROUP BY t2.[UserId] ORDER BY COUNT(*)", sql);
    }
    [Fact]
    public void Select()
    {
        var select = EmptyTable.Use("Users")
            .SqlJoin<User, UserRole>(EmptyTable.Use("UserRoles"))
            .On((u, r) => u.Id == r.UserId)
            .SqlGroupBy((u, r) => r.UserId)
            .ToCursor()
            .CountDesc()
            .ToSelect()
            .SelectKey()
            .Select<User, object>("t1", g => new { MaxAge = g.Max(u => u.Age) });

        var sql = _engine.Sql(select);
        Assert.Equal("SELECT t2.[UserId],MAX(t1.[Age]) AS MaxAge FROM [Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId] GROUP BY t2.[UserId] ORDER BY COUNT(*) DESC", sql);
    }
    [Fact]
    public void Select2()
    {
        var select = EmptyTable.Use("Users")
            .Join<User, UserRole>(EmptyTable.Use("UserRoles"))
            .And((u, r) => u.Id == r.UserId)
            .GroupBy((u, r) => r.UserId)
            .ToCursor()
            .Desc<User, int>("t1", g => g.Max(u => u.Age))
            .ToSelect()
            .SelectKey()
            .Select<User, int>(g => g.Max(u => u.Age));

        var sql = _engine.Sql(select);
        Assert.Equal("SELECT t2.[UserId],MAX([Age]) AS Age FROM [Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId] GROUP BY t2.[UserId] ORDER BY MAX(t1.[Age]) DESC", sql);
    }
}

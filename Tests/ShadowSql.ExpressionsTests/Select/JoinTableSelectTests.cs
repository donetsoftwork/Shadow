using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Expressions;
using ShadowSql.ExpressionsTests.Supports;
using ShadowSql.Tables;

namespace ShadowSql.ExpressionsTests.Select;

public class JoinTableSelectTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void SqlJoin()
    {
        var select = EmptyTable.Use("Users")
            .SqlJoin<User, UserRole>(EmptyTable.Use("UserRoles"))
            .On(u => u.Id, r => r.UserId)
            .Root
            .ToSelect();
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT * FROM [Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId]", sql);
    }
    [Fact]
    public void Select()
    {
        var select = EmptyTable.Use("Users")
            .SqlJoin<User, UserRole>(EmptyTable.Use("UserRoles"))
            .On((u, r) => u.Id == r.UserId)
            .ToSelect()
            .Select("Users", (User t1) => t1.Id);
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT t1.[Id] FROM [Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId]", sql);
    }
    [Fact]
    public void Select2()
    {
        var select = EmptyTable.Use("Users")
            .Join<User, UserRole>(EmptyTable.Use("UserRoles"))
            .And((u, r) => u.Id == r.UserId)
            .ToSelect()
            .Select("Users", (User t1) => new { t1.Id, t1.Name });
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT t1.[Id],t1.[Name] FROM [Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId]", sql);
    }
    [Fact]
    public void SelectTable()
    {
        var select = EmptyTable.Use("Users")
            .SqlJoin<User, UserRole>(EmptyTable.Use("UserRoles"))
            .On(u => u.Id, r => r.UserId)
            .ToSelect()
            .SelectTable("Users");
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT t1.* FROM [Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId]", sql);
    }
}

using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Join;
using ShadowSql.Expressions;
using ShadowSql.Tables;
using ShadowSql.ExpressionsTests.Supports;

namespace ShadowSql.ExpressionsTests.Cursors;

public class MultiTableCursorTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly DB _db = DB.Use("MyDb");

    [Fact]
    public void ToCursor()
    {
        int limit = 10;
        int offset = 10;
        var cursor = new MultiTableSqlQuery()
            .AddMembers(_db.From("Employees"), _db.From("Departments"))
            .ToCursor(limit, offset);
        Assert.Equal(limit, cursor.Limit);
        Assert.Equal(offset, cursor.Offset);
    }
    [Fact]
    public void ToCursor2()
    {
        int limit = 10;
        int offset = 10;
        var cursor = new MultiTableQuery()
            .AddMembers("Employees", "Departments")
            .ToCursor(limit, offset);
        Assert.Equal(limit, cursor.Limit);
        Assert.Equal(offset, cursor.Offset);
    }
    [Fact]
    public void ToCursor3()
    {
        int limit = 10;
        int offset = 10;
        var cursor = EmptyTable.Use("Users")
            .Join<User, UserRole>(EmptyTable.Use("UserRoles"))
            .And((u, r) => u.Id == r.UserId)
            .Root
            .ToCursor(limit, offset);
        Assert.Equal(limit, cursor.Limit);
        Assert.Equal(offset, cursor.Offset);
    }
    [Fact]
    public void ToCursor4()
    {
        int limit = 10;
        int offset = 10;
        var cursor = EmptyTable.Use("Users")
            .SqlJoin<User, UserRole>(EmptyTable.Use("UserRoles"))
            .On((u, r) => u.Id == r.UserId)
            .Root
            .ToCursor(limit, offset);
        Assert.Equal(limit, cursor.Limit);
        Assert.Equal(offset, cursor.Offset);
    }
    [Fact]
    public void Asc()
    {
        var cursor = EmptyTable.Use("Users")
            .Join<User, UserRole>(EmptyTable.Use("UserRoles"))
            .And((u, r) => u.Id == r.UserId)
            .Root
            .ToCursor()
            .Asc("t2", (UserRole t2) => t2.Id);
        var sql = _engine.Sql(cursor);
        Assert.Equal("[Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId] ORDER BY t2.[Id]", sql);
    }
    [Fact]
    public void Desc()
    {
        var cursor = EmptyTable.Use("Users")
            .As("u")
            .SqlJoin<User, UserRole>(EmptyTable.Use("UserRoles").As("r"))
            .On((u, r) => u.Id == r.UserId)
            .Root
            .ToCursor()
            .Desc("r", (UserRole t2) => t2.Id);
        var sql = _engine.Sql(cursor);
        Assert.Equal("[Users] AS u INNER JOIN [UserRoles] AS r ON u.[Id]=r.[UserId] ORDER BY r.[Id] DESC", sql);
    }
}

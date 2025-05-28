using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Expressions;
using ShadowSql.ExpressionsTests.Supports;
using ShadowSql.Tables;

namespace ShadowSql.ExpressionsTests.Delete;

public class MultiTableDeleteTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void Query()
    {
        var delete = EmptyTable.Use("Users")
            .Join<User, UserRole>(EmptyTable.Use("UserRoles"))
            .And((u, r) => u.Id == r.UserId)
            .Root
            .And<UserRole>("t2", r => r.Score == 0)
            .ToDelete();
        var sql = _engine.Sql(delete);
        Assert.Equal("DELETE t1 FROM [Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId] WHERE t2.[Score]=0", sql);
    }
    [Fact]
    public void SqlQuery()
    {
        var delete = EmptyTable.Use("Users")
            .SqlJoin<User, UserRole>(EmptyTable.Use("UserRoles"))
            .On(u => u.Id, r => r.UserId)
            .WhereRight(r => r.Score == 0)
            .Root
            .ToDelete();
        var sql = _engine.Sql(delete);
        Assert.Equal("DELETE t1 FROM [Users] AS t1 INNER JOIN [UserRoles] AS t2 ON t1.[Id]=t2.[UserId] WHERE t2.[Score]=0", sql);
    }    
}

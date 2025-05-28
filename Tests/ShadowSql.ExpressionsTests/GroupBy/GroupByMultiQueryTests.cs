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
}

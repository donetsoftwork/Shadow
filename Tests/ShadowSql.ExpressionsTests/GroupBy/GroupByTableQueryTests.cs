using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Expressions;
using ShadowSql.ExpressionsTests.Supports;
using ShadowSql.Tables;

namespace ShadowSql.ExpressionsTests.GroupBy;

public class GroupByTableQueryTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void GroupBy()
    {
        var query = EmptyTable.Use("UserRoles")
            .GroupBy<int, UserRole>(u => u.UserId);
        var sql = _engine.Sql(query);
        Assert.Equal("[UserRoles] GROUP BY [UserId]", sql);
    }
}

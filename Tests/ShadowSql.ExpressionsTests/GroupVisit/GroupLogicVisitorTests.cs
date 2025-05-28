using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Expressions;
using ShadowSql.Expressions.VisitSource;
using ShadowSql.ExpressionsTests.Supports;
using ShadowSql.Identifiers;
using ShadowSql.Logics;
using ShadowSql.Tables;
using System.Linq.Expressions;

namespace ShadowSql.ExpressionsTests.GroupVisit;

public class GroupLogicVisitorTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void Expression()
    {
        var groupBy = EmptyTable.Use("UserRoles")
            .GroupBy<int, UserRole>(u => u.UserId);
        Expression<Func<IGrouping<int, UserRole>, bool>> expression = g => g.Sum(r => r.Score) > 100;
        var fileds = new List<IFieldView>();
        var visitor = GroupByVisitor.Having(groupBy, groupBy._filter, expression);
        var sql = _engine.Sql(groupBy);
        Assert.Equal("[UserRoles] GROUP BY [UserId] HAVING SUM([Score])>100", sql);
    }
}

using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Expressions;
using ShadowSql.Expressions.VisitSource;
using ShadowSql.ExpressionsTests.Supports;
using ShadowSql.Identifiers;
using ShadowSql.Tables;
using System.Linq.Expressions;

namespace ShadowSql.ExpressionsTests.GroupVisit;

public class GroupSelectVisitorTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void Member()
    {
        var select = EmptyTable.Use("UserRoles")
            .GroupBy<int, UserRole>(u => u.UserId)
            .ToSelect();
        Expression<Func<IGrouping<int, UserRole>, int>> expression = g => g.Key;
        var visitor = GroupByVisitor.Select(select.Source, select._selected, expression);
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [UserId] FROM [UserRoles] GROUP BY [UserId]", sql);
    }
    [Fact]
    public void Member2()
    {
        var select = EmptyTable.Use("UserRoles")
            .GroupBy<UserScore, UserRole>(u => new UserScore { UserId = u.UserId, Score = u.Score })
            .ToSelect();
        Expression<Func<IGrouping<UserScore, UserRole>, int>> expression = g => g.Key.UserId;
        var visitor = GroupByVisitor.Select(select.Source, select._selected, expression);
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [UserId] FROM [UserRoles] GROUP BY [UserId],[Score]", sql);
    }
    [Fact]
    public void Member3()
    {
        var select = EmptyTable.Use("UserRoles")
            .GroupBy<UserScore, UserRole>(u => new UserScore { UserId = u.UserId, Score = u.Score })
            .ToSelect();
        Expression<Func<IGrouping<UserScore, UserRole>, UserScore>> expression = g => g.Key;
        var visitor = GroupByVisitor.Select(select.Source, select._selected, expression);
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [UserId],[Score] FROM [UserRoles] GROUP BY [UserId],[Score]", sql);
    }
    [Fact]
    public void New()
    {
        var select = EmptyTable.Use("UserRoles")
            .GroupBy<int, UserRole>(u => u.UserId)
            .ToSelect();
        Expression<Func<IGrouping<int, UserRole>, object>> expression = g => new { UserId = g.Key, Count = g.Count(), Avg = g.Average(u => u.Score) };
        var visitor = GroupByVisitor.Select(select.Source, select._selected, expression);
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [UserId],COUNT(*) AS Count,AVG([Score]) AS Avg FROM [UserRoles] GROUP BY [UserId]", sql);
    }
}

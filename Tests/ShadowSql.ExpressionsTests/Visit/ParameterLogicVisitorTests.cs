using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Expressions.VisitSource;
using ShadowSql.ExpressionsTests.Supports;
using ShadowSql.Logics;
using ShadowSql.Tables;
using System.Linq.Expressions;

namespace ShadowSql.ExpressionsTests.Visit;

public class ParameterLogicVisitorTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void Executor()
    {
        Expression<Func<User, UserParameter, bool>> expression = (u, p) => p.Name2.Equals(u.Name) && (u.Age == p.Age2 || u.Belief == null);
        var visitor = TableVisitor.Where(EmptyTable.Use("Users"), new AndLogic(), expression);
        var sql = _engine.Sql(visitor.Logic);
        Assert.Equal("@Name2=[Name] AND ([Age]=@Age2 OR [Belief] IS NULL)", sql);
    }
    [Fact]
    public void EqualsTest()
    {
        Expression<Func<User, UserParameter, bool>> expression = (u, p) => u.Name.Equals(p.Name2);
        var visitor = TableVisitor.Where(EmptyTable.Use("Users"), new AndLogic(), expression);
        var sql = _engine.Sql(visitor.Logic);
        Assert.Equal("[Name]=@Name2", sql);
    }
    [Fact]
    public void Contains()
    {
        Expression<Func<User, UserParameter, bool>> expression = (u, p) => p.Items.Contains(u.Id);
        var visitor = TableVisitor.Where(EmptyTable.Use("Users"), new AndLogic(), expression);
        var sql = _engine.Sql(visitor.Logic);
        Assert.Equal("[Id] IN @Items", sql);
    }
}

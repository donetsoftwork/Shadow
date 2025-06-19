using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Expressions;
using ShadowSql.Expressions.VisitSource;
using ShadowSql.ExpressionsTests.Supports;
using ShadowSql.Logics;
using ShadowSql.Tables;
using System.Linq.Expressions;

namespace ShadowSql.ExpressionsTests.Visit;

public class TableLogicVisitorTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void Constant()
    {      
        Expression<Func<User, bool>> expression = u => u.Name.Equals("张三") && (u.Age == 10 || u.Belief == null);
        var visitor = TableVisitor.Where(EmptyTable.Use("Users"), new AndLogic(), expression);
        var sql = _engine.Sql(visitor.Logic);
        Assert.Equal("[Name]='张三' AND ([Age]=10 OR [Belief] IS NULL)", sql);
    }
    [Fact]
    public void Constant2()
    {
        int id = 1;
        Expression<Func<User, bool>> expression = u => u.Id == id;
        var visitor = TableVisitor.Where(EmptyTable.Use("Users"), new AndLogic(), expression);
        var sql = _engine.Sql(visitor.Logic);
        Assert.Equal("[Id]=1", sql);
    }
    [Fact]
    public void Expression()
    {
        Expression<Func<User, bool>> expression = u => u.Id == u.Age;
        var visitor = TableVisitor.Where(EmptyTable.Use("Users"), new AndLogic(), expression);
        var sql = _engine.Sql(visitor.Logic);
        Assert.Equal("[Id]=[Age]", sql);
    }
    [Fact]
    public void JoinExpression()
    {
        Expression<Func<User, UserRole, bool>> expression = (u,r) => u.Id == r.UserId;
        var joinOn = EmptyTable.Use("Users").As("u")
            .Join<User, UserRole>(EmptyTable.Use("UserRoles").As("r"));
        var visitor = JoinOnVisitor.On(joinOn, new AndLogic(), expression);
        var sql = _engine.Sql(visitor.Logic);
        Assert.Equal("u.[Id]=r.[UserId]", sql);
    }
    [Fact]
    public void EqualsTest()
    {
        Expression<Func<User, bool>> expression = u => u.Id.Equals(u.Age);
        var visitor = TableVisitor.Where(EmptyTable.Use("Users"), new AndLogic(), expression);
        var sql = _engine.Sql(visitor.Logic);
        Assert.Equal("[Id]=[Age]", sql);
    }
    [Fact]
    public void NotEqualsTest()
    {
        Expression<Func<User, bool>> expression = u => !u.Id.Equals(u.Age);
        var visitor = TableVisitor.Where(EmptyTable.Use("Users"), new AndLogic(), expression);
        var sql = _engine.Sql(visitor.Logic);
        Assert.Equal("[Id]<>[Age]", sql);
    }
    [Fact]
    public void Contains()
    {
        var parameter = new UserParameter { Items = [1, 2, 3] };
        Expression<Func<User, bool>> expression = u => parameter.Items.Contains(u.Id);
        var visitor = TableVisitor.Where(EmptyTable.Use("Users"), new AndLogic(), expression);
        var sql = _engine.Sql(visitor.Logic);
        Assert.Equal("[Id] IN @Items", sql);
    }
    [Fact]
    public void Contains2()
    {
        int[] items = [1, 2, 3];
        Expression<Func<User, bool>> expression = u => items.Contains(u.Id);
        var visitor = TableVisitor.Where(EmptyTable.Use("Users"), new AndLogic(), expression);
        var sql = _engine.Sql(visitor.Logic);
        Assert.Equal("[Id] IN (1,2,3)", sql);
    }
}

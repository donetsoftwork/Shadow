using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Expressions;
using ShadowSql.Expressions.VisitSource;
using ShadowSql.ExpressionsTests.Supports;
using ShadowSql.Tables;
using System.Linq.Expressions;

namespace ShadowSql.ExpressionsTests.Visit;

public class TableSelectVisitorTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void Member()
    {
        Expression<Func<User, int>> expression = u => u.Id;
        var select = EmptyTable.Use("Users").ToSelect<User>();
        var visitor = TableVisitor.Select(select, expression);
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [Id] FROM [Users]", sql);
    }
    [Fact]
    public void Member2()
    {
        Expression<Func<User, User>> expression = u => u;
        var select = EmptyTable.Use("Users").ToSelect<User>();
        var visitor = TableVisitor.Select(select, expression);
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT * FROM [Users]", sql);
    }
    [Fact]
    public void New()
    {
        Expression<Func<User, object>> expression = u => new { u.Id, u.Name };
        var select = EmptyTable.Use("Users").ToSelect<User>();
        var visitor = TableVisitor.Select(select, expression);
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [Id],[Name] FROM [Users]", sql);
    }
    [Fact]
    public void New2()
    {
        Expression<Func<User, object>> expression = u => new { Id2 = u.Id };
        var select = EmptyTable.Use("Users").ToSelect<User>();
        var visitor = TableVisitor.Select(select, expression);
        var sql = _engine.Sql(select);
        Assert.Equal("SELECT [Id] AS Id2 FROM [Users]", sql);
    }
}

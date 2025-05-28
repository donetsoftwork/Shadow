using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Expressions;
using ShadowSql.Expressions.Visit;
using ShadowSql.Expressions.VisitSource;
using ShadowSql.ExpressionsTests.Supports;
using ShadowSql.Tables;
using System.Linq.Expressions;

namespace ShadowSql.ExpressionsTests.Visit;

public class TableUpdateVisitorTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void Member()
    {
        Expression<Func<User, int>> expression = u => u.Age;
        var table = EmptyTable.Use("Users");
        var update = table.ToUpdate<User>(u => u.Id == 1);
        var visitor = new UpdateVisitor(new TableVisitor(table, expression.Parameters[0]), update._assignInfos);
        visitor.Visit(expression.Body);
        var sql = _engine.Sql(update);
        Assert.Equal("UPDATE [Users] SET [Age]=@Age WHERE [Id]=1", sql);
    }
    [Fact]
    public void New()
    {
        Expression<Func<User, object>> expression = u => new { u.Name, u.Age };
        var table = EmptyTable.Use("Users");
        var update = table.ToUpdate<User>(u => u.Id == 1);
        var visitor = new UpdateVisitor(new TableVisitor(table, expression.Parameters[0]), update._assignInfos);
        visitor.Visit(expression.Body);
        var sql = _engine.Sql(update);
        Assert.Equal("UPDATE [Users] SET [Name]=@Name,[Age]=@Age WHERE [Id]=1", sql);
    }
    [Fact]
    public void New2()
    {
        Expression<Func<User, object>> expression = u => new { Age2 = u.Age + u.Age };
        var table = EmptyTable.Use("Users");
        var update = table.ToUpdate<User>(u => u.Id == 1);
        var visitor = new UpdateVisitor(new TableVisitor(table, expression.Parameters[0]), update._assignInfos);
        visitor.Visit(expression.Body);
        var sql = _engine.Sql(update);
        Assert.Equal("UPDATE [Users] SET [Age2]=([Age]+[Age]) WHERE [Id]=1", sql);
    }
    [Fact]
    public void New3()
    {
        Expression<Func<User, User>> expression = u => new User { Age = u.Age };
        var table = EmptyTable.Use("Users");
        var update = table.ToUpdate<User>(u => u.Id == 1);
        var visitor = new UpdateVisitor(new TableVisitor(table, expression.Parameters[0]), update._assignInfos);
        visitor.Visit(expression.Body);
        var sql = _engine.Sql(update);
        Assert.Equal("UPDATE [Users] SET [Age]=@Age WHERE [Id]=1", sql);
    }
    [Fact]
    public void New4()
    {
        Expression<Func<User, User>> expression = u => new User { Age = u.Age + 1 };
        var table = EmptyTable.Use("Users");
        var update = table.ToUpdate<User>(u => u.Id == 1);
        var visitor = new UpdateVisitor(new TableVisitor(table, expression.Parameters[0]), update._assignInfos);
        visitor.Visit(expression.Body);
        var sql = _engine.Sql(update);
        Assert.Equal("UPDATE [Users] SET [Age]=([Age]+1) WHERE [Id]=1", sql);
    }
}

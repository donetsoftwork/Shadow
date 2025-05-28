using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Expressions.Insert;
using ShadowSql.Expressions.Visit;
using ShadowSql.ExpressionsTests.Supports;
using ShadowSql.Tables;
using System.Linq.Expressions;

namespace ShadowSql.ExpressionsTests.Visit;

public class SingleInsertVisitorTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void New()
    {
        Expression<Func<User>> expression = () => new User { Name = "张三", Age = 18 };
        var table = EmptyTable.Use("Users");
        var visitor = new SingleInsertVisitor(table);
        visitor.Visit(expression.Body);
        var insert = new SingleInsert<User>(table, visitor.Items);
        var sql = _engine.Sql(insert);
        Assert.Equal("INSERT INTO [Users]([Name],[Age])VALUES('张三',18)", sql);
    }
    [Fact]
    public void New2()
    {
        Expression<Func<User, User>> expression = u => new User { Name = u.Name, Age = u.Age };
        var table = EmptyTable.Use("Users");
        var visitor = new SingleInsertVisitor(table);
        visitor.Visit(expression.Body);
        var insert = new SingleInsert<User>(table, visitor.Items);
        var sql = _engine.Sql(insert);
        Assert.Equal("INSERT INTO [Users]([Name],[Age])VALUES(@Name,@Age)", sql);
    }
}

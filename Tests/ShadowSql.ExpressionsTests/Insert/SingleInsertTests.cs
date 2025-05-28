using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Expressions;
using ShadowSql.ExpressionsTests.Supports;
using ShadowSql.Tables;

namespace ShadowSql.ExpressionsTests.Insert;

public class SingleInsertTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void ToInsert()
    {
        var insert = EmptyTable.Use("Users")
            .ToInsert(() => new User { Name = "张三", Age = 18 });
        var sql = _engine.Sql(insert);
        Assert.Equal("INSERT INTO [Users]([Name],[Age])VALUES('张三',18)", sql);
    }
    [Fact]
    public void ToInsert2()
    {
        var insert = EmptyTable.Use("Users")
            .ToInsert<UserParameter, User>(p => new User { Name = p.Name2, Age = p.Age2 });
        var sql = _engine.Sql(insert);
        Assert.Equal("INSERT INTO [Users]([Name],[Age])VALUES(@Name2,@Age2)", sql);
    }
    [Fact]
    public void ToInsert3()
    {
        var p = new User { Name = "张三", Age = 18 };
        var insert = EmptyTable.Use("Users")
            .ToInsert(() => new User { Name = p.Name, Age = p.Age });
        var sql = _engine.Sql(insert);
        Assert.Equal("INSERT INTO [Users]([Name],[Age])VALUES(@Name,@Age)", sql);
    }
    [Fact]
    public void Insert()
    {
        var insert = EmptyTable.Use("Users")
            .ToInsert<User>()
            .Insert(() => new User { Name = "张三", Age = 18 });
        var sql = _engine.Sql(insert);
        Assert.Equal("INSERT INTO [Users]([Name],[Age])VALUES('张三',18)", sql);
    }
    [Fact]
    public void Insert2()
    {
        var insert = EmptyTable.Use("Users")
            .ToInsert<User>()
            .Insert<User>(u => new User { Name = u.Name, Age = u.Age });
        var sql = _engine.Sql(insert);
        Assert.Equal("INSERT INTO [Users]([Name],[Age])VALUES(@Name,@Age)", sql);
    }
    [Fact]
    public void Insert3()
    {
        var parameter = new User { Name = "张三", Age = 18 };
        var insert = EmptyTable.Use("Users")
            .ToInsert<User>()
            .Insert(() => new User { Name = parameter.Name, Age = parameter.Age });
        var sql = _engine.Sql(insert);
        Assert.Equal("INSERT INTO [Users]([Name],[Age])VALUES(@Name,@Age)", sql);
    }
}

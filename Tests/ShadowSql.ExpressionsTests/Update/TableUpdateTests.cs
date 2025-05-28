using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Expressions;
using ShadowSql.ExpressionsTests.Supports;
using ShadowSql.Tables;

namespace ShadowSql.ExpressionsTests.Update;

public class TableUpdateTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void SetProperty()
    {
        var update = EmptyTable.Use("Users")
            .ToUpdate<User>(u => u.Id == 1)
            .Set(u => new User { Age = 18 });
        var sql = _engine.Sql(update);
        Assert.Equal("UPDATE [Users] SET [Age]=18 WHERE [Id]=1", sql);
    }
    [Fact]
    public void SetProperty2()
    {
        var update = EmptyTable.Use("Students")
            .ToUpdate<Student>(u => u.Score < 60 && u.Score > 55)
            .Set(u => new Student { Score = u.Score + 5 });
        var sql = _engine.Sql(update);
        Assert.Equal("UPDATE [Students] SET [Score]=([Score]+5) WHERE [Score]<60 AND [Score]>55", sql);
    }
    [Fact]
    public void SetProperty3()
    {
        var user = new User { Id =1, Age = 18 };
        var update = EmptyTable.Use("Users")
            .ToUpdate<User>(u => u.Id == user.Id)
            .Set(u => new User { Age = user.Age });
        var sql = _engine.Sql(update);
        Assert.Equal("UPDATE [Users] SET [Age]=@Age WHERE [Id]=@Id", sql);
    }
}

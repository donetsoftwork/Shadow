using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Expressions;
using ShadowSql.Expressions.Tables;
using ShadowSql.ExpressionsTests.Supports;
using ShadowSql.Tables;

namespace ShadowSql.ExpressionsTests.Tables;

public class TableSqlQueryTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void ToSqlQuery()
    {
        var query = EmptyTable.Use("User")
            .ToSqlQuery<User>()
            .Where(u => u.Name == "张三");
        var sql = _engine.Sql(query);
        Assert.Equal("[User] WHERE [Name]='张三'", sql);
    }
    [Fact]
    public void ToSqlOrQuery()
    {
        var query = EmptyTable.Use("User")
            .ToSqlOrQuery<User>()
            .Where(u => u.Age > 18)
            .Where(u => u.Status);
        var sql = _engine.Sql(query);
        Assert.Equal("[User] WHERE [Age]>18 OR [Status]=1", sql);
    }

    [Fact]
    public void TEntity()
    {
        var query = new TableSqlQuery<User>()
            .Where(u => u.Name == "张三");
        var sql = _engine.Sql(query);
        Assert.Equal("[User] WHERE [Name]='张三'", sql);
    }
    [Fact]
    public void TableName()
    {
        var query = new TableSqlQuery<User>("Users")
            .Where(u => u.Name == "张三");
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Name]='张三'", sql);
    }
    [Fact]
    public void Parameter()
    {
        var query = new TableSqlQuery<User>()
            .Where<UserParameter>((u, p) =>  u.Age > p.Age2);
        var sql = _engine.Sql(query);
        // 支持传入参数类型
        Assert.Equal("[User] WHERE [Age]>@Age2", sql);
    }

    [Fact]
    public void Parameter2()
    {
        var query = new TableSqlQuery<User>()
            .Where<UserParameter>((u, p) => p.Age2 > u.Age);
        var sql = _engine.Sql(query);
        // 支持传入参数类型
        Assert.Equal("[User] WHERE @Age2>[Age]", sql);
    }
}

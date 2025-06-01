using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Expressions;
using ShadowSql.Expressions.Tables;
using ShadowSql.ExpressionsTests.Supports;
using ShadowSql.Tables;

namespace ShadowSql.ExpressionsTests.Tables;

public class TableQueryTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void ToQuery()
    {
        var query = EmptyTable.Use("User")
            .ToQuery<User>()
            .And(u => u.Name == "张三");
        var sql = _engine.Sql(query);
        Assert.Equal("[User] WHERE [Name]='张三'", sql);
    }
    [Fact]
    public void ToSqlOrQuery()
    {
        var query = EmptyTable.Use("User")
            .ToOrQuery<User>()
            .Or(u => u.Age > 18)
            .Or(u => u.Status);
        var sql = _engine.Sql(query);
        Assert.Equal("[User] WHERE [Age]>18 OR [Status]=1", sql);
    }
    [Fact]
    public void TEntity()
    {
        var query = new TableQuery<User>()
            .And(u => u.Name == "张三");
        var sql = _engine.Sql(query);
        Assert.Equal("[User] WHERE [Name]='张三'", sql);
    }
    [Fact]
    public void TableName()
    {
        var query = new TableQuery<User>("Users")
            .And(u => u.Status);
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Status]=1", sql);
    }
    [Fact]
    public void Not()
    {
        var query = new TableQuery<User>()
            .And(u => !(u.Name == "张三"));
        var sql = _engine.Sql(query);
        Assert.Equal("[User] WHERE [Name]<>'张三'", sql);
    }
    [Fact]
    public void Parameter()
    {
        var query = new TableQuery<User>()
            .And<UserParameter>((u, p) => p.Age2 > u.Age);
        var sql = _engine.Sql(query);
        // 支持传入参数类型
        // 支持参数的位置在前面(最好是字段在前,参数在后),生成的sql会把字段提到前面,且运算符可能会变化
        Assert.Equal("[User] WHERE @Age2>[Age]", sql);
    }
    [Fact]
    public void OrParameter()
    {
        var query = new TableQuery<User>()
            .Or<UserParameter>((u, p) => u.Age > p.Age2 || u.Id == p.Id2);
        var sql = _engine.Sql(query);
        Assert.Equal("[User] WHERE [Age]>@Age2 OR [Id]=@Id2", sql);
    }
}

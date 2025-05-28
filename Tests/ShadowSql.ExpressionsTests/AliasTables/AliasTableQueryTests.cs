using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Expressions.Tables;
using ShadowSql.ExpressionsTests.Supports;

namespace ShadowSql.ExpressionsTests.AliasTables;

public class AliasTableQueryTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

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
            .And(u => u.Name == "张三");
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Name]='张三'", sql);
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
}

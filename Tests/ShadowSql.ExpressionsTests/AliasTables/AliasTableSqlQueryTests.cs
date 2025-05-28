using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Expressions;
using ShadowSql.ExpressionsTests.Supports;
using ShadowSql.Tables;

namespace ShadowSql.ExpressionsTests.AliasTables;

public class AliasTableSqlQueryTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void TEntity()
    {
        
        var query = EmptyTable.Use("Users")
            .As("u")
            .ToSqlQuery<User>()
            .Where(u => u.Name == "张三");
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] AS u WHERE u.[Name]='张三'", sql);
    }

    [Fact]
    public void Parameter()
    {
        var query = EmptyTable.Use("Users")
            .As("u")
            .ToSqlQuery<User>()
            .Where<UserParameter>((u, p) => p.Age2 > u.Age);
        var sql = _engine.Sql(query);
        // 支持传入参数类型
        // 支持参数的位置在前面(最好是字段在前,参数在后),生成的sql会把字段提到前面,且运算符可能会变化
        Assert.Equal("[Users] AS u WHERE @Age2>u.[Age]", sql);
    }
}

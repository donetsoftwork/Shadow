using ShadowSql;
using ShadowSql.Components;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;

namespace ShadowSqlTest.SqlValues;

public class SqlValueProviderTests
{
    const string trueExpression = "1";
    const string falseExpression = "0";
    const string nullExpression = "NULL";
    private readonly static SqlValueComponent provider = new(trueExpression, falseExpression, nullExpression);
    private readonly ISqlEngine _engine = new MsSqlEngine(new MsSqlSelectComponent(), provider, null);
    [Fact]
    public void True()
    {
        var sqlValue = provider.SqlValue(true);
        var sql = _engine.Sql(sqlValue);
        Assert.Equal(trueExpression, sql);
    }
    [Fact]
    public void False()
    {
        var sqlValue = provider.SqlValue(false);
        var sql = _engine.Sql(sqlValue);
        Assert.Equal(falseExpression, sql);
    }
    [Fact]
    public void Null()
    {
        string? str = null;
        var sqlValue = provider.SqlValue(str);
        var sql = _engine.Sql(sqlValue);
        Assert.Equal(nullExpression, sql);
    }
    [Fact]
    public void Int()
    {
        var sqlValue = provider.SqlValue(123);
        var sql = _engine.Sql(sqlValue);
        Assert.Equal("123", sql);
    }
    [Fact]
    public void Long()
    {
        long val = 789L;
        var sqlValue = provider.SqlValue(val);
        var sql = _engine.Sql(sqlValue);
        Assert.Equal("789", sql);
    }
    [Fact]
    public void Double()
    {
        double val = 789.3D;
        var sqlValue = provider.SqlValue(val);
        var sql = _engine.Sql(sqlValue);
        Assert.Equal("789.3", sql);
    }
    [Fact]
    public void Decimal()
    {
        decimal val = 789.3M;
        var sqlValue = provider.SqlValue(val);
        var sql = _engine.Sql(sqlValue);
        Assert.Equal("789.3", sql);
    }
    [Fact]
    public void Str()
    {
        string val = "张三";
        var sqlValue = provider.SqlValue(val);
        var sql = _engine.Sql(sqlValue);
        Assert.Equal("'张三'", sql);
    }
}

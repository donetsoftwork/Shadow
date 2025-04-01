using ShadowSql;
using ShadowSql.Components;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.SqlVales;

namespace ShadowSqlTest.SqlValues;

public class SqlValuesTests
{
    const string trueExpression = "1";
    const string falseExpression = "0";
    const string nullExpression = "NULL";
    private readonly static SqlValueComponent provider = new(trueExpression, falseExpression, nullExpression);
    private readonly ISqlEngine _engine = new MsSqlEngine(new MsSqlSelectComponent(), provider, null);
    [Fact]
    public void Bool()
    {
        var sqlValue = SqlValue.Values(true, false, true);
        var sql = _engine.Sql(sqlValue);
        Assert.Equal("(1,0,1)", sql);
    }
    [Fact]
    public void Int()
    {
        var sqlValue = SqlValue.Values(1, 2, 3);
        var sql = _engine.Sql(sqlValue);
        Assert.Equal("(1,2,3)", sql);
    }
    [Fact]
    public void Long()
    {
        long[] vals = [7L, 8L, 9L];
        var sqlValue = SqlValue.Values(vals);
        var sql = _engine.Sql(sqlValue);
        Assert.Equal("(7,8,9)", sql);
    }
    [Fact]
    public void Double()
    {
        double[] vals = [1.2D, 2.3D];
        var sqlValue = SqlValue.Values(vals);
        var sql = _engine.Sql(sqlValue);
        Assert.Equal("(1.2,2.3)", sql);
    }
    [Fact]
    public void Decimal()
    {
        decimal[] vals = [1.2M, 2.3M];
        var sqlValue = SqlValue.Values(vals);
        var sql = _engine.Sql(sqlValue);
        Assert.Equal("(1.2,2.3)", sql);
    }
    [Fact]
    public void Str()
    {
        string[] vals = ["张三", "李四"];
        var sqlValue = SqlValue.Values(vals);
        var sql = _engine.Sql(sqlValue);
        Assert.Equal("('张三','李四')", sql);
    }
}

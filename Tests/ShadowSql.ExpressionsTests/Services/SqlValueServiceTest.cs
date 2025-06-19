using ShadowSql.Expressions.Services;
using static ShadowSql.SqlVales.SqlValue;

namespace ShadowSql.ExpressionsTests.Services;

public class SqlValueServiceTest
{
    [Fact]
    public void From()
    {
        int id = 100;
        var val = SqlValueService.From(typeof(int), id);
        var wraper = val as SqlValueWraper<int>;
        Assert.Equal(id, wraper!._value);
    }
    [Fact]
    public void Values()
    {
        int[] ids = [1, 2, 3];
        var val = SqlValueService.Values(typeof(int), ids);
        var wraper = val as SqlValuesWraper<int>;
        Assert.Equal(ids, wraper!._values);
    }
}

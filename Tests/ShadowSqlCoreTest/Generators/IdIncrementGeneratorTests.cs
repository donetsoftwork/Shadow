using ShadowSql.Generators;

namespace ShadowSqlCoreTest.Generators;

public class IdIncrementGeneratorTests
{
    [Fact]
    public void NewName()
    {
        var generator = new IdIncrementGenerator("Product_");
        var name1 = generator.NewName();
        Assert.Equal("Product_1", name1);
        var name2 = generator.NewName();
        Assert.Equal("Product_2", name2);
    }
    [Fact]
    public void Step()
    {
        var generator = new IdIncrementGenerator("t", 8, 10);
        var name1 = generator.NewName();
        Assert.Equal("t18", name1);
        var name2 = generator.NewName();
        Assert.Equal("t28", name2);
    }
}

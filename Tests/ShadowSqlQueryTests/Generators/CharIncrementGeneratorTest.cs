using ShadowSql.Generators;

namespace ShadowSqlQueryTests.Generators;

public class CharIncrementGeneratorTest
{
    [Fact]
    public void NewName()
    {
        var generator = new CharIncrementGenerator('A');
        var name1 = generator.Create();
        Assert.Equal("A", name1);
        var name2 = generator.Create();
        Assert.Equal("B", name2);
    }
}

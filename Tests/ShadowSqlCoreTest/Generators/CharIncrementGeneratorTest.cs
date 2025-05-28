using ShadowSql.Generators;

namespace ShadowSqlCoreTest.Generators;

public class CharIncrementGeneratorTest
{
    [Fact]
    public void NewName()
    {
        var generator = new CharIncrementGenerator('A');
        var name1 = generator.NewName();
        Assert.Equal("A", name1);
        var name2 = generator.NewName();
        Assert.Equal("B", name2);
    }
}

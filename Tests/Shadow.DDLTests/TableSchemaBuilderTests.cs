using Shadow.DDL;

namespace Shadow.DDLTests;

public class TableSchemaBuilderTests
{
    [Fact]
    public void DefineKeys()
    {
        var builder = new TableSchemaBuilder("user_role", "tenant1")
            .DefineKeys("INT", "UserId", "RoleId")
            .DefinColumns("DATETIME", "CreateTime");
        var table = builder.Build();
        Assert.Equal("tenant1", table.Schema);
        Assert.Equal("user_role", table.Name);
        Assert.Equal(3, table.Columns.Length);
        Assert.Equal(2, table.Keys.Length);
    }
}

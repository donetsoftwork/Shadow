using Shadow.DDL;
using Shadow.DDL.SqlTypes;
using ShadowSql.Sqlite;

namespace Shadow.DDLTests;

public class TableSchemaBuilderTests
{
    [Fact]
    public void DefineKeys()
    {
        var builder = new TableSchemaBuilder("user_role", "tenant1")
            .DefineKeys(SqliteSqlType.Integer, "UserId", "RoleId")
            .DefinColumns(SqlType.Create<DateTime>(), "CreateTime");
        var table = builder.Build();
        Assert.Equal("tenant1", table.Schema);
        Assert.Equal("user_role", table.Name);
        Assert.Equal(3, table.Columns.Length);
        Assert.Equal(2, table.Keys.Length);
    }
}

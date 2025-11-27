using Dapper;
using Dapper.Shadow;
using ShadowSql;
using ShadowSql.Identifiers;
using ShadowSql.Insert;
using ShadowSql.Select;
namespace Dapper.ShadowCoreTests.Insert;

public class SelectInsertTests : ExecuteTestBase
{
    [Fact]
    public async Task SelectInsert()
    {
        var table = new StudentTable("Students");
        await using var resource = new TableResource<StudentTable>(SqliteExecutor, table);
        await resource.InitAsync();

        var name = Column.Use("Name");
        var age = Column.Use("Age");
        var select = new TableSelect(table)
            .Select(name, age);
        var backup = new Table("Backup2025")
            .AddColums(name, age);

        var table2 = new StudentTable("Backup2025");
        await using var resource2 = new TableResource<StudentTable>(SqliteExecutor, table2);
        await resource2.InitAsync();

        var insert = new SelectInsert(backup, select);
        var result = await insert.ExecuteAsync(SqliteExecutor);
        Assert.Equal(0, result);
    }
}

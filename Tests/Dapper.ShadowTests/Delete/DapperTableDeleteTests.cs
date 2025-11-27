using Dapper.Shadow;
using ShadowSql;

namespace Dapper.ShadowTests.Delete;

public class DapperTableDeleteTests : ExecuteTestBase
{
    [Fact]
    public async Task ToDapperDelete()
    {
        var table = new StudentTable();
        await using var resource = new TableResource<StudentTable>(SqliteExecutor, table);
        await resource.InitAsync();

        var result = await table.ToSqlQuery()
            .Where(table.Age.LessValue(7))
            .ToDapperDelete(SqliteExecutor)
            .ExecuteAsync();
        Assert.Equal(0, result);
    }

    [Fact]
    public async Task TableDelete()
    {
        await using var resource = new TableResource<StudentTable>(SqliteExecutor, new StudentTable());
        await resource.InitAsync();

        var result = await new StudentTable()
            .ToSqlQuery()
            .Where(table => table.Age.LessValue(7))
            .ToDelete()
            .ExecuteAsync(SqliteExecutor);
        Assert.Equal(0, result);
    }
}

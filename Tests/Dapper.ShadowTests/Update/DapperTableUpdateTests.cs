using Dapper.Shadow;
using ShadowSql;

namespace Dapper.ShadowTests.Update;

public class DapperTableUpdateTests : ExecuteTestBase
{
    [Fact]
    public async Task ToDapperUpdate()
    {
        var table = new StudentTable();
        await using var resource = new StudentTableResource(SqliteExecutor, table);
        await resource.InitAsync();

        var result = await table.ToSqlQuery()
            .Where(table.Age.LessValue(7))
            .ToDapperUpdate(SqliteExecutor)
            .Set(table.ClassId.AssignValue(1))
            .ExecuteAsync();
        Assert.Equal(0, result);
    }

    [Fact]
    public async Task TableUpdate()
    {
        await using var resource = new StudentTableResource(SqliteExecutor);
        await resource.InitAsync();

        var result = await new StudentTable()
            .ToSqlQuery()
            .Where(table => table.Age.LessValue(7))
            .ToUpdate()
            .Set(table => table.ClassId.AssignValue(1))
            .ExecuteAsync(SqliteExecutor);
        Assert.Equal(0, result);
    }
}

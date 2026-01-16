using ShadowSql;
using ShadowSql.Tables;
using ShadowSql.Update;

namespace Dapper.ShadowCoreTests.Update;

public class TableUpdateTests : ExecuteTestBase
{
    [Fact]
    public async Task Filter()
    {
        var table = new StudentTable();
        await using var resource = new TableResource<StudentTable>(SqliteExecutor, table);
        await resource.InitAsync();

        var update = new TableUpdate(table, table.Age.LessValue(7))
            .Set(table.ClassId.AssignValue(1));
        var result = await SqliteExecutor.ExecuteAsync(update);
        Assert.Equal(0, result);
    }

    [Fact]
    public async Task SqlQuery()
    {
        var table = new StudentTable();
        await using var resource = new TableResource<StudentTable>(SqliteExecutor, table);
        await resource.InitAsync();

        var query = new TableSqlQuery(table)
            .Where(table.Age.LessValue(7));
        var update = new TableUpdate(table, query.Filter)
            .Set(table.ClassId.AssignValue(1));
        var result = await SqliteExecutor.ExecuteAsync(update);
        Assert.Equal(0, result);
    }
}

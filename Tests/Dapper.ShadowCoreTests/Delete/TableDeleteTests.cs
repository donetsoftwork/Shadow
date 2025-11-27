using ShadowSql;
using ShadowSql.Delete;
using ShadowSql.Tables;

namespace Dapper.ShadowCoreTests.Delete;

public class TableDeleteTests : ExecuteTestBase
{
    [Fact]
    public async Task ToDapperDelete()
    {
        var table = new StudentTable();
        await using var resource = new TableResource<StudentTable>(SqliteExecutor, table);
        await resource.InitAsync();

        var query = new TableSqlQuery(table)
            .Where(table.Age.LessValue(7));
        var delete = new TableDelete(table, query.Filter);
        var result = await SqliteExecutor.ExecuteAsync(delete);
        //var result = delete.Execute(SqliteExecutor);
        Assert.Equal(0, result);
    }
}

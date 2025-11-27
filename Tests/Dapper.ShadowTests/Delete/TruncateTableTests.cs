using Dapper.Shadow;
using ShadowSql;

namespace Dapper.ShadowTests.Delete;

public class TruncateTableTests : ExecuteTestBase
{
    [Fact]
    public async Task Truncate()
    {
        await using var resource = new TableResource<StudentTable>(SqliteExecutor, new StudentTable());
        await resource.InitAsync();

        var result = await new StudentTable()
            .ToTruncate()
            .ExecuteAsync(SqliteExecutor);
        Assert.Equal(0, result);
    }
}

using Dapper.Shadow;
using Microsoft.Data.Sqlite;
using ShadowSql.Engines.Sqlite;

namespace Dapper.ShadowTests;

public class DapperExecutorTests
{
    [Fact]
    public void Executor()
    {
        var engine = new SqliteEngine();
        var connection = new SqliteConnection("Data Source=file::memory:;Cache=Shared");
        var executor = new DapperExecutor(engine, connection);
        Assert.IsAssignableFrom<IExecutor>(executor);
    }
}

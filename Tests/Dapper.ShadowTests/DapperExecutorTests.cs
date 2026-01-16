using Dapper.Shadow;
using Microsoft.Data.Sqlite;
using Hand.Data;
using ShadowSql.Engines.Sqlite;

namespace Dapper.ShadowTests;

public class DapperExecutorTests
{
    [Fact]
    public void Executor()
    {
        var engine = new SqliteEngine();
        var dataSource = new StringDataSource("Data Source=file::memory:;Cache=Shared", conn => new SqliteConnection(conn));
        var executor = new DapperExecutor(engine, new SqlSource(dataSource));
        Assert.IsAssignableFrom<IExecutor>(executor);
    }
}

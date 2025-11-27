using ShadowSql;
using Dapper.Shadow;
using Shadow.DDL;

namespace Shadow.DDLTests;

public class DropTableTests : ExecuteTestBase, IDisposable
{
    public DropTableTests()
    {       
    }
    [Fact]
    public void Drop()
    {
        var drop = new DropTable("Students");
        var sql = SqliteExecutor.Engine.Sql(drop);
        Assert.Equal("DROP TABLE \"Students\"", sql);
    }
    [Fact]
    public async Task Execute()
    {
        await CreateStudentsTable();
        var result = await new StudentTable()
            .ToDrop()
            .ExecuteAsync(SqliteExecutor);
        Assert.Equal(0, result);
        await DropStudentsTable();
    }
    void IDisposable.Dispose() {}
}

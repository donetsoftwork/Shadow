using ShadowSql;
using Dapper.Shadow;
using Shadow.DDL;

namespace Shadow.DDLTests;

public class DropTableTests : ExecuteTestBase, IDisposable
{
    public DropTableTests()
    {
        CreateStudentsTable();
    }
    [Fact]
    public void Drop()
    {
        var drop = new DropTable("Students");
        var sql = SqliteExecutor.Engine.Sql(drop);
        Assert.Equal("DROP TABLE \"Students\"", sql);
    }
    [Fact]
    public void Execute()
    {
        var result = new StudentTable()
            .ToDrop()
            .Execute(SqliteExecutor);
        Assert.Equal(0, result);
    }
    void IDisposable.Dispose()
     => DropStudentsTable();
}

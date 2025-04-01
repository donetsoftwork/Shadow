using Dapper.Shadow;
using ShadowSql;

namespace Dapper.ShadowTests.Delete;

public class DapperTableDeleteTests : ExecuteTestBase
{
    public DapperTableDeleteTests()
    {
        CreateStudentTable();
    }
    [Fact]
    public void ToDapperDelete()
    {
        var table = new StudentTable();
        var result = table.ToSqlQuery()
            .Where(table.Age.LessValue(7))
            .ToDapperDelete(SqliteExecutor)
            .Execute();
        Assert.Equal(0, result);
    }
}

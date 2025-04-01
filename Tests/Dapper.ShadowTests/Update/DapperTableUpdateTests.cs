using Dapper.Shadow;
using ShadowSql;

namespace Dapper.ShadowTests.Update;

public class DapperTableUpdateTests : ExecuteTestBase
{
    public DapperTableUpdateTests()
    {
        CreateStudentTable();
    }

    [Fact]
    public void ToDapperUpdate()
    {
        var table = new StudentTable();
        var result = table.ToQuery()
            .Where(table.Age.LessValue(7))
            .ToDapperUpdate(SqliteExecutor)
            .Set(table.ClassId.EqualToValue(1))
            .Execute();
        Assert.Equal(0, result);
    }
}

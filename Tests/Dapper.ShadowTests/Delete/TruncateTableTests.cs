using Dapper.Shadow;
using ShadowSql;

namespace Dapper.ShadowTests.Delete;

public class TruncateTableTests : ExecuteTestBase
{
    public TruncateTableTests()
    {
        CreateStudentTable();
    }
    [Fact]
    public void Truncate()
    {
        var result = new StudentTable()
            .ToTruncate()
            .Execute(SqliteExecutor);
        Assert.Equal(0, result);
    }
}

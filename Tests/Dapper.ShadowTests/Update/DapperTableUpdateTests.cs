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
        var result = table.ToSqlQuery()
            .Where(table.Age.LessValue(7))
            .ToDapperUpdate(SqliteExecutor)
            .Set(table.ClassId.AssignValue(1))
            .Execute();
        Assert.Equal(0, result);
    }

    [Fact]
    public void TableUpdate()
    {
        var result = new StudentTable()
            .ToSqlQuery()
            .Where(table => table.Age.LessValue(7))
            .ToUpdate()
            .Set(table => table.ClassId.AssignValue(1))
            .Execute(SqliteExecutor);
        Assert.Equal(0, result);
    }
}

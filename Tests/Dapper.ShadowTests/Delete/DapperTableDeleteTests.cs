using Dapper.Shadow;
using ShadowSql;

namespace Dapper.ShadowTests.Delete;

public class DapperTableDeleteTests : ExecuteTestBase, IDisposable
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

    [Fact]
    public void TableDelete()
    {
        var result = new StudentTable()
            .ToSqlQuery()
            .Where(table => table.Age.LessValue(7))
            .ToDelete()
            .Execute(SqliteExecutor);
        Assert.Equal(0, result);
    }

    void IDisposable.Dispose()
        => DropStudentTable();
}

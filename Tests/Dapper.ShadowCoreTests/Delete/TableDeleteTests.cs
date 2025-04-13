using ShadowSql;
using ShadowSql.Delete;
using ShadowSql.Tables;
using Dapper.Shadow;

namespace Dapper.ShadowCoreTests.Delete;

public class TableDeleteTests : ExecuteTestBase, IDisposable
{
    public TableDeleteTests()
    {
        CreateStudentTable();
    }
    [Fact]
    public void ToDapperDelete()
    {
        var table = new StudentTable();
        var query = new TableSqlQuery(table)
            .Where(table.Age.LessValue(7));
        var delete = new TableDelete(table, query.Filter);
        var result = SqliteExecutor.Execute(delete);
        //var result = delete.Execute(SqliteExecutor);
        Assert.Equal(0, result);
    }

    void IDisposable.Dispose()
        => DropStudentTable();
}

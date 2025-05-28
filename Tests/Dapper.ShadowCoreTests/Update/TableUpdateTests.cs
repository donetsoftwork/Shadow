using Dapper;
using Dapper.Shadow;
using ShadowSql;
using ShadowSql.Tables;
using ShadowSql.Update;

namespace Dapper.ShadowCoreTests.Update;

public class TableUpdateTests : ExecuteTestBase
{
    public TableUpdateTests()
    {
        CreateStudentTable();
    }

    [Fact]
    public void Filter()
    {
        var table = new StudentTable();
        var update = new TableUpdate(table, table.Age.LessValue(7))
            .Set(table.ClassId.AssignValue(1));
        var result = SqliteExecutor.Execute(update);
        Assert.Equal(0, result);
    }

    [Fact]
    public void SqlQuery()
    {
        var table = new StudentTable();
        var query = new TableSqlQuery(table)
            .Where(table.Age.LessValue(7));
        var update = new TableUpdate(table, query.Filter)
            .Set(table.ClassId.AssignValue(1));
        var result = SqliteExecutor.Execute(update);
        Assert.Equal(0, result);
    }
}

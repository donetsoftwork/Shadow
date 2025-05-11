using Dapper;
using Dapper.Shadow;
using ShadowSql;
using ShadowSql.Identifiers;
using ShadowSql.Insert;
using ShadowSql.Select;
namespace Dapper.ShadowCoreTests.Insert;

public class SelectInsertTests : ExecuteTestBase, IDisposable
{
    public SelectInsertTests()
    {
        CreateStudentTable("Students");
        CreateStudentTable("Backup2025");
    }

    [Fact]
    public void SelectInsert()
    {
        var table = new Table("Students");
        var name = Column.Use("Name");
        var age = Column.Use("Age");
        var select = new TableSelect(table)
            .Select(name, age);
        var backup = new Table("Backup2025")
            .AddColums(name, age);
        var insert = new SelectInsert(backup, select);
        var result = insert.Execute(SqliteExecutor);
        Assert.Equal(0, result);
    }

    void IDisposable.Dispose()
    {
        DropStudentTable("Students");
        DropStudentTable("Backup2025");
    }
}

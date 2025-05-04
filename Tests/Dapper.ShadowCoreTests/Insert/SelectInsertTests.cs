using Dapper;
using Dapper.Shadow;
using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
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

    //static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly DB _db = DB.Use("MyDB");
    static readonly Table _students = _db.From("Students");

    [Fact]
    public void SelectInsert()
    {
        var select = new TableSelect( _students);
        select.Select(_name, _age);
        var backup = _db.From("Backup2025")
            .AddColums(_name, _age);
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

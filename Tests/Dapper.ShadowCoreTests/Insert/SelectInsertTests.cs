using Dapper;
using Dapper.Shadow;
using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
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
    public void ToInsert()
    {
        var select = _students.ToSelect();
        select.Fields.Select(_name, _age);
        var insert = SqliteExecutor.From("Backup2025")
            .AddColums(_name, _age)
            .ToDapperInsert(select);
        var result = insert.Execute();
        Assert.Equal(0, result);
    }

    [Fact]
    public void InsertTo()
    {
        var backup = _db.From("Backup2025")
            .AddColums(_name, _age);
        var insert = _students.ToSelect()
            .Select(select => select.Fields.Select(_name, _age))
            .InsertTo(backup);
        var result = insert.Execute(SqliteExecutor);
        Assert.Equal(0, result);
    }

    void IDisposable.Dispose()
    {
        DropStudentTable("Students");
        DropStudentTable("Backup2025");
    }
}

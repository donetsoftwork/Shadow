using Dapper.Shadow;
using ShadowSql;
using ShadowSql.Identifiers;
namespace Dapper.ShadowTests.Insert;

public class SelectInsertTests : ExecuteTestBase
{
    static readonly DB _db = DB.Use("MyDB");
    static readonly Table _students = _db.From("Students");

    [Fact]
    public async Task ToInsert()
    {
        await using var resource = new TableResource<StudentTable>(SqliteExecutor, new StudentTable("Students"));
        await resource.InitAsync();
        await using var resource2 = new TableResource<StudentTable>(SqliteExecutor, new StudentTable("Backup2025"));
        await resource2.InitAsync();

        var select = _students.ToSelect()
            .Select(_name, _age);
        var insert = SqliteExecutor.From("Backup2025")
            .AddColums(_name, _age)
            .ToDapperInsert(select);
        var result = await insert.ExecuteAsync();
        Assert.Equal(0, result);
    }

    [Fact]
    public async Task InsertTo()
    {
        await using var resource = new TableResource<StudentTable>(SqliteExecutor, new StudentTable());
        await resource.InitAsync();
        await using var resource2 = new TableResource<StudentTable>(SqliteExecutor, new StudentTable("Backup2025"));
        await resource2.InitAsync();

        var backup = _db.From("Backup2025")
            .AddColums(_name, _age);
        var insert = _students.ToSelect()
            .Select(_name, _age)
            .InsertTo(backup);
        var result = await insert.ExecuteAsync(SqliteExecutor);
        Assert.Equal(0, result);
    }
}

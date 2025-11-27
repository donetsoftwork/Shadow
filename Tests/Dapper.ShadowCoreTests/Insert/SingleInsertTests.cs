using Dapper;
using Dapper.Shadow;
using ShadowSql;
using ShadowSql.Identifiers;
using ShadowSql.Insert;

namespace Dapper.ShadowCoreTests.Insert;

public class SingleInsertTests : ExecuteTestBase
{
    static readonly DB _db = DB.Use("MyDB");
    /// <summary>
    /// 按值添加
    /// </summary>
    [Fact]
    public async Task InsertValue()
    {
        var table = new StudentTable("Students");
        await using var resource = new TableResource<StudentTable>(SqliteExecutor, table);
        await resource.InitAsync();

        var table2 = _db.From("Students")
            .AddColums(_name, _age);
        var insert = new SingleInsert(table2)
            .Insert(_name.InsertValue("张三"))
            .Insert(_age.InsertValue(11));

        var result = await insert.ExecuteAsync(SqliteExecutor);
        Assert.Equal(1, result);        
    }

    [Fact]
    public async Task Table()
    {
        var table = new StudentTable();
        await using var resource = new TableResource<StudentTable>(SqliteExecutor, table);
        await resource.InitAsync();

        var insert = new  SingleInsert(table)
            .Insert(table.Name.Insert("StudentName"))
            .Insert(table.Age.InsertValue(11));

        var result = await insert.ExecuteAsync(SqliteExecutor, new { StudentName = "张三" });
        Assert.Equal(1, result);
    }
    [Fact]
    public async Task Execute()
    {
        var table = new StudentTable();
        await using var resource = new TableResource<StudentTable>(SqliteExecutor, table);
        await resource.InitAsync();

        var insert = new SingleInsert(table)
            .Insert(table.Name.Insert())
            .Insert(table.Age.InsertValue(11));

        var result = await insert.ExecuteAsync(SqliteExecutor, new { Name = "张三" });
        Assert.Equal(1, result);
    }
}

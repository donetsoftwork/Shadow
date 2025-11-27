using Dapper.Shadow;
using ShadowSql;

namespace Dapper.ShadowTests.Insert;

public class SingleInsertTests : ExecuteTestBase
{
    /// <summary>
    /// 按值添加
    /// </summary>
    [Fact]
    public async Task InsertValue()
    {
        await using var resource = new TableResource<StudentTable>(SqliteExecutor, new StudentTable());
        await resource.InitAsync();

        var insert = SqliteExecutor.From("Students")
            .AddColums(_name, _age)
            .ToDapperInsert()
            .Insert(_name.InsertValue("张三"))
            .Insert(s => s.Column("Age").InsertValue(11));

        var result = await insert.ExecuteAsync();
        Assert.Equal(1, result);        
    }

    [Fact]
    public async Task Table()
    {
        await using var resource = new TableResource<StudentTable>(SqliteExecutor, new StudentTable());
        await resource.InitAsync();

        var insert = new StudentTable()
            .ToInsert()
            .Insert(student => student.Name.Insert("StudentName"))
            .Insert(student => student.Age.InsertValue(11));

        var result = await insert.ExecuteAsync(SqliteExecutor, new { StudentName = "张三" });
        Assert.Equal(1, result);
    }
    [Fact]
    public async Task Execute()
    {
        await using var resource = new TableResource<StudentTable>(SqliteExecutor, new StudentTable());
        await resource.InitAsync();

        var insert = new StudentTable()
            .ToInsert()
            .Insert(student => student.Name.Insert())
            .Insert(student => student.Age.InsertValue(11));
        var result = await insert.ExecuteAsync(SqliteExecutor, new { Name = "张三" });
        Assert.Equal(1, result);
    }
}

using Dapper.Shadow;
using ShadowSql;

namespace Dapper.ShadowTests.Insert;

public class MultiInsertTests : ExecuteTestBase
{
    [Fact]
    public async Task Insert()
    {
        await using var resource = new TableResource<StudentTable>(SqliteExecutor, new StudentTable());
        await resource.InitAsync();

        var insert = SqliteExecutor.From("Students")
            .ToDapperMultiInsert()
            .Insert(_name.InsertValues("张三", "李四"))
            .Insert(_age.InsertValues(10, 11));
        var result = await insert.ExecuteAsync();
        Assert.Equal(2, result);
    }

    [Fact]
    public async Task Table()
    {
        await using var resource = new TableResource<StudentTable>(SqliteExecutor, new StudentTable());
        await resource.InitAsync();

        var insert = new StudentTable()
            .ToMultiInsert()
            .Insert(student => student.Name.InsertValues("张三", "李四", "王二"))
            //以第一个值数量为准,多余的忽略
            .Insert(student => student.Age.InsertValues(9, 11, 10, 12));

        var result = await insert.ExecuteAsync(SqliteExecutor);
        Assert.Equal(3, result);
    }

    [Fact]
    public async Task Exception()
    {
        await using var resource = new TableResource<StudentTable>(SqliteExecutor, new StudentTable());
        await resource.InitAsync();

        var insert = new StudentTable()
            .ToMultiInsert()
            .Insert(student => student.Name.InsertValues("张三", "李四", "王二"))
            //以第一个值数量为准,其他少于第一个的会触发异常
            .Insert(student => student.Age.InsertValues(9, 10));
        await Assert.ThrowsAsync<IndexOutOfRangeException>(() => insert.ExecuteAsync(SqliteExecutor));
    }
}

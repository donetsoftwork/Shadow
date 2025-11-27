using Dapper;
using Dapper.Shadow;
using ShadowSql;
using ShadowSql.Insert;

namespace Dapper.ShadowCoreTests.Insert;

public class MultiInsertTests : ExecuteTestBase
{
    [Fact]
    public async Task Insert()
    {
        var table = new StudentTable();
        await using var resource = new TableResource<StudentTable>(SqliteExecutor, table);
        await resource.InitAsync();

        var insert = new MultiInsert("Students")
            .Insert(_name.InsertValues("张三", "李四"))
            .Insert(_age.InsertValues(10, 11));
        var result = await insert.ExecuteAsync(SqliteExecutor);
        Assert.Equal(2, result);
    }

    [Fact]
    public async Task Table()
    {
        var table = new StudentTable();
        await using var resource = new TableResource<StudentTable>(SqliteExecutor, table);
        await resource.InitAsync();

        var insert = new MultiInsert(table)
            .Insert(table.Name.InsertValues("张三", "李四", "王二"))
            //以第一个值数量为准,多余的忽略
            .Insert(table.Age.InsertValues(9, 11, 10, 12));

        var result = await insert.ExecuteAsync(SqliteExecutor);
        Assert.Equal(3, result);
    }

    [Fact]
    public async Task Exception()
    {
        var table = new StudentTable();
        await using var resource = new TableResource<StudentTable>(SqliteExecutor, table);
        await resource.InitAsync();

        var insert = new  MultiInsert(table)
            .Insert(table.Name.InsertValues("张三", "李四", "王二"))
            //以第一个值数量为准,其他少于第一个的会触发异常
            .Insert(table.Age.InsertValues(9, 10));
        await Assert.ThrowsAsync<IndexOutOfRangeException>(() => insert.ExecuteAsync(SqliteExecutor));
    }
}

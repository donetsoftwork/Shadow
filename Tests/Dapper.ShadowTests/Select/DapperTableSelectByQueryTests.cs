using Dapper.Shadow;
using Microsoft.Data.Sqlite;
using ShadowSql;
using ShadowSql.Engines.Sqlite;
using ShadowSql.Identifiers;

namespace Dapper.ShadowTests.Select;

public class DapperTableSelectByQueryTests : ExecuteTestBase
{
    /// <summary>
    /// 逻辑运算查询
    /// </summary>
    [Fact]
    public async Task AtomicLogic()
    {
        await using var resource = new StudentTableResource(SqliteExecutor, new StudentTable());
        await resource.InitAsync();

        var ageFilter = Column.Use("Age").EqualValue(10);
        var query = SqliteExecutor.From("Students")
            .ToQuery()
            .And(ageFilter);
        int count = await query.CountAsync();
        Assert.True(count > 0);
        var students = await query.ToDapperSelect()
            .GetAsync<Student>();
        Assert.True(students.Any());
    }
    /// <summary>
    /// 自定义表查询
    /// </summary>
    [Fact]
    public async Task CustomizeTable()
    {
        var table = new StudentTable("Students");
        await using var resource = new StudentTableResource(SqliteExecutor, table);
        await resource.InitAsync();

        var query = table.ToQuery()
            .And(table.Age.EqualValue(10));
        int count = await query.CountAsync(SqliteExecutor);
        Assert.True(count > 0);
        var students = await query.ToSelect()
            .GetAsync<Student>(SqliteExecutor);
        Assert.True(students.Any());
    }

    [Fact]
    public async Task ToDapperSelect()
    {
        var table = new StudentTable("Students");
        await using var resource = new StudentTableResource(SqliteExecutor, table);
        await resource.InitAsync();

        var query = table.ToQuery()
            .And(table.Age.EqualValue(10));
        int count = await query.CountAsync(SqliteExecutor);
        Assert.True(count > 0);
        var students = await query.ToDapperSelect(SqliteExecutor)
            .GetAsync<Student>();
        Assert.True(students.Any());
    }

    [Fact]
    public async Task Field()
    {
        await using var resource = new StudentTableResource(SqliteExecutor, new StudentTable());
        await resource.InitAsync();

        var query = SqliteExecutor.From("Students")
            .ToQuery()
            .And(student => student.Field("Age").EqualValue(10));
        int count = await query.CountAsync();
        Assert.True(count > 0);
        var students = await query.ToDapperSelect()
            .GetAsync<Student>();
        Assert.True(students.Any());
    }

    [Fact]
    public async Task FieldContext()
    {
        await using var resource = new StudentTableResource(SqliteExecutor, new StudentTable());
        await resource.InitAsync();

        var connection = new SqliteConnection("Data Source=file::memory:;Cache=Shared");
        var excutor = new ParametricExecutor(new SqliteEngine(), connection);
        var query = excutor.From("Students")
            .ToQuery()
            .And(student => student.Field("Age").EqualValue(10));
        int count = await query.CountAsync();
        Assert.True(count > 0);
        var students = await query.ToDapperSelect()
            .GetAsync<Student>();
        Assert.True(students.Any());
    }
}

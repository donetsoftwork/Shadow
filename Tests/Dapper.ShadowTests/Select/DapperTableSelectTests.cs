using Dapper.Shadow;
using Microsoft.Data.Sqlite;
using ShadowSql;
using ShadowSql.Engines.Sqlite;
using ShadowSql.Identifiers;

namespace Dapper.ShadowTests.Select;

public class DapperTableSelectTests : ExecuteTestBase
{
    [Fact]
    public async Task Get()
    {
        await using var resource = new StudentTableResource(SqliteExecutor);
        await resource.InitAsync();

        var select = SqliteExecutor.From("Students")
            .ToDapperSelect();
        int count = await select.CountAsync();
        Assert.True(count > 0);
        var students = await select.GetAsync<Student>();
        Assert.True(students.Any());
    }
    /// <summary>
    /// 原生sql查询
    /// </summary>
    [Fact]
    public async Task Where()
    {
        await using var resource = new StudentTableResource(SqliteExecutor);
        await resource.InitAsync();

        var query = SqliteExecutor.From("Students")
            .ToSqlQuery()
            .Where("Age=10");
        int count = await query.CountAsync();
        Assert.True(count > 0);
        var students = await query.ToDapperSelect()
            .GetAsync<Student>();
        Assert.True(students.Any());
    }
    /// <summary>
    /// 逻辑运算查询
    /// </summary>
    [Fact]
    public async Task AtomicLogic()
    {
        await using var resource = new StudentTableResource(SqliteExecutor);
        await resource.InitAsync();

        var ageFilter = Column.Use("Age").EqualValue(10);
        var query = SqliteExecutor.From("Students")
            .ToSqlQuery()
            .Where(ageFilter);
        int count = await query.CountAsync();
        Assert.True(count > 0);
        var students = await query.ToDapperSelect()
            .GetAsync<Student>();
        Assert.True(students.Any());
    }
    /// <summary>
    /// Or查询
    /// </summary>
    [Fact]
    public async Task Or()
    {
        await using var resource = new StudentTableResource(SqliteExecutor);
        await resource.InitAsync();

        var query = SqliteExecutor.From("Students")
            .ToSqlQuery()
            .Where("Age=10")
            .ToOr()
            .Where("Name like '张%'");
        int count = await query.CountAsync();
        Assert.True(count > 0);
        var students = await query.ToDapperSelect()
            .GetAsync<Student>();
        Assert.True(students.Any());
    }

    /// <summary>
    /// 按列名
    /// </summary>
    [Fact]
    public async Task ColumnValue()
    {
        await using var resource = new StudentTableResource(SqliteExecutor);
        await resource.InitAsync();

        var query = SqliteExecutor.From("Students")
            .ToSqlQuery()
            .Where("Age=10");
        int count = await query.CountAsync();
        Assert.True(count > 0);
        var students = await query.ToDapperSelect()
            .GetAsync<Student>();
        Assert.True(students.Any());
    }
    /// <summary>
    /// 自定义列查询
    /// </summary>
    [Fact]
    public async Task CustomizeColumn()
    {
        await using var resource = new StudentTableResource(SqliteExecutor);
        await resource.InitAsync();

        var age = Column.Use("Age");
        var query = SqliteExecutor.From("Students")
            .ToSqlQuery()
            .Where(age.EqualValue(10));
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
        await using var resource = new StudentTableResource(SqliteExecutor);
        await resource.InitAsync();

        var table = new StudentTable("Students");
        var query = table.ToSqlQuery()
            .Where(table.Age.EqualValue(10));
        int count = await query.CountAsync(SqliteExecutor);
        Assert.True(count > 0);
        var students = await query.ToDapperSelect(SqliteExecutor)
            .GetAsync<Student>();
        Assert.True(students.Any());
    }
    /// <summary>
    /// 委托查询
    /// </summary>
    [Fact]
    public async Task Func()
    {
        await using var resource = new StudentTableResource(SqliteExecutor);
        await resource.InitAsync();

        var query = new StudentTable("Students")
            .ToSqlQuery()
            .Where(table => table.Age.EqualValue(10));
        int count = await query.CountAsync(SqliteExecutor);
        Assert.True(count > 0);
        var students = await query.ToDapperSelect(SqliteExecutor)
            .GetAsync<Student>();
        Assert.True(students.Any());
    }
    /// <summary>
    /// 表定义列查询
    /// </summary>
    [Fact]
    public async Task DefineColums()
    {
        await using var resource = new StudentTableResource(SqliteExecutor);
        await resource.InitAsync();

        var query = new Table("Students")
            .DefineColums("Age")
            .ToSqlQuery()
            .Where(student => student.Strict("Age").EqualValue(10));
        int count = await query.CountAsync(SqliteExecutor);
        Assert.True(count > 0);
        var students = await query.ToDapperSelect(SqliteExecutor)
            .GetAsync<Student>();
        Assert.True(students.Any());
    }

    /// <summary>
    /// 虚拟字段查询
    /// </summary>
    [Fact]
    public async Task Field()
    {
        await using var resource = new StudentTableResource(SqliteExecutor);
        await resource.InitAsync();

        var query = SqliteExecutor.From("Students")
            .ToSqlQuery()
            .Where(student => student.Field("Age").EqualValue(10));
        int count = await query.CountAsync();
        Assert.True(count > 0);
        var students = await query.ToDapperSelect()
            .GetAsync<Student>();
        Assert.True(students.Any());
    }
    [Fact]
    public async Task FieldContext()
    {
        await using var resource = new StudentTableResource(SqliteExecutor);
        await resource.InitAsync();

        var connection = new SqliteConnection("Data Source=file::memory:;Cache=Shared");
        var excutor = new ParametricExecutor(new SqliteEngine(), connection);
        var query = excutor.From("Students")
            .ToSqlQuery()
            .Where(table => table.Field("Age").EqualValue(10));
        int count = await query.CountAsync();
        Assert.True(count > 0);
        var students = await query.ToDapperSelect()
            .GetAsync<Student>();
        Assert.True(students.Any());
    }
    /// <summary>
    /// 自定义列过滤
    /// </summary>
    [Fact]
    public async Task FilterColumn()
    {
        await using var resource = new StudentTableResource(SqliteExecutor);
        await resource.InitAsync();

        var age = Column.Use("Age");
        var select = SqliteExecutor.From("Students")
            .ToDapperSelect(age.EqualValue(10));
        int count = await select.CountAsync();
        Assert.True(count > 0);
        var students = await select.GetAsync<Student>();
        Assert.True(students.Any());
    }
    /// <summary>
    /// 自定义表过滤
    /// </summary>
    [Fact]
    public async Task FilterTable()
    {
        await using var resource = new StudentTableResource(SqliteExecutor);
        await resource.InitAsync();

        var table = new StudentTable("Students");
        var select = table.ToSelect(table.Age.EqualValue(10));
        int count = await select.CountAsync(SqliteExecutor);
        Assert.True(count > 0);
        var students = await select.GetAsync<Student>(SqliteExecutor);
        Assert.True(students.Any());
    }
    /// <summary>
    /// Cursor(分页及排序)
    /// </summary>
    [Fact]
    public async Task Cursor()
    {
        await using var resource = new StudentTableResource(SqliteExecutor);
        await resource.InitAsync();

        var query = new StudentTable("Students")
            .ToSqlQuery()
            .Where(table => table.Age.GreaterEqualValue(9));
        int count = await query.CountAsync(SqliteExecutor);
        Assert.True(count > 0);
        var students = await query.ToCursor()
            .Desc(table => table.Id)
            .Skip(1)
            .Take(10)
            .ToSelect()
            .GetAsync<Student>(SqliteExecutor);
        Assert.True(students.Any());
    }
}

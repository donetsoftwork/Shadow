using Dapper.Shadow;
using Microsoft.Data.Sqlite;
using ShadowSql;
using Shadow.DDL;
using ShadowSql.Cursors;
using ShadowSql.Engines.Sqlite;
using ShadowSql.Identifiers;
using ShadowSql.Insert;
using ShadowSql.Select;
using ShadowSql.Tables;

namespace Dapper.ShadowCoreTests.Select;

public class DapperTableSelectTests : ExecuteTestBase
{
    [Fact]
    public async Task Get()
    {
        await using var resource = new StudentTableResource(SqliteExecutor);
        await resource.InitAsync();

        var table = new Table("Students");
        var count = await table.CountAsync(SqliteExecutor);
        Assert.True(count > 0);
        var select = new TableSelect(table);
        var students = await select.GetAsync<Student>(SqliteExecutor);
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

        var query = new TableSqlQuery("Students")
            .Where("Age=10");
        var count = await query.CountAsync(SqliteExecutor);
        Assert.True(count > 0);
        var select = new TableSelect(query);
        var students = await select.GetAsync<Student>(SqliteExecutor);
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
        var query = new TableSqlQuery("Students")
            .Where(ageFilter);
        var count = await query.CountAsync(SqliteExecutor);
        Assert.True(count > 0);
        var select = new TableSelect(query);
        var students = await select.GetAsync<Student>(SqliteExecutor);
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

        var query = new TableSqlQuery("Students")
           .Where("Age=10")
            .ToOr()
            .Where("Name like '张%'");
        var count = await query.CountAsync(SqliteExecutor);
        Assert.True(count > 0);
        var select = new TableSelect(query);
        var students = await select.GetAsync<Student>(SqliteExecutor);
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

        var query = new TableSqlQuery("Students")
            .Where("Age=10");
        var count = await query.CountAsync(SqliteExecutor);
        Assert.True(count > 0);
        var select = new TableSelect(query);
        var students = await select.GetAsync<Student>(SqliteExecutor);
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
        var query = new TableSqlQuery("Students")
            .Where(age.EqualValue(10));
        var count = await query.CountAsync(SqliteExecutor);
        Assert.True(count > 0);
        var select = new TableSelect(query);
        var students = await select.GetAsync<Student>(SqliteExecutor);
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
        var query = new TableSqlQuery(table)
            .Where(table.Age.EqualValue(10));
        var count = await query.CountAsync(SqliteExecutor);
        Assert.True(count > 0);
        var select = new TableSelect(query);
        var students = await select.GetAsync<Student>(SqliteExecutor);
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

        var table = new Table("Students")
            .DefineColums("Age");
        var query = new TableSqlQuery(table)
            .Where(student => student.Strict("Age").EqualValue(10));
        var count = await query.CountAsync(SqliteExecutor);
        Assert.True(count > 0);
        var select = new TableSelect(query);
        var students = await select.GetAsync<Student>(SqliteExecutor);
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

        var query = new TableSqlQuery("Students")
            .Where(student => student.Field("Age").EqualValue(10));
        var count = await query.CountAsync(SqliteExecutor);
        Assert.True(count > 0);
        var select = new TableSelect(query);
        var students = await select.GetAsync<Student>(SqliteExecutor);
        Assert.True(students.Any());
    }
    [Fact]
    public async Task FieldContext()
    {
        await using var resource = new StudentTableResource(SqliteExecutor);
        await resource.InitAsync();

        var connection = new SqliteConnection("Data Source=file::memory:;Cache=Shared");
        var excutor = new ParametricExecutor(new SqliteEngine(), connection);
        var query = new TableSqlQuery("Students")
            .Where(table => table.Field("Age").EqualValue(10));
        var count = await query.CountAsync(SqliteExecutor);
        Assert.True(count > 0);
        var select = new TableSelect(query);
        var students = await select.GetAsync<Student>(SqliteExecutor);
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

        var table = new Table("Students");
        var age = Column.Use("Age");
        var filter = new TableFilter(table, age.EqualValue(10));
        var select = new TableSelect(filter);
        var count = await select.CountAsync(SqliteExecutor);
        Assert.True(count > 0);
        var students = await select.GetAsync<Student>(SqliteExecutor);
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
        var filter = new TableFilter(table, table.Age.EqualValue(10));
        var select = new TableSelect(filter);
        var count = await select.CountAsync(SqliteExecutor);
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

        var table = new StudentTable("Students");
        var query = new TableSqlQuery(table)
            .Where(table.Age.GreaterEqualValue(9));
        var count = await query.CountAsync(SqliteExecutor);
        Assert.True(count > 0);
        var cursor = new TableCursor(query)
            .Desc(table.Id)
            .Skip(1)
            .Take(10);
        var select = new TableSelect(cursor);
        var students = await select.GetAsync<Student>(SqliteExecutor);
        Assert.True(students.Any());
    }

    class StudentTableResource(DapperExecutor executor, StudentTable table)
        : TableResource<StudentTable>(executor, table)
    {
        public StudentTableResource(DapperExecutor executor)
            : this(executor, new StudentTable("Students"))
        {
        }
        public override async ValueTask InitAsync()
        {
            try
            {
                await _table.ToCreate()
                    .ExecuteAsync(_executor);
                var insert = new SingleInsert(_table)
                    .InsertSelfColumns();
                await insert.ExecuteAsync(_executor, new Student(1, "张三", 10, 1));
                await insert.ExecuteAsync(_executor, new Student(2, "李四", 11, 1));
            }
            catch { }
        }
    }
}

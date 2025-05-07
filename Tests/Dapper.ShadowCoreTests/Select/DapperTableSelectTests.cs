using Dapper.Shadow;
using Microsoft.Data.Sqlite;
using ShadowSql;
using ShadowSql.Cursors;
using ShadowSql.Engines.Sqlite;
using ShadowSql.Identifiers;
using ShadowSql.Insert;
using ShadowSql.Select;
using ShadowSql.Simples;
using ShadowSql.Tables;

namespace Dapper.ShadowCoreTests.Select;

public class DapperTableSelectTests : ExecuteTestBase, IDisposable
{

    public DapperTableSelectTests()
    {
        var table = CreateStudentTable();
        var insert = new SingleInsert(table)
            .InsertSelfColumns();
        insert.Execute(SqliteExecutor, new Student(1, "张三", 10, 1));
        insert.Execute(SqliteExecutor, new Student(2, "李四", 11, 1));
    }

    [Fact]
    public void Get()
    {
        var table = SimpleDB.From("Students");        
        var count = table.Count(SqliteExecutor);
        Assert.True(count > 0);
        var select = new TableSelect(table);
        var students = select.Get<Student>(SqliteExecutor)
            .ToList();
        Assert.True(students.Count > 0);
    }
    /// <summary>
    /// 原生sql查询
    /// </summary>
    [Fact]
    public void Where()
    {
        var query = new TableSqlQuery("Students")
            .Where("Age=10");
        int count = query.Count(SqliteExecutor);
        Assert.True(count > 0);
        var select = new TableSelect(query);
        var students = select.Get<Student>(SqliteExecutor)
            .ToList();
        Assert.True(students.Count > 0);
    }
    /// <summary>
    /// 逻辑运算查询
    /// </summary>
    [Fact]
    public void AtomicLogic()
    {
        var ageFilter = Column.Use("Age").EqualValue(10);
        var query = new TableSqlQuery("Students")
            .Where(ageFilter);
        int count = query.Count(SqliteExecutor);
        Assert.True(count > 0);
        var select = new TableSelect(query);
        var students = select.Get<Student>(SqliteExecutor)
            .ToList();
        Assert.True(students.Count > 0);
    }
    /// <summary>
    /// Or查询
    /// </summary>
    [Fact]
    public void Or()
    {
        var query = new TableSqlQuery("Students")
           .Where("Age=10")
            .ToOr()
            .Where("Name like '张%'");
        int count = query.Count(SqliteExecutor);
        Assert.True(count > 0);
        var select = new TableSelect(query);
        var students = select.Get<Student>(SqliteExecutor)
            .ToList();
        Assert.True(students.Count > 0);
    }

    /// <summary>
    /// 按列名
    /// </summary>
    [Fact]
    public void ColumnValue()
    {
        var query = new TableSqlQuery("Students")
            .Where("Age=10");
        int count = query.Count(SqliteExecutor);
        Assert.True(count > 0);
        var select = new TableSelect(query);
        var students = select.Get<Student>(SqliteExecutor)
            .ToList();
        Assert.True(students.Count > 0);
    }
    /// <summary>
    /// 自定义列查询
    /// </summary>
    [Fact]
    public void CustomizeColumn()
    {
        var age = Column.Use("Age");
        var query = new TableSqlQuery("Students")
            .Where(age.EqualValue(10));
        int count = query.Count(SqliteExecutor);
        Assert.True(count > 0);
        var select = new TableSelect(query);
        var students = select.Get<Student>(SqliteExecutor)
            .ToList();
        Assert.True(students.Count > 0);
    }
    /// <summary>
    /// 自定义表查询
    /// </summary>
    [Fact]
    public void CustomizeTable()
    {
        var table = new StudentTable("Students");
        var query = new TableSqlQuery(table)
            .Where(table.Age.EqualValue(10));
        int count = query.Count(SqliteExecutor);
        Assert.True(count > 0);
        var select = new TableSelect(query);
        var students = select.Get<Student>(SqliteExecutor)
            .ToList();
        Assert.True(students.Count > 0);
    }
    /// <summary>
    /// 表定义列查询
    /// </summary>
    [Fact]
    public void DefineColums()
    {
        var table = new Table("Students")
            .DefineColums("Age");
        var query = new TableSqlQuery(table)
            .Where(student => student.Strict("Age").EqualValue(10));
        int count = query.Count(SqliteExecutor);
        Assert.True(count > 0);
        var select = new TableSelect(query);
        var students = select.Get<Student>(SqliteExecutor)
            .ToList();
        Assert.True(students.Count > 0);
    }

    /// <summary>
    /// 虚拟字段查询
    /// </summary>
    [Fact]
    public void Field()
    {
        var query = new TableSqlQuery("Students")
            .Where(student => student.Field("Age").EqualValue(10));
        int count = query.Count(SqliteExecutor);
        Assert.True(count > 0);
        var select = new TableSelect(query);
        var students = select.Get<Student>(SqliteExecutor)
            .ToList();
        Assert.True(students.Count > 0);
    }
    [Fact]
    public void FieldContext()
    {
        var connection = new SqliteConnection("Data Source=file::memory:;Cache=Shared");
        var excutor = new ParametricExecutor(new SqliteEngine(), connection);
        var query = new TableSqlQuery("Students")
            .Where(table => table.Field("Age").EqualValue(10));
        int count = query.Count(SqliteExecutor);
        Assert.True(count > 0);
        var select = new TableSelect(query);
        var students = select.Get<Student>(SqliteExecutor)
            .ToList();
        Assert.True(students.Count > 0);
    }
    /// <summary>
    /// 自定义列过滤
    /// </summary>
    [Fact]
    public void FilterColumn()
    {
        var table = SimpleDB.From("Students");
        var age = Column.Use("Age");
        var filter = new TableFilter(table, age.EqualValue(10));
        var select = new TableSelect(filter);
        var count = select.Count(SqliteExecutor);
        Assert.True(count > 0);
        var students = select.Get<Student>(SqliteExecutor)
            .ToList();
        Assert.True(students.Count > 0);
    }
    /// <summary>
    /// 自定义表过滤
    /// </summary>
    [Fact]
    public void FilterTable()
    {
        var table = new StudentTable("Students");
        var filter = new TableFilter(table, table.Age.EqualValue(10));
        var select = new TableSelect(filter);
        var count = select.Count(SqliteExecutor);
        Assert.True(count > 0);
        var students = select.Get<Student>(SqliteExecutor)
            .ToList();
        Assert.True(students.Count > 0);
    }
    /// <summary>
    /// Cursor(分页及排序)
    /// </summary>
    [Fact]
    public void Cursor()
    {
        var table = new StudentTable("Students");
        var query = new TableSqlQuery(table)
            .Where(table.Age.GreaterEqualValue(9));
        var count = query.Count(SqliteExecutor);
        Assert.True(count > 0);
        var cursor = new TableCursor(query)
            .Desc(table.Id)
            .Skip(1)
            .Take(10);
        var select = new TableSelect(cursor);
        var students = select.Get<Student>(SqliteExecutor)
            .ToList();
        Assert.True(students.Count > 0);
    }


    void IDisposable.Dispose()
    => DropStudentTable();
}

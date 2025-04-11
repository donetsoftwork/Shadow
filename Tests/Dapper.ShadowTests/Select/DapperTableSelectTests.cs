using Dapper.Shadow;
using Microsoft.Data.Sqlite;
using ShadowSql;
using ShadowSql.Engines.Sqlite;
using ShadowSql.Identifiers;

namespace Dapper.ShadowTests.Select;

public class DapperTableSelectTests : ExecuteTestBase, IDisposable
{

    public DapperTableSelectTests()
    {
        var studentInsert = CreateStudentTable()
            .ToInsert()
            .InsertSelfColumns();
        SqliteExecutor.Execute(studentInsert, new Student(1, "张三", 10, 1));
        SqliteExecutor.Execute(studentInsert, new Student(2, "李四", 11, 1));
    }

    [Fact]
    public void Get()
    {
        var select = SqliteExecutor.From("Students")
            .ToDapperSelect();
        var count = select.Count();
        Assert.True(count > 0);
        var students = select.Get<Student>()
            .ToList();
        Assert.True(students.Count > 0);
    }
    /// <summary>
    /// 原生sql查询
    /// </summary>
    [Fact]
    public void Where()
    {
        var query = SqliteExecutor.From("Students")
            .ToSqlQuery()
            .Where("Age=10");
        int count = query.Count();
        Assert.True(count > 0);
        var students = query.ToDapperSelect()
            .Get<Student>()
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
        var query = SqliteExecutor.From("Students")
            .ToSqlQuery()
            .Where(ageFilter);
        int count = query.Count();
        Assert.True(count > 0);
        var students = query.ToDapperSelect()
            .Get<Student>()
            .ToList();
        Assert.True(students.Count > 0);
    }
    /// <summary>
    /// Or查询
    /// </summary>
    [Fact]
    public void Or()
    {
        var query = SqliteExecutor.From("Students")
            .ToSqlQuery()
            .Where("Age=10")
            .ToOr()
            .Where("Name like '张%'");
        int count = query.Count();
        Assert.True(count > 0);
        var students = query.ToDapperSelect()
            .Get<Student>()
            .ToList();
        Assert.True(students.Count > 0);
    }

    /// <summary>
    /// 按列名
    /// </summary>
    [Fact]
    public void ColumnValue()
    {
        var query = SqliteExecutor.From("Students")
            .ToSqlQuery()
            .Where("Age=10");
        int count = query.Count();
        Assert.True(count > 0);
        var students = query.ToDapperSelect()
            .Get<Student>()
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
        var query = SqliteExecutor.From("Students")
            .ToSqlQuery()
            .Where(age.EqualValue(10));
        int count = query.Count();
        Assert.True(count > 0);
        var students = query.ToDapperSelect()
            .Get<Student>()
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
        var query = table.ToSqlQuery()
            .Where(table.Age.EqualValue(10));
        int count = query.Count(SqliteExecutor);
        Assert.True(count > 0);
        var students = query.ToSelect()
            .Get<Student>(SqliteExecutor)
            .ToList();
        Assert.True(students.Count > 0);
    }
    /// <summary>
    /// 委托查询
    /// </summary>
    [Fact]
    public void Func()
    {
        var query = new StudentTable("Students")
            .ToSqlQuery()
            .Where(table => table.Age.EqualValue(10));
        int count = query.Count(SqliteExecutor);
        Assert.True(count > 0);
        var students = query.ToSelect()
            .Get<Student>(SqliteExecutor)
            .ToList();
        Assert.True(students.Count > 0);
    }
    /// <summary>
    /// 表定义列查询
    /// </summary>
    [Fact]
    public void DefineColums()
    {
        var query = new Table("Students")
            .DefineColums("Age")
            .ToSqlQuery()
            .Where(student => student.Column("Age").EqualValue(10));
        int count = query.Count(SqliteExecutor);
        Assert.True(count > 0);
        var students = query.ToDapperSelect(SqliteExecutor)
            .Get<Student>()
            .ToList();
        Assert.True(students.Count > 0);
    }

    /// <summary>
    /// 虚拟字段查询
    /// </summary>
    [Fact]
    public void Field()
    {
        var query = SqliteExecutor.From("Students")
            .ToSqlQuery()
            .Where(student => student.Field("Age").EqualValue(10));
        int count = query.Count();
        Assert.True(count > 0);
        var students = query.ToDapperSelect()
            .Get<Student>()
            .ToList();
        Assert.True(students.Count > 0);
    }
    [Fact]
    public void FieldContext()
    {
        var connection = new SqliteConnection("Data Source=file::memory:;Cache=Shared");
        var excutor = new ParametricExecutor(new SqliteEngine(), connection);
        var query = excutor.From("Students")
            .ToSqlQuery()
            .Where(table => table.Field("Age").EqualValue(10));
        int count = query.Count();
        Assert.True(count > 0);
        var students = query.ToDapperSelect()
            .Get<Student>()
            .ToList();
        Assert.True(students.Count > 0);
    }
    /// <summary>
    /// 自定义列过滤
    /// </summary>
    [Fact]
    public void FilterColumn()
    {
        var age = Column.Use("Age");
        var select = SqliteExecutor.From("Students")
            .ToDapperSelect(age.EqualValue(10));
        var count = select.Count();
        Assert.True(count > 0);
        var students = select.Get<Student>()
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
        var select = table.ToSelect(table.Age.EqualValue(10));
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
        var query = new StudentTable("Students")
            .ToSqlQuery()
            .Where(table => table.Age.GreaterEqualValue(9));
        var count = query.Count(SqliteExecutor);
        Assert.True(count > 0);
        var students = query.ToCursor()
            .Desc(table => table.Id)
            .Skip(1)
            .Take(10)
            .ToSelect()
            .Get<Student>(SqliteExecutor)
            .ToList();
        Assert.True(students.Count > 0);
    }


    void IDisposable.Dispose()
    => DropStudentTable();
}

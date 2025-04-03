using Dapper.Shadow;
using ShadowSql;
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
        var students = SqliteExecutor.From("Students")
            .ToDapperSelect()
            .Get<Student>()
            .ToList();
        Assert.True(students.Count > 0);
    }
    /// <summary>
    /// 原生sql查询
    /// </summary>
    [Fact]
    public void Where()
    {
        var students = SqliteExecutor.From("Students")
            .ToSqlQuery()
            .Where("Age=10")
            .ToDapperSelect()
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
        var students = SqliteExecutor.From("Students")
            .ToSqlQuery()
            .Where(ageFilter)
            .ToDapperSelect()
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
        var students = SqliteExecutor.From("Students")
            .ToSqlQuery()
            .ColumnValue("Age", 10)
            .ToOr()
            .ColumnLikeValue("Name", "张%")
            .ToDapperSelect()
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
        var students = SqliteExecutor.From("Students")
            .ToSqlQuery()
            .ColumnValue("Age", 10)
            .ToDapperSelect()
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
        var students = SqliteExecutor.From("Students")
            .ToSqlQuery()
            .Where(age.EqualValue(10))
            .ToDapperSelect()
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
        var students = table.ToSqlQuery()
            .Where(table.Age.EqualValue(10))
            .ToSelect()
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
        var students = new StudentTable("Students")
            .ToSqlQuery()
            .Where(table => table.Age.EqualValue(10))
            .ToSelect()
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
        var students = new Table("Students")
            .DefineColums("Age")
            .ToSqlQuery()
            .Where(student => student.Column("Age").EqualValue(10))
            .ToDapperSelect(SqliteExecutor)
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
        var students = SqliteExecutor.From("Students")
            .ToSqlQuery()
            .Where(student => student.Field("Age").EqualValue(10))
            .ToDapperSelect()
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
        var students = SqliteExecutor.From("Students")
            .ToDapperSelect(age.EqualValue(10))
            .Get<Student>()
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
        var students = table.ToSelect(table.Age.EqualValue(10))
            .Get<Student>(SqliteExecutor)
            .ToList();
        Assert.True(students.Count > 0);
    }
    /// <summary>
    /// Fetch(分页及排序)
    /// </summary>
    [Fact]
    public void Fetch()
    {
        var table = new StudentTable("Students");
        var students = table.ToSqlQuery()
            .Where(table.Age.GreaterEqualValue(9))
            .ToCursor()
            .Desc(table.Id)
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

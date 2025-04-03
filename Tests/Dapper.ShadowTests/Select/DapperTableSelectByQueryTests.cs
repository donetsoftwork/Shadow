using Dapper.Shadow;
using Microsoft.Data.Sqlite;
using ShadowSql;
using ShadowSql.Engines.Sqlite;
using ShadowSql.Identifiers;

namespace Dapper.ShadowTests.Select;

public class DapperTableSelectByQueryTests : ExecuteTestBase, IDisposable
{

    public DapperTableSelectByQueryTests()
    {
        var studentInsert = CreateStudentTable()
            .ToInsert()
            .InsertSelfColumns();
        SqliteExecutor.Execute(studentInsert, new Student(1, "张三", 10, 1));
        SqliteExecutor.Execute(studentInsert, new Student(2, "李四", 11, 1));
    }

    /// <summary>
    /// 逻辑运算查询
    /// </summary>
    [Fact]
    public void AtomicLogic()
    {
        var ageFilter = Column.Use("Age").EqualValue(10);
        var students = SqliteExecutor.From("Students")
            .ToQuery()
            .And(ageFilter)
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
        var students = table.ToQuery()
            .And(table.Age.EqualValue(10))
            .ToSelect()
            .Get<Student>(SqliteExecutor)
            .ToList();
        Assert.True(students.Count > 0);
    }

    [Fact]
    public void ToDapperSelect()
    {
        var table = new StudentTable("Students");
        var students = table.ToQuery()
            .And(table.Age.EqualValue(10))
            .ToDapperSelect(SqliteExecutor)
            .Get<Student>()
            .ToList();
        Assert.True(students.Count > 0);
    }

    [Fact]
    public void Field()
    {
        var students = SqliteExecutor.From("Students")
            .ToQuery()
            .And(table => table.Field("Age").EqualValue(10))
            .ToDapperSelect()
            .Get<Student>()
            .ToList();
        Assert.True(students.Count > 0);
    }

    [Fact]
    public void FieldContext()
    {
        var connection = new SqliteConnection("Data Source=file::memory:;Cache=Shared");
        var excutor = new ParametricExecutor(new SqliteEngine(), connection);
        var students = excutor.From("Students")
            .ToQuery()
            .And(table => table.Field("Age").EqualValue(10))
            .ToDapperSelect()
            .Get<Student>()
            .ToList();
        Assert.True(students.Count > 0);
    }



    void IDisposable.Dispose()
        => DropStudentTable();
}

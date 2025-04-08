using Dapper.Shadow;
using ShadowSql;

namespace Dapper.ShadowTests.Update;

public class MultiTableUpdateTests : ExecuteTestBase, IDisposable
{
    public MultiTableUpdateTests()
    {
        var classInsert = CreateSchoolClassTable()
            .ToInsert()
            .InsertSelfColumns();
        SqliteExecutor.Execute(classInsert, new SchoolClass(1, "五(1)班", "王老师"));
        SqliteExecutor.Execute(classInsert, new SchoolClass(2, "五(2)班", "李老师"));

        var studentInsert = CreateStudentTable()
            .ToInsert()
            .InsertSelfColumns();
        SqliteExecutor.Execute(studentInsert, new Student(1, "张三", 10, 2));
        SqliteExecutor.Execute(studentInsert, new Student(2, "李四", 11, 1));
    }

    /// <summary>
    /// Sqlite不支持联表修改
    /// </summary>
    [Fact]
    public void Join()
    {
        //var s = new StudentAliasTable("s");
        //var c = new SchoolClassAliaTable("c");
        //var query = s.Join(c)
        //    .And(s.ClassId.Equal(c.Id))
        //    .Root
        //    .And(c.Teacher.EqualValue("李老师"));

        //var result = query.ToUpdate()
        //    .Update(s)
        //    .Set(s.Age.EqualToValue(11))
        //    .Execute(SqliteExecutor);
        //Assert.True(result > 0);

        //var select = query.ToSelect();
        //select.Fields.SelectTable(s);

        //var students = select.Get<Student>(SqliteExecutor)
        //    .ToList();
        //Assert.True(students.Count > 0);
    }


    void IDisposable.Dispose()
    {
        DropStudentTable();
        DropSchoolClassTable();
    }
}

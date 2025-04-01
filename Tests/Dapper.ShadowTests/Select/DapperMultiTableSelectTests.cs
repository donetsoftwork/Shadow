using Dapper.Shadow;
using ShadowSql;
using ShadowSql.Identifiers;

namespace Dapper.ShadowTests.Select;

public class DapperMultiTableSelectTests : ExecuteTestBase, IDisposable
{

    public DapperMultiTableSelectTests()
    {
        var classInsert = CreateSchoolClassTable()
            .ToInsert()
            .InsertSelfColumns();
        SqliteExecutor.Execute(classInsert, new SchoolClass(1, "五(1)班", "王老师"));

        var studentInsert = CreateStudentTable()
            .ToInsert()
            .InsertSelfColumns();
        SqliteExecutor.Execute(studentInsert, new Student(1, "张三", 10, 1));
        SqliteExecutor.Execute(studentInsert, new Student(2, "李四", 11, 1));
    }
    
    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public void Join()
    {
        var s = new StudentAliasTable("s");
        var c = new SchoolClassAliaTable("c");
        var select = s.Join(c)
            .On(s.ClassId.Equal(c.Id))
            .Root.ToSelect();
        select.Fields.Select(s.Id, s.Name, s.Age, c.Name.As("ClassName"), c.Teacher);

        var students = select.Get<StudentView>(SqliteExecutor)
            .ToList();
        Assert.True(students.Count > 0);
    }


    void IDisposable.Dispose()
    => DropStudentTable();
}


using Dapper.Shadow;
using ShadowSql;

namespace Dapper.ShadowTests.Update;

public class MultiTableUpdateTests : ExecuteTestBase
{
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
        //    .Set(s.Age.AssignValue(11))
        //    .Execute(SqliteExecutor);
        //Assert.True(result > 0);

        //var select = query.ToSelect();
        //select.SelectTable(s);

        //var students = select.Get<Student>(SqliteExecutor)
        //    .ToList();
        //Assert.True(students.Count > 0);
    }
}

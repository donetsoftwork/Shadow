using Dapper.Shadow;
using Shadow.DDL;
using ShadowSql;

namespace Dapper.ShadowTests.Select;

public class DapperMultiTableSelectTests : ExecuteTestBase
{    
    [Fact]
    public async Task Join()
    {
        await using var resource = new JoinResource(SqliteExecutor);
        await resource.InitAsync();

        var s = new StudentAliasTable("s");
        var c = new SchoolClassAliaTable("c");
        var query = s.Join(c)
            .And(s.ClassId.Equal(c.Id))
            .Root;
        int count = await query.CountAsync(SqliteExecutor);
        Assert.True(count > 0);
        var select = query.ToSelect()
            .Select(s.Id, s.Name, s.Age, c.Name.As("ClassName"), c.Teacher);

        var students = await select.GetAsync<StudentView>(SqliteExecutor);
        Assert.True(students.Any());
    }

    class JoinResource(DapperExecutor executor, StudentTable table)
        : TableResource<StudentTable>(executor, table)
    {
        private readonly SchoolClassTable _classTable = new();
        public JoinResource(DapperExecutor executor)
            : this(executor, new StudentTable("Students"))
        {
        }
        public override async ValueTask InitAsync()
        {
            try
            {
                await _classTable.ToCreate()
                    .ExecuteAsync(_executor);
                var classInsert = _classTable
                   .ToInsert()
                   .InsertSelfColumns();
                await _executor.ExecuteAsync(classInsert, new SchoolClass(1, "五(1)班", "王老师"));

                await _table.ToCreate()
                    .ExecuteAsync(_executor);   

                var studentInsert = _table.ToInsert()
                    .InsertSelfColumns();
                await _executor.ExecuteAsync(studentInsert, new Student(1, "张三", 10, 1));
                await _executor.ExecuteAsync(studentInsert, new Student(2, "李四", 11, 1));
            }
            catch { }
        }
    }
}


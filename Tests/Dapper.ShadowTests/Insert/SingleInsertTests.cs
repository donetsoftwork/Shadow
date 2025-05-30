using Dapper.Shadow;
using ShadowSql;
using ShadowSql.Identifiers;

namespace Dapper.ShadowTests.Insert;

public class SingleInsertTests : ExecuteTestBase, IDisposable
{
    public SingleInsertTests()
        => CreateStudentTable();

    /// <summary>
    /// 按值添加
    /// </summary>
    [Fact]
    public void InsertValue()
    {        
        var insert = SqliteExecutor.From("Students")
            .AddColums(_name, _age)
            .ToDapperInsert()
            .Insert(_name.InsertValue("张三"))
            .Insert(s => s.Column("Age").InsertValue(11));

        var result = insert.Execute();
        Assert.Equal(1, result);        
    }

    [Fact]
    public void Table()
    {
        var insert = new StudentTable()
            .ToInsert()
            .Insert(student => student.Name.Insert("StudentName"))
            .Insert(student => student.Age.InsertValue(11));

        var result = insert.Execute(SqliteExecutor, new { StudentName = "张三" });
        Assert.Equal(1, result);
    }
    [Fact]
    public void Execute()
    {
        var insert = new StudentTable()
            .ToInsert()
            .Insert(student => student.Name.Insert())
            .Insert(student => student.Age.InsertValue(11));
        var result = insert.Execute(SqliteExecutor, new { Name = "张三" });
        Assert.Equal(1, result);
    }

    void IDisposable.Dispose()
        => DropStudentTable();
}

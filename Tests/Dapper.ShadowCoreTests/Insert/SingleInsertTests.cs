using Dapper;
using Dapper.Shadow;
using ShadowSql;
using ShadowSql.Identifiers;
using ShadowSql.Insert;

namespace Dapper.ShadowCoreTests.Insert;

public class SingleInsertTests : ExecuteTestBase, IDisposable
{
    public SingleInsertTests()
        => CreateStudentTable();
    static readonly DB _db = DB.Use("MyDB");
    /// <summary>
    /// 按值添加
    /// </summary>
    [Fact]
    public void InsertValue()
    {
        var table = _db.From("Students")
            .AddColums(_name, _age);
        var insert = new SingleInsert(table)
            .Insert(_name.InsertValue("张三"))
            .Insert(_age.InsertValue(11));

        var result = insert.Execute(SqliteExecutor);
        Assert.Equal(1, result);        
    }

    [Fact]
    public void Table()
    {
        var insert = new StudentTable()
            .ToInsert()
            .Insert(student => student.Name.Insert("StudentName"))
            .Insert(student => student.Age.InsertValue(11))
            .Use(SqliteExecutor);

        var result = insert.Execute(new { StudentName = "张三" });
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

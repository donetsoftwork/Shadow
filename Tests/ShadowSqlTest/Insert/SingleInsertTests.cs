using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using TestSupports;

namespace ShadowSqlTest.Insert;

public class SingleInsertTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly IColumn _name = Column.Use("Name");
    //分数
    static readonly IColumn _score = Column.Use("Score");

    [Fact]
    public void Insert()
    {
        var insert = new Table("Students")
            .ToInsert()
            .Insert(_name.Insert())
            .Insert(_score.InsertValue(90));
        var sql = _engine.Sql(insert);
        Assert.Equal("INSERT INTO [Students]([Name],[Score])VALUES(@Name,90)", sql);
    }

    [Fact]
    public void Insert2()
    {
        var table = new StudentTable();
        var insert = table.ToInsert()
            .Insert(table.Name.Insert("StudentName"))
            .Insert(table.Score.InsertValue(90));
        var sql = _engine.Sql(insert);
        Assert.Equal("INSERT INTO [Students]([Name],[Score])VALUES(@StudentName,90)", sql);
    }

    [Fact]
    public void Table()
    {
        var insert = new StudentTable()
            .ToInsert()
            .Insert(student => student.Name.Insert("StudentName"))
            .Insert(student => student.Score.InsertValue(90));
        var sql = _engine.Sql(insert);
        Assert.Equal("INSERT INTO [Students]([Name],[Score])VALUES(@StudentName,90)", sql);
    }
}

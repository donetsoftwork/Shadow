using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Insert;
using TestSupports;

namespace ShadowSqlCoreTest.Insert;

public class SingleInsertTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly IColumn _name = Column.Use("Name");
    //分数
    static readonly IColumn _score = Column.Use("Score");

    [Fact]
    public void Insert()
    {
        var insert = new SingleInsert("Students")
            .Insert(_name.Insert())
            .Insert(_score.InsertValue(90));
        var sql = _engine.Sql(insert);
        Assert.Equal("INSERT INTO [Students]([Name],[Score])VALUES(@Name,90)", sql);
    }

    [Fact]
    public void Table()
    {
        var table = new StudentTable();
        var insert = new SingleInsert(table)
            .Insert(table.Name.Insert("StudentName"))
            .Insert(table.Score.InsertValue(90));
        var sql = _engine.Sql(insert);
        Assert.Equal("INSERT INTO [Students]([Name],[Score])VALUES(@StudentName,90)", sql);
    }
    [Fact]
    public void InsertColumn()
    {
        var table = new StudentTable();
        var insert = new SingleInsert(table)
            .InsertColumn(table.Name)
            .InsertColumn(table.Score);
        var sql = _engine.Sql(insert);
        Assert.Equal("INSERT INTO [Students]([Name],[Score])VALUES(@Name,@Score)", sql);
    }
    [Fact]
    public void InsertColumns()
    {
        var name = Column.Use("Name");
        var score = Column.Use("Score");
        var insert = new SingleInsert("Students")
            .InsertColumns(name, score);
        var sql = _engine.Sql(insert);
        Assert.Equal("INSERT INTO [Students]([Name],[Score])VALUES(@Name,@Score)", sql);
    }
    [Fact]
    public void InsertColumns2()
    {
        var table = new StudentTable();
        var insert = new SingleInsert(table)
            .InsertColumns(table.Name, table.Score);
        var sql = _engine.Sql(insert);
        Assert.Equal("INSERT INTO [Students]([Name],[Score])VALUES(@Name,@Score)", sql);
    }
    [Fact]
    public void InsertSelfColumns()
    {
        var table = new StudentTable();
        var insert = new SingleInsert(table)
            .InsertSelfColumns();
        var sql = _engine.Sql(insert);
        Assert.Equal("INSERT INTO [Students]([Name],[Score])VALUES(@Name,@Score)", sql);
    }
}

using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using TestSupports;

namespace ShadowSqlTest.Insert;

public class MultiInsertTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly IColumn _name = Column.Use("Name");
    //分数
    static readonly IColumn _score = Column.Use("Score");

    [Fact]
    public void Insert()
    {
        var insert = new Table("Students")
            .ToMultiInsert()
            .Insert(_name.InsertValues("张三", "李四"))
            .Insert(_score.InsertValues(90, 85));
        var sql = _engine.Sql(insert);
        Assert.Equal("INSERT INTO [Students]([Name],[Score])VALUES('张三',90),('李四',85)", sql);
    }

    [Fact]
    public void Table()
    {
        var insert = new StudentTable()
            .ToMultiInsert()
            .Insert(student => student.Name.InsertValues("张三", "李四", "王二"))
            //以第一个值数量为准,多余的忽略
            .Insert(student => student.Score.InsertValues(90, 85, 87, 100));
        var sql = _engine.Sql(insert);
        Assert.Equal("INSERT INTO [Students]([Name],[Score])VALUES('张三',90),('李四',85),('王二',87)", sql);
    }

    [Fact]
    public void Exception()
    {
        var insert = new StudentTable()
            .ToMultiInsert()
            .Insert(student => student.Name.InsertValues("张三", "李四", "王二"))
            //以第一个值数量为准,其他少于第一个的会触发异常
            .Insert(student => student.Score.InsertValues(90, 85));
        Assert.Throws<IndexOutOfRangeException>(() => _engine.Sql(insert));
    }
}

using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Insert;
using TestSupports;

namespace ShadowSqlCoreTest.Insert;

public class MultiInsertTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();

    [Fact]
    public void Insert()
    {
        var name = Column.Use("Name");
        var score = Column.Use("Score");
        var insert = new MultiInsert("Students")
            .Insert(name.InsertValues("张三", "李四"))
            .Insert(score.InsertValues(90, 85));
        var sql = _engine.Sql(insert);
        Assert.Equal("INSERT INTO [Students]([Name],[Score])VALUES('张三',90),('李四',85)", sql);
    }

    [Fact]
    public void Table()
    {
        var table = new StudentTable();
        var insert = new MultiInsert(table)
             .Insert(table.Name.InsertValues("张三", "李四", "王二"))
            //以第一个值数量为准,多余的忽略
            .Insert(table.Score.InsertValues(90, 85, 87, 100));
        var sql = _engine.Sql(insert);
        Assert.Equal("INSERT INTO [Students]([Name],[Score])VALUES('张三',90),('李四',85),('王二',87)", sql);
    }

    [Fact]
    public void Exception()
    {
        var table = new StudentTable();
        var insert = new MultiInsert(table)
            .Insert(table.Name.InsertValues("张三", "李四", "王二"))
            //以第一个值数量为准,其他少于第一个的会触发异常
            .Insert(table.Score.InsertValues(90, 85));
        Assert.Throws<IndexOutOfRangeException>(() => _engine.Sql(insert));
    }
}

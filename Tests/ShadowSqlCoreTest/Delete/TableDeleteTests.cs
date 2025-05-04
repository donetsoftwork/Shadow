using ShadowSql;
using ShadowSql.Delete;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Queries;
using ShadowSql.Tables;
using TestSupports;

namespace ShadowSqlCoreTest.Delete;

public class TableDeleteTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    //分数
    static readonly IColumn _score = Column.Use("Score");

    [Fact]
    public void Filter()
    {
        var delete = new TableDelete("Students", _score.LessValue(60));
        var sql = _engine.Sql(delete);
        Assert.Equal("DELETE FROM [Students] WHERE [Score]<60", sql);
    }
    [Fact]
    public void FilterSqlQuery()
    {
        var query = SqlQuery.CreateAndQuery()
            .And("Score<60");
        var delete = new TableDelete("Students", query);
        var sql = _engine.Sql(delete);
        Assert.Equal("DELETE FROM [Students] WHERE Score<60", sql);
    }
    [Fact]
    public void Query()
    {
        var table = new StudentTable();

        var delete = new TableDelete(table, table.Score.LessValue(60));
        var sql = _engine.Sql(delete);
        Assert.Equal("DELETE FROM [Students] WHERE [Score]<60", sql);
    }
    [Fact]
    public void QuerySql()
    {
        var query = new TableSqlQuery("Students")
            .Where(table => table.Field("Score").LessValue(60));
        var delete = new TableDelete("Students", query.Filter);
        var sql = _engine.Sql(delete);
        Assert.Equal("DELETE FROM [Students] WHERE [Score]<60", sql);
    }
    [Fact]
    public void ToDelete()
    {
        var table = new StudentTable();
        var delete = table.ToDelete(table.Score.LessValue(60));
        var sql = _engine.Sql(delete);
        Assert.Equal("DELETE FROM [Students] WHERE [Score]<60", sql);
    }
    [Fact]
    public void ToDelete2()
    {
        var delete = new StudentTable()
            .ToDelete(table => table.Score.LessValue(60));
        var sql = _engine.Sql(delete);
        Assert.Equal("DELETE FROM [Students] WHERE [Score]<60", sql);
    }

}

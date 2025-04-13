using ShadowSql;
using ShadowSql.Delete;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Queries;
using ShadowSql.Tables;

namespace ShadowSqlCoreTest.Delete;

public class TableDeleteTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly IDB _db = DB.Use("MyDB");
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
        var table = _db.From("Students");
        var query = new TableSqlQuery(table)
            .Where(table.Field("Score").LessValue(60));
        var delete = new TableDelete(table, query.Filter);
        var sql = _engine.Sql(delete);
        Assert.Equal("DELETE FROM [Students] WHERE [Score]<60", sql);
    }
}

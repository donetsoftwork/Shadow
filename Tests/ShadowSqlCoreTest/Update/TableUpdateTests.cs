using ShadowSql;
using ShadowSql.Assigns;
using ShadowSql.Delete;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Queries;
using ShadowSql.Tables;
using ShadowSql.Update;

namespace ShadowSqlCoreTest.Update;

public class TableUpdateTests 
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly IDB _db = DB.Use("MyDB");
    //分数
    static readonly IColumn _score = Column.Use("Score");

    [Fact]
    public void Filter()
    {
        var table = _db.From("Students");
        var update = new TableUpdate(table, _score.LessValue(60))
            .Set(_score.EqualToValue(60));
        var sql = _engine.Sql(update);
        Assert.Equal("UPDATE [Students] SET [Score]=60 WHERE [Score]<60", sql);
    }
    [Fact]
    public void FilterSqlQuery()
    {
        var query = SqlQuery.CreateAndQuery()
            .And("Score<60");
        var update = new TableUpdate("Students", query)
            .SetValue("Score", 60);
        var sql = _engine.Sql(update);
        Assert.Equal("UPDATE [Students] SET [Score]=60 WHERE Score<60", sql);
    }
    [Fact]
    public void Query()
    {
        var table = new UserTable();
        var query = new TableSqlQuery(table)
            .Where(table.Id.Equal());
        var update = new TableUpdate(table, query.Filter)
            .Set(table.Status.EqualToValue(false));
        var sql = _engine.Sql(update);
        Assert.Equal("UPDATE [Users] SET [Status]=0 WHERE [Id]=@Id", sql);
    }
}

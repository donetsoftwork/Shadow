using ShadowSql;
using ShadowSql.Assigns;
using ShadowSql.Delete;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Queries;
using ShadowSql.Tables;
using ShadowSql.Update;
using TestSupports;

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
    public void FilterTable()
    {
        var table = new UserTable();
        var update = new TableUpdate(table, table.Id.Equal())
            .Set(table.Status.EqualToValue(false));
        var sql = _engine.Sql(update);
        Assert.Equal("UPDATE [Users] SET [Status]=0 WHERE [Id]=@Id", sql);
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
    [Fact]
    public void Create()
    {
        var id = Column.Use("Id");
        var status = Column.Use("Status");
        var update = TableUpdate.Create("Users", id.EqualValue(1))
             .Set(status.EqualToValue(false));
        var sql = _engine.Sql(update);
        Assert.Equal("UPDATE [Users] SET [Status]=0 WHERE [Id]=1", sql);
    }
    [Fact]
    public void SetParameter()
    {
        var id = Column.Use("Id");
        var update = TableUpdate.Create("Users", id.EqualValue(1))
            .SetParameter("Status", "=", "DenyStatus");
        var sql = _engine.Sql(update);
        Assert.Equal("UPDATE [Users] SET [Status]=@DenyStatus WHERE [Id]=1", sql);
    }
    [Fact]
    public void EqualTo()
    {
        var id = Column.Use("Id");
        var update = TableUpdate.Create("Users", id.EqualValue(1))
            .SetEqualTo("Status", "DenyStatus");
        var sql = _engine.Sql(update);
        Assert.Equal("UPDATE [Users] SET [Status]=@DenyStatus WHERE [Id]=1", sql);
    }
    [Fact]
    public void SetValue()
    {
        var id = Column.Use("Id");
        var update = TableUpdate.Create("Students", id.EqualValue(1))
            .SetValue("Score", 8, "+=");
        var sql = _engine.Sql(update);
        Assert.Equal("UPDATE [Students] SET [Score]+=8 WHERE [Id]=1", sql);
    }
    [Fact]
    public void EqualToValue()
    {
        var id = Column.Use("Id");
        var update = TableUpdate.Create("Students", id.EqualValue(1))
            .SetEqualToValue("Score", 60);
        var sql = _engine.Sql(update);
        Assert.Equal("UPDATE [Students] SET [Score]=60 WHERE [Id]=1", sql);
    }
    [Fact]
    public void SetRaw()
    {
        var id = Column.Use("Id");
        var update = TableUpdate.Create("Students", id.EqualValue(1))
            .SetRaw("Score=60");
        var sql = _engine.Sql(update);
        Assert.Equal("UPDATE [Students] SET Score=60 WHERE [Id]=1", sql);
    }

}

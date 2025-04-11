using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Simples;
using ShadowSql.Tables;

namespace ShadowSqlCoreTest.Tables;

public class TableSqlQueryTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly IDB _db = SimpleDB.Use("MyDb");
    static readonly IColumn _id = ShadowSql.Identifiers.Column.Use("Id");

    [Fact]
    public void AndQuery()
    {
        var query = new TableSqlQuery("Users")
            .Where("Id=@Id", "Status=@Status");
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE Id=@Id AND Status=@Status", sql);
    }
    [Fact]
    public void OrQuery()
    {
        var query = new TableSqlQuery("Users")
            .ToOr()
            .Where("Id=@Id", "Status=@Status");
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE Id=@Id OR Status=@Status", sql);
    }
    [Fact]
    public void Where()
    {
        var query = new TableSqlQuery("Users")
            .Where("Id=@Id", "Status=@Status");
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE Id=@Id AND Status=@Status", sql);
    }
    [Fact]
    public void WhereAnd()
    {
        var query = new TableSqlQuery("Users")
            .Where(q => q
                .And("Id=@Id")
                .And("Status=@Status")
            );
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE Id=@Id AND Status=@Status", sql);
    }
    [Fact]
    public void WhereOr()
    {
        var query = new TableSqlQuery("Users")
            .Where(q => q
                .Or("Id=@Id")
                .Or("Status=@Status")
            );
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE Id=@Id OR Status=@Status", sql);
    }
    [Fact]
    public void Column()
    {
        var query = new TableSqlQuery("Users")
            .ColumnParameter("Id", "<", "LastId")
            .ColumnParameter("Status", "=", "state");
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]<@LastId AND [Status]=@state", sql);
    }

    [Fact]
    public void ColumnValue()
    {
        var query = new TableSqlQuery("Users")
            .ToOr()
            .ColumnValue("Id", 100, "<")
            .ColumnValue("Status", true);
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]<100 OR [Status]=1", sql);
    }

    [Fact]
    public void FieldCompare()
    {
        var query = new TableSqlQuery("Users")
            .Where(q => q.Field("Id").Less("LastId"));
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]<@LastId", sql);
    }
    [Fact]
    public void Logic()
    {
        var table = new UserTable();
        var query = new TableSqlQuery(table)
             .Where(table.Id.Less("LastId"))
             .Where(table.Status.EqualValue(true));
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]<@LastId AND [Status]=1", sql);
    }
}

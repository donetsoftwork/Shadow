using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.Identifiers;
using ShadowSql.Tables;
using TestSupports;

namespace ShadowSqlCoreTest.Tables;

public class TableSqlQueryTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly IColumn _id = ShadowSql.Identifiers.Column.Use("Id");
    static readonly IColumn _status = ShadowSql.Identifiers.Column.Use("Status");
    

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
            .Apply(q => q
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
            .Apply(q => q
                .Or("Id=@Id")
                .Or("Status=@Status")
            );
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE Id=@Id OR Status=@Status", sql);
    }
    [Fact]
    public void FieldCompare()
    {
        var query = new TableSqlQuery("Users")
            .Where(u => u.Field("Id").Less("LastId"));
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
    [Fact]
    public void Apply()
    {
        var query = new TableSqlQuery("Users")
            .Apply((q, u) => q
                .And(u.Field("Id").Less("LastId"))
                .And(u.Field("Status").EqualValue(true))
            );
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]<@LastId AND [Status]=1", sql);
    }
    [Fact]
    public void Apply2()
    {
        var query = new TableSqlQuery("Users")
            .Apply(q => q
                .And(_id.Less("LastId"))
                .And(_status.EqualValue(true))
            );
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]<@LastId AND [Status]=1", sql);
    }
}

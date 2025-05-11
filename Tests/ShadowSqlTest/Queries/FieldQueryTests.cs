using ShadowSql;
using ShadowSql.Engines;
using ShadowSql.Engines.MsSql;
using ShadowSql.FieldQueries;
using ShadowSql.Identifiers;
using ShadowSql.Join;
using ShadowSql.Tables;

namespace ShadowSqlTest.Queries;

public class FieldQueryTests
{
    static readonly ISqlEngine _engine = new MsSqlEngine();
    static readonly IDB _db = new DB("MyDb");

    [Fact]
    public void FieldParameter()
    {
        var query = new TableSqlQuery("Users")
            .FieldParameter("Id", "<", "LastId")
            .FieldEqual("Status", "state");
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]<@LastId AND [Status]=@state", sql);
    }

    [Fact]
    public void FieldValue()
    {
        var query = new TableSqlQuery("Users")
            .ToOr()
            .FieldValue("Id", 100, "<")
            .FieldEqualValue("Status", true);
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]<100 OR [Status]=1", sql);
    }
    [Fact]
    public void TableFieldParameter()
    {
        var query = new MultiTableSqlQuery()
            .AddMembers("Comments", "Posts")
            .Where("t1.PostId=t2.Id")            
            .TableFieldParameter("Comments", "Pick", "=", "PickState")
            .TableFieldParameter("Posts", "Author");
        var sql = _engine.Sql(query);
        Assert.Equal("[Comments] AS t1,[Posts] AS t2 WHERE t1.[Pick]=@PickState AND t2.[Author]=@Author AND t1.PostId=t2.Id", sql);
    }
    [Fact]
    public void TableFieldValue()
    {
        var query = _db.From("Comments")
            .SqlJoin(_db.From("Posts"))
            .OnColumn("PostId", "Id")
            .Root
            .TableFieldValue("Comments", "Pick", false)
            .TableFieldValue("Posts", "Author", "张三");
        var sql = _engine.Sql(query);
        Assert.Equal("[Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] WHERE t1.[Pick]=0 AND t2.[Author]='张三'", sql);
    }

    [Fact]
    public void Field()
    {
        var u = SimpleTable.Use("Users");
        var query = u.ToSqlQuery()
            .Where(u.Field("Id").GreaterEqual("LastId"))
            .Where(u => u.Field("Status").EqualValue(true));
        var sql = _engine.Sql(query);
        Assert.Equal("[Users] WHERE [Id]>=@LastId AND [Status]=1", sql);
    }
    [Fact]
    public void TableField()
    {
        var query = SimpleTable.Use("Comments")
            .SqlJoin(SimpleTable.Use("Posts"))
            .OnColumn("PostId", "Id")
            .Root
            .Where(join => join.From("Comments").Field("Pick").Equal())
            .Where("t2", p => p.Field("Author").EqualValue("张三"));
        var sql = _engine.Sql(query);
        Assert.Equal("[Comments] AS t1 INNER JOIN [Posts] AS t2 ON t1.[PostId]=t2.[Id] WHERE t1.[Pick]=@Pick AND t2.[Author]='张三'", sql);
    }
}
